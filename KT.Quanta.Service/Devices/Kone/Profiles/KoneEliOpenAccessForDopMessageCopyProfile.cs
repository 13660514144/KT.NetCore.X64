using AutoMapper;
using KT.Quanta.Service.Devices.Kone.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Profiles
{
    public class KoneEliOpenAccessForDopMessageCopyProfile : Profile
    {
        public KoneEliOpenAccessForDopMessageCopyProfile()
        {
            CreateMap<KoneEliOpenAccessForDopMessage, KoneEliOpenAccessForDopMessage>()
                .AfterMap((src, dest, o) =>
                {
                    dest.OpenEvents = new List<KoneEliOpenAccessForDopMessageOpenEventData>();
                    if (src.OpenEvents?.FirstOrDefault() != null)
                    {
                        foreach (var item in src.OpenEvents)
                        {
                            dest.OpenEvents.Add(o.Mapper.Map<KoneEliOpenAccessForDopMessageOpenEventData>(item));
                        }
                    }
                });
        }
    }
}
