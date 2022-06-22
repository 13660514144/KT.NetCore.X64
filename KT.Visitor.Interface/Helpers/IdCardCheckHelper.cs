
using ArcFaceSDK;
using ArcFaceSDK.Mothed;
using KT.Visitor.Common.Tools.ArcIdMatch.ArcIdSdk.Helpers;
using KT.Visitor.Interface.ViewModels;
using System;
using System.Drawing;

namespace KT.Visitor.Interface.Helpers
{
    public class IdCardCheckHelper
    {
        private ArcIdSdkHelper _arcIdCardHelper;
        private FaceEngine _FaceEngine;
        private FaceCheckMethod _FaceCheckMethod;
        public IdCardCheckHelper(ArcIdSdkHelper arcIdCardHelper,FaceEngine faceEngine,
            FaceCheckMethod faceCheckMethod)
        {
            _arcIdCardHelper = arcIdCardHelper;
            _FaceEngine = faceEngine;
            _FaceCheckMethod = faceCheckMethod;
        }
        /// <summary>
        /// 拍照对比
        /// </summary>
        /// <param name="idCardImage"></param>
        /// <param name="photoImage"></param>
        /// <param name="isCut"></param>
        /// <returns></returns>
        public IdCardCheckViewModel IdCardFaceCheck(Bitmap idCardImage, Image photoImage, bool isCut)
        {
            if (idCardImage == null)
            {
                var check = new IdCardCheckViewModel();
                //check.IdCardImage = new Bitmap(idCardImage);
                if (photoImage != null)
                {
                    check.PhotoImage = new Bitmap(photoImage);
                }
                check.Similarity = 0;
                check.Message = "人脸比对失败：身份证照片为空";

                return check;
            }

            if (photoImage == null)
            {
                var check = new IdCardCheckViewModel();
                check.IdCardImage = idCardImage;
                if (photoImage != null)
                {
                    check.PhotoImage = new Bitmap(photoImage);
                }
                check.Similarity = 0;
                check.Message = "人脸比对失败：照片为空";

                return check;
            }

            var idCardBitmap = new Bitmap(idCardImage);
            var photoBitmap = new Bitmap(photoImage);
            //GC.Collect();//释放资源
            return CheckIdCardFaceAsync(idCardBitmap, photoBitmap, isCut);
        }

        public IdCardCheckViewModel CheckIdCardFaceAsync(Bitmap idCardBitmap, Bitmap photoBitmap, bool isCut)
        {
            //x64
            IdCardCheckViewModel M = new IdCardCheckViewModel();
            M.IdCardImage = idCardBitmap;
            M.PhotoImage = photoBitmap;
            _FaceCheckMethod.ChooseMultiImg(idCardBitmap);
            _FaceCheckMethod.ChooseImg(photoBitmap);
            bool Flg = _FaceCheckMethod.CheckFaceFlg();
            if (Flg)
            {
                M.Face = new Bitmap("PhotoFace.jpg");
                M.Message = "人证核验成功";
            }
            else
            {
                M.Face = photoBitmap;
                M.Message = "人证核验失败";
            }
            M.Similarity = _FaceCheckMethod.CheckValue;
            return M;
            //x64
            /*
            //调用红软
            _arcIdCardHelper.IdCardDataFeatureExtraction(idCardBitmap);

            var idCardCheck = _arcIdCardHelper.FaceDataIdCardCompare(true, photoBitmap, isCut);
            //调用红软
            
            if (!idCardCheck.IsSuccess)
            {
                var check = new IdCardCheckViewModel();
                check.IdCardImage = idCardBitmap;
                if (idCardCheck.Face != null)
                {
                    check.Face = idCardCheck.Face;
                    check.PhotoImage = idCardCheck.Face;
                }
                else
                {
                    check.Face = photoBitmap;
                    check.PhotoImage = photoBitmap;
                }

                check.Similarity = idCardCheck.Similarity;
                check.Message = idCardCheck.Message;

                return check;
            }

            return null;
            */
        }

        public IdCardCheckViewModel IdCardFaceCheck(Image idCardImage, Image photoImage, bool isCut)
        {
            if (idCardImage == null)
            {
                var check = new IdCardCheckViewModel();
                if (photoImage != null)
                {
                    check.PhotoImage = new Bitmap(photoImage);
                }
                check.Similarity = 0;
                check.Message = "人脸比对失败：身份证照片为空";

                return check;
            }

            if (photoImage == null)
            {
                var check = new IdCardCheckViewModel();
                if (idCardImage != null)
                {
                    check.IdCardImage = new Bitmap(idCardImage);
                }
                check.Similarity = 0;
                check.Message = "人脸比对失败：照片为空";

                return check;
            }

            var idCardBitmap = new Bitmap(idCardImage);
            var photoBitmap = new Bitmap(photoImage);

            return CheckIdCardFaceAsync(idCardBitmap, photoBitmap, isCut);

        }
    }
}
