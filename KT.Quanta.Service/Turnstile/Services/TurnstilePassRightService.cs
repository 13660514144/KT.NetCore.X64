using AutoMapper;
using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.Services;
using KT.Quanta.Service.Turnstile.Dtos;
using KT.Turnstile.Unit.Entity.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Turnstile.Services
{
    public class TurnstilePassRightService : ITurnstilePassRightService
    {
        private readonly IPassRightDao _dao;
        private readonly ILogger<TurnstilePassRightService> _logger;
        private readonly ITurnstilePassRightDeviceDistributeService _passRightDistribute;
        private readonly IProcessorDao _processorDao;
        private readonly IFaceInfoService _faceInfoService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly TurnstilePassRightDistributeQueue _turnstilePassRightDistributeQueue;
        private readonly IMapper _mapper;

        public TurnstilePassRightService(IPassRightDao dao,
            ILogger<TurnstilePassRightService> logger,
            ITurnstilePassRightDeviceDistributeService passRightDistribute,
            IFaceInfoService faceInfoService,
            IServiceScopeFactory serviceScopeFactory,
            IProcessorDao processorDao,
            TurnstilePassRightDistributeQueue turnstilePassRightDistributeQueue,
            IMapper mapper)
        {
            _dao = dao;
            _logger = logger;
            _passRightDistribute = passRightDistribute;
            _faceInfoService = faceInfoService;
            _serviceScopeFactory = serviceScopeFactory;
            _processorDao = processorDao;
            _turnstilePassRightDistributeQueue = turnstilePassRightDistributeQueue;
            _mapper = mapper;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _dao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(TurnstilePassRightModel model)
        {
            model.RightType = RightTypeEnum.TURNSTILE.Value;

            //新增或修改数据
            var entity = await _dao.GetWithRightGroupsBySignAndAccessTypeAsync(model.Sign, model.AccessType, model.RightType);
            if (entity != null)
            {
                ////新增设备权限 
                //var deviceIds = new List<string>();
                //if (model.CardDeviceRightGroupIds?.FirstOrDefault() != null)
                //{
                //    //获取新增边缘处理器
                //    var newDevices = await _dao.RightGroupRelationCardDevicesByGroupIdsAsync(model.CardDeviceRightGroupIds);
                //    if (!string.IsNullOrEmpty(newDevices?.FirstOrDefault()?.CardDevice?.ProcessorId))
                //    {
                //        // 边缘处理器  
                //        var processorIds = newDevices.Select(x => x.CardDevice).Select(x => x.ProcessorId).ToList();
                //        deviceIds.AddRange(processorIds);

                //        // 读卡器
                //        var cardDeviceIds = newDevices.Select(x => x.CardDevice.Id).ToList();
                //        deviceIds.AddRange(cardDeviceIds);
                //    }
                //}

                await _dao.DeleteAsync(entity);
                
                //分发数据 
                model = _mapper.Map<TurnstilePassRightModel>(entity);

                var turnstileDistributeModel = new TurnstilePassRightDistributeQueueModel();
                turnstileDistributeModel.DistributeType = PassRightDistributeTypeEnum.Delete.Value;
                //turnstileDistributeModel.DistributeType = PassRightDistributeTypeEnum.DeleteByIds.Value;
                //turnstileDistributeModel.Ids = deviceIds;
                turnstileDistributeModel.PassRight = model;
                _turnstilePassRightDistributeQueue.Add(turnstileDistributeModel);
                
            }
        }

        public async Task<TurnstilePassRightModel> AddOrEditAsync(TurnstilePassRightModel model)
        {
            model.RightType = RightTypeEnum.TURNSTILE.Value;
            //_logger.LogInformation($"data-edit:{JsonConvert.SerializeObject(model)}");
            //新增或修改数据
            var entity = await _dao.GetWithRightGroupsBySignAndAccessTypeAsync(model.Sign, model.AccessType, model.RightType);

            ////如果信息更改，所有设备都要下发,对于修改姓名未知，所有都下发
            //var oldDeivceIds = new List<string>();
            //if (entity?.RelationCardDeviceRightGroups?.FirstOrDefault() != null)
            //{
            //    //获取新增边缘处理器
            //    var oldRightGroupIds = entity.RelationCardDeviceRightGroups.Select(x => x.CardDeviceRightGroupId).ToList();
            //    var oldDevices = await _dao.RightGroupRelationCardDevicesByGroupIdsAsync(oldRightGroupIds);
            //    if (!string.IsNullOrEmpty(oldDevices?.FirstOrDefault()?.CardDevice?.Processor?.Id))
            //    {
            //        // 边缘处理器  
            //        var processorIds = oldDevices.Select(x => x.CardDevice).Select(x => x.Processor.Id).ToList();
            //        oldDeivceIds.AddRange(processorIds);

            //        // 读卡器
            //        var cardDeviceIds = oldDevices.Select(x => x.CardDevice.Id).ToList();
            //        oldDeivceIds.AddRange(cardDeviceIds);
            //    }
            //}

            //新增设备权限 
            var newDeviceIds = new List<string>();
            if (model.CardDeviceRightGroupIds?.FirstOrDefault() != null)
            {
                //获取新增设备，无法判断哪些权限组是增是改，所有权限组都下发
                var newDevices = await _dao.RightGroupRelationCardDevicesByGroupIdsAsync(model.CardDeviceRightGroupIds);
                if (!string.IsNullOrEmpty(newDevices?.FirstOrDefault()?.CardDevice?.ProcessorId))
                {
                    // 边缘处理器  
                    var processorIds = newDevices.Select(x => x.CardDevice).Select(x => x.ProcessorId).ToList();
                    newDeviceIds.AddRange(processorIds);

                    // 读卡器
                    var cardDeviceIds = newDevices.Select(x => x.CardDevice.Id).ToList();
                    newDeviceIds.AddRange(cardDeviceIds);
                }
            }


            if (entity == null)
            {
                entity = TurnstilePassRightModel.ToEntity(model);

                //关联权限组
                entity.RelationCardDeviceRightGroups = new List<PassRightRelationCardDeviceRightGroupEntity>();
                if (model.CardDeviceRightGroupIds?.FirstOrDefault() != null)
                {
                    foreach (var item in model.CardDeviceRightGroupIds)
                    {
                        var cardDeviceRightGroup = await _dao.SelectRelevanceObjectAsync<CardDeviceRightGroupEntity>(item);
                        if (cardDeviceRightGroup == null)
                        {
                            _logger.LogWarning($"权限关联权限组为空：cardDeviceRightGroupId:{item} ");
                            continue;
                        }

                        var relationCardDeviceRightGroup = new PassRightRelationCardDeviceRightGroupEntity();
                        relationCardDeviceRightGroup.Id = IdUtil.NewId();
                        relationCardDeviceRightGroup.CardDeviceRightGroup = cardDeviceRightGroup;
                        relationCardDeviceRightGroup.PassRight = entity;

                        entity.RelationCardDeviceRightGroups.Add(relationCardDeviceRightGroup);
                    }
                }
                /*2021--06-29*/
                //_logger.LogInformation($"dataadd:{JsonConvert.SerializeObject(entity)}");
                entity = await _dao.AddAsync(entity);
                model = _mapper.Map(entity, model);
            }
            else
            {
                model.Id = entity.Id;
                entity = TurnstilePassRightModel.SetEntity(entity, model, null);

                //关联权限组
                if (entity.RelationCardDeviceRightGroups == null)
                {
                    entity.RelationCardDeviceRightGroups = new List<PassRightRelationCardDeviceRightGroupEntity>();
                }
                if (model.CardDeviceRightGroupIds?.FirstOrDefault() == null)
                {
                    entity.RelationCardDeviceRightGroups.Clear();
                }
                else
                {
                    entity.RelationCardDeviceRightGroups.RemoveAll(x => !model.CardDeviceRightGroupIds.Contains(x.CardDeviceRightGroupId));

                    foreach (var item in model.CardDeviceRightGroupIds)
                    {
                        if (entity.RelationCardDeviceRightGroups.Any(x => x.CardDeviceRightGroupId == item))
                        {
                            continue;
                        }

                        var relationCardDeviceRightGroup = new PassRightRelationCardDeviceRightGroupEntity();
                        relationCardDeviceRightGroup.Id = IdUtil.NewId();

                        var cardDeviceRightGroup = await _dao.SelectRelevanceObjectAsync<CardDeviceRightGroupEntity>(item);
                        if (cardDeviceRightGroup == null)
                        {
                            _logger.LogWarning($"权限关联权限组为空：cardDeviceRightGroupId:{item} ");
                            continue;
                        }
                        relationCardDeviceRightGroup.CardDeviceRightGroup = cardDeviceRightGroup;
                        relationCardDeviceRightGroup.PassRight = entity;

                        entity.RelationCardDeviceRightGroups.Add(relationCardDeviceRightGroup);
                    }
                }
                /**/
                //_logger.LogInformation($"data-edit:{JsonConvert.SerializeObject(entity)}");
                entity = await _dao.EditAsync(entity);

                model = _mapper.Map(entity, model);
            }

            /*暂时取消分发数据 2021-06-28*/
            //分发数据 
            await DistributeRightAsync(newDeviceIds, model);

            return model;
        }
        /// <summary>
        /// 分发数据，闸机修改权限前会修改权限组，所以还是要删除所有权限不存在的设备
        /// </summary> 
        /// <param name="newDeviceIds"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task DistributeRightAsync(List<string> newDeviceIds, TurnstilePassRightModel model)
        {
            //多权限组只能删除当前权限存在的设备
            // _logger.LogInformation($"下发权限设备：newIds:{newDeviceIds.ToCommaString()} ");
            _logger.LogInformation("分发数据==>>");
            using var scope = _serviceScopeFactory.CreateScope();
            var cardDeviceDao = scope.ServiceProvider.GetRequiredService<ICardDeviceDao>();
            var faceInfoService = scope.ServiceProvider.GetRequiredService<IFaceInfoService>();
            var passRightDistribute = scope.ServiceProvider.GetRequiredService<ITurnstilePassRightDeviceDistributeService>();

            //要删除删除旧数据的设备,闸机修改权限前会修改权限组，没有权限的设备都要删除一遍
            var notDevices = await cardDeviceDao.GetByNotAsync(newDeviceIds);
            if (notDevices?.FirstOrDefault() != null)
            {
                var notDeviceIds = new List<string>();

                //边缘处理器
                var processorIds = notDevices?.Select(x => x.ProcessorId)?.ToList();
                notDeviceIds.AddRange(processorIds);

                //读卡器
                var cardDeviceIds = notDevices?.Select(x => x.Id)?.ToList();
                notDeviceIds.AddRange(cardDeviceIds);

                //边缘处理器可能重复
                var distributeNoDeviceIds = notDeviceIds.Where(x => !newDeviceIds.Contains(x)).ToList();

                //删除旧权限
               // _logger.LogInformation($"下发删除旧数据设备：ids:{distributeNoDeviceIds.ToCommaString()} ");

                var turnstileDistributeDeleteModel = new TurnstilePassRightDistributeQueueModel();
                turnstileDistributeDeleteModel.DistributeType = PassRightDistributeTypeEnum.DeleteByIds.Value;
                turnstileDistributeDeleteModel.Ids = distributeNoDeviceIds;
                turnstileDistributeDeleteModel.PassRight = model;
                _turnstilePassRightDistributeQueue.Add(turnstileDistributeDeleteModel);
                //Thread.Sleep(1000);
            }

            //分发数据后卡号会补0
            model.Sign = ConvertUtil.ToLong(model.Sign, 0).ToString();

            //_logger.LogInformation($"下发数据设备：ids:{newDeviceIds.ToCommaString()} data:{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");

            //分发数据 
            var turnstileDistributeModel = new TurnstilePassRightDistributeQueueModel();
            turnstileDistributeModel.DistributeType = PassRightDistributeTypeEnum.AddOrEditByIds.Value;
            turnstileDistributeModel.Ids = newDeviceIds;
            turnstileDistributeModel.PassRight = model;
            if (model.AccessType == AccessTypeEnum.FACE.Value)
            {
                _logger.LogInformation($"不下发人脸！");
                //分发数据  
                var face = await faceInfoService.GetByIdAsync(model.Sign);
                if (face == null)
                {
                    _logger.LogWarning($"找不到人脸信息：sign:{model.Sign} ");
                    return;
                }
                //人脸不下发
                turnstileDistributeModel.Face = face;
            }
            else
            {
                //分发数据 
                _logger.LogInformation($"下发IC卡！");
            }            
            _turnstilePassRightDistributeQueue.Add(turnstileDistributeModel);
        }

        public async Task<List<TurnstilePassRightModel>> GetAllAsync()
        {
            var entities = await _dao.GetByRightTypeAsync(RightTypeEnum.TURNSTILE.Value);

            var models = _mapper.Map<List<TurnstilePassRightModel>>(entities);

            return models;
        }

        public async Task<TurnstilePassRightModel> GetBySignAndAccessTypeAsync(string sign, string accessType)
        {
            //新增或修改数据
            var entity = await _dao.GetWithRightGroupsBySignAndAccessTypeAsync(sign, accessType, RightTypeEnum.TURNSTILE.Value);
            var model = _mapper.Map<TurnstilePassRightModel>(entity);
            return model;
        }

        public async Task<List<TurnstileUnitPassRightEntity>> GetUnitPageAsync(int page, int size)
        {
            var results = new List<TurnstileUnitPassRightEntity>();
            var entities = await _dao.GetPageWithRightGroupAsync(page, size, RightTypeEnum.TURNSTILE.Value);

            if (entities == null || entities.FirstOrDefault() == null)
            {
                return results;
            }

            foreach (var item in entities)
            {
                var result = new TurnstileUnitPassRightEntity();
                result.Id = item.Id;
                result.CardNumber = item.Sign;
                result.TimeStart = item.TimeNow;
                result.TimeEnd = item.TimeOut;
                result.EditedTime = item.EditedTime;

                results.Add(result);

                if (item.RelationCardDeviceRightGroups?.FirstOrDefault() == null)
                {
                    _logger.LogWarning($"通行权限关联通行权限组为空：passRight:{item.Id} ");
                    continue;
                }
                else
                {
                    foreach (var obj in item.RelationCardDeviceRightGroups)
                    {
                        var passRightDetail = new TurnstileUnitPassRightDetailEntity();
                        passRightDetail.Id = obj.Id;
                        passRightDetail.EditedTime = obj.EditedTime;

                        passRightDetail.RightGroup = new TurnstileUnitRightGroupEntity();
                        passRightDetail.RightGroup.Id = obj.CardDeviceRightGroup?.Id;
                        passRightDetail.RightGroupId = obj.CardDeviceRightGroup?.Id;

                        result.Details.Add(passRightDetail);
                    }
                }
            }

            return results;
        }

        public async Task DeleteBySignAsync(TurnstilePassRightModel model)
        {
            model.RightType = RightTypeEnum.TURNSTILE.Value;
            //_logger.LogInformation($"del data:{JsonConvert.SerializeObject(model)}");
            //新增或修改数据
            var entities = await _dao.GetBySignAsync(model.Sign, RightTypeEnum.TURNSTILE.Value);
            _logger.LogInformation($"del data:{JsonConvert.SerializeObject(entities)}");
            if (entities?.FirstOrDefault() != null)
            {
                foreach (var item in entities)
                {
                    await _dao.DeleteAsync(item);

                    //分发数据 2021-06-28 暂时取消
                    
                    model = _mapper.Map<TurnstilePassRightModel>(item);

                    var turnstileDistributeModel = new TurnstilePassRightDistributeQueueModel();
                    turnstileDistributeModel.DistributeType = PassRightDistributeTypeEnum.Delete.Value;
                    turnstileDistributeModel.PassRight = model;
                    _turnstilePassRightDistributeQueue.Add(turnstileDistributeModel);
                    
                }
            }
        }
    }
}
