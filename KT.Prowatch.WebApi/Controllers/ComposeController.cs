using KT.Common.Core.Utils;
using KT.Prowatch.Service.DllModels;
using KT.Prowatch.Service.Helpers;
using KT.Prowatch.Service.IServices;
using KT.Prowatch.Service.Models;
using KT.Prowatch.WebApi.Common;
using KT.Prowatch.WebApi.Common.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProwatchAPICS;
using System.Collections.Generic;
using System.Linq;

namespace KT.Prowatch.WebApi.Controllers
{
    /// <summary>
    /// 组合功能
    /// </summary>
    [ApiController]
    [Route("")]
    [TypeFilter(typeof(LoginUserAttribute))]
    public class ComposeController : ControllerBase
    {
        private ILogger<ComposeController> _logger;
        private IProwatchService _prowatchService;

        public ComposeController(ILogger<ComposeController> logger, IProwatchService prowatchService)
        {
            _logger = logger;
            _prowatchService = prowatchService;
        }

        /// <summary>
        /// 添加人员与卡
        /// </summary>
        /// <param name="personCard">人员、卡 组合</param>
        /// <returns>人员Id</returns>
        [HttpPost("addPersonCard")]
        public ResponseData<string> AddPersonCard([FromBody] PersonCardModel personCard)
        {
            //增加人员与卡
            personCard = _prowatchService.AddPersonCard(personCard);

            //返回操作结果
            if (personCard.IsSuccess())
            {
                return ResponseData<string>.Ok(personCard?.Card?.PersonId);
            }
            return ResponseData<string>.Error(personCard.GetMessage());
        }

        /// <summary>
        /// 添加人员与卡
        /// </summary>
        /// <param name="personCards">人员、卡 组合</param>
        /// <returns>人员Id</returns>
        [HttpPost("addPersonCards")]
        public ResponseData<List<PersonCardModel>> AddPersonCards([FromBody] List<PersonCardModel> personCards)
        {
            List<PersonCardModel> results = _prowatchService.AddPersonCards(personCards);
            return ResponseData<List<PersonCardModel>>.Ok(results);
        }

        /// <summary>
        /// 删除人员与卡
        /// </summary>
        /// <param name="personCards">人员、卡 组合</param>
        /// <returns>人员Id</returns>
        [HttpPost("removePersonCards")]
        public ResponseData<List<PersonCardModel>> RemovePersonCards([FromBody] List<RemovePersonCardModel> personCards)
        {
            var results = _prowatchService.RemovePersonCards(personCards);
            return ResponseData<List<PersonCardModel>>.Ok(results);
        }

        /// <summary>
        /// 删除作人员与卡
        /// </summary> 
        /// <param name="removePersonCard">人员Id与卡Id</param>
        /// <returns>无</returns>
        [HttpPost("removePersonCard")]
        public ResponseData<string> removePersonCard([FromBody] RemovePersonCardModel removePersonCard)
        {
            _prowatchService.RemovePersonCard(removePersonCard);

            return ResponseData<string>.Ok("删除人员与卡成功！");
        }

        /// <summary>
        /// 添加人员与卡,并将访问码写入卡
        /// </summary>
        /// <param name="personCardACCode">人员、卡、访问码 组合</param>
        /// <returns></returns>
        [HttpPost("addPersonCardWithACCode")]
        public ResponseData<string> addPersonCardWithACCode([FromBody] PersonCardACCodeModel personCardACCode)
        {
            string personId = _prowatchService.AddPersonCardWithACCode(personCardACCode);
            return ResponseData<string>.Ok(personId);
        }

        /// <summary>
        /// 添加人员与卡,并将访问码、读卡器写入卡
        /// </summary>
        /// <param name="personCardACCodeReader">人员、卡、访问码、读卡器 组合</param>
        /// <returns></returns>
        [HttpPost("addPersonCardWithACCodeReader")]
        public ResponseData<string> AddPersonCardWithACCodeReader([FromBody] PersonCardACCodeReaderModel personCardACCodeReader)
        {
            string personId = _prowatchService.AddPersonCardWithACCodeReader(personCardACCodeReader);

            return ResponseData<string>.Ok(personId);
        }
    }
}