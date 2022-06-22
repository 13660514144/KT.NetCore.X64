using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Service.Devices.DeviceDistributes;
using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.IDaos;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Services
{
    public class PassRightService : IPassRightService
    {
        private IPassRightDao _passRightDataDao;
        private ILogger<PassRightService> _logger;
        private IPassRightDeviceDistributeService _passRightDistributeService;
        private IFaceInfoService _faceInfoService;

        public PassRightService(IPassRightDao passRightDataDao,
            ILogger<PassRightService> logger,
            IPassRightDeviceDistributeService passRightDistributeService,
            IFaceInfoService faceInfoService)
        {
            _passRightDataDao = passRightDataDao;
            _logger = logger;
            _passRightDistributeService = passRightDistributeService;
            _faceInfoService = faceInfoService;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _passRightDataDao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _passRightDataDao.GetWithPersonRrightsByIdAsync(id);

            if (entity != null)
            {
                await _passRightDataDao.RemoveAsync(entity);

                //分发数据 
                var model = PassRightModel.ToModel(entity);
                _passRightDistributeService.DeleteAsync(model);
            }
        }

        public async Task<PassRightModel> AddOrEditAsync(PassRightModel model)
        {
            //可去楼层包含所在楼层
            if (model.FloorIds == null)
            {
                model.FloorIds = new List<string>();
            }
            if (!string.IsNullOrEmpty(model.FloorId) && !model.FloorIds.Contains(model.FloorId))
            {
                model.FloorIds.Add(model.FloorId);
            }

            //新增或修改数据
            var entity = await _passRightDataDao.GetBySignAndAccessTypeAsync(model.Sign, model.AccessType);
            if (entity == null)
            {
                entity = PassRightModel.ToEntity(model);

                await _passRightDataDao.AddAsync(entity);
            }
            else
            {
                model.Id = entity.Id;
                entity = PassRightModel.SetEntity(entity, model);

                await _passRightDataDao.EditAsync(entity);
            }

            model = PassRightModel.SetModel(model, entity);

            //分发数据 
            if (model.AccessType == CardTypeEnum.FACE.Value)
            {
                //分发数据 
                var face = await _faceInfoService.GetByIdAsync(model.Sign);
                _passRightDistributeService.AddOrUpdateAsync(model, face);
            }
            else
            {
                //分发数据 
                _passRightDistributeService.AddOrUpdateAsync(model);
            }

            return model;
        }

        public async Task<List<PassRightModel>> GetAllAsync()
        {
            var entities = await _passRightDataDao.GetAllAsync();

            var models = PassRightModel.ToModels(entities);

            return models;
        }

        public async Task<PassRightModel> GetByIdAsync(string id)
        {
            var entity = await _passRightDataDao.GetByIdAsync(id);
            var model = PassRightModel.ToModel(entity);
            return model;
        }

        public async Task<List<UnitPassRightEntity>> GetUnitByPageAsync(int page, int size)
        {
            var results = new List<UnitPassRightEntity>();
            var entities = await _passRightDataDao.GetPageWithFloorAsync(page, size);

            if (entities == null || entities.FirstOrDefault() == null)
            {
                return results;
            }

            foreach (var item in entities)
            {
                var result = new UnitPassRightEntity();
                result.Id = item.Id;
                result.Sign = item.Sign;
                result.AccessType = item.AccessType;
                result.TimeStart = item.TimeNow;
                result.TimeEnd = item.TimeOut;

                results.Add(result);

                if (item.RelationFloors == null
                    || item.RelationFloors.FirstOrDefault() == null)
                {
                    continue;
                }

                foreach (var obj in item.RelationFloors)
                {
                    var passRightDetail = new UnitPassRightDetailEntity();
                    passRightDetail.FloorId = obj.Floor?.Id;
                    passRightDetail.FloorName = obj.Floor?.Name;
                    passRightDetail.RealFloorId = obj.Floor?.RealFloorId;
                    passRightDetail.IsPublic = obj.Floor?.IsPublic ?? false;

                    result.PassRightDetails.Add(passRightDetail);
                }
            }

            return results;
        }

        public async Task DeleteBySignAsync(string sign)
        {
            //删除数据，海康删除人脸权限要关联卡号，所以要把人员关联的权限全部查询出来
            var entities = await _passRightDataDao.DeleteReturnPersonRightsBySignAsync(sign);

            //分发数据 
            DistributeDeleteAsync(entities);
        }

        private async Task DistributeDeleteAsync(List<PassRightEntity> entities)
        {
            if (entities != null && entities.FirstOrDefault() != null)
            {
                foreach (var item in entities)
                {
                    var model = PassRightModel.ToModel(item);
                    await _passRightDistributeService.DeleteAsync(model);
                }
            }
        }

        public async Task<PassRightModel> GetBySignAsync(string sign)
        {
            var entity = await _passRightDataDao.GetWidthFloorsBySignAsync(sign);
            var model = PassRightModel.ToModel(entity);
            return model;
        }
    }
}
