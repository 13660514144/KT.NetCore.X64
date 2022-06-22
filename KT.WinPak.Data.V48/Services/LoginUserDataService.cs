using KT.WinPak.Data.Daos;
using KT.WinPak.Data.IDaos;
using KT.WinPak.Data.IServices;
using KT.WinPak.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.Data.Services
{
    public class LoginUserDataService : ILoginUserDataService
    {
        private ILoginUserDataDao dao;

        public LoginUserDataService(ILoginUserDataDao dao)
        {
            this.dao = dao;
        }

        public LoginUserModel AddOrUpdate(LoginUserModel model)
        {
            var newModel = dao.SelectByData(model);
            if (newModel != null)
            {
                return newModel;
            }
            else
            {
                model = dao.InsertAsync(model);
                return model;
            }
        }

        /// <summary>
        /// 获取最近一条记录
        /// </summary>
        /// <returns></returns>
        public LoginUserModel GetLast()
        {
            return dao.SelectLast();
        }
    }
}
