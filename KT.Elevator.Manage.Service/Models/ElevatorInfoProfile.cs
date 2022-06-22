using AutoMapper;
using KT.Elevator.Manage.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Models
{
    /// <summary>
    /// 电梯信息模型映射
    /// </summary>
    public class ElevatorInfoProfile : Profile
    {
        public ElevatorInfoProfile()
        {
            CreateMap<ElevatorInfoEntity, ElevatorInfoModel>().ReverseMap();
            //CreateMap<ElevatorInfoModel, ElevatorInfoEntity>()
            //    .ForMember(d => d.ElevatorGroup.Id, opt => opt.MapFrom(src => src.ElevatorGroupId));
        }
    }
}
