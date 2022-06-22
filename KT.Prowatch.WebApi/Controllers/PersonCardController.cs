
using KT.Common.Core.Utils;
using KT.Prowatch.Service.DllModels;
using KT.Prowatch.Service.Enums;
using KT.Prowatch.Service.Helpers;
using KT.Prowatch.Service.IServices;
using KT.Prowatch.Service.Models;
using KT.Prowatch.WebApi.Common;
using KT.Prowatch.WebApi.Common.Filters;
using Microsoft.AspNetCore.Mvc;
using ProwatchAPICS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Prowatch.WebApi.Controllers
{
    /// <summary>
    /// 人员与卡
    /// </summary>
    [ApiController]
    [Route("")]
    [TypeFilter(typeof(LoginUserAttribute))]
    public class PersonCardController : ControllerBase
    {
        private IProwatchService _prowatchService;
        private DownloadCardStateQueue _downloadCardStateQueue;

        public PersonCardController(IProwatchService prowatchService,
            DownloadCardStateQueue downloadCardStateQueue)
        {
            _prowatchService = prowatchService;
            _downloadCardStateQueue = downloadCardStateQueue;
        }

        /// <summary>
        ///  1. 按条件查找人员信息接口
        /// </summary>
        /// <param name="field">查询条件字段，支持的字段名包括（不区分大小写）： ("ID", "LNAME", "FNAME", "ISSUE_DATE", "EXPIRE_DATE")</param>
        /// <param name="oper">查询条件比较关键字，支持的关键字包括（不区分大小写）： (">", "&lt;", "=", ">=", "&lt;=", "!=", "like", "not like")</param>
        /// <param name="value">查询条件值</param>
        /// <returns>返回符合条件的人员信息列表</returns>
        [HttpGet("queryPersons")]
        public ResponseData<List<PersonData>> QueryPersons(String field, String oper, String value)
        {
            List<sPA_Person> data = new List<sPA_Person>();
            bool result = ApiHelper.PWApi.QueryPersons(field.IsNull(), oper.IsNull(), value.IsNull(), ref data);
            return ResponseData<List<PersonData>>.Result(result, "按条件查找人员信息失败！", data.ToModels());
        }

        /// <summary>
        /// 2. 按条件查找卡信息接口
        /// </summary>
        /// <param name="field">查询条件字段，支持的字段名包括（不区分大小写）：("ID", " CARDNO", " STAT_COD", "ISSUE_DATE", "EXPIRE_DATE") </param>
        /// <param name="oper">查询条件比较关键字，支持的关键字包括（不区分大小写）： (">", "&lt;", "=", ">=", "&lt;=", "!=", "like", "not like")</param>
        /// <param name="value">查询条件值</param>
        /// <returns>返回符合条件的卡信息列表</returns>
        [HttpGet("queryCards")]
        public ResponseData<List<CardData>> QueryCards(String field, String oper, String value)
        {
            List<sPA_Card> data = new List<sPA_Card>();
            bool result = ApiHelper.PWApi.QueryCards(field.IsNull(), oper.IsNull(), value.IsNull(), ref data);
            return ResponseData<List<CardData>>.Result(result, "按条件查找卡信息失败！", data.ToModels());
        }

        /// <summary>
        /// 3. 添加人员接口
        /// </summary> 
        /// <param name="person">人员信息，其中 sLNAME 为必需字段;badgeTypeId卡证类型ID，必需参数</param>
        /// <returns>返回所创建人员的ID（sPA_Person的ID字段）</returns>
        [HttpPost("addPerson")]
        public ResponseData<string> AddPerson([FromBody] PersonData person)
        {
            string personId = "";
            bool result = ApiHelper.PWApi.AddPerson(person.ToSPA(), person.BadgeTypeId.IsNull(), ref personId);
            return ResponseData<string>.Result(result, "添加人员失败！", personId);
        }

        /// <summary>
        /// 4. 删除人员接口
        /// </summary>
        /// <param name="personId">人员 ID（sPA_Person 的 ID 字段）</param>
        /// <returns>无</returns>
        [HttpPost("removePerson")]
        public ResponseData<string> RemovePerson([FromBody] RemovePersonModel removePerson)
        {
            bool result = ApiHelper.PWApi.RemovePerson(removePerson.PersonId.IsNull());
            return ResponseData<string>.Result(result, "删除人员失败！");
        }

        /// <summary>
        /// 5. 添加卡接口
        /// </summary>
        /// <param name="card">卡信息，sCardNO 为必需字段;公司类型ID，必需参数</param>
        /// <returns>无</returns>
        [HttpPost("addCard")]
        public ResponseData<string> AddCard([FromBody] CardData card)
        {
            bool result = ApiHelper.PWApi.AddCard(card.ToSPA(), card.CompanyId.IsNull());
            return ResponseData<string>.Result(result, "添加卡失败！");
        }

        /// <summary>
        /// 6. 删除卡接口
        /// </summary>
        /// <param name="cardNo">卡号（sPA_Card 的 sCardNO 字段）</param>
        /// <returns>无</returns>
        [HttpPost("removeCard")]
        public ResponseData<string> RemoveCard([FromBody] RemoveCardModel removeCard)
        {
            bool result = ApiHelper.PWApi.RemoveCard(removeCard?.CardNo.IsNull());
            return ResponseData<string>.Result(result, "删除卡失败！");
        }

        /// <summary>
        /// 7. 修改卡接口
        /// </summary>
        /// <param name="card">卡信息，sCardNO 为必需字段;公司类型ID，必需参数</param>
        /// <returns>无</returns>
        [HttpPost("updateCard")]
        public async Task<ResponseData<string>> UpdateCardAsync([FromBody] CardData card)
        {
            var result = await _prowatchService.UpdateCardAsync(card);
            return ResponseData<string>.Result(result, "修改卡失败！");
        }
    }
}