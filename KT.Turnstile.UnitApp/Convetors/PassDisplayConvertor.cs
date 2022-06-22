using KT.Common.Core.Utils;
using KT.Quanta.Common.Models;
using KT.Quanta.Unit.Model.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Turnstile.Unit.ClientApp.Convetors
{
    public class PassDisplayConvertor
    {
        public static PassDisplayModel ToModel(PassDisplayRequest request)
        {
            var model = new PassDisplayModel();
            model.DisplayType = request.DisplayType;
            model.Time = request.Time;
            model.AccessType = request.AccessType;
            model.Sign = request.Sign;
            model.ImageUrl = request.ImageUrl;

            return model;
        }
    }
}
