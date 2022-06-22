using KT.Common.Core.Utils;
using KT.Common.Data.Models;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.Utils;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Secondary.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Secondary.ClientApp.Service.Helpers;
using KT.Elevator.Unit.Secondary.ClientApp.Service.IServices;
using KT.Quanta.Common.Enums;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.Services
{
    public class PassRightService : IPassRightService
    {
        private FaceFactoryProxy _faceFactoryProxy;

        private IPassRightDao _dao;
        private ILogger _logger;
        private HubHelper _hubHelper;

        public PassRightService(IPassRightDao dao,
            ILogger logger,
            HubHelper hubHelper)
        {
            _dao = dao;
            _logger = logger;
            _hubHelper = hubHelper;
        }

        public async Task<List<UnitPassRightEntity>> AddOrUpdateAsync(List<UnitPassRightEntity> entities)
        {
            var results = new List<UnitPassRightEntity>();
            if (entities == null)
            {
                return results;
            }
            foreach (var entity in entities)
            {
                try
                {
                    var newEntity = await AddOrUpdateAsync(entity);
                    if (newEntity != null)
                    {
                        results.Add(newEntity);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("新增或修改权限错误：ex:{0} ", ex);
                }
            }
            return results;
        }

        public async Task<UnitPassRightEntity> AddOrUpdateAsync(UnitPassRightEntity entity)
        {
            //检查卡号是否存在
            var oldSignEntity = await _dao.GetByIdAndSignAsync(entity.Id, entity.Sign, entity.AccessType);
            if (oldSignEntity != null)
            {
                _logger.LogError($"相同的卡号已经存在：{JsonConvert.SerializeObject(entity, JsonUtil.JsonPrintSettings)} ");
                await _dao.DeleteAsync(oldSignEntity);
            }

            //人脸识别获取人脸特征
            if (entity.AccessType == AccessTypeEnum.FACE.Value
                && entity.Feature != null
                && entity.Feature.Length > 0)
            {
                //提取人脸特征
                System.Drawing.Image image = ImageConvert.BytesToImage(entity.Feature);

                //动态增加，否则会出现循环引用
                if (_faceFactoryProxy == null)
                {
                    _faceFactoryProxy = ContainerHelper.Resolve<FaceFactoryProxy>();
                }

                entity.Feature = _faceFactoryProxy.GetFaceFeatureBytes(image);
                if (entity.Feature == null || entity.Feature.Length == 0)
                {
                    _logger.LogWarning($"获取人脸特征为空但继续保存：sign:{entity.Sign} ");
                    entity.FeatureSize = 0;
                }
                else
                {
                    entity.FeatureSize = entity.Feature.Length;
                }
            }

            var oldEntity = await _dao.GetWithDetailsByIdAsync(entity.Id);
            if (oldEntity != null)
            {
                oldEntity.SetValues(entity);

                await _dao.EditAsync(oldEntity);

                return oldEntity;
            }
            else
            {
                await _dao.AddAsync(entity);

                return entity;
            }

        }

        public async Task Delete(string id, long editTime)
        {
            var oldEntity = await _dao.SelectByIdAsync(id);
            if (oldEntity != null)
            {
                if (oldEntity.EditedTime > editTime)
                {
                    _logger.LogWarning($"数据未更改：oldEditedTime:{oldEntity.EditedTime} EditedTime:{editTime} ");
                    return;
                }
                await _dao.DeleteAsync(oldEntity);
            }
        }

        public async Task<List<UnitPassRightEntity>> GetByPageAsync(string type, int page, int size)
        {
            return await _dao.SelectPageByLambdaAsync(x => x.AccessType == type, page, size);
        }

        public async Task<List<UnitPassRightEntity>> GetBySignsAsync(List<string> signs, string accessType)
        {
            return await _dao.GetBySignsAsync(signs, accessType);
        }
        public async Task<UnitPassRightEntity> GetBySignAsync(string sign, string accessType)
        {
            return await _dao.GetBySignAsync(sign, accessType);
        }
        public async Task<PageData<UnitPassRightEntity>> GetPageWithDetailAsync(int page, int size)
        {
            return await _dao.GetPageWithDetailsAsync(page, size);
        }

        public async Task Deletesync(UnitPassRightEntity passRight)
        {
            await _dao.DeleteAsync(passRight, false);

            await _dao.SaveChangesAsync();
        }
    }
}
