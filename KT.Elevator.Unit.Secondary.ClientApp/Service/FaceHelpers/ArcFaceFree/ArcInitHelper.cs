using ContralServer.CfgFileRead;
using KT.Common.Core.Exceptions;
using KT.Unit.Face.Arc.Free.Models;
using KT.Unit.Face.Arc.Free.SDKModels;
using KT.Unit.Face.Arc.Free.SDKUtil;
using KT.Unit.FaceRecognition.Models;
using Microsoft.Extensions.Logging;
using System;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.FaceHelpers.ArcFaceFree
{
    public class ArcInitHelper
    {
        private ILogger _logger;
        private ArcFaceSettings _arcFaceSettings;
        private string _ArcSoftFace_OrientPriority;
        public ArcInitHelper(ILogger logger,
            FaceRecognitionAppSettings arcFaceSettings)
        {
            _logger = logger;
            _arcFaceSettings = arcFaceSettings.ArcFreeFaceSettings;
            _ArcSoftFace_OrientPriority= AppConfigurtaionServices.Configuration["ArcSoftFace_OrientPriority"].ToString().Trim();
        }

        /// <summary>
        /// 初始化Arc动态库
        /// </summary>
        /// <param name="pImageEngine">图片引擎句柄</param>
        /// <param name="pVideoEngine">视频引擎句柄</param>
        /// <param name="pVideoRGBImageEngine">RGB引擎句柄</param>
        /// <param name="pVideoIRImageEngine">IR引擎句柄</param>
        public FaceEngine InitFaceEngine()
        {
            IntPtr pImageEngine = IntPtr.Zero;
            IntPtr pVideoEngine = IntPtr.Zero;
            IntPtr pVideoRGBImageEngine = IntPtr.Zero;
            IntPtr pVideoIRImageEngine = IntPtr.Zero;

            //判断CPU位数
            var is64CPU = Environment.Is64BitProcess;
            if (string.IsNullOrWhiteSpace(_arcFaceSettings.AppId)
                || string.IsNullOrWhiteSpace(is64CPU ? _arcFaceSettings.SdkKey64 : _arcFaceSettings.SdkKey32))
            {
                //禁用相关功能按钮
                throw CustomException.Run(string.Format("请在App.config配置文件中先配置APP_ID和SDKKEY{0}!", is64CPU ? "64" : "32"));
            }

            //在线激活引擎    如出现错误，1.请先确认从官网下载的sdk库已放到对应的bin中，2.当前选择的CPU为x86或者x64
            var retCode = ASFFunctions.ASFActivation(_arcFaceSettings.AppId, is64CPU ? _arcFaceSettings.SdkKey64 : _arcFaceSettings.SdkKey32);
            _logger.LogInformation($"在线激活引擎加载成功：retCode:{retCode} ");

            //int retCode = 0;
            //try
            //{
            //    retCode = ASFFunctions.ASFActivation(_arcFaceSettings.AppId, is64CPU ? _arcFaceSettings.SdkKey64 : _arcFaceSettings.SdkKey32);
            //    _logger.LogInformation($"在线激活引擎加载成功：retCode:{retCode} ");
            //}
            //catch (Exception ex)
            //{ 
            //    //禁用相关功能按钮
            //    if (ex.Message.Contains("无法加载 DLL"))
            //    {
            //        throw CustomException.Run("请将sdk相关DLL放入bin对应的x86或x64下的文件夹中!");
            //    }
            //    else
            //    {
            //        throw CustomException.Run("激活引擎失败!");
            //    }
            //}
            _logger.LogInformation("Activate Result:" + retCode);

            //初始化引擎
            uint detectMode = DetectionMode.ASF_DETECT_MODE_IMAGE;
            //Video模式下检测脸部的角度优先值
            int videoDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_HIGHER_EXT;
            //Image模式下检测脸部的角度优先值
            int imageDetectFaceOrientPriority = (_ArcSoftFace_OrientPriority=="1")
                ? ASF_OrientPriority.ASF_OP_0_ONLY
                : ASF_OrientPriority.ASF_OP_0_HIGHER_EXT;
            //人脸在图片中所占比例，如果需要调整检测人脸尺寸请修改此值，有效数值为2-32
            int detectFaceScaleVal = 16;
            //最大需要检测的人脸个数
            int detectFaceMaxNum = 5;
            //引擎初始化时需要初始化的检测功能组合
            int combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER | FaceEngineMask.ASF_FACE3DANGLE;
            //初始化引擎，正常值为0，其他返回值请参考http://ai.arcsoft.com.cn/bbs/forum.php?mod=viewthread&tid=19&_dsign=dbad527e
            retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pImageEngine);
            _logger.LogInformation("InitEngine Result:" + retCode);
            _logger.LogInformation((retCode == 0) ? "引擎初始化成功!\n" : string.Format("引擎初始化失败!错误码为:{0}\n", retCode));

            //初始化视频模式下人脸检测引擎
            uint detectModeVideo = DetectionMode.ASF_DETECT_MODE_VIDEO;
            int combinedMaskVideo = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION;
            retCode = ASFFunctions.ASFInitEngine(detectModeVideo, videoDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMaskVideo, ref pVideoEngine);
            _logger.LogInformation("InitEngine Result:" + retCode);

            //RGB视频专用FR引擎
            detectFaceMaxNum = 1;
            combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_LIVENESS;
            retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pVideoRGBImageEngine);
            _logger.LogInformation("InitEngine Result:" + retCode);

            //IR视频专用FR引擎
            combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_IR_LIVENESS;
            retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pVideoIRImageEngine);
            _logger.LogInformation("InitEngine Result:" + retCode);

            _logger.LogInformation("InitVideoEngine Result:" + retCode);

            var result = new FaceEngine();
            result.ImageEngine = pImageEngine;
            result.VideoEngine = pVideoEngine;
            result.VideoRGBImageEngine = pVideoRGBImageEngine;
            result.VideoIRImageEngine = pVideoIRImageEngine;

            return result;
        }

        /// <summary>
        /// 获取人脸特征
        /// </summary>
        public void Close(FaceEngine faceEngine)
        {
            try
            {
                //销毁引擎
                int retCode = ASFFunctions.ASFUninitEngine(faceEngine.ImageEngine);
                _logger.LogInformation("UninitEngine pImageEngine Result:" + retCode);
                //销毁引擎
                retCode = ASFFunctions.ASFUninitEngine(faceEngine.VideoEngine);
                _logger.LogInformation("UninitEngine pVideoEngine Result:" + retCode);

                //销毁引擎
                retCode = ASFFunctions.ASFUninitEngine(faceEngine.VideoRGBImageEngine);
                _logger.LogInformation("UninitEngine pVideoImageEngine Result:" + retCode);

                //销毁引擎
                retCode = ASFFunctions.ASFUninitEngine(faceEngine.VideoIRImageEngine);
                _logger.LogInformation("UninitEngine pVideoIRImageEngine Result:" + retCode);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("UninitEngine pImageEngine Error:" + ex);
            }
        }
    }
}
