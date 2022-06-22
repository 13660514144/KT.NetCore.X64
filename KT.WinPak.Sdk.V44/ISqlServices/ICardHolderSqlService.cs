using KT.WinPak.SDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.IServices
{
    /// <summary>
    /// 持卡人
    /// </summary>
    public interface ICardHolderSqlService
    {
        /// <summary>
        /// 获取所有持卡人
        /// </summary>
        /// <returns></returns>
        List<CardHolderModel> GetAll();

        /// <summary>
        /// 新增持卡人
        /// </summary>
        /// <param name="model">The new card holder details. </param>
        /// <returns></returns>
        CardHolderModel Add(CardHolderModel model);

        /// <summary>
        /// 修改持卡人
        /// </summary>
        /// <param name="lCardholderID">The card holder ID for which the details must be edited.  </param>
        /// <param name="model">The card holder details. </param>
        /// <returns></returns>
        bool Edit(int lCardholderID, CardHolderModel model);

        /// <summary>
        /// 删除持卡人
        /// </summary>
        /// <param name="lCHId">The ID of the card holder for which the details must be deleted.  </param>
        /// <param name="bDelCard">The option value of the card associated with the card holder. 
        /// The option value of the card associated with the card holder are as follows: 
        /// 0 – Detach the cards associated with the specified card holder.
        /// 1 – Deletes the cards associated with specified card holder.
        /// </param>
        /// <param name="bDelImage">The option value of the photo associated with the card holder.
        /// The option value of the photo associated with the card holder are as follows: 
        /// 0 – Detach the photos and signatures attached to the specified card holder.
        /// 1 – Deletes the photos and signatures attached with the specified card holder.
        /// </param>
        /// <returns></returns>
        bool Delete(int lCHId, int bDelCard, int bDelImage);
    }
}
