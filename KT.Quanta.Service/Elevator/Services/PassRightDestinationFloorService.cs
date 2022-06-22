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
    public class PassRightDestinationFloorService : IPassRightDestinationFloorService
    {
        private IPassRightDestinationFloorDao _dao;
        private ILogger<PassRightDestinationFloorService> _logger;
        private readonly IMapper _mapper;
        private readonly IFloorDao _floorDao;
        public PassRightDestinationFloorService(ILogger<PassRightDestinationFloorService> logger,
            IPassRightDestinationFloorDao dao,
            IMapper mapper,
            IFloorDao floorDao)
        {
            _logger = logger;
            _dao = dao;
            _mapper = mapper;
            _floorDao = floorDao;
        }

        public async Task AddOrEditsAsync(List<PassRightDestinationFloorModel> models)
        {
            if (models?.FirstOrDefault() == null)
            {
                return;
            }

            foreach (var model in models)
            {
                var entity = await _dao.GetWithFloorAsync(model.Sign, model.ElevatorGroupId);
                if (entity != null)
                {

                    entity.Floor = await _floorDao.FindIdAsync(entity.FloorId);
                    if (entity.Floor == null)
                    {
                        throw new ArgumentNullException($"楼层不存：id[{entity.FloorId}]");
                    }

                    await _dao.UpdateAsync(entity, false);
                }
                else
                {
                    entity = _mapper.Map<PassRightDestinationFloorEntity>(model);

                    entity.Floor = await _floorDao.FindIdAsync(entity.FloorId);
                    if (entity.Floor == null)
                    {
                        throw new ArgumentNullException($"楼层不存：id[{entity.FloorId}]");
                    }
                }
                await _dao.InsertAsync(entity, false);
            }

            await _dao.SaveChangesAsync();
        }

        public async Task<List<PassRightDestinationFloorModel>> GetWithDetailBySignAsync(string sign)
        {
            var results = await _dao.GetWithDetailBySignAsync(sign);

            return _mapper.Map<List<PassRightDestinationFloorModel>>(results);
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
