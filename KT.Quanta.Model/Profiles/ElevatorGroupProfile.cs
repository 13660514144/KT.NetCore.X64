using AutoMapper;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace KT.Quanta.Model.Profiles
{
    public class ElevatorGroupProfile : Profile
    {
        public ElevatorGroupProfile()
        {
            CreateMap<ElevatorGroupEntity, ElevatorGroupModel>()
                   .ForMember(dest => dest.ElevatorGroupFloorIds, opt => opt.MapFrom(src => GetElevatorGroupFloorIds(src.ElevatorGroupFloors)))
                   .ForMember(dest => dest.ElevatorServerIds, opt => opt.MapFrom(src => GetElevatorServerIds(src.ElevatorServers)))
                   .ForMember(dest => dest.ElevatorInfoIds, opt => opt.MapFrom(src => GetElevatorInfoIds(src.ElevatorInfos)));
        }

        private List<string> GetElevatorGroupFloorIds(List<ElevatorGroupFloorEntity> elevatorGroupFloors)
        {
            if (elevatorGroupFloors?.FirstOrDefault() == null)
            {
                return new List<string>();
            }

            return elevatorGroupFloors.Select(x => x.Id).ToList();
        }

        private List<string> GetElevatorServerIds(List<ElevatorServerEntity> elevatorGroupFloors)
        {
            if (elevatorGroupFloors?.FirstOrDefault() == null)
            {
                return new List<string>();
            }

            return elevatorGroupFloors.Select(x => x.Id).ToList();
        }

        private List<string> GetElevatorInfoIds(List<ElevatorInfoEntity> elevatorGroupFloors)
        {
            if (elevatorGroupFloors?.FirstOrDefault() == null)
            {
                return new List<string>();
            }

            return elevatorGroupFloors.Select(x => x.Id).ToList();
        }

    }
}
