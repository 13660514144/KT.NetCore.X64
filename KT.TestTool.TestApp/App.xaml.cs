using KT.Common.Core.Utils;
using KT.Common.WpfApp.Dependency;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Prowatch.Service.Base;
using KT.Prowatch.Service.Daos;
using KT.Prowatch.Service.Helpers;
using KT.Prowatch.Service.IDaos;
using KT.Prowatch.Service.IServices;
using KT.Prowatch.Service.Services;
using KT.Proxy.BackendApi.Apis;
using KT.TestTool.TestApp.Apis;
using KT.TestTool.TestApp.Daos;
using KT.TestTool.TestApp.Datas.Base;
using KT.TestTool.TestApp.Helpers;
using KT.TestTool.TestApp.IDaos;
using KT.TestTool.TestApp.Settings;
using KT.TestTool.TestApp.ViewModels;
using KT.TestTool.TestApp.Views;
using KT.TestTool.TestApp.Views.Common;
using KT.TestTool.TestApp.Views.Prowatch;
using KT.TestTool.TestApp.Views.WinPak;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.Tools.Printer.DocumentRenderer;
using KT.Visitor.Common.Tools.Printer.Helpers;
using KT.Visitor.Common.Tools.Printer.PrintOperator;
using KT.WinPak.Data.Base;
using KT.WinPak.Data.Daos;
using KT.WinPak.Data.IDaos;
using KT.WinPak.SDK;
using KT.WinPak.SDK.IServices;
using KT.WinPak.SDK.Services;
using KT.WinPak.Service.IServices;
using KT.WinPak.Service.Services;
using KT.WinPak.WebApi.Common.Queues;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace KT.TestTool.TestApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ILogger _logger;

        public App()
        {
            //更新数据库
            DbUpdateHelper.Init();

            //全局异常
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //服务注入
            DependencyService.StartServices((services) =>
            {
                services.Configure<AppSettings>(DependencyService.Configuration.GetSection("AppSettings").Bind);
                services.Configure<SocketSettings>(DependencyService.Configuration.GetSection("SocketSettings").Bind);
                services.Configure<ProwatchSettings>(DependencyService.Configuration.GetSection("ProwatchSettings").Bind);

                //// 配置本地数据库，设置绝对路径，避免Api生成目录在当前项目下与发布版本目录不一致
                //string dbPath = Path.Combine(AppContext.BaseDirectory, "LocalData.db");
                //services.AddDbContext<SqliteContext>(options => options.UseSqlite(@"Data Source=" + dbPath, p => p.MigrationsAssembly("KT.TestTool.TestApp")));

                //string winPakDbPath = Path.Combine(AppContext.BaseDirectory, "WinPakLocalData.db");
                //services.AddDbContext<WinPakSqliteContext>(options => options.UseSqlite(@"Data Source=" + winPakDbPath, p => p.MigrationsAssembly("KT.TestTool.TestApp")));

                //string prowatchDbPath = Path.Combine(AppContext.BaseDirectory, "ProwatchLocalData.db");
                //services.AddDbContext<ProwatchSqliteContext>(options => options.UseSqlite(@"Data Source=" + prowatchDbPath, p => p.MigrationsAssembly("KT.TestTool.TestApp")));

                services.AddDbContext<TestAppContext>();
                services.AddDbContext<WinPakSqliteContext>();
                services.AddDbContext<ProwatchSqliteContext>();
                services.AddDbContext<ProwatchContext>();

                //公用组件
                services.AddLogging(configure =>
                {
                    configure.AddLog4Net();
                });

                //依赖注入服务
                //Data
                services.AddTransient<ISystemConfigDataDao, SystemConfigDataDao>();

                //ViewModel
                services.AddTransient<MainWindowViewModel>();

                //局部页控件
                services.AddTransient<UdpControl>();
                services.AddTransient<UdpControlViewModel>();
                services.AddTransient<TcpControl>();
                services.AddTransient<TcpControlViewModel>();
                services.AddTransient<WinPakTestControl>();
                services.AddTransient<WinPakTestControlViewModel>();
                services.AddTransient<ProwatchTestControl>();
                services.AddTransient<ProwatchTestControlViewModel>();
                services.AddTransient<PhotographControl>();
                services.AddTransient<ProwatchQrShowWindow>();
                services.AddTransient<ProwatchQrShowWindowViewModel>();

                //ViewModel
                services.AddTransient<ScrollMessageViewModel>();
                services.AddTransient<SocketInfoViewModel>();

                //Apis
                services.AddTransient<WinPakApi>();
                services.AddTransient<ProwatchSdkApi>();
                services.AddTransient<ProwatchWebApi>();

                #region WinPak Services 
                //Local Data
                services.AddScoped<ILoginUserDataDao, LoginUserDataDao>();
                services.AddScoped<IUserTokenDataDao, UserTokenDataDao>();
                services.AddScoped<ILoginUserDataService, LoginUserDataService>();
                services.AddScoped<IUserTokenDataService, UserTokenDataService>();

                //HttpProxy
                services.AddSingleton<PushApi>();

                //SDK
                services.AddSingleton<IAllSdkService, AllSdkService>();
                services.AddSingleton<AccwEventSdk>();

                //SDK Serivce 
                services.AddScoped<ICardHolderSdkService, CardHolderSdkService>();
                services.AddScoped<ICardSdkService, CardSdkService>();
                services.AddScoped<IHWDeviceSdkService, HWDeviceSdkService>();
                services.AddScoped<ITimeZoneSdkService, TimeZoneSdkService>();
                services.AddScoped<IAccessLevelSdkService, AccessLevelSdkService>();

                // service
                services.AddScoped<IUserService, UserService>();

                //其它
                services.AddSingleton<SingletonExecuteQueue>();
                #endregion

                #region Prowatch Services 
                services.AddScoped<ILoginUserDao, LoginUserDao>();
                services.AddScoped<IUserTokenDao, UserTokenDao>();
                services.AddScoped<IProwatchDao, ProwatchDao>();
                services.AddScoped<ILoginUserService, LoginUserService>();
                services.AddScoped<IUserTokenService, UserTokenService>();
                services.AddScoped<IProwatchService, ProwatchService>();
                services.AddScoped<IPushEventService, PushEventService>();

                //HttpProxy
                services.AddSingleton<PushApi>();
                services.AddSingleton<InitHelper>();
                services.AddSingleton<PrintSourceProvider>();

                services.AddTransient<ProwatchPrintHelper>();
                services.AddTransient<PrintHandler>();
                services.AddTransient<SmallTicketOperator>();
                services.AddTransient<ConfigHelper>();
                services.AddTransient<DialogHelper>();
                services.AddTransient<VisitQrCodeRenderer>();

                services.AddTransient<IFunctionApi, FrontFunctionApi>();

                services.AddSingleton<ILogger>(new Log4gHelper());


                #endregion

                //依赖注入视图
                services.AddTransient(typeof(MainWindow));

                //系统服务注入 
            });

            //当前类日记记录
            _logger = DependencyService.ServiceProvider.GetRequiredService<ILogger<App>>();

            //更新本地数据库
            var dbContext = DependencyService.ServiceProvider.GetRequiredService<TestAppContext>();
            dbContext.Database.Migrate();

            _logger.LogInformation("Database Inited!");

            //启动主窗口
            var mainWindow = DependencyService.ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            _logger?.LogError("App_DispatcherUnhandledException Error:{0} ", JsonConvert.SerializeObject(e, JsonUtil.JsonSettings));
            e.Handled = true;
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger?.LogError("CurrentDomain_UnhandledException Error:{0} ", JsonConvert.SerializeObject(e, JsonUtil.JsonSettings));
        }
    }
}