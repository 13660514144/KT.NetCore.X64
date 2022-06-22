using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpModel;
using KT.Prowatch.Service.IDaos;
using KT.Prowatch.Service.Helpers;
using KT.Prowatch.Service.IServices;
using KT.Prowatch.Service.Models;
using KT.Prowatch.WebApi.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace KT.Prowatch.Service.Services
{
    public class LoginUserService : ILoginUserService
    {
        private ILogger<LoginUserService> _logger;
        private ILoginUserDao _dao;
        private IUserTokenDao _userTokenDao;
        private InitHelper _initHelper;
        private IPushEventService _pushEventService;
        private AppSettings _appSettings;

        public LoginUserService(ILogger<LoginUserService> logger,
            ILoginUserDao dao,
            IUserTokenDao userTokenDao,
            InitHelper initHelper,
            IPushEventService pushEventService,
            IOptions<AppSettings> appSettings)
        {
            _dao = dao;
            _logger = logger;
            _userTokenDao = userTokenDao;
            _initHelper = initHelper;
            _pushEventService = pushEventService;
            _appSettings = appSettings.Value;
        }


        public async Task<LoginUserModel> CheckedOrAddAsync(LoginUserModel connect)
        {
            // 1.检查是否存在连接字符串        
            var entity = LoginUserModel.ToEntity(connect);
            var oldEntity = await _dao.GetByDataAsync(entity);

            if (oldEntity != null)
            {
                oldEntity = await _dao.UpdateEditTimeAsync(oldEntity.Id);
                connect = LoginUserModel.ToModel(oldEntity);
            }
            else
            {
                //对数据库连接信息持久化
                await _dao.InsertAsync(entity);
                connect = LoginUserModel.ToModel(entity);
            }
            return connect;
        }

        public async Task<LoginUserModel> GetLastAsync()
        {
            var entity = await _dao.GetLastAsync();
            return LoginUserModel.ToModel(entity);
        }

        public async Task<LoginUserModel> AddAsync(LoginUserModel model)
        {
            var entity = LoginUserModel.ToEntity(model);
            await _dao.InsertAsync(entity);

            return model;
        }

        public async Task LoginLastAsync()
        {
            var entity = await _dao.GetLastOnTrackAsync();
            if (entity != null)
            {
                var model = LoginUserModel.ToModel(entity);
                await LoginAsync(model);
            }
        }

        public async Task<TokenResponse> LoginAsync(LoginUserModel loginUser)
        {
            _logger.LogInformation("开始初始化");

            //初始化Prowatch
            bool result = _initHelper.Init(loginUser);
            if (!result)
            {
                throw CustomException.Run("初始化连接失败！");
            }

            // 1.检查是否存在连接字符串  
            loginUser = await CheckedOrAddAsync(loginUser);

            // 2.初始化Token值
            UserTokenModel tokenData = new UserTokenModel();
            //设置连接信息值
            tokenData.TimeNow = DateTimeUtil.UtcNowMillis();
            tokenData.TimeOut = tokenData.TimeNow.AddDayMillis(3600);
            tokenData.LoginUserId = loginUser.Id;
            tokenData.LoginUser = loginUser;

            //执久化Token 
            var userToken = UserTokenModel.ToEntity(tokenData);
            await _userTokenDao.AddAsync(userToken);

            //winpak数据库连接地址
            loginUser.DBName = string.IsNullOrEmpty(loginUser.DBName) ? _appSettings.DBName : loginUser.DBName;
            SqlConnectHelper.ConnectString = $"Data Source={loginUser.DBAddr};Initial Catalog={loginUser.DBName};User ID={loginUser.DBUser};Password={loginUser.DBPassword}";

            if (tokenData == null)
            {
                throw CustomException.Run("登录失败！");
            }

            //设置当前Prowatch初始化的连接
            LoginHelper.Instance.CurrentConnect = tokenData.LoginUser;
            //当前token加入内存列表
            LoginHelper.Instance.LoginAdd(tokenData);
            ////开启事件监听
            //_pushEventService.InitPush();
            _logger.LogInformation("结束初始化");

            var tokenResponse = new TokenResponse(tokenData.Token, tokenData.TimeNow, tokenData.TimeOut);
            return tokenResponse;
        }
    }
}
