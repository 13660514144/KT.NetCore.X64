using KT.Common.Core.Utils;
using KT.Prowatch.Service.DllModels;
using KT.Prowatch.Service.Helpers;
using KT.Prowatch.Service.IServices;
using KT.Prowatch.WebApi.Common;
using KT.Prowatch.WebApi.Common.Filters;
using Microsoft.AspNetCore.Mvc;
using ProwatchAPICS;
using System;
using System.Collections.Generic;

namespace KT.Prowatch.WebApi.Controllers
{
    /// <summary>
    /// 基础信息接口
    /// </summary>
    [ApiController]
    [Route("")]
    [TypeFilter(typeof(LoginUserAttribute))]
    public class PWAPIController : ControllerBase
    {
        private IProwatchService _prowatchService;
        public PWAPIController(IProwatchService prowatchService)
        {
            _prowatchService = prowatchService;
        }

        /// <summary>
        /// 3、获取人员信息接口
        /// </summary>
        /// <returns>返回的人员信息列表</returns>
        [HttpGet("getAllPersons")]
        public ResponseData<List<PersonData>> GetAllPersons()
        {
            List<sPA_Person> data = new List<sPA_Person>();
            bool result = ApiHelper.PWApi.GetAllPersons(ref data);
            return ResponseData<List<PersonData>>.Result(result, "获取人员信息失败！", data.ToModels());
        }

        /// <summary>
        /// 4、获取卡证类型信息
        /// </summary>
        /// <param name="personId">人员 ID(sPA_Person 的 ID 字段)，如为空则返回所有卡证类型信息列表</param>
        /// <returns>返回卡证类型信息列表</returns>
        [HttpGet("getAllBadgeTypes")]
        public ResponseData<List<BadgeTypeData>> GetAllBadgeTypes(string personId)
        {
            List<sPA_BadgeType> data = new List<sPA_BadgeType>();
            bool result = ApiHelper.PWApi.GetAllBadgeTypes(personId.IsNull(), ref data);
            return ResponseData<List<BadgeTypeData>>.Result(result, "获取卡证类型失败！", data.ToModels());
        }

        /// <summary>
        /// 5、获取卡信息接口
        /// </summary>
        /// <param name="personId">人员 ID(sPA_Person 的 ID 字段)，如为空则返回所有卡</param>
        /// <returns>返回的卡信息列表</returns>
        [HttpGet("getAllCards")]
        public ResponseData<List<CardData>> GetAllCards(string personId)
        {
            List<sPA_Card> data = new List<sPA_Card>();
            bool result = ApiHelper.PWApi.GetAllCards(personId.IsNull(), ref data);
            return ResponseData<List<CardData>>.Result(result, "获取卡信息失败！", data.ToModels());
        }

        /// <summary>
        /// 6、获取公司信息接口
        /// </summary>
        /// <param name="cardNo">卡号(sPA_Card 的 sCardNO 字段)，如为空则返回所有公司信息</param>
        /// <returns>返回的公司信息列表</returns>
        [HttpGet("getAllCompanys")]
        public ResponseData<List<CompanyData>> GetAllCompanys(string cardNo)
        {
            var results = _prowatchService.GetCompaniesByCardNo(cardNo);

            return ResponseData<List<CompanyData>>.Result(true, "获取公司信息失败！", results);
        }

        /// <summary>
        /// 7、获取公司使用的访问码接口
        /// </summary>
        /// <param name="companyId">公司 ID(sPA_ Company 的 ID 字段)，不能为空</param>
        /// <returns>返回的访问码信息列表</returns>
        [HttpGet("getCompanyACCodes")]
        public ResponseData<List<AccessCodeData>> GetCompanyACCodes(string companyId)
        {
            List<sPA_AccessCode> data = new List<sPA_AccessCode>();
            bool result = ApiHelper.PWApi.GetCompanyACCodes(companyId.IsNull(), ref data);
            return ResponseData<List<AccessCodeData>>.Result(result, "获取公司使用的访问码失败！", data.ToModels());
        }

        /// <summary>
        /// 8、获取访问码的访问有效期限信息接口
        /// </summary>
        /// <param name="acCodeId">访问码 ID(sPA_AccessCode 中的 sID 字段)，不能为空</param>
        /// <returns>返回的访问码信息列表</returns>
        [HttpGet("getACCodeAccessDuration")]
        public ResponseData<AccessDurationData> GetACCodeAccessDuration(string acCodeId)
        {
            sPA_Access_Duration data = new sPA_Access_Duration();
            bool result = ApiHelper.PWApi.GetACCodeAccessDuration(acCodeId.IsNull(), ref data);
            return ResponseData<AccessDurationData>.Result(result, "获取访问码的访问有效期限信失败！", data.ToModel());
        }

        /// <summary>
        /// 9、获取读卡器信息接口
        /// </summary>
        /// <param name="acCodeId">访问码 ID(sPA_AccessCode 中的 sID 字段)，如为空则返回所有读卡器</param>
        /// <returns>返回的读卡器信息列表</returns>
        [HttpGet("getAllReaders")]
        public ResponseData<List<ReaderData>> GetAllReaders(string acCodeId)
        {
            List<sPA_Reader> data = new List<sPA_Reader>();
            bool result = ApiHelper.PWApi.GetAllReaders(acCodeId.IsNull(), ref data);
            return ResponseData<List<ReaderData>>.Result(result, "获取读卡器信息失败！", data.ToModels());
        }

        /// <summary>
        /// 10、获取时区信息接口
        /// </summary>
        /// <param name="acCodeId">访问码 ID(sPA_AccessCode 中的 sID 字段)</param>
        /// <param name="readerId">读卡器 ID(sPA_Reader 中的 sID 字段)</param>
        /// <returns>返回的时区信息</returns>
        [HttpGet("getAllTimeZones")]
        public ResponseData<List<TimeZoneData>> GetAllTimeZones(String acCodeId, String readerId)
        {
            List<sPA_TimeZone> data = new List<sPA_TimeZone>();
            bool result = ApiHelper.PWApi.GetAllTimeZones(acCodeId.IsNull(), readerId.IsNull(), ref data);
            return ResponseData<List<TimeZoneData>>.Result(result, "获取时区信息失败！", data.ToModels());
        }

        /// <summary>
        /// 11、获取访问码信息
        /// 如personId和cardNo都为空，则获取所有的访问码
        /// 如personId不为空，cardNo为空，则获取该人员下所有卡的访问码
        /// 如personId为空，cardNo不为空，则获取该卡的所有访问码
        /// 如personId和cardNO都不为空，则获取该卡的所有访问码，并且人员和卡要匹配
        /// </summary>
        /// <param name="personId">人员id</param>
        /// <param name="cardNo">卡号</param>
        /// <returns>访问码信息列表</returns>
        [HttpGet("getAllACCodes")]
        public ResponseData<List<AccessCodeData>> GetAllACCodes(string personId, string cardNo)
        {
            List<sPA_AccessCode> data = new List<sPA_AccessCode>();
            bool result = ApiHelper.PWApi.GetAllACCodes(personId.IsNull(), cardNo.IsNull(), ref data);
            return ResponseData<List<AccessCodeData>>.Result(result, "获取访问码失败！", data.ToModels());
        }

    }
}
