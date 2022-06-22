using KT.Common.Core.Exceptions;
using KT.Visitor.Common.Settings;
using KT.Visitor.Common.Tools.ArcIdMatch.ArcIdSdk.SDKModels;
using KT.Visitor.Common.Tools.ArcIdMatch.ArcIdSdk.SDKUtil;
using KT.Visitor.Common.Tools.ArcIdMatch.ArcIdSdk.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;

namespace KT.Visitor.Common.Tools.ArcIdMatch.ArcIdSdk.Helpers
{
    /// <summary>
    /// 人证比对，静态
    /// </summary>
    public class ArcIdSdkHelper : IArcIdMatchHelper
    {
        //引擎Handle
        private IntPtr pEngine = IntPtr.Zero;

        private ArcFaceSettings _arcFaceSettings;
        private ILogger _logger;
        public ArcIdSdkHelper(ArcFaceSettings arcFaceSettings, ILogger logger)
        {
            _arcFaceSettings = arcFaceSettings;
            _logger = logger;
        }

        /// <summary>
        /// 初始化引擎
        /// </summary>
        public void Init()
        {
            //读取配置文件 
            string appId = _arcFaceSettings.AppId;
            string sdkKey64 = _arcFaceSettings.SdkKey64;
            string sdkKey32 = _arcFaceSettings.SdkKey32;
            //x86  X64判断
            var is64CPU = Environment.Is64BitProcess;
            if (is64CPU)
            {
                if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(sdkKey64))
                {
                    throw CustomException.Run("请在App.config配置文件中先配置APP_ID和SDKKEY64!");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(sdkKey32))
                {
                    throw CustomException.Run("请在App.config配置文件中先配置APP_ID和SDKKEY32!");
                }
            }

            //激活引擎 如出现错误，1.请先确认从官网下载的sdk库已放到对应的bin中，2.当前选择的CPU为x86或者x64
            int retCode = ASIDCardFunctions.ArcSoft_FIC_Activate(appId, is64CPU ? sdkKey64 : sdkKey32);

            _logger.LogInformation("Activate Result:" + retCode);

            //初始化引擎
            retCode = ASIDCardFunctions.ArcSoft_FIC_InitialEngine(ref pEngine);

            _logger.LogInformation("ArcIdSdkHelper InitEngine Result:" + retCode);

            if (retCode != 0)
            {
                throw CustomException.Run($"ArcIdSdkHelper 引擎初始化失败!错误码为:{retCode} ");
            }
        }

        /// <summary>
        /// 初始化引擎
        /// </summary>
        public void Close()
        {
            //销毁引擎
            int retCode = ASIDCardFunctions.ArcSoft_FIC_UninitialEngine(pEngine);
            _logger.LogInformation($"UninitEngine Result:{retCode} ");
        }

        /// <summary>
        /// 提取图片特征值
        /// </summary>
        /// <param name="portrait"></param>
        public void IdCardDataFeatureExtraction(Image portrait)
        {
            if (portrait == null)
            {
                throw CustomException.Run("证件照片为空！");
            }
            var retCode = IDCardUtil.IdCardDataFeatureExtraction(pEngine, portrait);
            _logger.LogInformation($"IdCardDataFeatureExtraction Result:{retCode} ");
        }

