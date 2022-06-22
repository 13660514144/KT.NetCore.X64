using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Secondary.ClientApp.Events;
using KT.Elevator.Unit.Secondary.ClientApp.Service.IServices;
using KT.Quanta.Common.Enums;
using KT.Unit.Face.Arc.Pro.Entity;
using KT.Unit.Face.Arc.Pro.Models;
using KT.Unit.Face.Arc.Pro.SDKModels;
using KT.Unit.Face.Arc.Pro.Utils;
using KT.Unit.FaceRecognition.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.FaceHelpers.ArcFacePro
{
    /// <summary>
    /// 人脸静态数据 
    /// </summary>
    public class FaceProvider : IFaceProvider
    {
        /// <summary>
        /// 最大宽度
        /// </summary>
        private int maxWidth = 1536;

        /// <summary>
        /// 最大高度
        /// </summary>
        private int maxHeight = 1536;

        public int ProviderIndex { get; set; }

        public FaceEngineProvider FaceEngineProvider { get; set; }

        private static object _locker = new object();

        /// <summary>
        /// 人脸特征列表
        /// </summary>
        private List<FacePassRightModel> _faceFeatureList;

        private IEventAggregator _eventAggregator;
        private IPassRightService _passRightService;
        private ArcFaceSettings _arcFaceSettings;
        private ILogger _logger;
        private ArcInitHelper _arcInitHelper;

        public FaceProvider()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _passRightService = ContainerHelper.Resolve<IPassRightService>();
            _arcFaceSettings = ContainerHelper.Resolve<FaceRecognitionAppSettings>().ArcProFaceSettings;
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
                if (facePassRights?.FirstOrDefault() != null)
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
            _logger.LogInformation($"FaceFactory:DeletePassRight:FreeFaceFeature:start:id:{id} ");

            var oldItem = _faceFeatureList.FirstOrDefault(x => x.Id == id);
            if (oldItem != null)
            {
                _logger.LogInformation($"FaceFactory:DeletePassRight:FreeFaceFeature:{JsonConvert.SerializeObject(oldItem, JsonUtil.JsonPrintSettings)} ");

                var isTake = _faceFeatureList.Remove(oldItem);
                if (!isTake)
                {
                    _logger.LogError($"删除人脸内存失败：{JsonConvert.SerializeObject(oldItem, JsonUtil.JsonPrintSettings)} ");
                }
            }

            _logger.LogInformation($"FaceFactory:DeletePassRight:FreeFaceFeature:end!! ");
        }

        /// <summary>
        /// 得到feature比较结果
        /// </summary>
        /// <param name="feature"></param> 
        public async Task<List<FaceDistinguishResult>> CompareFeatureAsync(FaceFeature feature, bool isMask)
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
                    FaceEngineProvider.ImageEngine.ASFFaceFeatureCompare(feature, item.Feature, out similarity);

                    //不能开启，否则检测时间会很长
                    //_logger.LogInformation($"检测人脸结果：providerIndex:{ProviderIndex} sign:{item.Sign} similarity:{similarity} "); 
                    if (!isMask && similarity >= _arcFaceSettings.Threshold)
                    {
                        _logger.LogInformation($"检测人脸通过：providerIndex:{ProviderIndex} sign:{item.Sign} similarity:{similarity} mask:{isMask} ");

                        var result = new FaceDistinguishResult();
                        result.FacePassRight = item;
                        result.Similarity = similarity;
                        results.Add(result);
                    }
                    else if (isMask && similarity >= _arcFaceSettings.ThresholdMask)
                    {
                        _logger.LogInformation($"检测人脸通过：providerIndex:{ProviderIndex} sign:{item.Sign} similarity:{similarity} mask:{isMask} ");

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
                if (faceFeature == null || faceFeature.FaceFeature?.featureSize <= 0)
                {
                    _logger.LogWarning("提取人脸特征为空！");
                    return;
                }

                //创建人脸对象
                var result = new FacePassRightModel();
                result.Id = IdUtil.NewId();
                result.Sign = index.ToString();
                result.Feature = new FaceFeature();
                result.Feature.feature = faceFeature.FaceFeature.feature;
                result.Feature.featureSize = faceFeature.FaceFeature.featureSize;

                _faceFeatureList.Add(result);
            });
        }

        /// <summary>
        /// 裁剪Picture
        /// </summary>
        /// <returns></returns>
        public Image CropImage(string file)
        {
            Image image = ArcImageUtil.ReadFromFile(file);

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
            MultiFaceInfo multiFaceInfo;
            var retCode = FaceEngineProvider.ImageEngine.ASFDetectFacesEx(image, out multiFaceInfo);
            //判断检测结果
            if (retCode == 0 && multiFaceInfo.faceNum > 0)
            {
                //多人脸时，默认裁剪第一个人脸 
                MRECT rect = multiFaceInfo.faceRects[0];
                image = ImageUtil.CutImage(image, rect.left, rect.top, rect.right, rect.bottom);
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
        public FaceFullFeature GetFaceFeatureBytes(string fileUrl)
        {
            Image image = ArcImageUtil.ReadFromFile(fileUrl);
            return GetFaceFeatureBytes(image);
        }

        public FaceFullFeature GetFaceFeatureBytes(Image image)
        {
            try
            {
                return GetFaceFeatureBytesBase(image);
            }
            catch (Exception ex)
            {
                _logger.LogError($"获取人脸Bytes失败：{ex} ");
                return null;
            }
        }
        private FaceFullFeature GetFaceFeatureBytesBase(Image image)
        {
            if (image == null)
            {
                _logger.LogWarning("找不到图片！");
                return null;
            }

            //调整图像宽度，需要宽度为4的倍数
            if (image.Width % 4 != 0)
            {
                image = ImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
            }

            var featureResult = string.Empty;
            bool isMask;
            int retCode;
            var singleFaceInfo = new SingleFaceInfo();
            var feature = FaceUtil.ExtractFeature(FaceEngineProvider.ImageEngine, image,
                _arcFaceSettings.ThresholdImgNoMask,
                _arcFaceSettings.ThresholdImgMask,
                ASF_RegisterOrNot.ASF_REGISTER, out singleFaceInfo, out isMask, ref featureResult, out retCode);

            var faceFullFeature = new FaceFullFeature();
            faceFullFeature.FeatureResult = featureResult;
            faceFullFeature.IsMask = isMask;
            faceFullFeature.RetCode = retCode;
            faceFullFeature.SingleFaceInfo = singleFaceInfo;
            faceFullFeature.FaceFeature = feature;

            if (!string.IsNullOrEmpty(featureResult))
            {
                _logger.LogInformation(featureResult);

                return null;
            }

            //人脸检测
            MultiFaceInfo multiFaceInfo;
            retCode = FaceEngineProvider.ImageEngine.ASFDetectFacesEx(image, 
                out multiFaceInfo);
            //判断检测结果
            if (retCode != 0 && multiFaceInfo.faceNum <= 0)
            {
                _logger.LogWarning($"未检测到人脸！ ");
                return null;
            }

            return faceFullFeature;
        }

        /// <summary>
        /// 获取图片人脸特征
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns>Bytes:人脸特征</returns>
        public FaceFullFeature GetFaceFeature(string fileUrl)
        {
            Image image = ArcImageUtil.ReadFromFile(fileUrl);

            //调整图像宽度，需要宽度为4的倍数
            if (image.Width % 4 != 0)
            {
                image = ImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
            }

            var featureResult = string.Empty;
            bool isMask;
            int retCode;
            var singleFaceInfo = new SingleFaceInfo();
            var feature = FaceUtil.ExtractFeature(FaceEngineProvider.ImageEngine, image,
                _arcFaceSettings.ThresholdImgNoMask,
                _arcFaceSettings.ThresholdImgMask,
                ASF_RegisterOrNot.ASF_REGISTER, out singleFaceInfo, out isMask, ref featureResult, out retCode);

            var faceFullFeature = new FaceFullFeature();
            faceFullFeature.FeatureResult = featureResult;
            faceFullFeature.IsMask = isMask;
            faceFullFeature.RetCode = retCode;
            faceFullFeature.SingleFaceInfo = singleFaceInfo;
            faceFullFeature.FaceFeature = feature;

            if (!string.IsNullOrEmpty(featureResult))
            {
                _logger.LogInformation(featureResult);
                if (image != null)
                {
                    image.Dispose();
                }
                return null;
            }

            //人脸检测
            MultiFaceInfo multiFaceInfo;
            retCode = FaceEngineProvider.ImageEngine.ASFDetectFacesEx(image, out multiFaceInfo);
            //判断检测结果
            if (retCode != 0 && multiFaceInfo.faceNum <= 0)
            {
                _logger.LogWarning($"未检测到人脸:{fileUrl} ");
                if (image != null)
                {
                    image.Dispose();
                }
                return null;
            }

            if (image != null)
            {
                image.Dispose();
            }
            return faceFullFeature;
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
            result.Feature = new FaceFeature();
            result.Feature.feature = passRightEntity.Feature;
            result.Feature.featureSize = passRightEntity.Feature.Length;

            return result;
        }

        public void Close()
        {
            _arcInitHelper.Close(FaceEngineProvider);
        }
    }


    public class FaceDistinguishResult
    {
        public FacePassRightModel FacePassRight { get; set; }

        public float Similarity { get; set; }
    }

    public class FaceFullFeature
    {
        /// <summary>
        /// 提取特征判断
        /// </summary>
        public string FeatureResult { get; set; }
        public bool IsMask { get; set; }
        public int RetCode { get; set; }
        public SingleFaceInfo SingleFaceInfo { get; set; }

        public FaceFeature FaceFeature { get; set; }

        public FaceFullFeature()
        {
            SingleFaceInfo = new SingleFaceInfo();
        }
    }
}
