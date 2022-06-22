using KT.Common.Core.Exceptions;
using KT.WinPak.SDK.V48.IServices;
using KT.WinPak.SDK.V48.Message;
using KT.WinPak.SDK.V48.Models;
using KT.WinPak.SDK.V48.Queries;
using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.V48.Services
{
    /// <summary>
    /// 门禁级别
    /// </summary>
    public class AccessLevelSdkService : IAccessLevelSdkService
    {
        private IAllSdkService _allSdkService;

        public AccessLevelSdkService(IAllSdkService allSdkService)
        {
            _allSdkService = allSdkService;
        }

        /// <summary>
        /// 获取访问级别
        /// </summary>
        /// <returns></returns>
        public List<AccessLevelModel> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 添加门禁级别
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AccessLevelModel Add(AccessLevelModel model)
        {
            //转换参数
            var entity = _allSdkService.GetAccessLevelClass();
            entity = AccessLevelModel.ToEntity(entity, model);
       
            //查询条件
            var query = new AddAccessLevelQuery();
            query.pAccesslevel = entity;

            //执行操作
            query = _allSdkService.AddAccessLevel(query);

            //返回结果
            if (query.pStatus != 0)
            {
                throw CustomException.Run("添加门禁级别失败：" + MessageEnum.GetMessageByCode(query.pStatus));
            }
            model.AccessLevelID = query.pAccesslevel.AccessLevelID;
            return model;
        }

        /// <summary>
        /// 修改门禁级别
        /// </summary>
        /// <param name="bstrAccl"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(string bstrAccl, AccessLevelModel model)
        {
            //转换参数
            var entity = _allSdkService.GetAccessLevelClass();
            entity = AccessLevelModel.ToEntity(entity, model);

            //查询条件
            var query = new EditAccessLevelQuery();
            query.bstrAccl = bstrAccl;
            query.pAccesslevel = entity;

            //执行操作
            query = _allSdkService.EditAccessLevel(query);

            //返回结果
            if (query.pStatus != 0)
            {
                throw CustomException.Run("修改门禁级别失败：" + MessageEnum.GetMessageByCode(query.pStatus));
            }
            return true;
        }

        /// <summary>
        /// 添加门禁级别
        /// </summary>
        /// <param name="bstrAcclName"></param>
        /// <returns></returns>
        public bool Delete(string bstrAcclName, string bstrAcctName)
        {
            //查询条件
            var query = new DeleteAccessLevelQuery();
            query.bstrAcclName = bstrAcclName;
            query.bstrAcctName = bstrAcctName;

            //执行操作
            query = _allSdkService.DeleteAccessLevel(query);
            if (query.pStatus != 0)
            {
                throw CustomException.Run("删除门禁级别失败：" + MessageEnum.GetMessageByCode(query.pStatus));
            }
            return true;
        }

        /// <summary>
        /// 根据名称获取门禁级别
        /// </summary>
        /// <param name="bstrAcclName"></param>
        /// <returns></returns>
        public AccessLevelModel GetByName(string bstrAcclName, string bstrAcctName)
        {
            //查询条件
            var query = new GetAccessLevelByNameQuery();
            query.bstrAcclName = bstrAcclName;
            query.bstrAcctName = bstrAcctName;

            //执行操作
            query = _allSdkService.GetAccessLevelByName(query);

            //返回结果
            if (query.VAccessLevel == null)
            {
                return null;
            }
            var entity = query.VAccessLevel;
            return AccessLevelModel.FromEntity(entity);
        }

        public bool Configure(ConfigureAccessLevelModel model)
        {
            //转换参数
            var readers = model.DeviceName.ToArray();

            //查询条件
            var query = new ConfigureAccessLevelQuery();
            query.bstrAcclName = model.AccessLevelName;
            query.bstrAcctName = model.AccountName;
            query.vReaders = readers;
            query.bstrTZName = model.TimeZoneName;

            //执行操作
            query = _allSdkService.ConfigureAccessLevel(query);

            //返回结果
            if (query.pStatus != 0)
            {
                throw CustomException.Run("门禁级别关联读卡器失败：" + MessageEnum.GetMessageByCode(query.pStatus));
            }
            return true;
        }

        public bool ConfigureEntranceAccess(ConfigureEntranceModel model)
        {
            //查询条件
            var query = new ConfigureEntranceAccessQuery();
            query.bstrAcclName = model.AccessLevelName;
            query.bstrAcctName = model.AccountName;
            query.bstrReaderName = model.DeviceName;
            query.bstrTZName = model.TimeZoneName;
            query.bstrGroupName = model.GroupName;

            //执行操作
            query = _allSdkService.ConfigureEntranceAccess(query);

            //返回结果
            if (query.pStatus != 0)
            {
                throw CustomException.Run("门禁级别关联读卡器失败：" + MessageEnum.GetMessageByCode(query.pStatus));
            }
            return true;
        }
    }
}
