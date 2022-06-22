using KT.Common.Data.Daos;
using KT.Quanta.Entity.Kone;
using KT.Quanta.IDao.Kone;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Daos.Kone
{
    public class DopMaskRecordDao : BaseDataDao<DopMaskRecordEntity>, IDopMaskRecordDao
    {
        private QuantaDbContext _context;
        public DopMaskRecordDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }


    }
}
