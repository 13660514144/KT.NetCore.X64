using KT.Common.Core.Utils;
using KT.Prowatch.Service.DllModels;
using KT.Prowatch.Service.Helpers;
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
    /// 读卡器信息
    /// </summary>
    [ApiController]
    [Route("")]
    [TypeFilter(typeof(LoginUserAttribute))]
    public class LogicalDevController : ControllerBase
    {
        /// <summary>
        /// 1. 按条件查找读卡器接口
        /// </summary>
        /// <param name="field">查询条件字段，支持的字段名包括（不区分大小写）：("ID", "DESCRP", "LOCATION", "ALT_DESCRP", "PANEL", "SITE")</param>
        /// <param name="oper">查询条件比较关键字，支持的关键字包括（不区分大小写）：(">", "&lt;", "=", ">=", "&lt;=", "!=", "like", "not like")</param>
        /// <param name="value">查询条件值</param>
        /// <returns>返回的读卡器信息列表</returns>
        [HttpGet("queryReaders")]
        public ResponseData<List<ReaderData>> QueryReaders(String field, String oper, String value)
        {
            List<sPA_Reader> data = new List<sPA_Reader>();
            bool result = ApiHelper.PWApi.QueryReaders(field.IsNull(), oper.IsNull(), value.IsNull(), ref data);
            return ResponseData<List<ReaderData>>.Result(result, "按条件查找读卡器失败！", data.ToModels());
        }

        /// <summary>
        /// 2. 读卡器控制
        /// </summary>
        /// <param name="readerControl">readerId：读卡器 ID(sPA_Reader 中的 sID 字段) cmd:控制类型，支持包括如下类型：1：再启用，2：瞬间解锁，3：解锁，4：锁</param> 
        /// <returns>无</returns>
        [HttpGet("readerControl")]
        public ResponseData<string> ReaderControl(ReaderControlModel readerControl)
        {
            if (readerControl == null)
            {
                return ResponseData<string>.Error("参数错误：未接收到传入的参数");
            }
            bool result = ApiHelper.PWApi.ReaderControl(readerControl?.ReaderId.IsNull(), readerControl.Cmd);
            return ResponseData<string>.Result(result, "读卡器控制失败！");
        }

        /// <summary>
        /// 3. 获取读卡器状态接口
        /// </summary>
        /// <param name="readerId"> 读卡器 ID(sPA_Reader 中的 sID 字段)</param>
        /// <returns>返回的读卡器状态，包括：NORMAL：正常，LOCKED：闭锁，OPEN：打开</returns>
        [HttpGet("getReaderStatus")]
        public ResponseData<string> GetReaderStatus(String readerId)
        {
            string sStatus = string.Empty;
            bool result = ApiHelper.PWApi.GetReaderStatus(readerId.IsNull(), ref sStatus);
            return ResponseData<string>.Result(result, "获取读卡器状态接口失败", sStatus);
        }
    }
}