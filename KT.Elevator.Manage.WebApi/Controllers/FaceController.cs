using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpApi;
using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Manage.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.WebApi.Controllers
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
            if (result.IsExists)
            {
                return DataResponse<FaceInfoModel>.Result(10001, result.FaceInfo);
            }
            return DataResponse<FaceInfoModel>.Ok(result.FaceInfo);
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
            if (!System.IO.File.Exists(fileInfo.SourceUrl))
            {
                return null;
            }

            //获取文件的ContentType
            var fileExt = Path.GetExtension(fileInfo.SourceUrl);
            var provider = new FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];

            var stream = System.IO.File.OpenRead(fileInfo.SourceUrl);
            var file = File(stream, memi, "FileName.png");

            return file;
        }
    }
}
