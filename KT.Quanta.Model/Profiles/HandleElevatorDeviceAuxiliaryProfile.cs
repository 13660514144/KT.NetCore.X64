﻿using AutoMapper;
using KT.Quanta.Entity.Entities;
using KT.Quanta.Model.Elevator.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Model.Profiles
{
    public class HandleElevatorDeviceAuxiliaryProfile : Profile
    {
        public HandleElevatorDeviceAuxiliaryProfile()
        {
            CreateMap<HandleElevatorDeviceAuxiliaryEntity, HandleElevatorDeviceAuxiliaryModel>();
        }
    }
}
