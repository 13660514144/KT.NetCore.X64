using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;

namespace KT.Quanta.Service.Daos
{
    public class FaceInfoDao : BaseDataDao<FaceInfoEntity>, IFaceInfoDao
    {
        public FaceInfoDao(QuantaDbContext context) : base(context)
        {
        }
    }
}
