using AutoMapper;
using KT.Quanta.Service.Devices.Kone.Models;
using KT.Quanta.Service.Devices.Kone.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Profiles
{
    public class KoneEliOpenAccessForDopWithCallTypeMessageCopyProfile : Profile
    {
        public KoneEliOpenAccessForDopWithCallTypeMessageCopyProfile()
        {
            CreateMap<KoneEliOpenAccessForDopWithCallTypeMessage, KoneEliOpenAccessForDopWithCallTypeMessage>()
                .AfterMap((src, dest, o) =>
                {
                    dest.CallTypeDatas = new List<KoneEliOpenAccessForDopCallTypeData>();
                    foreach (var item in src.CallTypeDatas)
                    {
                        dest.CallTypeDatas.Add(o.Mapper.Map<KoneEliOpenAccessForDopCallTypeData>(item));
                    }
                });
        }
    }
}
