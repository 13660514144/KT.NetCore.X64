using AutoMapper;
using KT.Quanta.Model.Elevator;
using KT.Quanta.Service.Entities;

namespace KT.Quanta.Model.Profiles
{
    public class ElevatorGroupFloorProfile : Profile
    {
        public ElevatorGroupFloorProfile()
        {
            CreateMap<ElevatorGroupFloorEntity, ElevatorGroupFloorModel>();
        }
    }
}
