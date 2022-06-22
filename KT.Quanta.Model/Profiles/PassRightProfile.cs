using AutoMapper;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace KT.Quanta.Model.Profiles
{
    public class PassRightProfile : Profile
    {
        public PassRightProfile()
        {
            CreateMap<PassRightEntity, PassRightModel>()
                .AfterMap((src, dest, o) =>
                {
                    dest.FloorIds = new List<string>();
                    dest.Floors = new List<FloorModel>();
                    if (src.RelationFloors?.FirstOrDefault() != null)
                    {
                        foreach (var item in src.RelationFloors)
                        {
                            dest.FloorIds.Add(item.FloorId);
                            if (item.Floor != null)
                            {
                                dest.Floors.Add(o.Mapper.Map<FloorModel>(item.Floor));
                            }
                        }
                    }
                });
        }
    }
}
