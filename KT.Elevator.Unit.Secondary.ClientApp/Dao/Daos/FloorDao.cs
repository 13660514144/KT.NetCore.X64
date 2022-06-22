using KT.Common.Core.Utils;
using KT.Common.Data.Daos;
using KT.Common.Data.Models;
using KT.Elevator.Unit.Secondary.ClientApp.Dao.Base;
using KT.Elevator.Unit.Secondary.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Dao.Daos
{
    public class FloorDao : BaseDataDao<UnitFloorEntity>, IFloorDao
    {
        private ElevatorUnitDbContext _context;
        public FloorDao(ElevatorUnitDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<UnitFloorEntity>> GetAllByElevatorGroupIdAsync(string elevatorGroupId)
        {
            var results = await _context.Floors
                .Where(x => x.ElevatorGroupId == elevatorGroupId)
                .ToListAsync();
            return results;
        }

        public async Task<PageData<UnitFloorEntity>> GetPageByElevatorGroupIdAsync(string elevatorGroupId, int page, int size)
        {
            var pageData = new PageData<UnitFloorEntity>();
            pageData.Page = page;
            pageData.Size = size;

            var count = await _context.Floors
                .Where(x => x.ElevatorGroupId == elevatorGroupId)
                .CountAsync();

            pageData.Totals = count;
            pageData.Pages = count.GetPages(size);

            var results = await _context.Floors
                .Where(x => x.ElevatorGroupId == elevatorGroupId)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            pageData.List = results;

            return pageData;
        }
    }
}
