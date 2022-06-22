using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace KT.Quanta.Service.Services
{
    public class PersonService : IPersonService
    {
        private IPersonDao _dao;
        private IPassRightDao _passRightDao;
        private IElevatorPassRightDeviceDistributeService _passRightDistribute;
        private ElevatorPassRightDistributeQueue _elevatorPassRightDistributeQueue;        
        public PersonService(IPersonDao dao,
            IElevatorPassRightDeviceDistributeService passRightDistribute,
            ElevatorPassRightDistributeQueue elevatorPassRightDistributeQueue,
            IPassRightDao passRightDao)
        {
            _dao = dao;
            _passRightDistribute = passRightDistribute;
            _elevatorPassRightDistributeQueue = elevatorPassRightDistributeQueue;
            _passRightDao = passRightDao;            
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _dao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            //删除权限
            var passRights = await _passRightDao.GetWithPersonByLamdbalAsync(id, RightTypeEnum.ELEVATOR.Value);

            var isExistsRight = await _dao.HasRelevanceInstanceAsync<PassRightEntity>(x => x.Person.Id == id
                                     && x.RightType != RightTypeEnum.ELEVATOR.Value);
            if (!isExistsRight)
            {
                await _dao.DeleteByIdAsync(id);
            }
            /*
            if (passRights?.FirstOrDefault() != null)
            {
                foreach (var item in passRights)
                {
                    var model = PassRightModel.ToModel(item);

                    var distributeModel = new ElevatorPassRightDistributeQueueModel();
                    distributeModel.DistributeType = PassRightDistributeTypeEnum.Delete.Value;
                    distributeModel.PassRight = model;
                    _elevatorPassRightDistributeQueue.Add(distributeModel);
                }
            }
            */
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
                await _dao.AttachAsync(entity);
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

        public async Task<PersonModel> GetWithRights(string id)
        {
            var entity = await _dao.GetWithPassRightsByAsync(id);
            var model = PersonModel.ToModel(entity);
            return model;
        }
    }
}
