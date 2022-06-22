using KT.Common.Core.Exceptions;
using KT.Common.Data.Models;
using KT.Common.WebApi.HttpModel;
using KT.WinPak.Data.V48.Models;
using KT.WinPak.SDK.V48;
using KT.WinPak.SDK.V48.Entities.Part;
using KT.WinPak.SDK.V48.IServices;
using KT.WinPak.SDK.V48.Models;
using KT.WinPak.SDK.V48.Queries;
using KT.WinPak.SDK.V48.Settings;
using KT.WinPak.Service.V48.IServices;
using KT.WinPak.Service.V48.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KT.WinPak.Service.V48.Services
{
    /// <summary>
    /// 用户操作
    /// </summary>
    public class UserService : IUserService
    {
        //注入
        private ILogger<IUserService> _logger;
        private IAllSdkService _allSdkService;
        private AccwEventSdk _accwEventSdk;
        private ILoginUserDataService _userDataService;
        private IUserTokenDataService _tokenDataService;
        private AppSettings _appSettings;

        public UserService(ILogger<IUserService> logger,
            IAllSdkService allSdkService,
            AccwEventSdk accwEventSdk,
            ILoginUserDataService userDataService,
            IUserTokenDataService tokenDataService,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _allSdkService = allSdkService;
            _accwEventSdk = accwEventSdk;
            _userDataService = userDataService;
            _tokenDataService = tokenDataService;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// 登录WinPak数据库COM
        /// </summary>
        /// <returns>用户ID</returns>
        private int LoginNci(LoginUserModel user, bool isLoginAccwEvent = true)
        {
            //较验
            if (string.IsNullOrEmpty(user.PCUser) || string.IsNullOrEmpty(user.PCPassword))
            {
                throw CustomException.Run("账号或密码不能为空！");
            }

            //查询条件
            var query = new LoginQuery();
            query.bstrUserName = user.PCUser;
            query.bstrPassword = user.PCPassword;
            query.bstrDomainName = string.Empty;

            //执行操作
            query = _allSdkService.Login(query);
            if (query.plUserID <= 0)
            {
                throw CustomException.Run("登录失败：account:{0} ", user.PCUser);
            }

            //登录通信
            if (isLoginAccwEvent)
            {
                _accwEventSdk.Login(user.PCUser, user.PCPassword);
            }

            //系统销毁时退出登录
            MasterInfo.Logout = new Action(() =>
            {
                var query = new LogoutQuery();
                query = _allSdkService.Logout(query);
                _accwEventSdk.Logout();
            });

            return query.plUserID;
        }

        /// <summary>
        /// 登录，初始化登录
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public async Task<TokenResponse> LoginAsync(LoginUserModel loginUser)
        {
            //登录Com组件
            var userId = LoginNci(loginUser);

            //登录成功存储本地
            loginUser = await _userDataService.AddOrUpdateAsync(loginUser);
            loginUser.UserId = userId;

            //保存token数据             
            UserTokenModel userToken = new UserTokenModel();
            userToken.LoginUserId = loginUser.Id;
            userToken.LoginUser = loginUser;
            await _tokenDataService.AddAsync(userToken);
            //向token队列中增加数据
            MasterInfo.TokenDatas.AddOrUpdate(userToken.Token, userToken, UpdateTokenDataFactory);

            //设置登录信息
            SetLoginUser(loginUser);

            //返回Token数据
            return new TokenResponse(userToken.Token, userToken.TimeNow, userToken.TimeOut);
        }

        /// <summary>
        /// 登录App，已存在的用户登录
        /// </summary>
        /// <param name="userToken"></param>
        /// <param name="isLoginAccwEvent">是否登录数据上传</param>
        /// <returns></returns>
        public bool LoginApp(LoginUserModel loginUser, bool isLoginAccwEvent = true)
        {
            //退出App登录 
            var query = new LogoutQuery();
            query = _allSdkService.Logout(query);

            //登录数据库
            LoginNci(loginUser, isLoginAccwEvent);

            //设置登录信息
            SetLoginUser(loginUser);

            return true;
        }

        /// <summary>
        /// 设置登录信息
        /// </summary>
        /// <param name="userToken"></param>
        private void SetLoginUser(LoginUserModel loginUser)
        {
            //当前用户加入缓存队列 
            MasterInfo.LoginUsers.AddOrUpdate(loginUser.Id, loginUser, UpdateLoginUserFactory);
            //设置当前登录用户
            MasterInfo.LoginUser = loginUser;
            //数据存储层当前登录用户
            DataStaticInfo.CurrentUserId = loginUser.Id;
            //Sdk上传地址
            SdkStaticInfo.PushUrl = loginUser.ServerAddress;
            //winpak数据库连接地址
            loginUser.DBName = string.IsNullOrEmpty(loginUser.DBName) ? _appSettings.DBName : loginUser.DBName;
            SqlConnectHelper.ConnectString = $"Data Source={loginUser.DBAddr};Initial Catalog={loginUser.DBName};User ID={loginUser.DBUser};Password={loginUser.DBPassword}";
        }

        private LoginUserModel UpdateLoginUserFactory(string arg1, LoginUserModel arg2)
        {
            return arg2;
        }

        /// <summary>
        /// 根据最新初始化数据登录
        /// </summary>
        public void Login(bool isLoginAccwEvent = true)
        {
            LoginUserModel loginUser;
            if (MasterInfo.LoginUser != null)
            {
                loginUser = MasterInfo.LoginUser;
            }
            else
            {
                loginUser = _userDataService.GetLast();
            }
            if (loginUser != null)
            {
                //登录数据库
                LoginApp(loginUser, isLoginAccwEvent);

                //设置登录信息
                SetLoginUser(loginUser);
            }
        }

        /// <summary>
        /// 重新创建SDK对象并登录
        /// </summary>
        public void ReloadAndLoginApp()
        {
            _allSdkService.LoadClass();
            Login(false);
        }

        /// <summary>
        /// 反初始化WinPak服务连接
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Logout()
        {
            //退出登录
            var query = new LogoutQuery();
            query = _allSdkService.Logout(query);
            //设置当前登录用户
            MasterInfo.LoginUser = null;
            //数据存储层当前登录用户
            DataStaticInfo.CurrentUserId = string.Empty;
            //Sdk上传地址
            SdkStaticInfo.PushUrl = string.Empty;
            //winpak数据库连接地址
            SqlConnectHelper.ConnectString = string.Empty;

            return true;
        }

        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        /// <param name="token">Token值</param>
        /// <returns></returns>
        public async Task<UserTokenModel> GetByTokenAsync(string token)
        {
            //从内存列表中查询Token信息
            var userToken = MasterInfo.TokenDatas.FirstOrDefault(x => x.Key == token).Value;

            if (userToken == null)
            {
                //列表中不存在从数据库中查找
                userToken = await _tokenDataService.GetByTokenAsync(token);
                if (userToken != null)
                {
                    //TokenData加入内存列表
                    MasterInfo.TokenDatas.AddOrUpdate(userToken.Token, userToken, UpdateTokenDataFactory);
                }
            }

            return userToken;
        }

        private UserTokenModel UpdateTokenDataFactory(string arg1, UserTokenModel arg2)
        {
            return arg2;
        }

    }
}
