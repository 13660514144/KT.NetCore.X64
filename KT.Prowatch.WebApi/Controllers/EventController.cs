using KT.Prowatch.Service.DllModels;
using KT.Prowatch.Service.Helpers;
using KT.Prowatch.WebApi.Common;
using KT.Prowatch.WebApi.Common.Filters;
using Microsoft.AspNetCore.Mvc;
using ProwatchAPICS;
using System.Collections.Generic;

namespace KT.Prowatch.WebApi.Controllers
{
    /// <summary>
    /// 事件
    /// </summary>
    [ApiController]
    [Route("")]
    [TypeFilter(typeof(LoginUserAttribute))]
    public class EventController : ControllerBase
    {
        /// <summary>
        /// 1. 开始接收实时事件接口
        /// </summary>
        /// <returns>无</returns>
        [HttpGet("startRecvRealEvent")]
        public ResponseData<string> StartRecvRealEvent()
        {
            bool result = ApiHelper.PWApi.StartRecvRealEvent();
            return ResponseData<string>.Result(result, "开始接收实时事件失败！");
        }

        /// <summary>
        /// 2. 接收实时事件接口
        /// </summary>
        /// <returns>无</returns>
        [HttpGet("stopRecvRealEvent")]
        public ResponseData<string> StopRecvRealEvent()
        {
            ApiHelper.PWApi.StopRecvRealEvent();
            return ResponseData<string>.Ok();
        }

        /// <summary>
        /// 2. 接收实时事件接口
        /// </summary>
        /// <returns>事件数据</returns>
        [HttpGet("getHistoryEvent")]
        public ResponseData<List<EventData>> GetHistoryEvent(int begin, int end, bool occurTime)
        {
            List<sPA_Event> data = new List<sPA_Event>();
            bool result = ApiHelper.PWApi.GetHistoryEvent(begin, end, occurTime, ref data);
            return ResponseData<List<EventData>>.Result(result, "接收实时事件失败！", data.ToModels());
        }

    }
}