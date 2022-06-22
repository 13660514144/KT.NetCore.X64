using KT.Quanta.Common.Models;
using KT.Quanta.Unit.Model.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using KT.Common.Core.Utils;

namespace KT.Device.Quanta.Convertors
{
    public class PassDisplayConvertor
    {
        public static PassDisplayRequest ToRequest(PassDisplayModel model)
        {
            var request = new PassDisplayRequest();
            request.DisplayType = model.DisplayType;
            request.Time = model.Time;
            request.AccessType = model.AccessType;
            request.Sign = model.Sign;
            request.ImageUrl = model.ImageUrl;

            return request;
        }
    }
}
