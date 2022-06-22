using KT.Common.Core.Utils;
using KT.Common.Data.Daos;
using KT.Common.Data.Models;
using KT.Elevator.Unit.Processor.ClientApp.Dao.Base;
using KT.Elevator.Unit.Processor.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Dao.Daos
{
    public class FloorDao : BaseDataDao<UnitFloorEntity>, IFloorDao
    {
        private ElevatorUnitDbContext _context;
        public FloorDao(ElevatorUnitDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<UnitFloorEntity>> GetByIdsAsync(List<string> ids)
        {
            var results = await _context.Floors
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
            return results;
        }

        public async Task<PageData<UnitFloorEntity>> GetPageByElevatorGroupIdAsync(string elevatorGroupId, int page, int size)
        {
            var pageData = new PageData<UnitFloorEntity>();
            pageData.Page = page;
            pageData.Size = size;

            var elevatorGroupFloors = await _context.ElevatorGroupFloors
                .Where(x => x.ElevatorGroupId == elevatorGroupId)
                .ToListAsync();

            pageData.Totals = elevatorGroupFloors.Count;
            pageData.Pages = pageData.Totals.GetPages(size);

            var floorIds = elevatorGroupFloors.Select(x => x.FloorId).ToList();

            var results = await _context.Floors
                .Where(x => floorIds.Contains(x.Id))
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            pageData.List = results;

            return pageData;
        }
    }
}
