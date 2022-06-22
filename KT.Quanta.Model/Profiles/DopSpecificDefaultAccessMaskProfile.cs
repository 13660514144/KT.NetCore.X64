using AutoMapper;
using KT.Quanta.Entity.Kone;
using KT.Quanta.Model.Kone;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Model.Profiles
{
    public class DopSpecificDefaultAccessMaskProfile : Profile
    {
        public DopSpecificDefaultAccessMaskProfile()
        {
            CreateMap<DopSpecificDefaultAccessMaskEntity, DopSpecificDefaultAccessMaskModel>();
        }
    }
}
