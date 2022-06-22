using AutoMapper;
using KT.Quanta.Service.Devices.Kone.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Profiles
{
    public class KoneEliDopGlobalDefaultAccessMaskMessageCopyProfile : Profile
    {
        public KoneEliDopGlobalDefaultAccessMaskMessageCopyProfile()
        {
            CreateMap<KoneEliDopGlobalDefaultAccessMaskMessage, KoneEliDopGlobalDefaultAccessMaskMessage>();
        }
    }
}
