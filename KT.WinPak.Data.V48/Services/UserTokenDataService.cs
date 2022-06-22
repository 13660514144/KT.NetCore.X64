using KT.WinPak.Data.IDaos;
using KT.WinPak.Data.IServices;
using KT.WinPak.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.Data.Services
{
    public class UserTokenDataService : IUserTokenDataService
    {
        private IUserTokenDataDao dao;

        public UserTokenDataService(IUserTokenDataDao dao)
        {
            this.dao = dao;
        }

        public UserTokenModel AddOrUpdateByUser(UserTokenModel userToken)
        {
            var result = dao.GetByUserId(userToken.LoginUser?.Id);
            if(result != null)
            {
                return Update(userToken);
            }
                return Insert(userToken);
            
        }

        public UserTokenModel Update(UserTokenModel userToken)
        {
            return dao.Update(userToken);
        }


        /// <summary>
        /// 获取登录用户Token数据
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public UserTokenModel GetByToken(string token)
        {
            return dao.GetByToken(token);
        }

        public UserTokenModel Insert(UserTokenModel userToken)
        {
            return dao.Insert(userToken);
        }
    }
}
