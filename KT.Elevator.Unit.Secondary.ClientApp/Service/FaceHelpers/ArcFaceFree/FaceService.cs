using KT.Elevator.Unit.Secondary.ClientApp.Service.IServices;
using KT.Quanta.Common.Enums;
using KT.Unit.Face.Arc.Free.Models;
using KT.Unit.FaceRecognition.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.FaceHelpers.ArcFaceFree
{
    public class FaceService : IFaceService
    {
        private IPassRightService _passRightService;
        private FaceFactory _faceFactory;
        private ArcFaceSettings _arcFaceSettings;
        private ILogger _logger;
        private readonly FaceRecognitionAppSettings _faceRecognitionAppSettings;

        public FaceService(IPassRightService passRightService,
            FaceFactory faceFactory,
            FaceRecognitionAppSettings faceRecognitionAppSettings,
            ILogger logger)
        {
            _passRightService = passRightService;
            _faceFactory = faceFactory;
            _faceRecognitionAppSettings = faceRecognitionAppSettings;
            _arcFaceSettings = _faceRecognitionAppSettings.ArcFreeFaceSettings;
            _logger = logger;
        }

        /// <summary>
        /// 加载本地人脸
        /// </summary>
        public async Task InitFaceAsync()
        {
            try
            {
                //初始化人脸数据 
                await InitByPageAsync(1, _arcFaceSettings.InitPageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError($"初始化人脸数据失败：page:{1} size:{_arcFaceSettings.InitPageSize} ex:{ex} ");
            }
        }

        /// <summary>
        /// 初始化本地人脸数据，从数据库中获取数据
        /// </summary>
        /// <param name="page">页</param>
        /// <param name="size">每页条数</param> 
        private async Task InitByPageAsync(int page, int size)
        {
            var faceRights = await _passRightService.GetByPageAsync(AccessTypeEnum.FACE.Value, page, size);
            if (faceRights != null && faceRights.FirstOrDefault() != null)
            {
                _faceFactory.InitPageDataAsync(faceRights);
                //获取下一页内容
                await InitByPageAsync(page + 1, size);
            }
        }

    }
}
