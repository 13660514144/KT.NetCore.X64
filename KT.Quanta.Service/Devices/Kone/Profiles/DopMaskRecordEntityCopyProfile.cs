using AutoMapper;
using KT.Quanta.Entity.Kone;
using KT.Quanta.Service.Devices.Kone.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Profiles
{
    public class DopMaskRecordEntityCopyProfile : Profile
    {
        public DopMaskRecordEntityCopyProfile()
        {
            CreateMap<DopMaskRecordEntity, DopMaskRecordEntity>();
        }
    }
}
