using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Models
{
    /// <summary>
    /// 时区
    /// </summary>
    public class TimeZoneModel
    {
        public int TimeZoneID { get; set; }
        public string AccountName { get; set; }
        public string TimeZoneName { get; set; }
        public string TimeZoneDesc { get; set; }
        public int PanelTZSlotIndex { get; set; }

        public static TimeZoneModel FromEntity(ITimeZone entity)
        {
            if (entity == null)
            {
                return null;
            }
            TimeZoneModel model = new TimeZoneModel();

            model.TimeZoneID = entity.TimeZoneID;
            model.AccountName = entity.AccountName;
            model.TimeZoneName = entity.TimeZoneName;
            model.TimeZoneDesc = entity.TimeZoneDesc;
            model.PanelTZSlotIndex = entity.PanelTZSlotIndex;

            return model;
        }


        internal static List<TimeZoneModel> FromSqlEntities(List<Entities.TimeZone> entities)
        {
            var models = new List<TimeZoneModel>();
            if (entities == null)
            {
                return models;
            }
            foreach (var item in entities)
            {
                var model = FromSqlEntity(item);
                models.Add(model);
            }
            return models;
        }

        private static TimeZoneModel FromSqlEntity(Entities.TimeZone entity)
        {
            var model = new TimeZoneModel();
            model.TimeZoneID = entity.RecordId;
            model.TimeZoneName = entity.Name;
            model.TimeZoneDesc = entity.Description;
            //model.PanelTZSlotIndex = entity.PanelTZSlotIndex;
            model.AccountName = "";

            return model;
        }
    }
}
