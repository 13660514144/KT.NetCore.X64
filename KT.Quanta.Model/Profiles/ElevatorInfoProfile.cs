using AutoMapper;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Models;

namespace KT.Quanta.Model.Profiles
{
    public class ElevatorInfoProfile : Profile
    {
        public ElevatorInfoProfile()
        {
            CreateMap<ElevatorInfoEntity, ElevatorInfoModel>();
        }
    }
}
