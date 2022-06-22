using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpApi;
using KT.Elevator.Unit.Secondary.ClientApp.Expands;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;

namespace KT.Proxy.BackendApi.Apis
{
    public class KoalaFaceApi : HttpApiBase
    {
        public ILogger<KoalaFaceApi> _logger;

        public KoalaFaceApi(ILogger<KoalaFaceApi> logger) : base(logger)
        {
            _logger = logger;
        }

        //上传数据是完整地址，不能设置BaseUrl
        public override string BaseUrl
        {
            get
            {
                return string.Empty;
            }
        }

        public async Task<KoalaResponse> PushRecordAsync(string url, Bitmap image, int rectleft, int recttop, int rectright, int rectbottom)
        {
            _logger.LogInformation($"考拉人脸匹对！ ");

            var cutFace = CutFace(image, rectleft, recttop, rectright, rectbottom);

            //上传图片到服务器 
            var imageBytes = Common.WpfApp.Utils.ImageConvert.ImageToBytes(image);

            //保存图片
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files\\Images\\Portraits", $"{DateTimeUtil.UtcNowMillis()}.jpg");
            KT.Common.Core.Utils.ImageUtil.SaveToFile(imageBytes, path, true, KT.Common.WpfApp.Utils.ImageUtil.GetFormat(".jpg"));

            var filePaths = new Dictionary<string, string>();
            filePaths.Add("image", path);

            var response = await ExecutePostFilesAsync(url, filePaths);

            var result = JsonConvert.DeserializeObject<KoalaResponse>(response.Content, JsonUtil.JsonSettings);

            return result;
        }

        public Bitmap CutFace(Bitmap bitmap, int rectleft, int recttop, int rectright, int rectbottom)
        {
            var cutExtension = new Thickness(0.1, 0.3, 0.1, 0.3);
            var width = rectright - rectleft;
            var height = rectbottom - recttop;

            var left = rectleft - (width * cutExtension.Left);
            if (left < 0)
            {
                left = 0;
            }
            var right = rectright + (width * cutExtension.Right);
            if (right > bitmap.Width)
            {
                right = bitmap.Width;
            }

            var top = recttop - (height * cutExtension.Top);
            if (top < 0)
            {
                top = 0;
            }
            var bottom = rectbottom + (height * cutExtension.Bottom);
            if (bottom > bitmap.Height)
            {
                bottom = bitmap.Height;
            }

            var newBitmap = ImageUtil.CutImage(bitmap, (int)left, (int)top, (int)right, (int)bottom);
            return newBitmap;
        }

    }
}
