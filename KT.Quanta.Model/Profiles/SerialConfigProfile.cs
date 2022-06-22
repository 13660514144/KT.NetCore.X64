using AutoMapper;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Models;

namespace KT.Quanta.Model.Profiles
{
    public class SerialConfigProfile : Profile
    {
        public SerialConfigProfile()
        {
            CreateMap<SerialConfigEntity, SerialConfigModel>();
        }
    }
}
