using KT.Common.Core.Utils;
using KT.Prowatch.Service.DllModels;
using KT.Prowatch.Service.Helpers;
using KT.Prowatch.Service.IServices;
using KT.Prowatch.Service.Models;
using KT.Prowatch.WebApi.Common;
using KT.Prowatch.WebApi.Common.Filters;
using Microsoft.AspNetCore.Mvc;
using ProwatchAPICS;
using System;
using System.Collections.Generic;

namespace KT.Prowatch.WebApi.Controllers
{
    /// <summary>
    /// 权限管理
    /// </summary>
    [ApiController]
    [Route("")]
    [TypeFilter(typeof(LoginUserAttribute))]
    public class PermitController : ControllerBase
    {
        private IProwatchService _prowatchService;
        public PermitController(IProwatchService prowatchService)
        {
            _prowatchService = prowatchService;
        }

        /// <summary>
        /// 1. 向卡片添加访问码接口
        /// </summary>
        /// <param name="acCodeId">访问码 ID(sPA_AccessCode 中的 sID 字段)，不能为空</param>
        /// <param name="cardNo">卡号(sPA_Card 的 cardNo 字段) ，不能为空</param>
        /// <returns>无</returns>
        [HttpPost("addACCodeToCard")]
        public ResponseData<string> AddACCodeToCard([FromBody] ACCodeToCardModel acCodeToCard)
        {
            bool result = ApiHelper.PWApi.AddACCodeToCard(acCodeToCard?.ACCodeId.IsNull(), acCodeToCard?.CardNo.IsNull());
            return ResponseData<string>.Result(result, "向卡片添加访问码失败！");
        }
        /// <summary>
        /// 2. 删除卡片中的访问码接口
        /// </summary>
        /// <param name="acCodeId">访问码 ID(sPA_AccessCode 中的 sID 字段)，不能为空</param>
        /// <param name="cardNo">卡号(sPA_Card 的 cardNo 字段) ，不能为空</param>
        /// <returns>无</returns>
        [HttpPost("removeACCodeFromCard")]
        public ResponseData<string> RemoveACCodeFromCard([FromBody] ACCodeToCardModel acCodeToCard)
        {
            bool result = ApiHelper.PWApi.RemoveACCodeFromCard(acCodeToCard?.ACCodeId.IsNull(), acCodeToCard?.CardNo.IsNull());

            _prowatchService.DownloadCardStateExecQueueCardRetryDownload(acCodeToCard?.CardNo.IsNull());

            return ResponseData<string>.Result(result, "删除卡片中的访问码失败！");
        }

        /// <summary>
        /// 3. 将读卡器添加到访问码接口
        /// </summary>
        /// <param name="acCodeId">访问码 ID(sPA_AccessCode 中的 sID 字段)，不能为空</param>
        /// <param name="readerId">读卡器 ID(sPA_Reader 中的 sID 字段)，不能为空</param>
        /// <param name="timeZoneId">时区类型ID(sPA_TimeZone中的sID字段)，如空则使用访问码默认时区</param>
        /// <returns>无</returns>
        [HttpPost("addReaderToACCode")]
        public ResponseData<string> AddReaderToACCode([FromBody] ReaderToACCodeModel readerToACCode)
        {
            bool result = ApiHelper.PWApi.AddReaderToACCode(readerToACCode?.AcCodeId.IsNull(), readerToACCode?.ReaderId.IsNull(), readerToACCode?.TimeZoneId.IsNull());
            return ResponseData<string>.Result(result, "将读卡器添加到访问码失败！");
        }

        /// <summary>
        /// 4. 将读卡器从访问码中移除接口
        /// </summary>
        /// <param name="acCodeId">访问码 ID(sPA_AccessCode 中的 sID 字段)，不能为空</param>
        /// <param name="readerId">读卡器 ID(sPA_Reader 中的 sID 字段)，不能为空</param>
        /// <param name="timeZoneId">时区类型ID(sPA_TimeZone中的sID字段)，如空则使用访问码默认时区</param>
        /// <returns>无</returns>
        [HttpPost("removeReaderFromACCode")]
        public ResponseData<string> RemoveReaderFromACCode([FromBody] ReaderToACCodeModel readerToACCode)
        {
            bool result = ApiHelper.PWApi.RemoveReaderFromACCode(readerToACCode?.AcCodeId.IsNull(), readerToACCode?.ReaderId.IsNull(), readerToACCode?.TimeZoneId.IsNull());
            return ResponseData<string>.Result(result, "将读卡器从访问码中移除失败！");
        }

