using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.Utils;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Apis;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.ViewModels;
using System.Drawing;
using System.Threading.Tasks;

namespace KT.Visitor.Interface.Views.Register
{
    public class ReadIdCardControlViewModel : BindableBase
    {
        private IFunctionApi _functionApi;
        private IdCardCheckHelper _idCardCheckHelper;
        private DialogHelper _dialogHelper;

        public ReadIdCardControlViewModel()
        {
            _functionApi = ContainerHelper.Resolve<IFunctionApi>();
            _idCardCheckHelper = ContainerHelper.Resolve<IdCardCheckHelper>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();
        }

        public async Task<bool> CheckIdCardFaceAsync(Image idCardImage, string photoUrl)
        {
            var visitorConfig = await _functionApi.GetConfigParmsAsync();
            if (visitorConfig?.OpenVisitorCheck == true)
            {
                if (string.IsNullOrEmpty(photoUrl))
                {
                    ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("访客预约未上传人脸图片，无法人证对比");
                    return false;
                }
                else
                {
                    string[] url = photoUrl.Trim().Split('/');
                    if (url[3].Length < 1)
                    {
                        ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("访客预约未上传人脸图片，无法人证对比");
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
                   
            IdCardCheckViewModel openVisitorCheck = null;

            var photoInfo = await _functionApi.GetPictureAsync(photoUrl);
            var lenth = photoInfo.Bytes?.Length;
            if (lenth.IfNullOrLessEqualZero())
            {
                openVisitorCheck = new IdCardCheckViewModel();
                if (idCardImage != null)
                {
                    openVisitorCheck.IdCardImage = new Bitmap(idCardImage);
                }
                openVisitorCheck.Similarity = 0;
                openVisitorCheck.Message = "人脸比对失败：照片为空";
            }
            else
            {
                //人证比对 
                //var visitorConfig = await _functionApi.GetConfigParmsAsync();
                //人证比对
                if (visitorConfig?.OpenVisitorCheck == true)
                {
                    var photoImage = ImageConvert.BytesToBitmap(photoInfo.Bytes);
                    //openVisitorCheck = _idCardCheckHelper.IdCardFaceCheck(idCardImage, photoImage, false);
                    openVisitorCheck = _idCardCheckHelper.CheckIdCardFaceAsync(new Bitmap(idCardImage), photoImage, false);
                }
            }

            if (openVisitorCheck != null)
            {
                var warnWindow = ContainerHelper.Resolve<IdCardCheckWarnWindow>();
                warnWindow.SetButtonText("重刷证件");
                warnWindow.ViewModel.IdCardCheck = openVisitorCheck;
                var warnResult = _dialogHelper.ShowDialog(warnWindow);
                if (!warnResult.HasValue || !warnResult.Value)
                {
                    //重新拍照 
                    return false;
                }
            }
            return true;
        }
    }
}
