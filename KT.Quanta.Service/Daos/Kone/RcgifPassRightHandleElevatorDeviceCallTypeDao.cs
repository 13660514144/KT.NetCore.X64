using KT.Common.Data.Daos;
using KT.Quanta.Entity.Entities;
using KT.Quanta.Service.IDaos;

namespace KT.Quanta.Service.Daos
{
    public class RcgifPassRightHandleElevatorDeviceCallTypeDao
        : BaseDataDao<RcgifPassRightHandleElevatorDeviceCallTypeEntity>, IRcgifPassRightHandleElevatorDeviceCallTypeDao
    {
        private QuantaDbContext _context;
        public RcgifPassRightHandleElevatorDeviceCallTypeDao(QuantaDbContext context)
            : base(context)
        {
            _context = context;
        }

    }
}
