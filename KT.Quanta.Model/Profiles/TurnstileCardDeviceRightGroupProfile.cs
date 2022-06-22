using AutoMapper;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Turnstile.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace KT.Quanta.Model.Profiles
{
    public class TurnstileCardDeviceRightGroupProfile : Profile
    {
        public TurnstileCardDeviceRightGroupProfile()
        {
            CreateMap<CardDeviceRightGroupEntity, TurnstileCardDeviceRightGroupModel>()
                .AfterMap((src, dest, o) =>
                {
                    dest.CardDeviceIds = new List<string>();
                    dest.CardDevices = new List<TurnstileCardDeviceModel>();
                    if (src.RelationCardDevices?.FirstOrDefault() != null)
                    {
                        foreach (var item in src.RelationCardDevices)
                        {
                            dest.CardDeviceIds.Add(item.CardDeviceId);
                            if (item.CardDevice != null)
                            {
                                dest.CardDevices.Add(o.Mapper.Map<TurnstileCardDeviceModel>(item.CardDevice));
                            }
                        }
                    }
                });
        }
    }
}
