using AutoMapper;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Models;

namespace KT.Quanta.Model.Profiles
{
    public class HandleElevatorDeviceProfile : Profile
    {
        public HandleElevatorDeviceProfile()
        {
            CreateMap<HandleElevatorDeviceEntity, HandleElevatorDeviceModel>();
        }
    }
}
