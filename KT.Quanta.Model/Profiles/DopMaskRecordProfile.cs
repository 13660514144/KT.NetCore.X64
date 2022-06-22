using AutoMapper;
using KT.Quanta.Entity.Kone;
using KT.Quanta.Model.Kone;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Models;

namespace KT.Quanta.Model.Profiles
{
    public class DopMaskRecordProfile : Profile
    {
        public DopMaskRecordProfile()
        {
            CreateMap<DopMaskRecordEntity, DopMaskRecordModel>();
        }
    }
}
