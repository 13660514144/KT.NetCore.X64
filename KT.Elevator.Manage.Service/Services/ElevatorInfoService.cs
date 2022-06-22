using AutoMapper;
using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.IDaos;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Services
{
    public class ElevatorInfoService : IElevatorInfoService
    {
        private ILogger<ElevatorInfoService> _logger;
        private IElevatorInfoDao _dao;
        private IMapper _mapper;
        private IElevatorGroupDao _elevatorGroupDao;
        public ElevatorInfoService(ILogger<ElevatorInfoService> logger,
            IElevatorInfoDao dao,
            IMapper mapper,
            IElevatorGroupDao elevatorGroupDao)
        {
            _dao = dao;
            _mapper = mapper;
            _elevatorGroupDao = elevatorGroupDao;
            _logger = logger;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _dao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            await _dao.DeleteByIdAsync(id);
        }

        public async Task<ElevatorInfoModel> AddOrEditAsync(ElevatorInfoModel model)
        {
            var entity = await _dao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = _mapper.Map<ElevatorInfoEntity>(model);
                await _dao.AddAsync(entity);
            }
            else
            {
                entity = ElevatorInfoModel.SetEntity(entity, model);
                await _dao.EidtAsync(entity);
            }

            model = _mapper.Map<ElevatorInfoModel>(entity);

            return model;
        }

        public async Task<List<ElevatorInfoModel>> GetAllAsync()
        {
            var entities = await _dao.SelectAllAsync();

            var models = _mapper.Map<List<ElevatorInfoModel>>(entities);

            return models;
        }

        public async Task<ElevatorInfoModel> GetByIdAsync(string id)
        {
            var entity = await _dao.SelectByIdAsync(id);
            var model = _mapper.Map<ElevatorInfoModel>(entity);
            return model;
        }

        public async Task<ElevatorInfoModel> GetByRealIdAsync(string realId)
        {
            var entity = await _dao.SelectFirstByLambdaAsync(x => x.RealId == realId);
            var model = _mapper.Map<ElevatorInfoModel>(entity);
            return model;
        }

        public async Task<ElevatorGroupModel> AddOrEditByElevatorGroupId(ElevatorGroupModel model)
        {
            var now = DateTimeUtil.UtcNowMillis();
            var elevatorGroup = await _elevatorGroupDao.GetWithElevatorInfosByIdAsync(model.Id);
            if (elevatorGroup == null)
            {
                throw CustomException.Run($"找不到边缘处理器信息：id:{model.Id} ");
            }
            elevatorGroup.ElevatorInfos = new List<Entities.ElevatorInfoEntity>();
            if (model.ElevatorInfos?.FirstOrDefault() != null)
            {
                foreach (var item in model.ElevatorInfos)
                {
                    var oldElevatorInfo = await _dao.SelectFirstByLambdaAsync(x => x.ElevatorGroupId == model.Id && x.RealId == item.RealId);
                    if (oldElevatorInfo == null)
                    {
                        var elevatorGroupFloor = _mapper.Map<ElevatorInfoEntity>(item);
                        //var elevatorGroupFloor = ElevatorInfoModel.ToEntity(item);
                        if (elevatorGroupFloor.Id.IsIdNull())
                        {
                            elevatorGroupFloor.Id = IdUtil.NewId();
                        }
                        elevatorGroupFloor.ElevatorGroup = elevatorGroup;
                        elevatorGroupFloor.CreatedTime = elevatorGroupFloor.EditedTime = now;

                        elevatorGroup.ElevatorInfos.Add(elevatorGroupFloor);
                    }
                    else
                    {
                        item.Id = oldElevatorInfo.Id;
                        oldElevatorInfo = ElevatorInfoModel.SetEntity(oldElevatorInfo, item);
                        oldElevatorInfo.ElevatorGroup = elevatorGroup;
                        oldElevatorInfo.EditedTime = now;

                        elevatorGroup.ElevatorInfos.Add(oldElevatorInfo);
                    }
                }
            }

            await _elevatorGroupDao.AttachUpdateAsync(elevatorGroup);

            var result = ElevatorGroupModel.ToModel(elevatorGroup, null);
            return result;
        }

        public async Task DeleteByElevatorGroupId(string elevatorGroupId)
        {
            var elevatorGroup = await _elevatorGroupDao.GetWithElevatorInfosByIdAsync(elevatorGroupId);
            if (elevatorGroup == null)
            {
                _logger.LogWarning($"未找到电梯组，未删除任何电梯信息数据！");
                return;
            }
            elevatorGroup.ElevatorInfos = null;

            await _elevatorGroupDao.AttachUpdateAsync(elevatorGroup);
        }
    }
}
