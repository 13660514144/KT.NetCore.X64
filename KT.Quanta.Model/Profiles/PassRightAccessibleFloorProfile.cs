using AutoMapper;
using KT.Quanta.Entity.Entities;
using KT.Quanta.Model.Elevator.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Model.Profiles
{
    public class PassRightAccessibleFloorProfile : Profile
    {
        public PassRightAccessibleFloorProfile()
        {
            CreateMap<PassRightAccessibleFloorEntity, PassRightAccessibleFloorModel>();
        }
    }
}
