using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Apis
{
    public interface IUploadImgApi
    {
        Task<string> UploadFileAsync(string path);
        Task<string> UploadPortraitAsync(string path, bool isCheck);
        Task<string> UploadPortraitAsync(byte[] img, bool isCheck, string extension);
    }
}
