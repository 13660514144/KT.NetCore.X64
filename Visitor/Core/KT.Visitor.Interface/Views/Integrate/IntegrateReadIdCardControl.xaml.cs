using KT.Proxy.WebApi.Backend.Models;
using KT.Visitor.IdReader;
using KT.Visitor.IdReader.Common;
using KT.Visitor.Interface.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace KT.Visitor.Interface.Views.Auth
{
    /// <summary>
    /// readIDNumber.xaml 的交互逻辑
    /// </summary>
    public partial class IntegrateReadIdCardControl : UserControl
    {
        private ILogger<IntegrateReadIdCardControl> _logger;
        private IntegrateIdentityAuthActiveControl _identityAuthActivePage;
        private InviteAuthActivePage _inviteAuthActivePage;
        private MainFrameHelper _mainFrameHelper;
        private ConfigHelper _configHelper;
        private ReaderFactory _readerFactory;
        private IContainerProvider _containerProvider;

        public IntegrateReadIdCardControl(ILogger<IntegrateReadIdCardControl> logger,
            IntegrateIdentityAuthActiveControl identityAuthActivePage,
            InviteAuthActivePage inviteAuthActivePage,
            MainFrameHelper mainFrameHelper,
            ConfigHelper configHelper,
            ReaderFactory readerFactory,
            IContainerProvider containerProvider)
        {
            InitializeComponent();

            _logger = logger;
            _identityAuthActivePage = identityAuthActivePage;
            _inviteAuthActivePage = inviteAuthActivePage;
            _mainFrameHelper = mainFrameHelper;
            _configHelper = configHelper;
            _readerFactory = readerFactory;
            _containerProvider = containerProvider;
        }
        public int authCategory = 0;
        private IReader reader;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public List<VisitorInfoModel> records = new List<VisitorInfoModel>();
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitAsync();
        }

        private async Task InitAsync()
        {
            //初始化身份证阅读器对象  
            reader = _readerFactory.GetReader(_configHelper.LocalConfig.Reader);

            //定时去读取卡片
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Person person = ReadIDCard();

            if (person == null)
            {
                return;
            }

            SetPeople(person);
        }

        /// <summary>
        /// 设置身份证信息
        /// </summary>
        private void SetPeople(Person person)
        {
            if (records.Count == 0)
            {
                var ui_msg = _containerProvider.Resolve<AuthMsgPage>();
                ui_msg.ErrMsg = "未能匹配到记录";
                ui_msg.OperateType = this.authCategory;
                _mainFrameHelper.Link(ui_msg, false);
                return;
            }
            //比较姓名是否一致
            records = records.Where(x => x.Name == person.Name).ToList();
            if (records?.Count <= 0)
            {
                var ui_msg = _containerProvider.Resolve<AuthMsgPage>();
                ui_msg.ErrMsg = "名字校验不一致";
                ui_msg.OperateType = this.authCategory;
                _mainFrameHelper.Link(ui_msg, false);
                return;
            }
            if (authCategory == 0)
            {
                _identityAuthActivePage.IdNumber = person.IdCode;
                _identityAuthActivePage.visName = person.Name;
                _identityAuthActivePage.records = this.records;
                _mainFrameHelper.Link(_identityAuthActivePage);
            }
            else if (authCategory == 1)
            {
                _inviteAuthActivePage.Record = records[0];
                _mainFrameHelper.Link(_identityAuthActivePage);
            }
        }

        /// <summary>
        /// 读取身份证
        /// </summary>
        private Person ReadIDCard()
        {
            Person person = null;
            try
            {
                person = reader.Read();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex?.Message);
                return null;
            }
            if (person == null)
            {
                return null;
            }

            return person;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
        }
    }
}
