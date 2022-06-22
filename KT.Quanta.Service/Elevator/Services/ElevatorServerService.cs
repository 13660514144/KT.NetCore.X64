using AutoMapper;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
{
    public class ElevatorServerService : IElevatorServerService
    {
        private IElevatorServerDao _dao;
        private readonly IMapper _mapper;

        public ElevatorServerService(IElevatorServerDao dao,
            IMapper mapper)
        {
            _dao = dao;
            _mapper = mapper;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _dao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            await _dao.DeleteByIdAsync(id);
        }

        public async Task<ElevatorServerModel> AddOrEditAsync(ElevatorServerModel model)
        {
            var entity = await _dao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = ElevatorServerModel.ToEntity(model);
                await _dao.AddAsync(entity);
            }
            else
            {
                entity = ElevatorServerModel.SetEntity(entity, model);
                await _dao.EditAsync(entity);
            }

            model = _mapper.Map(entity, model);

            return model;
        }

        public async Task<List<ElevatorServerModel>> GetAllAsync()
        {
            var entities = await _dao.GetAllAsync();

            var models = _mapper.Map<List<ElevatorServerModel>>(entities);

            return models;
        }

        public async Task<ElevatorServerModel> GetByIdAsync(string id)
        {
            var entity = await _dao.GetByIdAsync(id);
            var model = _mapper.Map<ElevatorServerModel>(entity);
            return model;
        }
    }
}
