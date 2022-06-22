using KT.Common.Core.Utils;
using KT.Prowatch.Service.DllModels;
using KT.WinPak.SDK.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KT.TestTool.TestApp.Common.JsonData
{
    public class JsonDataHelper
    {
        public static List<CardModel> GetWinPakCards()
        {
            var uri = AppContext.BaseDirectory + "WinPakCardData.Json";
            var json = File.ReadAllText(uri);
            return JsonConvert.DeserializeObject<List<CardModel>>(json, JsonUtil.JsonSettings);
        }


        public static List<CardData> GetProwatchCards()
        {
            var uri = AppContext.BaseDirectory + "ProwatchCardData.Json";
            var json = File.ReadAllText(uri);
            return JsonConvert.DeserializeObject<List<CardData>>(json, JsonUtil.JsonSettings);
        }

    }
}
