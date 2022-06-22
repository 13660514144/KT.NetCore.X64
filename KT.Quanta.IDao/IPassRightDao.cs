using KT.Common.Data.Daos;
using KT.Common.Data.Models;
using KT.Quanta.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IDaos
{
    public interface IPassRightDao : IBaseDataDao<PassRightEntity>
    {
        Task<List<PassRightEntity>> GetWithFloorsByRightTypeAsync(string rightType);
        Task<PassRightEntity> GetWithFloorsByIdAsync(string id);
        Task<PassRightEntity> AddAsync(PassRightEntity entity);
        Task<PassRightEntity> EditAsync(PassRightEntity entity);
        Task RemoveAsync(PassRightEntity entity);
        Task<List<PassRightEntity>> DeleteBySignAsync(string sign, string rightType);
        Task<List<PassRightEntity>> GetPageWithFloorAsync(int page, int size, string rightType);
        Task<PassRightEntity> GetWithFloorAndFloorsBySignAndAccessTypeAsync(string sign, string accessType, string rightType);
        Task<PassRightEntity> GetWithFloorsBySignAsync(string sign, string rightType);
        Task<List<PassRightEntity>> DeleteReturnPersonRightsBySignAsync(string sign, string rightType);
        Task<PassRightEntity> GetWithPersonRrightsBySignAsync(string sign);
        Task<List<PassRightEntity>> GetPageWithRightGroupAsync(int page, int size, string rightType);
        Task<List<PassRightEntity>> GetBySignAsync(string sign, string rightType);
        Task<List<CardDeviceRightGroupRelationCardDeviceEntity>> RightGroupRelationCardDevicesByGroupIdsAsync(List<string> cardDeviceRightGroupIds);
        Task<List<PassRightEntity>> GetWithPersonByLamdbalAsync(string personId, string rightType);
        Task<PassRightEntity> GetWithPersonBySignAsync(string sign);
        Task<List<PassRightEntity>> GetAllWithPersonBySignAsync(string sign);
        Task<PassRightEntity> GetWithRightGroupsBySignAndAccessTypeAsync(string sign, string accessType, string rightType);
        Task<List<PassRightEntity>> GetByRightTypeAsync(string rightType);
        //Task<List<PassRightEntity>> GetWithCardDeviceByRightTypeAndDeviceIdsAsync(string rightType, List<string> deviceIds);
        Task<bool> IsExistsByIdAndCardDeviceIdAsync(string id, string cardDeviceId);
        Task<bool> IsExistsByIdAndProcessorIdAsync(string id, string processorId);
        Task<bool> IsExistsByIdAndCardDeviceIdExcludeRightGroupIdAsync(string id, string addCardDeviceId, string excludeRightGroupId);
        Task<bool> IsExistsByIdAndProcessorIdExcludeRightGroupIdAsync(string id, string processorId, string excludeRightGroupId);
        Task<PageData<PassRightEntity>> GetPageWithFloorsByRightTypeAsync(int page, int size, string rightType, string name, string sign);
        Task<PassRightEntity> GetWithPersonByLambdaAsync(string passRightSign, string accessType);
    }
}
