using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Secondary.ClientApp.Events;
using KT.Elevator.Unit.Secondary.ClientApp.Service.IServices;
using KT.Quanta.Common.Enums;
using KT.Unit.Face.Arc.Free.Models;
using KT.Unit.Face.Arc.Free.SDKModels;
using KT.Unit.Face.Arc.Free.SDKUtil;
using KT.Unit.Face.Arc.Free.Utils;
using KT.Unit.FaceRecognition.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.FaceHelpers.ArcFaceFree
{
    /// <summary>
    /// 人脸静态数据 
    /// </summary>
    public class FaceProvider : IFaceProvider
    {
        public int ProviderIndex { get; set; }

        public FaceEngine FaceEngine { get; set; }

        /// <summary>
        /// 人脸特征列表
        /// </summary>
        private List<FacePassRightModel> _faceFeatureList;

        private IEventAggregator _eventAggregator;
        private IPassRightService _passRightService;
        private ArcFaceSettings _arcFaceSettings;
        private ILogger _logger;
        private ArcInitHelper _arcInitHelper;

        private static object _locker = new object();

        public FaceProvider()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _passRightService = ContainerHelper.Resolve<IPassRightService>();
            _arcFaceSettings = ContainerHelper.Resolve<FaceRecognitionAppSettings>().ArcFreeFaceSettings;
            _logger = ContainerHelper.Resolve<ILogger>();
            _arcInitHelper = ContainerHelper.Resolve<ArcInitHelper>();

            _faceFeatureList = new List<FacePassRightModel>();

            _eventAggregator.GetEvent<DeletePassRightEvent>().Subscribe(DeletePassRight);
        }

        public async Task AddPassRightsAsync(List<UnitPassRightEntity> faceRights)
        {
            await Task.Run(() =>
            {
                var facePassRights = FaceEntitiesToModels(faceRights);

                lock (_locker)
                {
                    _faceFeatureList.AddRange(facePassRights);
                }
            });
        }
        public async Task AddPassRightAsync(UnitPassRightEntity faceRight)
        {
            await Task.Run(() =>
            {
                var facePassRight = FaceEntityToModel(faceRight);
                if (facePassRight != null)
                {
                    _faceFeatureList.Add(facePassRight);
                }
            });
        }

        /// <summary>
        /// 删除人脸通行权限
        /// </summary>
        /// <param name="id"></param>
        public void DeletePassRight(string id)
        {
            lock (_locker)
            {
                var oldItem = _faceFeatureList.FirstOrDefault(x => x.Id == id);
                if (oldItem != null)
                {
                    MemoryUtil.Destroy<ASF_FaceFeature>(oldItem.Feature);

                    var isTake = _faceFeatureList.Remove(oldItem);
                    if (!isTake)
                    {
                        _logger.LogError($"删除人脸内存失败：{JsonConvert.SerializeObject(oldItem, JsonUtil.JsonPrintSettings)} ");
                    }
                }
            }
        }

        /// <summary>
        /// 得到feature比较结果
        /// </summary>
        /// <param name="feature"></param> 
        public async Task<List<FaceDistinguishResult>> CompareFeatureAsync(IntPtr feature)
        {
            return await Task.Run(() =>
            {
                var results = new List<FaceDistinguishResult>();

                var timeStart = DateTimeUtil.UtcNowMillis();
                decimal timeSpan;
                var similarity = 0f;

                //如果人脸库不为空，则进行人脸匹配
                if (_faceFeatureList == null || _faceFeatureList.FirstOrDefault() == null)
                {
                    return null;
                }

                foreach (var item in _faceFeatureList)
                {
                    //调用人脸匹配方法，进行匹配
                    ASFFunctions.ASFFaceFeatureCompare(FaceEngine.VideoRGBImageEngine, feature, item.Feature, ref similarity);

                    //不能开启，否则检测时间会很长
                    //_logger.LogInformation($"检测人脸结果：providerIndex:{ProviderIndex} sign:{item.Sign} similarity:{similarity} "); 
                    if (similarity >= _arcFaceSettings.Threshold)
                    {
                        _logger.LogInformation($"检测人脸通过：providerIndex:{ProviderIndex} sign:{item.Sign} similarity:{similarity} ");

                        var result = new FaceDistinguishResult();
                        result.FacePassRight = item;
                        result.Similarity = similarity;
                        results.Add(result);
                    }
                }

                timeSpan = (DateTimeUtil.UtcNowMillis() - timeStart) / 1000M;
                _logger.LogInformation($"检测人脸阶段结束：providerIndex:{ProviderIndex} timeSpan:{timeSpan }");

                return results;
            });
        }

        public async Task AddAsync(int index, string fullFileName)
        {
            await Task.Run(() =>
            {
                _logger.LogInformation($"提取人脸特征：providerIndex:{ProviderIndex} index:{index} file:{fullFileName} ");

                var faceFeature = GetFaceFeature(fullFileName);
                if (!faceFeature.IsSuccess)
                {
                    _logger.LogError(faceFeature.Message);
                    return;
                }
                //创建人脸对象
                var result = new FacePassRightModel();
                result.Id = IdUtil.NewId();
                result.Sign = index.ToString();
                result.Feature = faceFeature.Feature;

                _faceFeatureList.Add(result);
            });
        }

        /// <summary>
        /// 裁剪Picture
        /// </summary>
        /// <returns></returns>
        public Image CropImage(string file)
        {
            Image image = ArcImageUtil.readFromFile(file);

            return CropImage(image);
        }

        /// <summary>
        /// 裁剪Picture
        /// </summary>
        /// <returns></returns>
        public Image CropImage(Image image)
        {
            if (image == null)
            {
                return null;
            }
            if (image.Width > 1536 || image.Height > 1536)
            {
                image = ArcImageUtil.ScaleImage(image, 1536, 1536);
            }
            if (image == null)
            {
                return null;
            }
            if (image.Width % 4 != 0)
            {
                image = ArcImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
            }
            //人脸检测
            ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(FaceEngine.ImageEngine, image);
            //判断检测结果
            if (multiFaceInfo.faceNum > 0)
            {
                MRECT rect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects);
                image = ArcImageUtil.CutImage(image, rect.left, rect.top, rect.right, rect.bottom);
            }
            else
            {
                if (image != null)
                {
                    image.Dispose();
                }
            }

            return image;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns>Bytes:人脸特征</returns>
        public (bool IsSuccess, byte[] Bytes, string Message) GetFaceFeatureBytes(string fileUrl)
        {
            System.Drawing.Image image = ArcImageUtil.readFromFile(fileUrl);
            return GetFaceFeatureBytes(image);
        }

        public (bool IsSuccess, byte[] Bytes, string Message) GetFaceFeatureBytes(Image image)
        {
            try
            {
                return GetFaceFeatureBytesBase(image);
            }
            catch (Exception ex)
            {
                _logger.LogError($"获取人脸Bytes失败：{ex} ");
                return (false, null, string.Empty);
            }
        }
        private (bool IsSuccess, byte[] Bytes, string Message) GetFaceFeatureBytesBase(Image image)
        {
            if (image == null)
            {
                return (false, null, "找不到图片。");
            }
            var featureBytes = FaceUtil.ExtractFeatureBytes(FaceEngine.ImageEngine, image);
            if (image != null)
            {
                image.Dispose();
            }

            //检测结果
            if (featureBytes.SingleFaceInfo.faceRect.left == 0 && featureBytes.SingleFaceInfo.faceRect.right == 0)
            {
                var message = string.Format("未检测到人脸\r\n");
                return (false, featureBytes.Feature, message);
            }
            else
            {
                var message = string.Format("已提取人脸特征值，[left:{0},right:{1},top:{2},bottom:{3},orient:{4}]\r\n",
                      featureBytes.SingleFaceInfo.faceRect.left,
                      featureBytes.SingleFaceInfo.faceRect.right,
                      featureBytes.SingleFaceInfo.faceRect.top,
                      featureBytes.SingleFaceInfo.faceRect.bottom,
                      featureBytes.SingleFaceInfo.faceOrient);

                return (true, featureBytes.Feature, message);
            }
        }


        /// <summary>
        /// 获取图片人脸特征
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns>Bytes:人脸特征</returns>
        public (bool IsSuccess, IntPtr Feature, string Message, byte[] Bytes) GetFaceFeature(string fileUrl)
        {
            Image image = ArcImageUtil.readFromFile(fileUrl);
            var featureBytes = FaceUtil.ExtractFeatureBytes(FaceEngine.ImageEngine, image);
            if (image != null)
            {
                image.Dispose();
            }

            if (featureBytes.Feature == null)
            {
                var message = string.Format($"未检测到人脸：{fileUrl} ");
                return (false, IntPtr.Zero, message, null);
            }

            IntPtr feature = GetFaceFeature(featureBytes.Feature);

            //检测结果
            if (featureBytes.SingleFaceInfo.faceRect.left == 0 && featureBytes.SingleFaceInfo.faceRect.right == 0)
            {
                var message = string.Format($"未检测到人脸：{fileUrl} ");

                return (false, IntPtr.Zero, message, null);
            }
            else
            {
                var message = string.Format("已提取{0}人脸特征值，[left:{1},right:{2},top:{3},bottom:{4},orient:{5}]\r\n",
                       fileUrl,
                      featureBytes.SingleFaceInfo.faceRect.left,
                      featureBytes.SingleFaceInfo.faceRect.right,
                      featureBytes.SingleFaceInfo.faceRect.top,
                      featureBytes.SingleFaceInfo.faceRect.bottom,
                      featureBytes.SingleFaceInfo.faceOrient);

                return (true, feature, message, featureBytes.Feature);
            }
        }

        /// <summary>
        /// 图片bytes获取特性
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public IntPtr GetFaceFeature(byte[] bytes)
        {
            ASF_FaceFeature localFeature = new ASF_FaceFeature();
            localFeature.feature = MemoryUtil.Malloc(bytes.Length);
            MemoryUtil.Copy(bytes, 0, localFeature.feature, bytes.Length);
            localFeature.featureSize = bytes.Length;
            IntPtr pLocalFeature = MemoryUtil.Malloc(MemoryUtil.SizeOf<ASF_FaceFeature>());
            MemoryUtil.StructureToPtr(localFeature, pLocalFeature);

            return pLocalFeature;
        }

        public List<FacePassRightModel> FaceEntitiesToModels(List<UnitPassRightEntity> passRightEntities)
        {
            var results = new List<FacePassRightModel>();
            if (passRightEntities == null || passRightEntities.FirstOrDefault() == null)
            {
                return results;
            }
            foreach (var item in passRightEntities)
            {
                var result = FaceEntityToModel(item);
                if (result != null)
                {
                    results.Add(result);
                }
            }
            return results;
        }

        public FacePassRightModel FaceEntityToModel(UnitPassRightEntity passRightEntity)
        {
            if (passRightEntity == null)
            {
                _logger.LogError($"通行权限为空！");
                return null;
            }
            if (passRightEntity.AccessType == AccessTypeEnum.FACE.Value)
            {
                if (passRightEntity.Feature == null || passRightEntity.Feature.Length == 0)
                {
                    //_logger.LogError($"人脸通行权限人脸特征不能为空：data:{JsonConvert.SerializeObject(passRightEntity, JsonUtil.JsonPrintSettings)} ");
                    return null;
                }
                var result = EntityToModel(passRightEntity);
                return result;
            }
            return null;
        }

        private FacePassRightModel EntityToModel(UnitPassRightEntity passRightEntity)
        {
            var result = new FacePassRightModel();
            result.Id = passRightEntity.Id;
            result.Sign = passRightEntity.Sign;
            result.TimeStart = passRightEntity.TimeStart;
            result.TimeEnd = passRightEntity.TimeEnd;

            //_logger.LogInformation($"人脸特征：bytes:{passRightEntity.Feature?.ToSeparateCommaStr()} ");

            //byte数组转图片特性
            result.Feature = GetFaceFeature(passRightEntity.Feature);

            return result;
        }

        public void Close()
        {
            _arcInitHelper.Close(FaceEngine);
        }
    }


    public class FaceDistinguishResult
    {
        public FacePassRightModel FacePassRight { get; set; }

        public float Similarity { get; set; }
    }
}
