using AutoMapper;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Turnstile.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
{
    public class TurnstilePersonService : ITurnstilePersonService
    {
        private IPersonDao _dao;
        private ITurnstilePassRightDeviceDistributeService _passRightDistribute;
        private readonly TurnstilePassRightDistributeQueue _turnstilePassRightDistributeQueue;
        private readonly IMapper _mapper;

        public TurnstilePersonService(IPersonDao dao,
            ITurnstilePassRightDeviceDistributeService passRightDistribute,
            TurnstilePassRightDistributeQueue turnstilePassRightDistributeQueue,
            IMapper mapper)
        {
            _dao = dao;
            _passRightDistribute = passRightDistribute;
            _turnstilePassRightDistributeQueue = turnstilePassRightDistributeQueue;
            _mapper = mapper;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _dao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            //删除权限
            var passRights = await _dao.DeleteRelevenceObjectsByLambdaAsync<PassRightEntity>(x => x.Person.Id == id && x.RightType == RightTypeEnum.TURNSTILE.Value);

            var isExistsRight = await _dao.HasRelevanceInstanceAsync<PassRightEntity>(x => x.Person.Id == id && x.RightType != RightTypeEnum.TURNSTILE.Value);
            if (!isExistsRight)
            {
                await _dao.DeleteByIdAsync(id);
            }
            /*
            //分发数据 
            if (passRights.FirstOrDefault() != null)
            {
                foreach (var item in passRights)
                {
                    var model = _mapper.Map<TurnstilePassRightModel>(item);
                    
                    var turnstileDistributeModel = new TurnstilePassRightDistributeQueueModel();
                    turnstileDistributeModel.DistributeType = PassRightDistributeTypeEnum.Delete.Value;
                    turnstileDistributeModel.PassRight = model;
                    _turnstilePassRightDistributeQueue.Add(turnstileDistributeModel);
                    
                }
            }
            */
            ////分发数据
            //DistributeDelete(passRights);
        }

        //private async Task DistributeDelete(List<PassRightEntity> passRights)
        //{
        //    ////分发数据
        //    //var facePassRights = passRights.Where(x => x.AccessType == AccessTypeEnum.FACE.Value );
        //    //foreach (var item in facePassRights)
        //    //{
        //    //    var model = TurnstilePassRightModel.ToModel(item);
        //    //    await _passRightDistribute.DeleteAsync(model);
        //    //}

        //    //分发数据
        //    var otherPassRights = passRights.Where(x => x.AccessType != AccessTypeEnum.FACE.Value  );
        //    foreach (var item in otherPassRights)
        //    {
        //        var model = TurnstilePassRightModel.ToModel(item);
        //        await _passRightDistribute.DeleteAsync(model);
        //    }

        //}

        public async Task<TurnstilePersonModel> AddOrEditAsync(TurnstilePersonModel model)
        {
           
                var entity = await _dao.SelectByIdAsync(model.Id);
                if (entity == null)
                {
                    entity = TurnstilePersonModel.ToEntity(model);
                    await _dao.InsertAsync(entity);
                }
                else
                {
                    entity = TurnstilePersonModel.SetEntity(entity, model);
                    await _dao.AttachAsync(entity);
                }
            
            model = TurnstilePersonModel.SetModel(model, entity);

            return model;
        }

        public async Task<List<TurnstilePersonModel>> GetAllAsync()
        {
            var entities = await _dao.SelectAllAsync();

            var models = TurnstilePersonModel.ToModels(entities);

            return models;
        }

        public async Task<TurnstilePersonModel> GetByIdAsync(string id)
        {
            var entity = await _dao.SelectByIdAsync(id);
            var model = TurnstilePersonModel.ToModel(entity);
            return model;
        }
    }
}
