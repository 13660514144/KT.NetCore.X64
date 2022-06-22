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
    public class HandleElevatorDeviceAuxiliaryService : IHandleElevatorDeviceAuxiliaryService
    {
        private IHandleElevatorDeviceAuxiliaryDao _dao;
        private ILogger<HandleElevatorDeviceAuxiliaryService> _logger;
        private readonly IMapper _mapper;

        public HandleElevatorDeviceAuxiliaryService(ILogger<HandleElevatorDeviceAuxiliaryService> logger,
            IHandleElevatorDeviceAuxiliaryDao dao,
            IMapper mapper)
        {
            _logger = logger;
            _dao = dao;
            _mapper = mapper;
        }

        public async Task AddOrEditsAsync(List<HandleElevatorDeviceAuxiliaryModel> models)
        {
            if (models?.FirstOrDefault() == null)
            {
                return;
            }
            foreach (var model in models)
            {
                var entity = await _dao.GetByHandleElevatorDeviceIdAsync(model.HandleElevatorDeviceId);
                if (entity != null)
                {
                    model.Id = entity.Id;
                    entity = _mapper.Map(model, entity);
                    await _dao.UpdateAsync(entity, true);
                }
                else
                {
                    entity = _mapper.Map<HandleElevatorDeviceAuxiliaryEntity>(model);

                    await _dao.InsertAsync(entity, true);
                }
            }
        }

        public async Task<List<HandleElevatorDeviceAuxiliaryModel>> GetAllAsync()
        {
            var entities = await _dao.SelectAllAsync();
            return _mapper.Map<List<HandleElevatorDeviceAuxiliaryModel>>(entities);
        }
    }
}
