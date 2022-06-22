using AutoMapper;
using KT.Quanta.Service.Daos;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.Turnstile.Dtos;
using KT.Turnstile.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Turnstile.Services
{
    public class TurnstileCardDeviceRightGroupService : ITurnstileCardDeviceRightGroupService
    {
        private readonly ICardDeviceRightGroupDao _dao;
        private readonly ITurnstileCardDeviceRightGroupDeviceDistributeService _rightGroupDistribute;
        private readonly IPassRightDao _passRightDao;
        private readonly TurnstilePassRightDistributeQueue _turnstilePassRightDistributeQueue;
        private readonly IMapper _mapper;

        public TurnstileCardDeviceRightGroupService(ICardDeviceRightGroupDao dao,
            ITurnstileCardDeviceRightGroupDeviceDistributeService rightGroupDistribute,
             IPassRightDao passRightDao,
             TurnstilePassRightDistributeQueue turnstilePassRightDistributeQueue,
             IMapper mapper)
        {
            _dao = dao;
            _rightGroupDistribute = rightGroupDistribute;
            _passRightDao = passRightDao;
            _turnstilePassRightDistributeQueue = turnstilePassRightDistributeQueue;
            _mapper = mapper;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _dao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _dao.GetByIdAsync(id);
            if (entity != null)
            {
                await _dao.DeleteAsync(id);
            }

            //分发数据 
            var model = _mapper.Map<TurnstileCardDeviceRightGroupModel>(entity);
            await _rightGroupDistribute.DeleteAsync(model);
        }

        public async Task<TurnstileCardDeviceRightGroupModel> AddOrEditAsync(TurnstileCardDeviceRightGroupModel model)
        {
            var oldCardDeviceIds = new List<string>();
            var newCardDeviceIds = new List<string>();
            var addCardDeviceIds = new List<string>();
            var removeCardDeviceIds = new List<string>();

            var oldProcessorIds = new List<string>();
            var newProcessorIds = new List<string>();
            var addProcessorIds = new List<string>();
            var removeProcessorIds = new List<string>();


            var entity = await _dao.GetWithPassRightsByIdAsync(model.Id);
            if (entity == null)
            {
                entity = TurnstileCardDeviceRightGroupModel.ToEntity(model);
                entity = await _dao.AddAsync(entity);
                model = _mapper.Map(entity, model);
            }
            else
            {
                //获取旧设备id
                if (entity.RelationCardDevices?.FirstOrDefault() != null)
                {
                    //获取读卡器id
                    oldCardDeviceIds = entity.RelationCardDevices.Select(x => x.CardDeviceId).ToList();
                    //获取边缘处理器id
                    oldProcessorIds = entity.RelationCardDevices.Select(x => x.CardDevice?.ProcessorId).Where(y => !string.IsNullOrEmpty(y)).ToList();
                }

                entity = TurnstileCardDeviceRightGroupModel.SetEntity(entity, model);
                entity = await _dao.EditAsync(entity);

                //获取新备id
                if (entity.RelationCardDevices?.FirstOrDefault() != null)
                {
                    //获取读卡器id
                    newCardDeviceIds = entity.RelationCardDevices.Select(x => x.CardDeviceId).ToList();
                    //获取边缘处理器id
                    newProcessorIds = entity.RelationCardDevices.Select(x => x.CardDevice?.ProcessorId).Where(y => !string.IsNullOrEmpty(y)).ToList();
                }

                //新增的设备
                addCardDeviceIds = newCardDeviceIds.Where(x => !oldCardDeviceIds.Contains(x)).ToList();
                //删除的设备
                removeCardDeviceIds = oldCardDeviceIds.Where(x => !newCardDeviceIds.Contains(x)).ToList();
                //新增的设备
                addProcessorIds = newProcessorIds.Where(x => !oldProcessorIds.Contains(x)).ToList();
                //删除的设备
                removeProcessorIds = oldProcessorIds.Where(x => !newProcessorIds.Contains(x)).ToList();

                model = _mapper.Map(entity, model);
            }

            //分发数据 
            await _rightGroupDistribute.AddOrUpdateAsync(model);

            //删除读卡器权限
            if (removeCardDeviceIds?.FirstOrDefault() != null && entity.RelationPassRights?.FirstOrDefault() != null)
            {
                //一个一个权限来
                foreach (var removeCardDeviceId in removeCardDeviceIds)
                {
                    //一个一个权限来
                    foreach (var relation in entity.RelationPassRights)
                    {
                        var passRight = relation.PassRight;
                        //检测当前权限是否与当前读卡器关联
                        var isExists = await _passRightDao.IsExistsByIdAndCardDeviceIdAsync(passRight.Id, removeCardDeviceId);
                        if (!isExists)
                        {
                            //如果不存在则删除权限
                            var turnstileDistributeDeleteModel = new TurnstilePassRightDistributeQueueModel();
                            turnstileDistributeDeleteModel.DistributeType = PassRightDistributeTypeEnum.DeleteByIds.Value;
                            turnstileDistributeDeleteModel.Ids = new List<string>() { removeCardDeviceId };
                            turnstileDistributeDeleteModel.PassRight = _mapper.Map<TurnstilePassRightModel>(passRight);
                            _turnstilePassRightDistributeQueue.Add(turnstileDistributeDeleteModel);
                        }
                    }
                }
            }
            //删除边缘处理器权限
            if (removeProcessorIds?.FirstOrDefault() != null && entity.RelationPassRights?.FirstOrDefault() != null)
            {
                //一个一个权限来
                foreach (var removeProcessorId in removeProcessorIds)
                {
                    //一个一个权限来
                    foreach (var relation in entity.RelationPassRights)
                    {
                        var passRight = relation.PassRight;
                        //检测当前权限是否与当前读卡器关联
                        var isExists = await _passRightDao.IsExistsByIdAndProcessorIdAsync(passRight.Id, removeProcessorId);
                        if (!isExists)
                        {
                            //如果不存在则删除权限
                            var turnstileDistributeDeleteModel = new TurnstilePassRightDistributeQueueModel();
                            turnstileDistributeDeleteModel.DistributeType = PassRightDistributeTypeEnum.DeleteByIds.Value;
                            turnstileDistributeDeleteModel.Ids = new List<string>() { removeProcessorId };
                            turnstileDistributeDeleteModel.PassRight = _mapper.Map<TurnstilePassRightModel>(passRight);
                            _turnstilePassRightDistributeQueue.Add(turnstileDistributeDeleteModel);
                        }
                    }
                }
            }

            //新增读卡器权限
            if (addCardDeviceIds?.FirstOrDefault() != null && entity.RelationPassRights?.FirstOrDefault() != null)
            {
                //一个一个权限来
                foreach (var addCardDeviceId in addCardDeviceIds)
                {
                    //一个一个权限来
                    foreach (var relation in entity.RelationPassRights)
                    {
                        var passRight = relation.PassRight;
                        //检测当前权限是否与当前读卡器关联
                        var isExists = await _passRightDao.IsExistsByIdAndCardDeviceIdExcludeRightGroupIdAsync(passRight.Id, addCardDeviceId, entity.Id);
                        if (!isExists)
                        {
                            //如果其它权限组中不关联当前设备与权限，则下发权限 
                            var turnstileDistributeModel = new TurnstilePassRightDistributeQueueModel();
                            turnstileDistributeModel.DistributeType = PassRightDistributeTypeEnum.AddOrEditByIds.Value;
                            turnstileDistributeModel.Ids = new List<string>() { addCardDeviceId };
                            turnstileDistributeModel.PassRight = _mapper.Map<TurnstilePassRightModel>(passRight);
                            _turnstilePassRightDistributeQueue.Add(turnstileDistributeModel);
                        }
                    }
                }
            }

            //新增读卡器权限
            if (addProcessorIds?.FirstOrDefault() != null && entity.RelationPassRights?.FirstOrDefault() != null)
            {
                //一个一个权限来
                foreach (var addProcessorId in addProcessorIds)
                {
                    //一个一个权限来
                    foreach (var relation in entity.RelationPassRights)
                    {
                        var passRight = relation.PassRight;
                        //检测当前权限是否与当前读卡器关联
                        var isExists = await _passRightDao.IsExistsByIdAndProcessorIdExcludeRightGroupIdAsync(passRight.Id, addProcessorId, entity.Id);
                        if (!isExists)
                        {
                            //如果其它权限组中不关联当前设备与权限，则下发权限 
                            var turnstileDistributeModel = new TurnstilePassRightDistributeQueueModel();
                            turnstileDistributeModel.DistributeType = PassRightDistributeTypeEnum.AddOrEditByIds.Value;
                            turnstileDistributeModel.Ids = new List<string>() { addProcessorId };
                            turnstileDistributeModel.PassRight = _mapper.Map<TurnstilePassRightModel>(passRight);
                            _turnstilePassRightDistributeQueue.Add(turnstileDistributeModel);
                        }
                    }
                }
            }

            return model;
        }

        public async Task<List<TurnstileCardDeviceRightGroupModel>> GetAllAsync()
        {
            var entities = await _dao.GetAllAsync();

            var models = _mapper.Map<List<TurnstileCardDeviceRightGroupModel>>(entities);

            return models;
        }

        public async Task<TurnstileCardDeviceRightGroupModel> GetByIdAsync(string id)
        {
            var entity = await _dao.GetByIdAsync(id);
            var model = _mapper.Map<TurnstileCardDeviceRightGroupModel>(entity);
            return model;
        }

        public async Task<List<TurnstileUnitRightGroupEntity>> GetUnitAllAsync(int page, int size)
        {
            var results = new List<TurnstileUnitRightGroupEntity>();
            var entities = await _dao.GetPageAsync(page, size);
            if (entities == null)
            {
                return results;
            }

            foreach (var item in entities)
            {
                var result = new TurnstileUnitRightGroupEntity();
                result.Id = item.Id;
                result.EditedTime = item.EditedTime;

                results.Add(result);

                if (item.RelationCardDevices == null)
                {
                    continue;
                }
                foreach (var obj in item.RelationCardDevices)
                {
                    if (obj.CardDevice == null)
                    {
                        continue;
                    }
                    var detail = new TurnstileUnitRightGroupDetailEntity();
                    detail.EditedTime = item.EditedTime;

                    detail.CardDevice = new TurnstileUnitCardDeviceEntity();
                    detail.CardDevice.Id = obj.CardDevice.Id;
                    detail.CardDeviceId = obj.CardDevice.Id;

                    result.Details.Add(detail);
                }
            }

            return results;
        }
    }
}
