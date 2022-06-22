using KT.Common.WebApi.HttpApi;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Common
{
    /// <summary>
    /// 文件
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FaceController : ControllerBase
    {
        private ILogger<FaceController> _logger;
        private IFaceInfoService _faceInfoService;
        private IPassRightService _passRightService;

        public FaceController(ILogger<FaceController> logger,
            IFaceInfoService faceInfoService,
            IPassRightService passRightService)
        {
            _logger = logger;
            _faceInfoService = faceInfoService;
            _passRightService = passRightService;
        }

        [HttpPost("upload")]
        public async Task<DataResponse<FaceInfoModel>> UploadAsync([FromForm] FaceRequestModel faceRequest)
        {
            var result = await _faceInfoService.AddOrEditAsync(faceRequest);

            //返回结果 
            return DataResponse<FaceInfoModel>.Ok(result);
        }

        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(FaceInfoModel fileInfo)
        {
            await _faceInfoService.DeleteAsync(fileInfo.Id);
            await _passRightService.DeleteBySignAsync(fileInfo.Id);
            return VoidResponse.Ok();
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadAsync(string id)
        {
            var fileInfo = await _faceInfoService.GetByIdAsync(id);
            if (fileInfo == null)
            {
                return null;
            }
            if (!System.IO.File.Exists(fileInfo.FaceUrl))
            {
                return null;
            }

            //获取文件的ContentType
            var fileExt = Path.GetExtension(fileInfo.FaceUrl);
            var provider = new FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];

            using var stream = System.IO.File.OpenRead(fileInfo.FaceUrl);
            var file = File(stream, memi, "FileName.jpg");

            return file;
        }
    }
}
