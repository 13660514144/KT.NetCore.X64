using KT.Common.Data.Daos;
using KT.Common.Data.Models;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Secondary.ClientApp.Dao.Base;
using KT.Elevator.Unit.Secondary.ClientApp.Dao.IDaos;
using KT.Quanta.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Dao.Daos
{
    public class PassRightDao : BaseDataDao<UnitPassRightEntity>, IPassRightDao
    {
        private ElevatorUnitDbContext _context;
        private ILogger _logger;

        public PassRightDao(ElevatorUnitDbContext context,
            ILogger logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(UnitPassRightEntity entity)
        {
            //新增权限
            await _context.PassRights.AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(UnitPassRightEntity entity)
        {
            //更改权限
            _context.PassRights.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<List<UnitPassRightEntity>> GetBySignsAsync(List<string> signs, string accessType)
        {
            if (accessType == AccessTypeEnum.IC_CARD.Value || accessType == AccessTypeEnum.QR_CODE.Value)
            {
                var entities = await _context.PassRights
                    .Include(x => x.PassRightDetails)
                    .Where(x => signs.Contains(x.Sign) &&
                         (x.AccessType == AccessTypeEnum.IC_CARD.Value || x.AccessType == AccessTypeEnum.QR_CODE.Value))
                    .ToListAsync();

                return entities;
            }
            else
            {
                var entities = await _context.PassRights
                    .Include(x => x.PassRightDetails)
                    .Where(x => signs.Contains(x.Sign) &&
                         x.AccessType == accessType)
                    .ToListAsync();

                return entities;
            }
        }
        public async Task<UnitPassRightEntity> GetBySignAsync(string sign, string accessType)
        {
            if (accessType == AccessTypeEnum.IC_CARD.Value || accessType == AccessTypeEnum.QR_CODE.Value)
            {
                var entitie = await _context.PassRights
                    .Include(x => x.PassRightDetails)
                    .FirstOrDefaultAsync(x => x.Sign == sign &&
                         (x.AccessType == AccessTypeEnum.IC_CARD.Value || x.AccessType == AccessTypeEnum.QR_CODE.Value));

                return entitie;
            }
            else
            {
                var entities = await _context.PassRights
                    .Include(x => x.PassRightDetails)
                    .FirstOrDefaultAsync(x => x.Sign == sign && x.AccessType == accessType);

                return entities;
            }
        }
        public async Task<UnitPassRightEntity> GetByIdAndSignAsync(string id, string sign, string accessType)
        {
            if (accessType == AccessTypeEnum.IC_CARD.Value || accessType == AccessTypeEnum.QR_CODE.Value)
            {
                var passRight = await _context.PassRights
                     .Include(x => x.PassRightDetails)
                     .FirstOrDefaultAsync(x => x.Sign == sign
                           && x.Id != id
                           && (x.AccessType == AccessTypeEnum.IC_CARD.Value || x.AccessType == AccessTypeEnum.QR_CODE.Value));

                return passRight;
            }
            else
            {
                var passRight = await _context.PassRights
                    .Include(x => x.PassRightDetails)
                    .FirstOrDefaultAsync(x => x.Sign == sign
                                           && x.Id != id
                                           && x.AccessType == accessType);

                return passRight;
            }
        }

        public async Task<UnitPassRightEntity> GetWithDetailsByIdAsync(string id)
        {
            var right = await _context.PassRights
                    .Include(x => x.PassRightDetails)
                    .FirstOrDefaultAsync(x => x.Id == id);

            return right;
        }

        public async Task<PageData<UnitPassRightEntity>> GetPageWithDetailsAsync(int page, int size)
        {
            var pageData = new PageData<UnitPassRightEntity>();
            pageData.Page = page;
            pageData.Size = size;

            var results = await _context.PassRights
                .Include(x => x.PassRightDetails)
                .Skip((page - 1) * size)
                .Take(size)
                .OrderByDescending(x => x.EditedTime)
                .ToListAsync();

            pageData.List = results;

            return pageData;
        }
    }
}
