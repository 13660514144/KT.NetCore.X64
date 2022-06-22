using AutoMapper;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Models;

namespace KT.Quanta.Model.Profiles
{
    public class HandleElevatorInputDeviceProfile : Profile
    {
        public HandleElevatorInputDeviceProfile()
        {
            CreateMap<HandleElevatorInputDeviceEntity, HandleElevatorInputDeviceModel>();
        }
    }
}
