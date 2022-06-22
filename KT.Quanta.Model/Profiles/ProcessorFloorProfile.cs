using AutoMapper;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Models;
using System.Collections.Generic;

namespace KT.Quanta.Model.Profiles
{
    public class ProcessorFloorProfile : Profile
    {
        public ProcessorFloorProfile()
        {
            CreateMap<ProcessorFloorEntity, ProcessorFloorModel>();
        }
    }
}
