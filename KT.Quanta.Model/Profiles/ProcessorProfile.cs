using AutoMapper;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace KT.Quanta.Model.Profiles
{
    public class ProcessorProfile : Profile
    {
        public ProcessorProfile()
        {
            CreateMap<ProcessorEntity, ProcessorModel>()
                   .ForMember(dest => dest.CardDeviceIds, opt => opt.MapFrom(src => GetCardDeviceIds(src.CardDevices)))
                   .ForMember(dest => dest.ProcessorFloorIds, opt => opt.MapFrom(src => GetProcessorFloorIds(src.ProcessorFloors)));
        }

        private List<string> GetCardDeviceIds(List<CardDeviceEntity> cardDevices)
        {
            if (cardDevices?.FirstOrDefault() == null)
            {
                return new List<string>();
            }

            return cardDevices.Select(x => x.Id).ToList();
        }

        private List<string> GetProcessorFloorIds(List<ProcessorFloorEntity> elevatorGroupFloors)
        {
            if (elevatorGroupFloors?.FirstOrDefault() == null)
            {
                return new List<string>();
            }

            return elevatorGroupFloors.Select(x => x.Id).ToList();
        }
    }
}
