using KT.Common.Core.Exceptions;
using KT.WinPak.SDK.IServices;
using KT.WinPak.SDK.Message;
using KT.WinPak.SDK.Models;
using KT.WinPak.SDK.Queries;
using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.Services
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
            List<AccessLevelModel> results = new List<AccessLevelModel>();

            //查询所有账户 
            var getAccountsQuery = new GetAccountsQuery();
            getAccountsQuery = _allSdkService.GetAccounts(getAccountsQuery);

            //账户为空
            if (getAccountsQuery.vAccounts == null)
            {
                return results;
            }

            //转换账户信息
            object[] accounts = (object[])getAccountsQuery.vAccounts;
            if (accounts == null || accounts.Length <= 0)
            {
                return results;
            }

            //遍历账户查询数据
            foreach (var item in accounts)
            {
                IAccount account = (IAccount)item;

                //查询条件
                var query = new GetAccessLevelsByAccountNameQuery();
                query.bstrAccountName = account.AccountName;

                //执行查询
                query = _allSdkService.GetAccessLevelsByAccountName(query);

                //查询结果为空
                if (query.vAccessLevels == null)
                {
                    continue;
                }

                //转换结果数据 
                if (query.vAccessLevels == null || query.vAccessLevels.FirstOrDefault() == null)
                {
                    continue;
                }

                //处理返回数据
                foreach (var obj in query.vAccessLevels)
                { 
                    //过滤重复数据
                    if (results.FirstOrDefault(x => x.AccessLevelID == obj.AccessLevelID) != null)
                    {
                        continue;
                    }
                    var result = AccessLevelModel.FromEntity(obj);
                    result.AccountName = account.AccountName;
                    results.Add(result);
                }
            }

            return results;
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
        public bool Delete(string bstrAcclName)
        {
            //查询条件
            var query = new DeleteAccessLevelQuery();
            query.bstrAcclName = bstrAcclName;

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
        public AccessLevelModel GetByName(string bstrAcclName)
        {
            //查询条件
            var query = new GetAccessLevelByNameQuery();
            query.bstrAcclName = bstrAcclName;

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
