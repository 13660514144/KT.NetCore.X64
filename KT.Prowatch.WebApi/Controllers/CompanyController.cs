using KT.Prowatch.Service.DllModels;
using KT.Prowatch.Service.Helpers;
using KT.Prowatch.Service.Models;
using KT.Prowatch.WebApi.Common;
using KT.Prowatch.WebApi.Common.Filters;
using Microsoft.AspNetCore.Mvc;

namespace KT.Prowatch.WebApi.Controllers
{
    /// <summary>
    /// 人员与卡
    /// </summary>
    [ApiController]
    [Route("")]
    [TypeFilter(typeof(LoginUserAttribute))]
    public class CompanyController :  ControllerBase
    {
        /// <summary>
        /// 1. 添加公司
        /// </summary>
        /// <param name="company">卡信息，sCardNO 为必需字段;公司类型ID，必需参数</param>
        /// <returns>返回公司的Id</returns>
        [HttpPost("addCompany")]
        public ResponseData<string> AddCompany([FromBody] CompanyData company)
        {
            string companyId = string.Empty;
            bool result = ApiHelper.PWApi.AddCompany(company.ToSPA(), ref companyId);
            return ResponseData<string>.Result(result, "添加卡失败！", companyId);
        }
    }
}