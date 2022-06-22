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
    public class TimeZoneSdkService : ITimeZoneSdkService
    {
        private IAllSdkService _allSdkService;

        public TimeZoneSdkService(IAllSdkService allSdkService)
        {
            _allSdkService = allSdkService;
        }

        /// <summary>
        /// 获取所有时区
        /// </summary>
        /// <returns></returns>
        public List<TimeZoneModel> GetAll()
        {
            List<TimeZoneModel> results = new List<TimeZoneModel>();

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
                var query = new GetTimeZonesByAccountNameQuery();
                query.bstrAcctName = account.AccountName;

                //执行查询
                query = _allSdkService.GetTimeZonesByAccountName(query);

                //查询结果为空
                if (query.vTimezones == null)
                {
                    continue;
                }

                //转换结果数据 
                if (query.vTimezones == null || query.vTimezones.FirstOrDefault() == null)
                {
                    continue;
                }

                //处理返回数据
                foreach (var obj in query.vTimezones)
                {
                    //去除重复
                    if (results.FirstOrDefault(x => x.TimeZoneID == obj.TimeZoneID) != null)
                    {
                        continue;
                    }
                    var result = TimeZoneModel.FromEntity(obj);
                    result.AccountName = account.AccountName;
                    results.Add(result);
                }
            }

            return results;
        }
    }
}