        /// <summary>
        /// 5. 获取卡能访问的所有读卡器接口
        /// </summary>
        /// <param name="cardNo">卡号(sPA_Card 的 cardNo 字段)</param>
        /// <returns>返回的读卡器信息列表</returns>
        [HttpGet("getCardReaders")]
        public ResponseData<List<ReaderData>> GetCardReaders(String cardNo)
        {
            List<sPA_Reader> data = new List<sPA_Reader>();
            bool result = ApiHelper.PWApi.GetCardReaders(cardNo.IsNull(), ref data);
            return ResponseData<List<ReaderData>>.Result(result, "获取卡能访问的所有读卡器失败！", data.ToModels());
        }

        /// <summary>
        /// 6. 获取卡片访问读卡器的时区和有效期限接口
        /// </summary>
        /// <param name="cardNo">卡号(sPA_Card 的 cardNo 字段)</param>
        /// <param name="readerId">读卡器 ID(sPA_Reader 中的 sID 字段)，不能为空</param>
        /// <returns> vTimeZone：返回的时区信息；sAccessDuration： 返回的访问有效期限信息（和时区一一对应）</returns>
        [HttpGet("getCardReaderTimeZones")]
        public ResponseData<TimeZoneAccessDurationModel> GetCardReaderTimeZones(String cardNo, String readerId)
        {
            List<sPA_TimeZone> vTimeZone = new List<sPA_TimeZone>();
            List<sPA_Access_Duration> vAccessDuration = new List<sPA_Access_Duration>();
            bool result = ApiHelper.PWApi.GetCardReaderTimeZones(cardNo.IsNull(), readerId.IsNull(), ref vTimeZone, ref vAccessDuration);

            TimeZoneAccessDurationModel vTimeZoneAccessDuration = new TimeZoneAccessDurationModel();
            vTimeZoneAccessDuration.TimeZones = vTimeZone.ToModels();
            vTimeZoneAccessDuration.AccessDurations = vAccessDuration.ToModels();
            return ResponseData<TimeZoneAccessDurationModel>.Result(result, "获取卡片访问读卡器的时区和有效期限失败！", vTimeZoneAccessDuration);
        }

        /// <summary>
        /// 7. 向卡添加读卡器访问接口
        /// </summary>
        /// <returns>无</returns>
        [HttpPost("addReaderToCard")]
        public ResponseData<string> AddReaderToCard([FromBody] ReaderToCardModel readerToCard)
        {
            bool result = ApiHelper.PWApi.AddReaderToCard(readerToCard.CardNo.IsNull(), readerToCard.ReaderId.IsNull(), readerToCard.TimeZoneId.IsNull(), readerToCard.TempAccess.IsNull(), readerToCard.TempAccessStartTime.IsNull(), readerToCard.TempAccessEndTime.IsNull());
            return ResponseData<string>.Result(result, "向卡添加读卡器访问失败！");
        }

        /// <summary>
        /// 8. 删除卡中的读卡器访问接口
        /// </summary>
        /// <param name="cardNo">卡号(sPA_Card 的 cardNo 字段)，不能为空</param>
        /// <param name="readerId">读卡器 ID(sPA_Reader 中的 sID 字段)，不能为空</param>
        /// <returns>无</returns>
        [HttpPost("removeReaderFromCard")]
        public ResponseData<string> RemoveReaderFromCard([FromBody] RemoveReaderCardModel removeReaderCard)
        {
            bool result = ApiHelper.PWApi.RemoveReaderFromCard(removeReaderCard?.CardNo.IsNull(), removeReaderCard?.ReaderId.IsNull());
            return ResponseData<string>.Result(result, "删除卡中的读卡器访问失败！");
        }

    }
}