using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Daos
{
    public class PersonDao : BaseDataDao<PersonEntity>, IPersonDao
    {
        private QuantaDbContext _context;
        public PersonDao(QuantaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PersonEntity> GetWithPassRightsByAsync(string passRightSign)
        {
            var entity = await _context.Persons
                .Include(x => x.PassRights)
                .FirstOrDefaultAsync(x => x.PassRights.Any(x => x.Sign == passRightSign));

            return entity;
        }
    }
}
