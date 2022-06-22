using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.Utils;
using KT.Proxy.BackendApi.Apis;
using KT.Visitor.Common.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Common.Helpers
{
    public class UploadPhotoHelper
    {
        private IFunctionApi _functionApi;
        private ILogger _logger;
        private AppSettings _appSettings;

        public UploadPhotoHelper()
        {
            _functionApi = ContainerHelper.Resolve<IFunctionApi>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();

        }

        public async Task<string> UploadPortraitAsync(Bitmap bitmap, bool isCheckPhoto)
        {
            if (bitmap != null)
            {
                //上传图片到服务器 
                var imageBytes = ImageConvert.ImageToBytes(bitmap);

                //string imageUrl = await ContainerHelper.Resolve<UploadImgApi>().UploadPortraitAsync(imageBytes, isCheckPhoto, _appSettings.PhotoExtenstion);

                ////保存图片
                //KT.Common.Core.Utils.ImageUtil.SaveToFile(imageBytes, "Files\\Images\\Portraits", $"{DateTimeUtil.UtcNowMillis()}{_appSettings.PhotoExtenstion}", true, KT.Common.WpfApp.Utils.ImageUtil.GetFormat(_appSettings.PhotoExtenstion));

                //保存图片
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files\\Images\\Portraits", $"{DateTimeUtil.UtcNowMillis()}{_appSettings.PhotoExtenstion}");
                KT.Common.Core.Utils.ImageUtil.SaveToFile(imageBytes, path, true, KT.Common.WpfApp.Utils.ImageUtil.GetFormat(_appSettings.PhotoExtenstion));

                //上传图片
                string imageUrl = await ContainerHelper.Resolve<IUploadImgApi>().UploadPortraitAsync(path, isCheckPhoto);

                //返回照片地址
                return imageUrl;
            }
            throw CustomException.Run("照片转换出错，请重试");
        }
    }
}
