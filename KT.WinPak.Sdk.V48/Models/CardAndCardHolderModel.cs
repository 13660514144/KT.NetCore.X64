using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Models
{
    public class CardAndCardHolderModel
    {
        /// <summary>
        /// 卡
        /// </summary>
        public CardModel Card { get; set; }

        /// <summary>
        /// 持卡人
        /// </summary>
        public CardHolderModel CardHolder { get; set; }

        public static CardAndCardHolderModel Create()
        {
            var model = new CardAndCardHolderModel();
            model.Card = new CardModel();
            model.Card.AccessLevels = new List<string>();
            model.CardHolder = new CardHolderModel();
            return model;
        }
    }
}