        /// <summary>
        /// 人证比对
        /// </summary>
        /// <param name="isVideo"></param>
        /// <param name="bitmap"></param>
        public FaceCompareModel FaceDataIdCardCompare(bool isVideo, Bitmap bitmap, bool isCut)
        {
            var faceCompare = new FaceCompareModel();
            //人证比对
            if (bitmap == null)
            {
                faceCompare.IsSuccess = false;
                faceCompare.Message = "照片为空";
                return faceCompare;
            }

            //图片转换为4的倍数
            Image image = bitmap;
            if (image.Width % 4 != 0)
            {
                image = ImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
            }
            bitmap = new Bitmap(image);

            var faceInfo = new AFIC_FSDK_FACERES();
            int result = IDCardUtil.FaceDataFeatureExtraction(pEngine, isVideo, bitmap, ref faceInfo);
            if (result != 0)
            {
                faceCompare.IsSuccess = false;
                faceCompare.Message = $"人证核验失败：错误码:{result}";
                return faceCompare;
            }
            if (faceInfo.nFace <= 0)
            {
                faceCompare.IsSuccess = false;
                faceCompare.Message = $"检测不到人脸";
                return faceCompare;
            }

            float pSimilarScore = 0;
            int pResult = 0;
            float threshold = _arcFaceSettings.Threshold;

            result = IDCardUtil.FaceIdCardCompare(ref pSimilarScore, ref pResult, pEngine, threshold);
            if (result == 0)
            {
                if (threshold > pSimilarScore)
                {
                    faceCompare.IsSuccess = false;
                    faceCompare.Message = $"人证核验失败";
                    faceCompare.Similarity = (int)(pSimilarScore * 100);
                    return faceCompare;
                }
                else
                {
                    faceCompare.IsSuccess = true;
                    faceCompare.Message = $"人证核验成功";
                    faceCompare.Similarity = (int)(pSimilarScore * 100);
                    faceCompare.Rect = faceInfo.rcFace;
                    if (isCut)
                    {
                        faceCompare.Face = CutFace(bitmap, faceInfo.rcFace);
                    }
                    return faceCompare;
                }
            }
            else
            {
                faceCompare.IsSuccess = true;
                faceCompare.Message = $"人证核验失败，错误码：{result}";
                faceCompare.Similarity = (int)(pSimilarScore * 100);
                return faceCompare;
            }
        }
        /// <summary>
        /// 图片剪切
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public Bitmap CutFace(Bitmap bitmap, RECT rect)
        {
            //return bitmap;
            
            var width = rect.right - rect.left;
            var height = rect.bottom - rect.top;

            var left = rect.left - (width * _arcFaceSettings.CutExtension.Left);
            if (left < 0)
            {
                left = 0;
            }
            var right = rect.right + (width * _arcFaceSettings.CutExtension.Right);
            if (right > bitmap.Width)
            {
                right = bitmap.Width;
            }

            var top = rect.top - (height * _arcFaceSettings.CutExtension.Top);
            if (top < 0)
            {
                top = 0;
            }
            var bottom = rect.bottom + (height * _arcFaceSettings.CutExtension.Bottom);
            if (bottom > bitmap.Height)
            {
                bottom = bitmap.Height;
            }

            var newBitmap = ImageUtil.CutImage(bitmap, (int)left, (int)top, (int)right, (int)bottom);
            return newBitmap;
            
        }

        /// <summary>
        /// 人证比对
        /// </summary>
        /// <param name="isVideo"></param>
        /// <param name="bitmap"></param>
        public FaceCompareModel GetCutFace(Bitmap bitmap)
        {
            var faceCompare = new FaceCompareModel();
            //人证比对
            if (bitmap == null)
            {
                faceCompare.IsSuccess = false;
                faceCompare.Message = "照片为空";
                return faceCompare;
            }

            //图片转换为4的倍数
            Image image = bitmap;
            if (image.Width % 4 != 0)
            {
                image = ImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
            }
            bitmap = new Bitmap(image);

            var faceInfo = new AFIC_FSDK_FACERES();
            int result = IDCardUtil.FaceDataFeatureExtraction(pEngine, false, bitmap, ref faceInfo);
            if (result != 0)
            {
                faceCompare.IsSuccess = false;
                faceCompare.Message = $"人证核验失败：错误码:{result}";
                return faceCompare;
            }
            if (faceInfo.nFace <= 0)
            {
                faceCompare.IsSuccess = false;
                faceCompare.Message = $"检测不到人脸";
                return faceCompare;
            }

            faceCompare.Face = CutFace(bitmap, faceInfo.rcFace);

            return faceCompare;
        }
    }

    public class FaceCompareModel
    {
        public bool IsSuccess { get; set; }

        public int Similarity { get; set; }

        public string Message { get; set; }

        public RECT Rect { get; set; }

        public Bitmap Face { get; set; }
    }

}
