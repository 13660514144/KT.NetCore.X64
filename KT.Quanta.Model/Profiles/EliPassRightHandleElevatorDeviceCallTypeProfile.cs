using AutoMapper;
using KT.Quanta.Entity.Entities;
using KT.Quanta.Model.Kone;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Model.Profiles
{
    public class EliPassRightHandleElevatorDeviceCallTypeProfile : Profile
    {
        public EliPassRightHandleElevatorDeviceCallTypeProfile()
        {
            CreateMap<EliPassRightHandleElevatorDeviceCallTypeEntity, EliPassRightHandleElevatorDeviceCallTypeModel>();
        }
    }
}
