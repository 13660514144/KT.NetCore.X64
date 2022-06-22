using ArcFaceSDK.Entity;
using ArcFaceSDK.SDKModels;
using ArcFaceSDK.Utils;
using ArcSoftFace.Entity;
using KT.Common.WpfApp.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace ArcFaceSDK.Mothed
{
    public class FaceCheckMethod
    {
        /// <summary>
        /// 图片最大大小限制
        /// </summary>
        public long maxSize = 1024 * 1024 * 2;

        /// <summary>
        /// 最大宽度
        /// </summary>
        public int maxWidth = 1536;

        /// <summary>
        /// 最大高度
        /// </summary>
        public int maxHeight = 1536;
        /// <summary>
        /// 比对模型
        /// </summary>
        public ASF_CompareModel compareModel = ASF_CompareModel.ASF_ID_PHOTO;
        /// <summary>
        /// 用于标记是否需要清除比对结果
        /// </summary>
        public bool isCompare = false;
        private FaceEngine imageEngine;
        private FaceRecognitionAppSettings _faceRecognitionAppSettings;
        private ILogger _logger;
        public FaceFeature Leftimg;
        public FaceFeature Rightimg;
        public int CheckValue;
        public FaceCheckMethod()
        {
            imageEngine = ContainerHelper.Resolve<FaceEngine>();
            _faceRecognitionAppSettings = ContainerHelper.Resolve<FaceRecognitionAppSettings>();
            Leftimg = new FaceFeature();
            Rightimg = new FaceFeature();
            _logger = ContainerHelper.Resolve<ILogger>();
        }

        /// <summary>
        /// 字节数组生成图片
        /// </summary>
        /// <param name="Bytes">字节数组</param>
        /// <returns>图片</returns>
        public Image byteArrayToImage(byte[] Bytes)
        {
            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                Image outputImg = Image.FromStream(ms);
                return outputImg;
            }
        }
        #region 注册人脸      
        /// <summary>
        /// 人脸库图片注册人脸
        /// </summary>
        public void ChooseMultiImg(string ImgagePath)
        {
            try
            {
                Leftimg = new FaceFeature();                
                Image image = ImageUtil.ReadFromFile(ImgagePath);
                //调整图像宽度，需要宽度为4的倍数
                if (image.Width % 4 != 0)
                {
                    image = ImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
                }
                //提取特征判断
                string featureResult = string.Empty;
                bool isMask;
                int retCode;
                SingleFaceInfo singleFaceInfo = new SingleFaceInfo();
                FaceFeature feature = FaceUtil.ExtractFeature(imageEngine, 
                    image,
                    _faceRecognitionAppSettings.ArcProFaceSettings.ThresholdImgRegister,// thresholdImgRegister, 
                    _faceRecognitionAppSettings.ArcProFaceSettings.ThresholdImgMask,//thresholdImgMask, 
                    ASF_RegisterOrNot.ASF_REGISTER, 
                    out singleFaceInfo, out isMask, 
                    ref featureResult, out retCode);
                //人脸检测
                MultiFaceInfo multiFaceInfo;
                retCode = imageEngine.ASFDetectFacesEx(image, out multiFaceInfo);
                if (!string.IsNullOrEmpty(featureResult))
                {

                    if (image != null)
                    {
                        image.Dispose();
                    }
                    
                }
                //判断检测结果
                if (retCode == 0 && multiFaceInfo.faceNum > 0)
                {
                    //多人脸时，默认裁剪第一个人脸                    
                    MRECT rect = multiFaceInfo.faceRects[0];
                    image = ImageUtil.CutImage(image, rect.left, rect.top, rect.right, rect.bottom);
                    image.Save("facephoto.jpg");
                    Leftimg = feature;
                }
                else
                {
                    //AppendText("未检测到人脸");
                    _logger.LogInformation("未检测到人脸");
                    if (image != null)
                    {
                        image.Dispose();
                    }
                   
                }
                //人脸检测和剪裁
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"人脸检测错误:{ex}");
            }
        }
        public void ChooseMultiImg(Image Img)
        {
            try
            {
                Leftimg = new FaceFeature();
                Image image = Img;
                //调整图像宽度，需要宽度为4的倍数
                if (image.Width % 4 != 0)
                {
                    image = ImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
                }
                //提取特征判断
                string featureResult = string.Empty;
                bool isMask;
                int retCode;
                SingleFaceInfo singleFaceInfo = new SingleFaceInfo();
                FaceFeature feature = FaceUtil.ExtractFeature(imageEngine,
                    image,
                    _faceRecognitionAppSettings.ArcProFaceSettings.ThresholdImgRegister,// thresholdImgRegister, 
                    _faceRecognitionAppSettings.ArcProFaceSettings.ThresholdImgMask,//thresholdImgMask, 
                    ASF_RegisterOrNot.ASF_REGISTER,
                    out singleFaceInfo, out isMask,
                    ref featureResult, out retCode);
                //人脸检测
                MultiFaceInfo multiFaceInfo;
                retCode = imageEngine.ASFDetectFacesEx(image, out multiFaceInfo);
                if (!string.IsNullOrEmpty(featureResult))
                {

                    if (image != null)
                    {
                        image.Dispose();
                    }

                }
                //判断检测结果
                if (retCode == 0 && multiFaceInfo.faceNum > 0)
                {
                    //多人脸时，默认裁剪第一个人脸                    
                    MRECT rect = multiFaceInfo.faceRects[0];
                    image = ImageUtil.CutImage(image, rect.left, rect.top, rect.right, rect.bottom);
                    image.Save("CodeFace.jpg");
                    Leftimg = feature;
                }
                else
                {
                    //AppendText("未检测到人脸");
                    _logger.LogInformation("未检测到人脸");
                }
                //人脸检测和剪裁
                if (image != null)
                {
                    image.Dispose();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"人脸检测错误:{ex}");
            }
        }
        #endregion

        #region 开始匹配事件
        /// <summary>
        /// 匹配人脸事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool CheckFaceFlg()
        {
            CheckValue = 0;
            try
            {
                if (Leftimg==null)
                {                    
                    return false;
                }

                if (Rightimg == null)
                {
                    /*if (picImageCompare.Image == null)
                    {
                        MessageBox.Show("请选择识别图!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("比对失败，识别图未提取到特征值!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }*/
                    return false;
                }
                //标记已经做了匹配比对，在开启视频的时候要清除比对结果
                isCompare = true;
                FaceFeature tempFaceFeature = Rightimg;
                FaceFeature feature = Leftimg;
                float similarity = 0f;
                float compareSimilarity = 0f;
                imageEngine.ASFFaceFeatureCompare(tempFaceFeature, feature, out similarity, compareModel);
                CheckValue = (int)(similarity*100);
                //增加异常值处理
                if (similarity.ToString().IndexOf("E") > -1)
                {
                    similarity = 0f;
                }
                if (similarity > compareSimilarity)
                {
                    compareSimilarity = similarity;                    
                }
                if (compareSimilarity >= _faceRecognitionAppSettings.ArcProFaceSettings.Threshold)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {                
                _logger.LogError($"check err :{ex}");
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 拍照人脸
        /// </summary>
        public void ChooseImg(string ImgPath)
        {
            Rightimg = new FaceFeature();
            try
            {
               
                //判断引擎是否初始化成功
                if (!imageEngine.GetEngineStatus())
                {
                    //禁用相关功能按钮
                    _logger.LogInformation("请先初始化引擎!");                    
                    return;
                }
                //检测图片格式
                if (!CheckImage(ImgPath))
                {
                    return;
                }
                //获取文件，拒绝过大的图片
                FileInfo fileInfo = new FileInfo(ImgPath);
                if (fileInfo.Length > maxSize)
                {
                    _logger.LogError("图像文件最大为2MB，请压缩后再导入!");
                    return;
                }

                Image srcImage = ImageUtil.ReadFromFile(ImgPath);
                //校验图片宽高
                CheckImageWidthAndHeight(ref srcImage);
                if (srcImage == null)
                {
                    _logger.LogError("图像数据获取失败，请稍后重试!");
                    return;
                }
                //调整图像宽度，需要宽度为4的倍数
                if (srcImage.Width % 4 != 0)
                {
                    srcImage = ImageUtil.ScaleImage(srcImage, srcImage.Width - (srcImage.Width % 4), srcImage.Height);
                }
                //人脸检测
                MultiFaceInfo multiFaceInfo;
                int retCode = imageEngine.ASFDetectFacesEx(srcImage, out multiFaceInfo);
                if (retCode != 0)
                {

                    _logger.LogError("图像人脸检测失败，请稍后重试!");
                    return;
                }
                if (multiFaceInfo.faceNum < 1)
                {
                    return;
                }
                //年龄检测
                int retCode_Age = -1;
                AgeInfo ageInfo = FaceUtil.AgeEstimation(imageEngine, srcImage, multiFaceInfo, out retCode_Age);
                //性别检测
                int retCode_Gender = -1;
                GenderInfo genderInfo = FaceUtil.GenderEstimation(imageEngine, srcImage, multiFaceInfo, out retCode_Gender);
                //3DAngle检测
                int retCode_3DAngle = -1;
                Face3DAngle face3DAngleInfo = FaceUtil.Face3DAngleDetection(imageEngine, srcImage, multiFaceInfo, out retCode_3DAngle);

                MRECT[] mrectTemp = new MRECT[multiFaceInfo.faceNum];
                int[] ageTemp = new int[multiFaceInfo.faceNum];
                int[] genderTemp = new int[multiFaceInfo.faceNum];
                bool[] maskTemp = new bool[multiFaceInfo.faceNum];
                SingleFaceInfo singleFaceInfo;

                MRECT rect = multiFaceInfo.faceRects[0];
               
                int orient = multiFaceInfo.faceOrients[0];
                int age = 0;
                //年龄检测
                if (retCode_Age != 0)
                {
                    _logger.LogError(string.Format("年龄检测失败，返回{0}!", retCode_Age));
                }
                else
                {
                    age = ageInfo.ageArray[0];
                }
                //性别检测
                int gender = -1;
                if (retCode_Gender != 0)
                {
                    _logger.LogError(string.Format("性别检测失败，返回{0}!", retCode_Gender));
                }
                else
                {
                    gender = genderInfo.genderArray[0];
                }
                //3DAngle检测
                float roll = 0f;
                float pitch = 0f;
                float yaw = 0f;
                if (retCode_3DAngle != 0)
                {
                    _logger.LogError(string.Format("3DAngle检测失败，返回{0}!", retCode_3DAngle));
                }
                else
                {
                    //roll为侧倾角，pitch为俯仰角，yaw为偏航角
                    roll = face3DAngleInfo.roll[0];
                    pitch = face3DAngleInfo.pitch[0];
                    yaw = face3DAngleInfo.yaw[0];
                }
                //口罩检测和提取人脸特征
                bool isMask;
                string faceFeatureStr = string.Empty;
                FaceFeature tempFaceFeature = FaceUtil.ExtractFeature(imageEngine,
                    srcImage,
                    _faceRecognitionAppSettings.ArcProFaceSettings.ThresholdImgNoMask,//thresholdImgNoMask, 
                    _faceRecognitionAppSettings.ArcProFaceSettings.ThresholdImgMask,//thresholdImgMask, 
                    ASF_RegisterOrNot.ASF_RECOGNITION,
                    out singleFaceInfo, out isMask, ref faceFeatureStr, out retCode, 0);
                if (retCode.Equals(0) && string.IsNullOrEmpty(faceFeatureStr))
                {
                    Rightimg = tempFaceFeature;
                }
                
                maskTemp[0] = isMask;
                mrectTemp[0] = rect;
                ageTemp[0] = age;
                genderTemp[0] = gender;                
            }
            catch (Exception ex)
            {
                _logger.LogError($"检测错误:{ex}");
            }
        }
        public void ChooseImg(Image Img)
        {
            Rightimg = new FaceFeature();
            try
            {

                //判断引擎是否初始化成功
                if (!imageEngine.GetEngineStatus())
                {
                    //禁用相关功能按钮
                    _logger.LogInformation("请先初始化引擎!");
                    return;
                }

                Image srcImage = Img;
                //校验图片宽高
                CheckImageWidthAndHeight(ref srcImage);
                if (srcImage == null)
                {
                    _logger.LogError("图像数据获取失败，请稍后重试!");
                    return;
                }
                //调整图像宽度，需要宽度为4的倍数
                if (srcImage.Width % 4 != 0)
                {
                    srcImage = ImageUtil.ScaleImage(srcImage, srcImage.Width - (srcImage.Width % 4), srcImage.Height);
                }
                //人脸检测
                MultiFaceInfo multiFaceInfo;
                int retCode = imageEngine.ASFDetectFacesEx(srcImage, out multiFaceInfo);
                if (retCode != 0)
                {

                    _logger.LogError("图像人脸检测失败，请稍后重试!");
                    return;
                }
                if (multiFaceInfo.faceNum < 1)
                {
                    return;
                }
                //年龄检测
                int retCode_Age = -1;
                AgeInfo ageInfo = FaceUtil.AgeEstimation(imageEngine, srcImage, multiFaceInfo, out retCode_Age);
                //性别检测
                int retCode_Gender = -1;
                GenderInfo genderInfo = FaceUtil.GenderEstimation(imageEngine, srcImage, multiFaceInfo, out retCode_Gender);
                //3DAngle检测
                int retCode_3DAngle = -1;
                Face3DAngle face3DAngleInfo = FaceUtil.Face3DAngleDetection(imageEngine, srcImage, multiFaceInfo, out retCode_3DAngle);

                MRECT[] mrectTemp = new MRECT[multiFaceInfo.faceNum];
                int[] ageTemp = new int[multiFaceInfo.faceNum];
                int[] genderTemp = new int[multiFaceInfo.faceNum];
                bool[] maskTemp = new bool[multiFaceInfo.faceNum];
                SingleFaceInfo singleFaceInfo;

                MRECT rect = multiFaceInfo.faceRects[0];
                Image image = ImageUtil.CutImage(srcImage, rect.left-50, rect.top-220 , rect.right+50 , rect.bottom+340 );
                image.Save("PhotoFace.jpg");

                int orient = multiFaceInfo.faceOrients[0];
                int age = 0;
                //年龄检测
                if (retCode_Age != 0)
                {
                    _logger.LogError(string.Format("年龄检测失败，返回{0}!", retCode_Age));
                }
                else
                {
                    age = ageInfo.ageArray[0];
                }
                //性别检测
                int gender = -1;
                if (retCode_Gender != 0)
                {
                    _logger.LogError(string.Format("性别检测失败，返回{0}!", retCode_Gender));
                }
                else
                {
                    gender = genderInfo.genderArray[0];
                }
                //3DAngle检测
                float roll = 0f;
                float pitch = 0f;
                float yaw = 0f;
                if (retCode_3DAngle != 0)
                {
                    _logger.LogError(string.Format("3DAngle检测失败，返回{0}!", retCode_3DAngle));
                }
                else
                {
                    //roll为侧倾角，pitch为俯仰角，yaw为偏航角
                    roll = face3DAngleInfo.roll[0];
                    pitch = face3DAngleInfo.pitch[0];
                    yaw = face3DAngleInfo.yaw[0];
                }
                //口罩检测和提取人脸特征
                bool isMask;
                string faceFeatureStr = string.Empty;
                FaceFeature tempFaceFeature = FaceUtil.ExtractFeature(imageEngine,
                    srcImage,
                    _faceRecognitionAppSettings.ArcProFaceSettings.ThresholdImgNoMask,//thresholdImgNoMask, 
                    _faceRecognitionAppSettings.ArcProFaceSettings.ThresholdImgMask,//thresholdImgMask, 
                    ASF_RegisterOrNot.ASF_RECOGNITION,
                    out singleFaceInfo, out isMask, ref faceFeatureStr, out retCode, 0);
                if (retCode.Equals(0) && string.IsNullOrEmpty(faceFeatureStr))
                {
                    Rightimg = tempFaceFeature;
                }

                maskTemp[0] = isMask;
                mrectTemp[0] = rect;
                ageTemp[0] = age;
                genderTemp[0] = gender;
            }
            catch (Exception ex)
            {
                _logger.LogError($"检测错误:{ex}");
            }
        }
        /// 校验图片
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public bool CheckImage(string imagePath)
        {
            try
            {
                if (imagePath == null)
                {                    
                    _logger.LogError($"图片不存在，请确认后再导入");
                    return false;
                }
                try
                {
                    //判断图片是否正常，如将其他文件把后缀改为.jpg，这样就会报错
                    Image image = ImageUtil.ReadFromFile(imagePath);
                    if (image == null)
                    {
                        throw new ArgumentException(" image is null");
                    }
                    else
                    {
                        image.Dispose();
                    }
                }
                catch
                {                    
                    _logger.LogError($"图片格式有问题，请确认后再导入");
                    return false;
                }
                FileInfo fileCheck = new FileInfo(imagePath);
                if (!fileCheck.Exists)
                {                    
                    _logger.LogError(string.Format("{0} 不存在", fileCheck.Name));
                    return false;
                }
                else if (fileCheck.Length > maxSize)
                {                    
                    _logger.LogError("{0} 图片大小超过2M，请压缩后再导入", fileCheck.Name);
                    return false;
                }
                else if (fileCheck.Length < 2)
                {                    
                    _logger.LogError(string.Format("{0} 图像质量太小，请重新选择", fileCheck.Name));
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"检测错误:{ex}");
            }
            return true;
        }
        /// <summary>
        /// 检查图片宽高
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public void CheckImageWidthAndHeight(ref Image image)
        {
            if (image == null)
            {
                return;
            }
            try
            {
                if (image.Width > maxWidth || image.Height > maxHeight)
                {
                    image = ImageUtil.ScaleImage(image, maxWidth, maxHeight);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"检测错误:{ex}");
            }
        }

        /// <summary>
        /// 检查图片宽高
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public void CheckBitmapWidthAndHeight(ref Bitmap bitmap)
        {
            if (bitmap == null)
            {
                return;
            }
            try
            {
                if (bitmap.Width > maxWidth || bitmap.Height > maxHeight)
                {
                    bitmap = (Bitmap)ImageUtil.ScaleImage(bitmap, maxWidth, maxHeight);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"检测错误:{ex}");
            }
        }
    }
}
