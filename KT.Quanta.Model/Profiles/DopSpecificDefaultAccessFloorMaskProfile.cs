using AutoMapper;
using KT.Quanta.Entity.Kone;
using KT.Quanta.Model.Kone;

namespace KT.Quanta.Model.Profiles
{
    public class DopSpecificDefaultAccessFloorMaskProfile : Profile
    {
        public DopSpecificDefaultAccessFloorMaskProfile()
        {
            CreateMap<DopSpecificDefaultAccessFloorMaskEntity, DopSpecificDefaultAccessFloorMaskModel>();
        }
    }
}
