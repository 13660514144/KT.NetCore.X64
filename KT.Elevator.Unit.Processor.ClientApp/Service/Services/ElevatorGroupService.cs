using KT.Common.Core.Utils;
using KT.Elevator.Unit.Processor.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Enums;
using KT.Elevator.Unit.Entity.Models;
using KT.Elevator.Unit.Processor.ClientApp.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.Services
{
    public class ElevatorGroupService : IElevatorGroupService
    {
        private IElevatorGroupDao _dao;
        private readonly IFloorDao _floorDao;
        public ElevatorGroupService(IElevatorGroupDao dao,
            IFloorDao floorDao)
        {
            _dao = dao;
            _floorDao = floorDao;
        }

        public async Task AddOrEditFromDeviceAsync(UnitHandleElevatorDeviceModel model)
        {
            var elevatorGroup = await _dao.GetWithRelevanceFloorsByIdAsync(model.ElevatorGroupId);
            if (elevatorGroup == null)
            {
                elevatorGroup = new UnitElevatorGroupEntity();
                elevatorGroup.Id = model.ElevatorGroupId;
                elevatorGroup.ElevatorGroupFloors = new List<UnitElevatorGroupFloorEntity>();
                if (model.Floors?.FirstOrDefault() != null)
                {
                    foreach (var item in model.Floors)
                    {
                        var elevatorGroupFloor = new UnitElevatorGroupFloorEntity();
                        var floor = await _floorDao.SelectByIdAsync(item.Id);
                        if (floor == null)
                        {
                            item.ElevatorGroupId = elevatorGroup.Id;
                            await _floorDao.InsertAsync(item);

                            elevatorGroupFloor.FloorId = item.Id;
                        }
                        else
                        {
                            floor.Name = item.Name;
                            floor.RealFloorId = item.RealFloorId;
                            floor.IsPublic = item.IsPublic;
                            floor.EdificeId = item.EdificeId;
                            floor.EdificeName = item.EdificeName;
                            floor.CreatedTime = item.CreatedTime;
                            floor.EditedTime = DateTimeUtil.UtcNowMillis();

                            floor.ElevatorGroupId = elevatorGroup.Id;
                            await _floorDao.UpdateAsync(floor);

                            elevatorGroupFloor.FloorId = floor.Id;
                        }

                        elevatorGroup.ElevatorGroupFloors.Add(elevatorGroupFloor);
                    }
                }

                await _dao.InsertAsync(elevatorGroup);
            }
            else
            {
                if (elevatorGroup.ElevatorGroupFloors == null)
                {
                    elevatorGroup.ElevatorGroupFloors = new List<UnitElevatorGroupFloorEntity>();
                }

                if (model.Floors?.FirstOrDefault() == null)
                {
                    elevatorGroup.ElevatorGroupFloors = new List<UnitElevatorGroupFloorEntity>();
                }
                else
                {
                    foreach (var item in model.Floors)
                    {
                        var elevatorGroupFloor = elevatorGroup.ElevatorGroupFloors?.FirstOrDefault(x => x.FloorId == item.Id);
                        if (elevatorGroupFloor == null)
                        {
                            elevatorGroupFloor = new UnitElevatorGroupFloorEntity();
                            elevatorGroup.ElevatorGroupFloors.Add(elevatorGroupFloor);
                        }

                        var floor = await _floorDao.SelectByIdAsync(item.Id);
                        if (floor == null)
                        {
                            item.ElevatorGroupId = elevatorGroup.Id;
                            await _floorDao.InsertAsync(item);

                            elevatorGroupFloor.FloorId = item.Id;
                        }
                        else
                        {
                            floor.Name = item.Name;
                            floor.RealFloorId = item.RealFloorId;
                            floor.IsPublic = item.IsPublic;
                            floor.EdificeId = item.EdificeId;
                            floor.EdificeName = item.EdificeName;
                            floor.CreatedTime = item.CreatedTime;
                            floor.EditedTime = DateTimeUtil.UtcNowMillis();

                            floor.ElevatorGroupId = elevatorGroup.Id;
                            await _floorDao.UpdateAsync(floor);

                            elevatorGroupFloor.FloorId = floor.Id;
                        }
                    }

                    //清除不存在的关系楼层
                    elevatorGroup.ElevatorGroupFloors.RemoveAll(x => !model.Floors.Any(y => y.Id == x.FloorId));
                }

                await _dao.UpdateAsync(elevatorGroup);
            }
        }
    }
}
