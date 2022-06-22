using AutoMapper;
using KT.Common.Core.Utils;
using KT.Common.Data.Models;
using KT.Elevator.Unit.Entity.Entities;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Turnstile.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
{
    public class PassRightService : IPassRightService
    {
        private IPassRightDao _passRightDataDao;
        private ILogger<PassRightService> _logger;
        private IElevatorPassRightDeviceDistributeService _passRightDistributeService;
        private ITurnstilePassRightDeviceDistributeService _turnstilePassRightDeviceDistributeService;
        private IFaceInfoService _faceInfoService;
        private ElevatorPassRightDistributeQueue _elevatorPassRightDistributeQueue;
        private TurnstilePassRightDistributeQueue _turnstilePassRightDistributeQueue;
        private readonly IMapper _mapper;

        public PassRightService(IPassRightDao passRightDataDao,
            ILogger<PassRightService> logger,
            IElevatorPassRightDeviceDistributeService passRightDistributeService,
            ITurnstilePassRightDeviceDistributeService turnstilePassRightDeviceDistributeService,
            IFaceInfoService faceInfoService,
            ElevatorPassRightDistributeQueue elevatorPassRightDistributeQueue,
            TurnstilePassRightDistributeQueue turnstilePassRightDistributeQueue,
            IMapper mapper)
        {
            _passRightDataDao = passRightDataDao;
            _logger = logger;
            _passRightDistributeService = passRightDistributeService;
            _turnstilePassRightDeviceDistributeService = turnstilePassRightDeviceDistributeService;
            _faceInfoService = faceInfoService;
            _elevatorPassRightDistributeQueue = elevatorPassRightDistributeQueue;
            _turnstilePassRightDistributeQueue = turnstilePassRightDistributeQueue;
            _mapper = mapper;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _passRightDataDao.HasInstanceByIdAsync(id);
        }
        public async Task OnlyDeleteAsync(string sign)
        {
            var entity = await _passRightDataDao.GetWithPersonRrightsBySignAsync(sign);
            if (entity != null)
            {
                await _passRightDataDao.RemoveAsync(entity);
                //var model = PassRightModel.ToModel(entity);
            }
        }
        public async Task DeleteAsync(string sign)
        {
            var entity = await _passRightDataDao.GetWithPersonRrightsBySignAsync(sign);

            if (entity != null)
            {
                await _passRightDataDao.RemoveAsync(entity);

                //分发数据 
                var model = PassRightModel.ToModel(entity);

                var distributeModel = new ElevatorPassRightDistributeQueueModel();
                distributeModel.DistributeType = PassRightDistributeTypeEnum.Delete.Value;
                distributeModel.PassRight = model;
                _elevatorPassRightDistributeQueue.Add(distributeModel);
            }
        }
        public async Task<PassRightModel> OnlyAddOrEditAsync(PassRightModel model)
        {
            model.RightType = RightTypeEnum.ELEVATOR.Value;

            //分发数据 dont 
            

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
            var entity = await _passRightDataDao.GetWithFloorAndFloorsBySignAndAccessTypeAsync(model.Sign, model.AccessType, model.RightType);
            if (entity == null)
            {
                entity = PassRightModel.ToEntity(model);
                //API保存  新增权限
                await _passRightDataDao.AddAsync(entity);
            }
            else
            {
                //存储旧数据用于下发删除

                model.Id = entity.Id;
                entity = PassRightModel.SetEntity(entity, model, null);

                //关联楼层
                if (entity.RelationFloors?.FirstOrDefault() == null)
                {
                    entity.RelationFloors = new List<PassRightRelationFloorEntity>();
                }
                if (model.FloorIds.FirstOrDefault() == null)
                {
                    entity.RelationFloors.Clear();
                }
                else
                {
                    entity.RelationFloors.RemoveAll(x => !model.FloorIds.Contains(x.Floor.Id));
                    foreach (var item in model.FloorIds)
                    {
                        if (entity.RelationFloors.Any(x => x.Floor.Id == item))
                        {
                            continue;
                        }

                        //增加新的关联楼层
                        var relationFloor = new PassRightRelationFloorEntity();
                        relationFloor.Id = IdUtil.NewId();

                        var floor = await _passRightDataDao.SelectRelevanceObjectAsync<FloorEntity>(item);
                        if (floor == null)
                        {
                            _logger.LogWarning($"权限关联楼层为空：floorId:{item} ");
                            continue;
                        }
                        relationFloor.PassRight = entity;
                        relationFloor.Floor = floor;

                        entity.RelationFloors.Add(relationFloor);
                    }
                }
                ////API保存  修改权限
                entity = await _passRightDataDao.EditAsync(entity);
            }
            model = PassRightModel.SetModel(model, entity);
            return model;
        }

        public async Task<PassRightModel> AddOrEditAsync(PassRightModel model)
        {
            model.RightType = RightTypeEnum.ELEVATOR.Value;

            //分发数据 
            var distributeModel = new ElevatorPassRightDistributeQueueModel();

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
            var entity = await _passRightDataDao.GetWithFloorAndFloorsBySignAndAccessTypeAsync(model.Sign, model.AccessType, model.RightType);
            if (entity == null)
            {
                entity = PassRightModel.ToEntity(model);
                //API保存  新增权限
                await _passRightDataDao.AddAsync(entity);
            }
            else
            {
                //存储旧数据用于下发删除
                distributeModel.OldPassRight = PassRightModel.ToModel(entity);

                model.Id = entity.Id;
                entity = PassRightModel.SetEntity(entity, model, null);

                //关联楼层
                if (entity.RelationFloors?.FirstOrDefault() == null)
                {
                    entity.RelationFloors = new List<PassRightRelationFloorEntity>();
                }
                if (model.FloorIds.FirstOrDefault() == null)
                {
                    entity.RelationFloors.Clear();
                }
                else
                {
                    entity.RelationFloors.RemoveAll(x => !model.FloorIds.Contains(x.Floor.Id));
                    foreach (var item in model.FloorIds)
                    {
                        if (entity.RelationFloors.Any(x => x.Floor.Id == item))
                        {
                            continue;
                        }

                        //增加新的关联楼层
                        var relationFloor = new PassRightRelationFloorEntity();
                        relationFloor.Id = IdUtil.NewId();

                        var floor = await _passRightDataDao.SelectRelevanceObjectAsync<FloorEntity>(item);
                        if (floor == null)
                        {
                            _logger.LogWarning($"权限关联楼层为空：floorId:{item} ");
                            continue;
                        }
                        relationFloor.PassRight = entity;
                        relationFloor.Floor = floor;

                        entity.RelationFloors.Add(relationFloor);
                    }
                }
                ////API保存  修改权限
                entity = await _passRightDataDao.EditAsync(entity);
            }

            //分发数据 

            model = PassRightModel.SetModel(model, entity);

            #region  不下发权限 2021-08-06
            distributeModel.DistributeType = PassRightDistributeTypeEnum.AddOrEdit.Value;
            distributeModel.PassRight = model;
            if (model.AccessType == AccessTypeEnum.FACE.Value)
            {
                //分发数据  人脸不下发
                var face = await _faceInfoService.GetByIdAsync(model.Sign);
                if (face == null)
                {
                    //找不到人脸信息继续授权，用于迅达派梯
                    _logger.LogWarning($"找不到人脸信息，继续授权：sign:{model.Sign} ");
                }
                distributeModel.Face = face;
            }
            _elevatorPassRightDistributeQueue.Add(distributeModel);
            #endregion
            return model;
        }

        public async Task<List<PassRightModel>> GetAllAsync()
        {
            var entities = await _passRightDataDao.GetWithFloorsByRightTypeAsync(RightTypeEnum.ELEVATOR.Value);

            var models = PassRightModel.ToModels(entities);

            return models;
        }

        public async Task<PageData<PassRightModel>> GetAllPageAsync(int page, int size, string name, string sign)
        {
            var entities = await _passRightDataDao.GetPageWithFloorsByRightTypeAsync(page, size, RightTypeEnum.ELEVATOR.Value, name, sign);

            var pageData = new PageData<PassRightModel>();
            pageData.Page = entities.Page;
            pageData.Pages = entities.Pages;
            pageData.Size = entities.Size;
            pageData.Totals = entities.Totals;

            pageData.List = PassRightModel.ToModels(entities.List);

            if (pageData.List?.FirstOrDefault() != null)
            {
                foreach (var item in pageData.List)
                {
                    if (item.Floors == null)
                    {
                        item.Floors = new List<FloorModel>();
                    }
                    if (item.Floors?.FirstOrDefault(x => x.Id == item.Floor.Id) == null)
                    {
                        item.Floors.Add(item.Floor);
                    }

                    item.Floors = item.Floors.OrderBy(x => ConvertUtil.ToInt32(x.PhysicsFloor)).ToList();
                }
            }
            return pageData;
        }

        public async Task<PassRightModel> GetByIdAsync(string id)
        {
            var entity = await _passRightDataDao.GetWithFloorsByIdAsync(id);
            var model = PassRightModel.ToModel(entity);
            return model;
        }

        public async Task<List<UnitPassRightEntity>> GetUnitByPageAsync(int page, int size)
        {
            var results = new List<UnitPassRightEntity>();
            var entities = await _passRightDataDao.GetPageWithFloorAsync(page, size, RightTypeEnum.ELEVATOR.Value);

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

                //人脸
                if (item.AccessType == AccessTypeEnum.FACE.Value)
                {
                    var face = await _faceInfoService.GetByIdAsync(item.Sign);
                    if (face == null)
                    {
                        _logger.LogWarning($"找不到人脸信息：sign:{item.Sign} ");
                        continue;
                    }
                    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, face.FaceUrl);
                    if (File.Exists(path))
                    {
                        var faceFile = new FileStream(path, FileMode.Open);
                        byte[] faceBytes = new byte[faceFile.Length];
                        faceFile.Read(faceBytes, 0, faceBytes.Length);
                        faceFile.Close();

                        result.Feature = faceBytes;
                        result.FeatureSize = faceBytes.Length;
                    }
                    else
                    {
                        _logger.LogWarning($"找不到人脸信息：path:{path} ");
                        continue;
                    }
                }

                //可去楼层
                if (item.RelationFloors.FirstOrDefault() != null)
                {
                    foreach (var obj in item.RelationFloors)
                    {
                        var passRightDetail = new UnitPassRightDetailEntity();
                        passRightDetail.FloorId = obj.Floor?.Id;
                        passRightDetail.FloorName = obj.Floor?.Name;

                        result.PassRightDetails.Add(passRightDetail);
                    }
                }

                results.Add(result);
            }

            return results;
        }

        public async Task DeleteBySignAsync(string sign)
        {
            //删除数据，海康删除人脸权限要关联卡号，所以要把人员关联的权限全部查询出来
            var entities = await _passRightDataDao.GetAllWithPersonBySignAsync(sign);
            if (entities?.FirstOrDefault() == null)
            {
                _logger.LogInformation($"不存在需要删除的数据：sign:{sign} ");
                return;
            }

            foreach (var item in entities)
            {
                await _passRightDataDao.DeleteAsync(item,false);
            }
            await _passRightDataDao.SaveChangesAsync();

            //分发数据  dont
            DistributeDeleteAsync(entities);
        }

        private async Task DistributeDeleteAsync(List<PassRightEntity> entities)
        {
            if (entities != null && entities.FirstOrDefault() != null)
            {
                foreach (var item in entities)
                {
                    //删除闸机
                    var turnstileModel = _mapper.Map<TurnstilePassRightModel>(item);
                    var turnstileDistributeModel = new TurnstilePassRightDistributeQueueModel();
                    turnstileDistributeModel.DistributeType = PassRightDistributeTypeEnum.Delete.Value;
                    turnstileDistributeModel.PassRight = turnstileModel;
                    _turnstilePassRightDistributeQueue.Add(turnstileDistributeModel);

                    //删除梯控
                    var model = PassRightModel.ToModel(item);

                    var distributeModel = new ElevatorPassRightDistributeQueueModel();
                    distributeModel.DistributeType = PassRightDistributeTypeEnum.Delete.Value;
                    distributeModel.PassRight = model;
                    _elevatorPassRightDistributeQueue.Add(distributeModel);
                }
            }
        }

        public async Task<PassRightModel> GetBySignAsync(string sign)
        {
            var entity = await _passRightDataDao.GetWithFloorsBySignAsync(sign, RightTypeEnum.ELEVATOR.Value);
            var model = PassRightModel.ToModel(entity);
            return model;
        }

        public async Task<List<PassRightModel>> GetByPersonIdAsync(string personId)
        {
            var entities = await _passRightDataDao.SelectByLambdaAsync(x => x.Person.Id == personId);

            var models = PassRightModel.ToModels(entities);

            return models;
        }
    }
}
