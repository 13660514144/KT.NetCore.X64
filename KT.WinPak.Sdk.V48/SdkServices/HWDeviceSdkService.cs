using KT.WinPak.SDK.V48.IServices;
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
    /// 设备
    /// </summary>
    public class HWDeviceSdkService : IHWDeviceSdkService
    {
        private IAllSdkService _allSdkService;

        public HWDeviceSdkService(IAllSdkService allSdkService)
        {
            _allSdkService = allSdkService;
        }
        /// <summary>
        /// 获取设置列表
        /// </summary>
        /// <returns></returns>
        public List<HWDeviceModel> GetAll()
        {
            List<HWDeviceModel> results = new List<HWDeviceModel>();

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
                var query = new GetADVDetailsByAccountNameQuery();
                query.bstrAcctName = account.AccountName;

                //执行查询
                query = _allSdkService.GetADVDetailsByAccountName(query);

                //查询结果为空
                if (query.vDevices == null)
                {
                    continue;
                }

                //转换结果数据 
                if (query.vDevices == null || query.vDevices.FirstOrDefault() == null)
                {
                    continue;
                }

                //处理返回数据
                foreach (var obj in query.vDevices)
                {
                    //去除重复数据
                    if (results.FirstOrDefault(x => x.DeviceID == obj.DeviceID) != null)
                    {
                        continue;
                    }

                    var result = HWDeviceModel.FromEntity(obj);
                    result.AccountID = account.AccountID;
                    results.Add(result);
                }
            }

            return results;
        }

        /// <summary>
        /// 获取所有卡
        /// </summary>
        /// <returns></returns>
        public List<HWDeviceModel> GetAllReaders()
        {
            List<HWDeviceModel> results = new List<HWDeviceModel>();

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
                var query = new GetReadersByAccountNameQuery();
                query.bstrAcctName = account.AccountName;

                //执行查询
                query = _allSdkService.GetReadersByAccountName(query);

                //查询结果为空
                if (query.vReaders == null)
                {
                    continue;
                }

                //转换结果数据 
                if (query.vReaders == null || query.vReaders.FirstOrDefault() == null)
                {
                    continue;
                }

                //处理返回数据
                foreach (var obj in query.vReaders)
                {
                    //去除重复数据
                    if (results.FirstOrDefault(x => x.DeviceID == obj.DeviceID) != null)
                    {
                        continue;
                    }

                    var result = HWDeviceModel.FromEntity(obj);
                    result.AccountID = account.AccountID;
                    results.Add(result);
                }
            }

            return results;
        }
    }
}
