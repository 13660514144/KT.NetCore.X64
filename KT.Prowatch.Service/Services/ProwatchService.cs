using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Prowatch.Service.DllModels;
using KT.Prowatch.Service.Enums;
using KT.Prowatch.Service.Helpers;
using KT.Prowatch.Service.IDaos;
using KT.Prowatch.Service.IServices;
using KT.Prowatch.Service.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProwatchAPICS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace KT.Prowatch.Service.Services
{
    public class ProwatchService : IProwatchService
    {
        private ILogger<ProwatchService> _logger;
        private IProwatchDao _prowatchDao;
        private DownloadCardStateQueue _downloadCardStateQueue;
        private AppSettings _appSettings;

        public ProwatchService(ILogger<ProwatchService> logger,
            IProwatchDao prowatchDao,
            DownloadCardStateQueue downloadCardStateQueue,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _prowatchDao = prowatchDao;
            _downloadCardStateQueue = downloadCardStateQueue;
            _appSettings = appSettings.Value;
        }

        public void DownloadCardStateExecQueueCardRetryDownload(string cardNo)
        {
            _prowatchDao.DownloadCardStateExecQueueCardRetryDownload(cardNo);
        }

        /// <summary>
        /// 重复下载卡，卡状态改变不会再重复下载
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="cardNo"></param>
        /// <param name="cardState">要修改的卡状态</param>
        public async Task<bool> DownloadCardStateAsync(string personId, string cardNo, string stateCode)
        {
            var oldModel = CardDetail(cardNo);
            if (oldModel == null)
            {
                _logger.LogWarning($"重新更新卡不存在：cardNo:{cardNo} data:{JsonConvert.SerializeObject(oldModel, JsonUtil.JsonSettings)} ");
                return false;
            }
            if (oldModel.StatCode != stateCode)
            {
                _logger.LogWarning($"卡状态改变，不能重复操作卡：" +
                    $"cardNo:{cardNo} " +
                    $"dbStateCode:{oldModel.StatCode} " +
                    $"updateStateCode:{stateCode} " +
                    $"data:{JsonConvert.SerializeObject(oldModel, JsonUtil.JsonSettings)} ");
                return false;
            }
            if (string.IsNullOrEmpty(oldModel.PersonId))
            {
                oldModel.PersonId = personId;
            }
            var companies = GetCompaniesByCardNo(oldModel.CardNo);
            var company = companies?.FirstOrDefault();
            if (company == null)
            {
                _logger.LogWarning($"重新更新卡公司不存在：cardNo:{cardNo} data:{JsonConvert.SerializeObject(oldModel, JsonUtil.JsonSettings)} ");
                return false;
            }
            oldModel.CompanyId = company.Id;

            //启用或禁用卡
            await EnableOrDisableCardAsync(oldModel);

            return true;
            ////启用或禁用卡
            //if (stateCode == CardStateEnum.ENABLE.Value)
            //{
            //    await EnableOrDisableCardAsync(oldModel);
            //}
            //else
            //{
            //    await DisableCardAsync(oldModel);
            //}
        }

        /// <summary>
        /// 数据库激活
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="cardNo"></param>
        private async Task DownloadCardStateFromDbAsync(string personId, string cardNo)
        {
            //数据库激活
            if (_appSettings.RedownloadCardStateByDbCommandTimes > 0)
            {
                for (int i = _appSettings.RedownloadCardStateByDbCommandTimes; i >= 0; i--)
                {
                    _prowatchDao.DownloadCardStateDeleteHiQueue(personId);
                }
                for (int i = _appSettings.RedownloadCardStateByDbCommandTimes; i >= 0; i--)
                {
                    _prowatchDao.DownloadCardStateExecHiQueueCommand(personId, cardNo);
                }
                for (int i = _appSettings.RedownloadCardStateByDbCommandTimes; i >= 0; i--)
                {
                    _prowatchDao.DownloadCardStateExecQueueCardRetryDownload(cardNo);
                }

                await Task.Delay((int)(_appSettings.RedownloadCardStateByDbCommandSecondTime * 1000));
                _prowatchDao.DownloadCardStateDeleteHiQueue(personId);
            }
        }

        /// <summary>
        /// 启用卡
        /// </summary>
        /// <param name="oldModel"></param>
        private async Task EnableOrDisableCardAsync(CardData oldModel)
        {
            oldModel.IssueDate = DateTime.Now.ToDayString();
            //oldModel.StatCode = CardStateEnum.ENABLE.Value;
            try
            {
                ApiHelper.PWApi.UpdateCard(oldModel.ToSPA(), oldModel.CompanyId);

                //存储过程激活
                await DownloadCardStateFromDbAsync(oldModel.PersonId, oldModel.CardNo);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"重新更新卡失败：cardData:{JsonConvert.SerializeObject(oldModel, JsonUtil.JsonSettings)} ex:{ex} ");
            }
        }

        ///// <summary>
        ///// 禁用卡
        ///// </summary>
        ///// <param name="oldModel"></param>
        //private async Task DisableCardAsync(CardData oldModel)
        //{
        //    oldModel.IssueDate = DateTime.Now.ToDayString();
        //    oldModel.StatCode = CardStateEnum.DISABLE.Value;
        //    try
        //    {
        //        ApiHelper.PWApi.UpdateCard(oldModel.ToSPA(), oldModel.CompanyId);

        //        //存储过程激活
        //        await DownloadCardStateFromDbAsync(oldModel.PersonId, oldModel.CardNo);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogInformation($"重新启用卡失败：cardData:{JsonConvert.SerializeObject(oldModel, JsonUtil.JsonSettings)} ex:{ex} ");
        //    }
        //}

        public PersonCardModel AddPersonCard(PersonCardModel personCard)
        {
            _logger.LogInformation("开始添加人员");
            string personId = string.Empty;
            //增加人员
            bool result = ApiHelper.PWApi.AddPerson(personCard.Person.ToSPA(), personCard.Person?.BadgeTypeId.IsNull(), ref personId);
            if (!result || string.IsNullOrEmpty(personId))
            {
                personCard.Person.OperationCode = 500;
                personCard.Person.OperationMessage = "增加人员失败！";
                return personCard;
            }
            personCard.Person.OperationCode = 200;
            //增加卡 
            _logger.LogInformation("开始添加卡");
            personCard.Card.PersonId = personId;
            bool result2 = ApiHelper.PWApi.AddCard(personCard.Card.ToSPA(), personCard.Card?.CompanyId.IsNull());
            if (!result2)
            {
                personCard.Card.OperationCode = 500;
                personCard.Card.OperationMessage = "增加卡失败！";
                return personCard;
            }
            personCard.Card.OperationCode = 200;
            _logger.LogInformation("结束添加人员与卡");
            return personCard;
        }

        public string AddPersonCardWithACCode(PersonCardACCodeModel personCardACCode)
        {
            string personId = string.Empty;
            //增加人员
            bool result = ApiHelper.PWApi.AddPerson(personCardACCode.Person.ToSPA(), personCardACCode.Person?.BadgeTypeId.IsNull(), ref personId);
            if (!result || string.IsNullOrEmpty(personId))
            {
                throw CustomException.Run("增加人员失败！");
            }
            //增加卡
            personCardACCode.Card.PersonId = personId;
            bool result2 = ApiHelper.PWApi.AddCard(personCardACCode.Card.ToSPA(), personCardACCode.Card?.CompanyId.IsNull());
            if (!result2)
            {
                throw CustomException.Run("增加卡失败！");
            }
            //向卡中增加访问码
            bool result3 = ApiHelper.PWApi.AddACCodeToCard(personCardACCode.ACCodeId, personCardACCode.Card?.CardNo.IsNull());
            if (!result3)
            {

                throw CustomException.Run("向卡中增加访问码失败！");
            };

            return personId;
        }

        public List<PersonCardModel> AddPersonCards(List<PersonCardModel> personCards)
        {
            List<PersonCardModel> results = new List<PersonCardModel>();
            foreach (var item in personCards)
            {
                results.Add(AddPersonCard(item));
            }

            return results;
        }

        public string AddPersonCardWithACCodeReader(PersonCardACCodeReaderModel personCardACCodeReader)
        {
            string personId = string.Empty;
            //增加人员
            bool result = ApiHelper.PWApi.AddPerson(personCardACCodeReader.Person.ToSPA(), personCardACCodeReader.Person?.BadgeTypeId.IsNull(), ref personId);
            if (!result || string.IsNullOrEmpty(personId))
            {
                throw CustomException.Run("增加人员失败！");
            }
            //增加卡
            personCardACCodeReader.Card.PersonId = personId;
            bool result2 = ApiHelper.PWApi.AddCard(personCardACCodeReader.Card.ToSPA(), personCardACCodeReader.Card?.CompanyId.IsNull());
            if (!result2)
            {
                throw CustomException.Run("增加卡失败！");
            }
            //向卡中增加访问码
            bool result3 = ApiHelper.PWApi.AddACCodeToCard(personCardACCodeReader.ACCodeId.IsNull(), personCardACCodeReader.Card?.CardNo.IsNull());
            if (!result3)
            {
                throw CustomException.Run("向卡中增加访问码失败！");
            }
            //将读卡器写入卡
            var sPAReaderCard = personCardACCodeReader.ReaderCard;
            bool result4 = ApiHelper.PWApi.AddReaderToCard(sPAReaderCard.CardNo, sPAReaderCard.ReaderId, sPAReaderCard.TimeZoneId, sPAReaderCard.TempAccess, sPAReaderCard.TempAccessStartTime, sPAReaderCard.TempAccessEndTime);
            if (!result4)
            {
                throw CustomException.Run("向卡中增加读卡器失败！");
            }

            return personId;
        }

        public PersonCardModel RemovePersonCardNoBreak(RemovePersonCardModel removePersonCard)
        {
            removePersonCard.PersonId = removePersonCard.PersonId.ToLower();

            PersonCardModel personCard = new PersonCardModel();
            personCard.Person.Id = removePersonCard.PersonId;

            string personId = string.Empty;
            //增加人员
            bool result = ApiHelper.PWApi.RemovePerson(removePersonCard?.PersonId.IsNull());
            if (!result || string.IsNullOrEmpty(personId))
            {
                personCard.Person.OperationCode = 500;
                personCard.Person.OperationMessage = "删除人员失败！";
            }
            else
            {
                personCard.Person.OperationCode = 200;
            }

            //增加卡
            personCard.Card.PersonId = removePersonCard.PersonId;
            personCard.Card.CardNo = removePersonCard.CardNo;
            bool result2 = ApiHelper.PWApi.RemoveCard(removePersonCard?.CardNo.IsNull());
            if (!result2)
            {
                personCard.Card.OperationCode = 500;
                personCard.Card.OperationMessage = "删除卡失败！";
            }
            else
            {
                personCard.Card.OperationCode = 200;
            }

            return personCard;
        }

        public void RemovePersonCard(RemovePersonCardModel removePersonCard)
        {
            var personId = removePersonCard?.PersonId;
            if (string.IsNullOrEmpty(personId))
            {
                var spDatas = new List<sPA_Card>();
                bool result = ApiHelper.PWApi.QueryCards("CARDNO", "=", removePersonCard?.CardNo.IsNull(), ref spDatas);
                List<CardData> data = spDatas.ToModels();
                personId = data?.FirstOrDefault()?.PersonId;
                if (!result || string.IsNullOrEmpty(personId))
                {
                    throw CustomException.Run("查找人员信息失败失败！");
                }
            }

            bool result3 = ApiHelper.PWApi.RemoveCard(removePersonCard?.CardNo.IsNull());
            if (!result3)
            {
                throw CustomException.Run("删除卡失败！");
            }

            bool result2 = ApiHelper.PWApi.RemovePerson(personId);
            if (!result2)
            {
                CustomException.Run("删除人员失败！");
            }
        }

        public List<PersonCardModel> RemovePersonCards(List<RemovePersonCardModel> personCards)
        {
            List<PersonCardModel> results = new List<PersonCardModel>();
            foreach (var item in personCards)
            {
                results.Add(RemovePersonCardNoBreak(item));
            }
            return results;
        }

        public CardData CardDetail(string cardNo)
        {
            var spCards = new List<sPA_Card>();
            bool result = ApiHelper.PWApi.QueryCards("CARDNO", "=", cardNo.IsNull(), ref spCards);
            if (!result || spCards == null || spCards.FirstOrDefault() == null)
            {
                return null;
            }

            List<CardData> datas = spCards.ToModels();
            var card = datas.FirstOrDefault();
            return card;
        }

        public bool IsExistAcCodeByCardNo(string cardNo, string acCodeId)
        {
            List<sPA_AccessCode> data = new List<sPA_AccessCode>();
            bool result = ApiHelper.PWApi.GetAllACCodes(string.Empty, cardNo.IsNull(), ref data);
            if (data != null && data.FirstOrDefault(x => x.sID == acCodeId) != null)
            {
                return true;
            }
            return false;
        }

        public List<CompanyData> GetCompaniesByCardNo(string cardNo)
        {
            List<sPA_Company> data = new List<sPA_Company>();
            bool result = ApiHelper.PWApi.GetAllCompanys(cardNo.IsNull(), ref data);
            var results = data.ToModels();
            return results;
        }

        public async Task<bool> UpdateCardAsync(CardData card)
        {
            //要小写，否则返回正确但数据没有更改
            card.PersonId = card.PersonId.IsNull().ToLower();
            card.CompanyId = card.CompanyId.IsNull().ToLower();

            //更新卡
            bool result = ApiHelper.PWApi.UpdateCard(card.ToSPA(), card.CompanyId);

            //重复更新卡
            //if (card.StatCode == CardStateEnum.ENABLE.Value)
            //{
            await DownloadCardStateFromDbAsync(card.PersonId, card.CardNo);
            var stateModel = new DownloadCardStateModel(card.PersonId, card.CardNo, card.StatCode);
            _downloadCardStateQueue.Enqueue(stateModel);
            //}
            return result;
        }
    }
}
