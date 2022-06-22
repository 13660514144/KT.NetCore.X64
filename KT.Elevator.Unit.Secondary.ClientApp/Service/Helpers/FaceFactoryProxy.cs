using KT.Common.WpfApp.Helpers;
using KT.Unit.FaceRecognition.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.Helpers
{
    public class FaceFactoryProxy
    {
        private readonly ILogger _logger;
        private readonly FaceRecognitionAppSettings _faceRecognitionAppSettings;

        private readonly FaceHelpers.ArcFaceFree.FaceFactory _freeFaceFactory;
        private readonly FaceHelpers.ArcFacePro.FaceFactory _proFaceFactory;
        private readonly FaceHelpers.ArcFaceFree.IFaceService _freeFaceService;
        private readonly FaceHelpers.ArcFacePro.IFaceService _proFaceService;
        public FaceFactoryProxy(ILogger logger,
            FaceRecognitionAppSettings faceRecognitionAppSettings)
        {
            _logger = logger;
            _faceRecognitionAppSettings = faceRecognitionAppSettings;


            if (_faceRecognitionAppSettings.FaceRecognitionType == FaceRecognitionTypeEnum.ArcFree22.Value
                || _faceRecognitionAppSettings.FaceAuxiliaryType == FaceRecognitionTypeEnum.ArcFree22.Value)
            {
                _freeFaceFactory = ContainerHelper.Resolve<FaceHelpers.ArcFaceFree.FaceFactory>();
                _freeFaceService = ContainerHelper.Resolve<FaceHelpers.ArcFaceFree.IFaceService>();
            }

            if (_faceRecognitionAppSettings.FaceRecognitionType == FaceRecognitionTypeEnum.ArcPro40.Value
                || _faceRecognitionAppSettings.FaceAuxiliaryType == FaceRecognitionTypeEnum.ArcPro40.Value)
            {
                _proFaceFactory = ContainerHelper.Resolve<FaceHelpers.ArcFacePro.FaceFactory>();
                _proFaceService = ContainerHelper.Resolve<FaceHelpers.ArcFacePro.IFaceService>();
            }
        }

        public byte[] GetFaceFeatureBytes(Image image)
        {
            if (_faceRecognitionAppSettings.FaceAuxiliaryType == FaceRecognitionTypeEnum.ArcFree22.Value)
            {
                var provider = _freeFaceFactory.GetRandomProvider();
                var feature = provider?.GetFaceFeatureBytes(image);
                if (feature == null)
                {
                    _logger.LogError($"获取人脸特征为空！ ");
                    return null;
                }

                if (feature.Value.IsSuccess)
                {
                    return feature.Value.Bytes;
                }
                else
                {
                    _logger.LogWarning($"获取人脸特：{feature.Value.Message}");
                }
            }
            else if (_faceRecognitionAppSettings.FaceAuxiliaryType == FaceRecognitionTypeEnum.ArcPro40.Value)
            {
                var provider = _proFaceFactory.GetRandomProvider();
                var feature = provider?.GetFaceFeatureBytes(image);
                if (feature == null)
                {
                    _logger.LogError($"获取人脸特征为空！ ");
                    return null;
                }

                return feature.FaceFeature?.feature;
            }
            else
            {
                throw new System.Exception($"找不到人脸识别类型：faceAuxiliaryType:{_faceRecognitionAppSettings.FaceAuxiliaryType} ");
            }

            return null;
        }


        public async Task InitAsync()
        {
            try
            {
                //初始化人脸识别 
                if (_faceRecognitionAppSettings.FaceRecognitionType == FaceRecognitionTypeEnum.ArcFree22.Value
                    || _faceRecognitionAppSettings.FaceAuxiliaryType == FaceRecognitionTypeEnum.ArcFree22.Value)
                {
                    await _freeFaceFactory.InitFaceProvidesAsync();
                }
                //离线激活
                else if (_faceRecognitionAppSettings.FaceRecognitionType == FaceRecognitionTypeEnum.ArcPro40.Value
                     || _faceRecognitionAppSettings.FaceAuxiliaryType == FaceRecognitionTypeEnum.ArcPro40.Value)
                {
                    await _proFaceFactory.InitFaceProvidesAsync();
                }
                else
                {
                    throw new System.Exception($"找不到人脸识别类型：{_faceRecognitionAppSettings.FaceRecognitionType} {_faceRecognitionAppSettings.FaceAuxiliaryType} ");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"初始化人脸识别失败：{ex} ");
            }

            //初始化人脸识别 
            if (_faceRecognitionAppSettings.FaceAuxiliaryType == FaceRecognitionTypeEnum.ArcFree22.Value)
            {
                //初始化人脸数据 
                await _freeFaceService.InitFaceAsync();
            }
            else if (_faceRecognitionAppSettings.FaceAuxiliaryType == FaceRecognitionTypeEnum.ArcPro40.Value)
            {
                //初始化人脸数据 
                await _proFaceService.InitFaceAsync();
            }
            else
            {
                throw new System.Exception($"找不到人脸识别类型：faceAuxiliaryType:{_faceRecognitionAppSettings.FaceAuxiliaryType} ");
            }
        }

        public void Close()
        {
            if (_freeFaceFactory != null)
            {
                _freeFaceFactory.Close();
            }
            if (_proFaceFactory != null)
            {
                _proFaceFactory.Close();
            }
        }
    }
}
