﻿using KT.WinPak.SDK.V48.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.IServices
{
    public interface ICardSdkService
    {
        /// <summary>
        /// 获取所有卡
        /// </summary>
        /// <returns></returns>
        List<CardModel> GetAll();

        /// <summary>
        /// 新增卡
        /// </summary>
        /// <returns></returns>
        CardModel Add(CardModel pcard);

        /// <summary>
        /// 修改卡
        /// </summary>
        /// <param name="bstrCardno">The card number for which the details must be edited.  </param>
        /// <param name="bstrAcctName">The account name of the card specified in bstrCardno. </param>
        /// <param name="pcard">The card details. </param>
        /// <returns></returns>
        bool Edit(string bstrCardno, string bstrAcctName, string bstrSubAcctName, CardModel model);

        /// <summary>
        /// 删除卡
        /// </summary>
        /// <param name="bstrCardno">The card number to be deleted. </param>
        /// <param name="bstrAcctName">The account name of the card specified in bstrCardno. </param>
        /// <returns></returns>
        bool Delete(string bstrCardno, string bstrAcctName, string bstrSubAcctName);

        /// <summary>
        /// 新增持卡人与卡
        /// </summary>
        /// <param name="model"></param>
        CardAndCardHolderModel AddWithCardHolder(CardAndCardHolderModel model);

        /// <summary>
        /// 新增持卡人与卡
        /// </summary>
        /// <param name="model"></param>
        CardAndCardHolderModel EditWithCardHolder(CardAndCardHolderModel model);

        /// <summary>
        /// 删除持卡人与卡
        /// </summary>
        /// <param name="model"></param>
        CardAndCardHolderModel DeleteWithCardHolder(CardAndCardHolderModel model);

        /// <summary>
        /// 批量新增持卡人与卡
        /// </summary>
        /// <param name="model"></param>
        List<CardAndCardHolderModel> AddWithCardHolders(string accountName, string subAccountName, List<CardAndCardHolderModel> models);

        /// <summary>
        /// 批量删除持卡人与卡
        /// </summary>
        /// <param name="model"></param>
        List<CardAndCardHolderModel> DeleteWithCardHolders(string accountName, string subAccountName, List<CardAndCardHolderModel> models);

        /// <summary>
        /// 根据卡号查询卡信息
        /// </summary>
        /// <param name="cardNumber">卡号</param>
        /// <param name="accountName">账号</param>
        /// <returns>卡信息</returns>
        CardModel GetCardByCardNumber(string cardNumber, string accountName, string subAccountName);
    }
}
