using AutoMapper;
using KT.Quanta.Service.Devices.Kone.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Profiles
{
    public class KoneEliDopSpecificDefaultAccessMaskMessageCopyProfile : Profile
    {
        public KoneEliDopSpecificDefaultAccessMaskMessageCopyProfile()
        {
            CreateMap<KoneEliDopSpecificDefaultAccessMaskMessage, KoneEliDopSpecificDefaultAccessMaskMessage>();
        }
    }
}
