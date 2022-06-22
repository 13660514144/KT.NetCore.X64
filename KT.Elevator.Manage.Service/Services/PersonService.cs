using KT.Elevator.Manage.Service.IDaos;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Services
{
    public class PersonService : IPersonService
    {
        private IPersonDao _dao;

        public PersonService(IPersonDao dao)
        {
            _dao = dao;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _dao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            await _dao.DeleteByIdAsync(id);
        }

        public async Task<PersonModel> AddOrEditAsync(PersonModel model)
        {
            var entity = await _dao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = PersonModel.ToEntity(model);
                await _dao.InsertAsync(entity);
            }
            else
            {
                entity = PersonModel.SetEntity(entity, model);
                await _dao.AttachUpdateAsync(entity);
            }

            model = PersonModel.SetModel(model, entity);

            return model;
        }

        public async Task<List<PersonModel>> GetAllAsync()
        {
            var entities = await _dao.SelectAllAsync();

            var models = PersonModel.ToModels(entities);

            return models;
        }

        public async Task<PersonModel> GetByIdAsync(string id)
        {
            var entity = await _dao.SelectByIdAsync(id);
            var model = PersonModel.ToModel(entity);
            return model;
        }
    }
}
