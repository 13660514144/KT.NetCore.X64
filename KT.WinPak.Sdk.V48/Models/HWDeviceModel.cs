using KT.Common.Core.Utils;
using KT.WinPak.SDK.V48.Entities;
using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Models
{
    /// <summary>
    /// 设备
    /// </summary>
    public class HWDeviceModel
    {
        public string DeviceName { get; set; }
        public string DeviceDesc { get; set; }
        public int DeviceID { get; set; }
        public int DeviceType { get; set; }
        public int HWDeviceID { get; set; }
        public int AccountID { get; set; }

        public static HWDeviceClass ToEntity(HWDeviceClass entity, HWDeviceModel model)
        {
            entity.DeviceName = model.DeviceName;
            entity.DeviceDesc = model.DeviceDesc;
            entity.DeviceID = model.DeviceID;
            entity.DeviceType = model.DeviceType;
            entity.HWDeviceID = model.HWDeviceID;
            entity.AccountID = model.AccountID;

            return entity;
        }

        public static HWDeviceModel FromEntity(IHWDevice entity)
        {
            if (entity == null)
            {
                return null;
            }
            HWDeviceModel model = new HWDeviceModel();

            model.DeviceName = entity.DeviceName;
            model.DeviceDesc = entity.DeviceDesc;
            model.DeviceID = entity.DeviceID;
            model.DeviceType = entity.DeviceType;
            model.HWDeviceID = entity.HWDeviceID;
            model.AccountID = entity.AccountID;

            return model;
        }

        internal static List<HWDeviceModel> FromSqlEntities(List<Entities.HwindependentDevice> entities)
        {
            var models = new List<HWDeviceModel>();
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

        private static HWDeviceModel FromSqlEntity(Entities.HwindependentDevice entity)
        {
            var model = new HWDeviceModel();
            model.HWDeviceID = entity.HwdeviceId == null ? 0 : entity.HwdeviceId.Value;
            model.DeviceName = entity.Name;
            model.DeviceDesc = entity.Description;
            model.DeviceID = entity.DeviceId == null ? 0 : entity.DeviceId.Value;
            model.DeviceType = ConvertUtil.ToInt32(entity.DeviceType, 0);
            model.AccountID = ConvertUtil.ToInt32(entity.AccountId, 0);
            //model.AccountName = "";

            return model;
        }

    }
}
