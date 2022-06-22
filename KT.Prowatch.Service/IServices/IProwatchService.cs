using KT.Prowatch.Service.DllModels;
using KT.Prowatch.Service.Models;
using KT.Prowatch.WebApi.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Prowatch.Service.IServices
{
    public interface IProwatchService
    {
        /// <summary>
        /// 添加人员与卡
        /// </summary>
        PersonCardModel AddPersonCard(PersonCardModel personCard);

        /// <summary>
        /// 添加人员与卡
        /// </summary>
        /// <param name="personCards">人员、卡 组合</param>
        /// <returns>人员Id</returns>
        List<PersonCardModel> AddPersonCards(List<PersonCardModel> personCards);

        /// <summary>
        /// 删除人员与卡
        /// </summary>
        /// <param name="personCards">人员、卡 组合</param>
        /// <returns>人员Id</returns>
        PersonCardModel RemovePersonCardNoBreak(RemovePersonCardModel item);

        /// <summary>
        /// 查询卡明细
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        CardData CardDetail(string cardNo);

        /// <summary>
        /// 删除人员与卡
        /// </summary>
        /// <param name="personCards">人员、卡 组合</param>
        /// <returns>人员Id</returns>
        List<PersonCardModel> RemovePersonCards(List<RemovePersonCardModel> personCards);

        /// <summary>
        /// 删除作人员与卡
        /// </summary> 
        /// <param name="removePersonCard">人员Id与卡Id</param>
        /// <returns>无</returns>
        void RemovePersonCard(RemovePersonCardModel removePersonCard);

        /// <summary>
        /// 添加人员与卡,并将访问码写入卡
        /// </summary>
        /// <param name="personCardACCode">人员、卡、访问码 组合</param>
        /// <returns></returns>
        string AddPersonCardWithACCode(PersonCardACCodeModel personCardACCode);

        /// <summary>
        /// 添加人员与卡,并将访问码、读卡器写入卡
        /// </summary>
        /// <param name="personCardACCodeReader">人员、卡、访问码、读卡器 组合</param>
        /// <returns></returns>
        string AddPersonCardWithACCodeReader(PersonCardACCodeReaderModel personCardACCodeReader);

        /// <summary>
        /// 访问码是否存在卡中
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="acCodeId"></param>
        /// <returns></returns>
        bool IsExistAcCodeByCardNo(string cardNo, string acCodeId);

        /// <summary>
        /// 下载卡状态
        /// </summary>
        /// <param name="cardNo"></param>
        Task<bool> DownloadCardStateAsync(string personId, string cardNo, string stateCode);
        List<CompanyData> GetCompaniesByCardNo(string cardNo);
        Task<bool> UpdateCardAsync(CardData card);
        void DownloadCardStateExecQueueCardRetryDownload(string cardNo);
    }
}
