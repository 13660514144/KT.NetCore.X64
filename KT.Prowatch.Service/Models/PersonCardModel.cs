using KT.Prowatch.Service.DllModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KT.Prowatch.Service.Models
{
    /// <summary>
    /// 人员卡组合类
    /// </summary>
    public class PersonCardModel
    {
        /// <summary>
        /// 人员
        /// </summary>
        //[JsonProperty("person")]
        public PersonData Person { get; set; }

        /// <summary>
        /// 卡
        /// </summary>
        //[JsonProperty("card")]
        public CardData Card { get; set; }


        public PersonCardModel()
        {
            Person = new PersonData();
            Card = new CardData();
        }

        public string GetMessage()
        {
            if (Person?.OperationCode == 200)
            {
                return Card?.OperationMessage;
            }
            else
            {
                return Person?.OperationMessage;
            }
        }

        public bool IsSuccess()
        {
            return Person?.OperationCode == 200 && Card?.OperationCode == 200;
        }

        public static PersonCardModel Create()
        {
            var model = new PersonCardModel();
            model.Card = new CardData();
            model.Person = new PersonData();
            return model;
        }
    }
}

