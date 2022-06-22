using AutoMapper;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Turnstile.Dtos;

namespace KT.Quanta.Model.Profiles
{
    public class TurnstileProcessorProfile : Profile
    {
        public TurnstileProcessorProfile()
        {
            CreateMap<ProcessorEntity, TurnstileProcessorModel>();
        }
    }
}
