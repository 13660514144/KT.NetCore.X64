using AutoMapper;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Turnstile.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace KT.Quanta.Model.Profiles
{
    public class TurnstilePassRightProfile : Profile
    {
        public TurnstilePassRightProfile()
        {
            CreateMap<PassRightEntity, TurnstilePassRightModel>()
                .AfterMap((src, dest, o) =>
                {
                    dest.CardDeviceRightGroupIds = new List<string>();
                    dest.CardDeviceRightGroups = new List<TurnstileCardDeviceRightGroupModel>();
                    if (src.RelationCardDeviceRightGroups?.FirstOrDefault() != null)
                    {
                        foreach (var item in src.RelationCardDeviceRightGroups)
                        {
                            dest.CardDeviceRightGroupIds.Add(item.CardDeviceRightGroupId);
                            if (item.CardDeviceRightGroup != null)
                            {
                                dest.CardDeviceRightGroups.Add(o.Mapper.Map<TurnstileCardDeviceRightGroupModel>(item.CardDeviceRightGroup));
                            }
                        }
                    }
                });
        }
    }
}
