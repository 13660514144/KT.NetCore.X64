using KT.Prowatch.Service.Models;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace KT.Prowatch.Service.Helpers
{
    /// <summary>
    /// 用户登录
    /// </summary>
    public class LoginHelper
    {
        private static LoginHelper instace;
        /// <summary>
        /// 用户登录实例
        /// </summary>
        public static LoginHelper Instance
        {
            get
            {
                if (instace == null)
                {
                    lock (locker)
                    {
                        if (instace == null)
                        {
                            instace = new LoginHelper();
                        }
                    }
                }
                return instace;
            }
        }


        private static object locker = new object();

        private LoginHelper()
        {
            _tokenDatas = new ConcurrentDictionary<string, UserTokenModel>();
        }

        /// <summary>
        /// 当前初始化的数据库与服务器
        /// </summary>
        public LoginUserModel CurrentConnect;


        /// <summary>
        /// 添加登录用户，用户登录完成后添加
        /// </summary>
        /// <param name="tokenData"></param>
        public void LoginAdd(UserTokenModel tokenData)
        {
            CurrentConnect = tokenData?.LoginUser;
            _tokenDatas.AddOrUpdate(tokenData.Token, tokenData, UpdateValueFactory(tokenData));
        }

        private Func<string, UserTokenModel, UserTokenModel> UpdateValueFactory(UserTokenModel data)
        {
            return (key, oldData) => data;
        }

        private ConcurrentDictionary<string, UserTokenModel> _tokenDatas;
        public ConcurrentDictionary<string, UserTokenModel> TokenDatas
        {
            get
            {
                return _tokenDatas;
            }

            set
            {
                _tokenDatas = value;
            }
        }
    }
}