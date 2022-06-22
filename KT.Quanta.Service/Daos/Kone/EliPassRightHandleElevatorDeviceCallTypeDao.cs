using KT.Common.Data.Daos;
using KT.Quanta.Entity.Entities;
using KT.Quanta.Service.IDaos;

namespace KT.Quanta.Service.Daos
{
    public class EliPassRightHandleElevatorDeviceCallTypeDao
        : BaseDataDao<EliPassRightHandleElevatorDeviceCallTypeEntity>, IEliPassRightHandleElevatorDeviceCallTypeDao
    {
        private QuantaDbContext _context;
        public EliPassRightHandleElevatorDeviceCallTypeDao(QuantaDbContext context)
            : base(context)
        {
            _context = context;
        }

    }
}
