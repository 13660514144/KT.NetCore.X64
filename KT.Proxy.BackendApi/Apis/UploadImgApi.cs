using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpApi;
using KT.Proxy.BackendApi.Helpers;
using KT.Proxy.BackendApi.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Apis
{
    public class UploadImgApi : BackendApiBase, IUploadImgApi
    {
        public UploadImgApi(ILogger<UploadImgApi> logger) : base(logger)
        {

        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="path"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public async Task<string> UploadFileAsync(string path)
        {
            var result = await PostFileAsync<string>("/image/upload", path);
            return result;
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="path"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public async Task<string> UploadPortraitAsync(string path, bool isCheck)
        {
            var parmeters = new Dictionary<string, object>();
            parmeters.Add("check", isCheck);

            var result = await PostFileAsync<string>("/image/upload", path, parmeters);
            return result;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="img"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public async Task<string> UploadPortraitAsync(byte[] img, bool isCheck, string extension)
        {
            var parmeters = new Dictionary<string, object>();
            parmeters.Add("check", isCheck);
            var result = await PostFileBytesAsync<string>("/image/upload", img, $"file{extension}", parmeters);
            return result;
        }
    }
}
