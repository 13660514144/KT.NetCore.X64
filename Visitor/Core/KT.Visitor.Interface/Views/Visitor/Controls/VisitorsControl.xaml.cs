using CommonUtils;
using KT.Proxy.WebApi.Backend.Apis;
using KT.Proxy.WebApi.Backend.Models;
using KT.Visitor.IdReader;
using KT.Visitor.IdReader.Common;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Tools.Printer.PrintOperator;
using KT.Visitor.Interface.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    /// <summary>
    /// VisitorsControl.xaml 的交互逻辑
    /// </summary>
    public partial class VisitorsControl : System.Windows.Controls.UserControl
    {
        public VisitorsControlViewModel ViewModel { get; set; }

        private BlacklistApi _blacklistApi;
        private ConfigHelper _configHelper;
        private ReaderFactory _readerFactory;
        private ILogger<VisitorRegisterPage> _logger;
        private IContainerProvider _containerProvider;

        //证件阅读器
        private IReader reader;
        //证件阅读器定时器,用于定时读取身份证件信息
        private Timer readerTimer;

        public VisitorsControl(VisitorsControlViewModel viewModel,
            BlacklistApi blacklistApi,
            ConfigHelper configHelper,
            ReaderFactory readerFactory,
            ILogger<VisitorRegisterPage> logger,
            IContainerProvider containerProvider)
        {
            InitializeComponent();

            ViewModel = viewModel;

            _blacklistApi = blacklistApi;

            _configHelper = configHelper;
            _readerFactory = readerFactory;
            _logger = logger;
            _containerProvider = containerProvider;

            this.DataContext = ViewModel;
            //初始化事件
            this.Loaded += Page_Loaded;
            this.Unloaded += Page_Unloaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // 初始化身份证阅读器对象
            InitReader();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            readerTimer.Stop();
        }

        /// <summary>
        /// 初始化读卡器对像
        /// </summary>
        private void InitReader()
        {
            //初始化身份证阅读器对象 
            reader = _readerFactory.GetReader(_configHelper.LocalConfig.Reader);
            //阅读器读取身份证
            readerTimer = new Timer();
            readerTimer.Tick += ReaderTimer_Tick;
            readerTimer.Interval = 1000;
            readerTimer.Start();
        }

        /// <summary>
        /// 读取身份证信息事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReaderTimer_Tick(object sender, EventArgs e)
        {
            Person person = ReadIDCard();
            if (person == null)
            {
                readerTimer.Start();
                return;
            }
            else
            {
                SetPersonAsync(person);
            }
        }

        /// <summary>
        /// 设置人员信息
        /// </summary>
        /// <param name="person"></param>
        private async Task SetPersonAsync(Person person)
        {
            //2.存在证件号相同的陪同访客
            foreach (var item in ViewModel.VisitorControls)
            {
                if (item.ViewModel.VisitorInfo.CertificateNumber == person.IdCode)
                {
                    //2.1证件号相同姓名不同则录入
                    if (person.Name != item.ViewModel.VisitorInfo.Name)
                    {
                        await SetAccompanyPersonAsync(item.ViewModel, person);
                        return;
                    }
                    //1.2证件号相同姓名相同不再录入
                    return;
                }

                //4.陪同访客陪同访客证件号为空则录入  
                if (string.IsNullOrEmpty(item.ViewModel.VisitorInfo.CertificateNumber))
                {
                    await SetAccompanyPersonAsync(item.ViewModel, person);
                    return;
                }

                //5.1光标在主访客
                if (item.ViewModel.IsFocus)
                {
                    await SetAccompanyPersonAsync(item.ViewModel, person);
                    return;
                }
            }

            //6.默认修改主访客
            await SetAccompanyPersonAsync(ViewModel.VisitorControls.FirstOrDefault().ViewModel, person);
        }

        public Func<CompanyViewModel> GetCompanyFunc;

        private async Task<bool> IsBacklistByIdCodeAsync(string idCode)
        {
            //验证黑名单 
            var company = GetCompanyFunc?.Invoke();
            if (company != null)
            {
                var isBlack = await _blacklistApi.IsBlackAsync(string.Empty, idCode, company.FloorId, company.Id);
                if (isBlack)
                {
                    readerTimer.Stop();
                    var result = MessageWarnBox.Show("抱歉，疑似黑名单用户，不能登记！");
                    readerTimer.Start();
                    return true;
                }
            }
            return false;
        }

        private async Task SetAccompanyPersonAsync(AccompanyVisitorControlViewModel controlViewModel, Person person)
        {
            var isBlack = await IsBacklistByIdCodeAsync(person.IdCode);
            if (isBlack)
            {
                return;
            }

            controlViewModel.VisitorInfo.CertificateType = person.CardType.ToString();
            controlViewModel.VisitorInfo.Name = person.Name;
            controlViewModel.VisitorInfo.CertificateNumber = person.IdCode;
            controlViewModel.VisitorInfo.HeadImg = ImageConvert.ImageToBitmapImage(person.Portrait, ImageFormat.Png);
            controlViewModel.VisitorInfo.Gender = person.Gender;
        }

        /// <summary>
        /// 读取身份证
        /// </summary>
        /// <returns></returns>
        private Person ReadIDCard()
        {
            Person person = reader.Read();
            if (person == null)
            {
                return null;
            }
            return person;
        }


        private void Addvistor_Click(object sender, RoutedEventArgs e)
        {
            var control = _containerProvider.Resolve<AccompanyVisitorControl>();
            control.ControlRemove += Control_ControlRemove;
            ViewModel.VisitorControls.Add(control);
        }

        /// <summary>
        /// 删除控件
        /// </summary>
        /// <param name="sender">操作的控件</param>
        /// <param name="e"></param>
        private void Control_ControlRemove(object sender, EventArgs e)
        {
            var control = (AccompanyVisitorControl)sender;
            ViewModel.VisitorControls.Remove(control);
        }
    }
}
