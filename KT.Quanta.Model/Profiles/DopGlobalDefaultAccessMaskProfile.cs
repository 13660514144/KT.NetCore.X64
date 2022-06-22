using AutoMapper;
using KT.Quanta.Entity.Kone;
using KT.Quanta.Model.Kone;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Model.Profiles
{
    public class DopGlobalDefaultAccessMaskProfile : Profile
    {
        public DopGlobalDefaultAccessMaskProfile()
        {
            CreateMap<DopGlobalDefaultAccessMaskEntity, DopGlobalDefaultAccessMaskModel>();
        }
    }
}
