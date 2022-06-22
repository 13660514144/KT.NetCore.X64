using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KT.Quanta.Service.Turnstile.Dtos;
using KT.Quanta.Service.Devices.Common;
using System;
using System.IO;
using KT.Common.Core.Utils;
using KT.Common.Data.Models;
using KT.Quanta.Model.Elevator.Dtos;
using KT.Quanta.Entity.Entities;
using AutoMapper;

namespace KT.Quanta.Service.Services
{
    public class PassRightAccessibleFloorService : IPassRightAccessibleFloorService
    {
        private IPassRightAccessibleFloorDao _dao;
        private ILogger<PassRightAccessibleFloorService> _logger;
        private readonly IMapper _mapper;
        private readonly IFloorDao _floorDao;
        public PassRightAccessibleFloorService(ILogger<PassRightAccessibleFloorService> logger,
            IPassRightAccessibleFloorDao dao,
            IMapper mapper,
            IFloorDao floorDao)
        {
            _logger = logger;
            _dao = dao;
            _mapper = mapper;
            _floorDao = floorDao;
        }

        public async Task AddOrEditsAsync(List<PassRightAccessibleFloorModel> models)
        {
            if (models?.FirstOrDefault() == null)
            {
                return;
            }

            foreach (var model in models)
            {
                var entity = await _dao.GetWithDetailBySignAndElevatorGroupIdAsync(model.Sign, model.ElevatorGroupId);
                if (entity != null)
                {
                    if (entity.PassRightAccessibleFloorDetails?.FirstOrDefault() == null)
                    {
                        entity.PassRightAccessibleFloorDetails = new List<PassRightAccessibleFloorDetailEntity>();
                    }
                    
                    if (model.PassRightAccessibleFloorDetails?.FirstOrDefault() != null)
                    {
                        foreach (var detailModel in model.PassRightAccessibleFloorDetails)
                        {
                            var detailEntity = entity.PassRightAccessibleFloorDetails.FirstOrDefault(x => x.FloorId == detailModel.FloorId); 
                            if (detailEntity == null)
                            {
                                detailEntity = _mapper.Map<PassRightAccessibleFloorDetailEntity>(detailModel);
                                if (string.IsNullOrEmpty(detailEntity.Id))
                                {
                                    detailEntity.Id = IdUtil.NewId();
                                }
                            }
                            else
                            {
                                detailModel.Id = detailEntity.Id;
                                detailEntity = _mapper.Map(detailModel, detailEntity);
                            }

                            detailEntity.Floor = await _floorDao.FindIdAsync(detailEntity.FloorId);
                            if (detailEntity.Floor == null)
                            {
                                throw new ArgumentNullException($"楼层不存：id[{detailEntity.FloorId}]");
                            }

                            entity.PassRightAccessibleFloorDetails.Add(detailEntity);
                        }
                    }
                    await _dao.UpdateAsync(entity, false);
                }
                else
                {
                    entity = _mapper.Map<PassRightAccessibleFloorEntity>(model);
                    if (entity.PassRightAccessibleFloorDetails?.FirstOrDefault() != null)
                    {
                        foreach (var item in entity.PassRightAccessibleFloorDetails)
                        {
                            if (string.IsNullOrEmpty(item.Id))
                            {
                                item.Id = IdUtil.NewId();
                            }
                            item.Floor = await _floorDao.FindIdAsync(item.FloorId);
                            if (item.Floor == null)
                            {
                                throw new ArgumentNullException($"楼层不存：id[{item.FloorId}]");
                            }
                        }
                    }
                    await _dao.InsertAsync(entity, false);
                }
            }

            await _dao.SaveChangesAsync();
        }

        public async Task<List<PassRightAccessibleFloorModel>> GetWithDetailBySignAsync(string sign)
        {
            var results = await _dao.GetWithDetailBySignAsync(sign);

            return _mapper.Map<List<PassRightAccessibleFloorModel>>(results);
        }

        public async Task DeleteBySignAsync(string sign)
        {
            await _dao.DeleteByLambdaAsync(x => x.Sign == sign);
        }

        public async Task DeleteBySignAndElevatorGroupIdAsync(string sign, string handleElevatorDeviceId)
        {
            await _dao.DeleteByLambdaAsync(x => x.Sign == sign && x.ElevatorGroupId == handleElevatorDeviceId);
        }
    }
}
