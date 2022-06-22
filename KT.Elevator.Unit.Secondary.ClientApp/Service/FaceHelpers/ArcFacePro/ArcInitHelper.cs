using ContralServer.CfgFileRead;
using KT.Unit.Face.Arc.Pro.Models;
using KT.Unit.Face.Arc.Pro.SDKModels;
using KT.Unit.FaceRecognition.Models;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.FaceHelpers.ArcFacePro
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
            _arcFaceSettings = arcFaceSettings.ArcProFaceSettings;
            _ArcSoftFace_OrientPriority = AppConfigurtaionServices.Configuration["ArcSoftFace_OrientPriority"].ToString().Trim();
        }

        /// <summary>
        /// 初始化Arc动态库
        /// </summary>
        /// <param name="pImageEngine">图片引擎句柄</param>
        /// <param name="pVideoEngine">视频引擎句柄</param>
        /// <param name="pVideoRGBImageEngine">RGB引擎句柄</param>
        /// <param name="pVideoIRImageEngine">IR引擎句柄</param>
        public FaceEngineProvider InitFaceEngine()
        {
            var faceEngineProvider = new FaceEngineProvider();

            var message = string.Empty;
            //读取配置文件 
            var rgbCameraIndex = _arcFaceSettings.RgbCameraIndex;
            var irCameraIndex = _arcFaceSettings.IrCameraIndex;
            var frMatchTime = _arcFaceSettings.FrMatchTime;
            var liveMatchTime = _arcFaceSettings.LivenessMatchTime;

            int retCode = 0;
            bool isOnlineActive = _arcFaceSettings.IsOnlineActive;//true(在线激活) or false(离线激活)
            try
            {
                if (isOnlineActive)
                {
                    #region 读取在线激活配置信息
                    string appId = _arcFaceSettings.AppId;
                    string sdkKey64 = _arcFaceSettings.SdkKey64;
                    string sdkKey32 = _arcFaceSettings.SdkKey32;
                    string activeKey64 = _arcFaceSettings.ActiveKey64;
                    string activeKey32 = _arcFaceSettings.ActiveKey32;
                    //判断CPU位数
                    var is64CPU = Environment.Is64BitProcess;
                    if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(is64CPU ? sdkKey64 : sdkKey32) || string.IsNullOrWhiteSpace(is64CPU ? activeKey64 : activeKey32))
                    {
                        _logger.LogError(string.Format("请在App.config配置文件中先配置APP_ID和SDKKEY{0}、ACTIVEKEY{0}!", is64CPU ? "64" : "32"));
                        return faceEngineProvider;
                    }
                    #endregion
                    //在线激活引擎    如出现错误，1.请先确认从官网下载的sdk库已放到对应的bin中，2.当前选择的CPU为x86或者x64
                    retCode = faceEngineProvider.ImageEngine.ASFOnlineActivation(appId, is64CPU ? sdkKey64 : sdkKey32, is64CPU ? activeKey64 : activeKey32);
                }
                else
                {
                    #region 读取离线激活配置信息
                    string offlineActiveFilePath = _arcFaceSettings.OfflineActiveFilePath;
                    if (string.IsNullOrWhiteSpace(offlineActiveFilePath) || !File.Exists(offlineActiveFilePath))
                    {
                        string deviceInfo;
                        retCode = faceEngineProvider.ImageEngine.ASFGetActiveDeviceInfo(out deviceInfo);
                        if (retCode != 0)
                        {
                            _logger.LogError("获取设备信息失败，错误码:" + retCode);
                        }
                        else
                        {
                            File.WriteAllText("ActiveDeviceInfo.txt", deviceInfo);
                            _logger.LogInformation("获取设备信息成功，已保存到运行根目录ActiveDeviceInfo.txt文件，请在官网执行离线激活操作，将生成的离线授权文件路径在App.config里配置后再重新运行");
                        }
                        System.Environment.Exit(0);
                    }
                    #endregion
                    //离线激活
                    retCode = faceEngineProvider.ImageEngine.ASFOfflineActivation(offlineActiveFilePath);
                }
                if (retCode != 0 && retCode != 90114)
                {
                    _logger.LogError("激活SDK失败,错误码:" + retCode);
                    return faceEngineProvider;
                }
                _logger.LogInformation($"\r\n==>成功激活");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("无法加载 DLL"))
                {
                    _logger.LogError("请将SDK相关DLL放入bin对应的x86或x64下的文件夹中!");
                }
                else
                {
                    _logger.LogError("激活SDK失败,请先检查依赖环境及SDK的平台、版本是否正确!");
                    _logger.LogError($"激活SDK失败{ex.Message}");
                }
                return faceEngineProvider;
            }
                       
            //初始化引擎
            DetectionMode detectMode = DetectionMode.ASF_DETECT_MODE_IMAGE;
            //Video模式下检测脸部的角度优先值
            //ASF_OrientPriority videoDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_ALL_OUT;
            ASF_OrientPriority videoDetectFaceOrientPriority = (_ArcSoftFace_OrientPriority == "1")
                ? ASF_OrientPriority.ASF_OP_0_ONLY
                : ASF_OrientPriority.ASF_OP_ALL_OUT;
            //Image模式下检测脸部的角度优先值
            //ASF_OrientPriority imageDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_ALL_OUT;
            ASF_OrientPriority imageDetectFaceOrientPriority = (_ArcSoftFace_OrientPriority == "1")
                ? ASF_OrientPriority.ASF_OP_0_ONLY
                : ASF_OrientPriority.ASF_OP_ALL_OUT;
            //最大需要检测的人脸个数
            int detectFaceMaxNum = 6;            
            //引擎初始化时需要初始化的检测功能组合
            
            int combinedMask = FaceEngineMask.ASF_FACE_DETECT |
                FaceEngineMask.ASF_FACERECOGNITION |
                FaceEngineMask.ASF_AGE | 
                FaceEngineMask.ASF_GENDER |
                FaceEngineMask.ASF_FACE3DANGLE |
                FaceEngineMask.ASF_IMAGEQUALITY | 
                FaceEngineMask.ASF_MASKDETECT;
            
            //初始化引擎，正常值为0，其他返回值请参考http://ai.arcsoft.com.cn/bbs/forum.php?mod=viewthread&tid=19&_dsign=dbad527e
            retCode = faceEngineProvider.ImageEngine.ASFInitEngine(
                detectMode, 
                imageDetectFaceOrientPriority,
                detectFaceMaxNum,
                combinedMask);
            Console.WriteLine("InitEngine Result:" + retCode);
            message += ((retCode == 0) ? "图片引擎初始化成功!" : string.Format("图片引擎初始化失败!错误码为:{0}", retCode));
            if (retCode != 0)
            {
                //禁用相关功能按钮
                _logger.LogError(message);
                return faceEngineProvider;
            }

            //初始化视频模式下人脸检测引擎
            DetectionMode detectModeVideo = DetectionMode.ASF_DETECT_MODE_VIDEO;
            int combinedMaskVideo = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_FACELANDMARK;            
            retCode = faceEngineProvider.VideoEngine.ASFInitEngine(detectModeVideo, 
                videoDetectFaceOrientPriority, 
                detectFaceMaxNum, combinedMaskVideo);
            message += ((retCode == 0) ? "视频引擎初始化成功!" : string.Format("视频引擎初始化失败!错误码为:{0}", retCode));
            if (retCode != 0)
            {
                //禁用相关功能按钮
                _logger.LogError(message);
                return faceEngineProvider;
            }

            //RGB视频专用FR引擎
            combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_LIVENESS | FaceEngineMask.ASF_MASKDETECT;
            
            retCode = faceEngineProvider.VideoRGBImageEngine.ASFInitEngine(detectMode, 
                videoDetectFaceOrientPriority, 
                detectFaceMaxNum, combinedMask);
            message += ((retCode == 0) ? "RGB处理引擎初始化成功!" : string.Format("RGB处理引擎初始化失败!错误码为:{0}", retCode));
            if (retCode != 0)
            {
                //禁用相关功能按钮
                _logger.LogError(message);
                return faceEngineProvider;
            }
            //设置活体阈值
            faceEngineProvider.VideoRGBImageEngine.ASFSetLivenessParam(_arcFaceSettings.ThresholdRgb);

            //IR视频专用FR引擎
            combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_IR_LIVENESS;            
            retCode = faceEngineProvider.VideoIRImageEngine.ASFInitEngine(detectModeVideo, 
                videoDetectFaceOrientPriority,
                detectFaceMaxNum, combinedMask);
            message += ((retCode == 0) ? "IR处理引擎初始化成功!\r\n" : string.Format("IR处理引擎初始化失败!错误码为:{0}\r\n", retCode));
            if (retCode != 0)
            {
                //禁用相关功能按钮
                _logger.LogError(message);
                return faceEngineProvider;
            }
            //设置活体阈值
            faceEngineProvider.VideoIRImageEngine.ASFSetLivenessParam(_arcFaceSettings.ThresholdRgb, _arcFaceSettings.ThresholdIr);

            _logger.LogInformation($"引擎初始化成功：reCode:{retCode} ");
            
            return faceEngineProvider;









            //IntPtr pImageEngine = IntPtr.Zero;
            //IntPtr pVideoEngine = IntPtr.Zero;
            //IntPtr pVideoRGBImageEngine = IntPtr.Zero;
            //IntPtr pVideoIRImageEngine = IntPtr.Zero;

            ////判断CPU位数
            //var is64CPU = Environment.Is64BitProcess;
            //if (string.IsNullOrWhiteSpace(_arcFaceSettings.AppId)
            //    || string.IsNullOrWhiteSpace(is64CPU ? _arcFaceSettings.SdkKey64 : _arcFaceSettings.SdkKey32))
            //{
            //    //禁用相关功能按钮
            //    throw CustomException.Run(string.Format("请在App.config配置文件中先配置APP_ID和SDKKEY{0}!", is64CPU ? "64" : "32"));
            //}

            ////在线激活引擎    如出现错误，1.请先确认从官网下载的sdk库已放到对应的bin中，2.当前选择的CPU为x86或者x64
            //var retCode = ASFFunctions.ASFActivation(_arcFaceSettings.AppId, is64CPU ? _arcFaceSettings.SdkKey64 : _arcFaceSettings.SdkKey32);
            //_logger.LogInformation($"在线激活引擎加载成功：retCode:{retCode} ");

            ////int retCode = 0;
            ////try
            ////{
            ////    retCode = ASFFunctions.ASFActivation(_arcFaceSettings.AppId, is64CPU ? _arcFaceSettings.SdkKey64 : _arcFaceSettings.SdkKey32);
            ////    _logger.LogInformation($"在线激活引擎加载成功：retCode:{retCode} ");
            ////}
            ////catch (Exception ex)
            ////{ 
            ////    //禁用相关功能按钮
            ////    if (ex.Message.Contains("无法加载 DLL"))
            ////    {
            ////        throw CustomException.Run("请将sdk相关DLL放入bin对应的x86或x64下的文件夹中!");
            ////    }
            ////    else
            ////    {
            ////        throw CustomException.Run("激活引擎失败!");
            ////    }
            ////}
            //_logger.LogInformation("Activate Result:" + retCode);

            ////初始化引擎
            //uint detectMode = DetectionMode.ASF_DETECT_MODE_IMAGE;
            ////Video模式下检测脸部的角度优先值
            //int videoDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_HIGHER_EXT;
            ////Image模式下检测脸部的角度优先值
            //int imageDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_ONLY;
            ////人脸在图片中所占比例，如果需要调整检测人脸尺寸请修改此值，有效数值为2-32
            //int detectFaceScaleVal = 16;
            ////最大需要检测的人脸个数
            //int detectFaceMaxNum = 5;
            ////引擎初始化时需要初始化的检测功能组合
            //int combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER | FaceEngineMask.ASF_FACE3DANGLE;
            ////初始化引擎，正常值为0，其他返回值请参考http://ai.arcsoft.com.cn/bbs/forum.php?mod=viewthread&tid=19&_dsign=dbad527e
            //retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pImageEngine);
            //_logger.LogInformation("InitEngine Result:" + retCode);
            //_logger.LogInformation((retCode == 0) ? "引擎初始化成功!\n" : string.Format("引擎初始化失败!错误码为:{0}\n", retCode));

            ////初始化视频模式下人脸检测引擎
            //uint detectModeVideo = DetectionMode.ASF_DETECT_MODE_VIDEO;
            //int combinedMaskVideo = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION;
            //retCode = ASFFunctions.ASFInitEngine(detectModeVideo, videoDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMaskVideo, ref pVideoEngine);
            //_logger.LogInformation("InitEngine Result:" + retCode);

            ////RGB视频专用FR引擎
            //detectFaceMaxNum = 1;
            //combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_LIVENESS;
            //retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pVideoRGBImageEngine);
            //_logger.LogInformation("InitEngine Result:" + retCode);

            ////IR视频专用FR引擎
            //combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_IR_LIVENESS;
            //retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pVideoIRImageEngine);
            //_logger.LogInformation("InitEngine Result:" + retCode);

            //_logger.LogInformation("InitVideoEngine Result:" + retCode);

            //var result = new FaceEngine();
            //result.ImageEngine = pImageEngine;
            //result.VideoEngine = pVideoEngine;
            //result.VideoRGBImageEngine = pVideoRGBImageEngine;
            //result.VideoIRImageEngine = pVideoIRImageEngine;

            //return result;
        }

        /// <summary>
        /// 获取人脸特征
        /// </summary>
        public void Close(FaceEngineProvider faceEngineProvider)
        {
            try
            {
                //销毁引擎
                faceEngineProvider.ImageEngine.ASFUninitEngine();
                _logger.LogInformation("UninitEngine pImageEngine Result:");
                //销毁引擎
                faceEngineProvider.VideoEngine.ASFUninitEngine();
                _logger.LogInformation("UninitEngine pVideoEngine Result:");

                //销毁引擎
                faceEngineProvider.VideoRGBImageEngine.ASFUninitEngine();
                _logger.LogInformation("UninitEngine pVideoImageEngine Result:");

                //销毁引擎
                faceEngineProvider.VideoIRImageEngine.ASFUninitEngine();
                _logger.LogInformation("UninitEngine pVideoIRImageEngine Result:");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("UninitEngine pImageEngine Error:" + ex);
            }
        }
    }
}
