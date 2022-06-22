using KT.Common.WpfApp.Helpers;
using KT.Front.WriteCard.SDK;
using KT.Visitor.Common.Helpers;
using KT.Visitor.IdReader.Common;
using KT.Visitor.Interface.Events;
using Microsoft.Extensions.Logging;
using Prism.Events;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Text;

namespace KT.Visitor.Interface.Views.Register
{
    /// <summary>
    /// VisitorInputControl.xaml 的交互逻辑
    /// </summary>
    public partial class VisitorInputControl : UserControl
    {
        private ConfigHelper _configHelper;
        private ILogger _logger;

        public VisitorInputControlViewModel ViewModel { get; set; }

        private DialogHelper _dialogHelper;
        private IEventAggregator _eventAggregator;

        public VisitorInputControl()
        {
            InitializeComponent();

            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            ViewModel = ContainerHelper.Resolve<VisitorInputControlViewModel>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();

            _eventAggregator.GetEvent<ReadedPersonEvent>().Subscribe(SetPerson);

            _ = InitAsync();

            this.Loaded += VisitorInputControl_Loaded;
            
        }

        private void VisitorInputControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists("Radio.ini"))
            {
                System.IO.StreamReader file =   new System.IO.StreamReader("Radio.ini");
                string line = file.ReadLine();
                file.Close();
                file.Dispose();
                string[] ArrRadio = line.Split(':');
                switch (ArrRadio[1])
                {
                    case "R1":
                        this.R1.IsChecked = true;
                        break;
                    case "R2":
                        this.R2.IsChecked = true;
                        break;
                    case "R3":
                        this.R3.IsChecked = true;
                        break;
                    case "R4":
                        this.R4.IsChecked = true;
                        break;
                }
            }
            else
            {
                string radio = "type:R1";
                File.WriteAllText("Radio.ini", radio, Encoding.UTF8);
                this.R1.IsChecked = true;
            }

            if (File.Exists("Auth.ini"))
            {
                System.IO.StreamReader file = new System.IO.StreamReader("Auth.ini");
                string line = file.ReadLine();
                file.Close();
                file.Dispose();
                string[] auth = line.Split(':');
                if (auth[0] == "Auth1")
                {
                    this.Auth1.IsChecked = true;
                }
                else if (auth[0] == "Auth2")
                {
                    this.Auth2.IsChecked = true;
                    this.Auth3.Text = auth[1];
                }
            }
            else
            {
                File.WriteAllText("Auth.ini", "Auth1:", Encoding.UTF8);
                this.Auth1.IsChecked = true;
            }
                //每次加载都连接写卡器
                WriteCardDiviceConnectAsync();
        }

        public async void WriteCardDiviceConnectAsync()
        {
            await D300M.ConnectAsync();
        }

        private async void SetPerson(Person person)
        {
            try
            {
                await SetPersonAsync(person);
            }
            catch (Exception ex)
            { 
            }
        }

        private Task InitAsync()
        {
            //初始化数据
            ViewModel.InitControl(ScanAndSetPerson);

            this.DataContext = ViewModel;

            return Task.CompletedTask;
        }

        /// <summary>
        /// 扫描证件  手动点击按钮
        /// </summary>
        /// <param name="operateIdType"></param>
        /// <param name="controlViewModel"></param>
        /// <returns></returns>
        private void ScanAndSetPerson(string operateIdType, AccompanyVisitorControlViewModel controlViewModel)
        {
            //异步扫描证件
            Task.Run(async () =>
            {
                _logger.LogInformation("开始扫描证件：readerType:{0} ", operateIdType);
                var person = await ReaderFactory.Reader?.ScanAsync(operateIdType);
                if (person != null)
                {
                    await SetAccompanyPersonAsync(controlViewModel, person);
                }
            });
        }

        /// <summary>
        /// 设置人员信息
        /// </summary>
        /// <param name="person"></param>
        private async Task SetPersonAsync(Person person)
        {
            if (ViewModel.AccompanyVisitorControl.ViewModel.VisitorInfo.CertificateNumber == person.IdCode)
            {
                //2.1证件号相同姓名不同则录入
                if (person.Name != ViewModel.AccompanyVisitorControl.ViewModel.VisitorInfo.Name)
                {
                    await SetAccompanyPersonAsync(ViewModel.AccompanyVisitorControl.ViewModel, person);
                    return;
                }
                //1.2证件号相同姓名相同不再录入
                return;
            }

            //4.陪同访客陪同访客证件号为空则录入  
            if (string.IsNullOrEmpty(ViewModel.AccompanyVisitorControl.ViewModel.VisitorInfo.CertificateNumber))
            {
                await SetAccompanyPersonAsync(ViewModel.AccompanyVisitorControl.ViewModel, person);
                return;
            }

            //5.1光标在主访客
            if (ViewModel.AccompanyVisitorControl.ViewModel.IsFocus)
            {
                await SetAccompanyPersonAsync(ViewModel.AccompanyVisitorControl.ViewModel, person);
                return;
            }

            //陪同访客为空则替换主访客
            if (ViewModel.AccompanyVisitorsWindowViewModel?.VisitorControls == null)
            {
                await SetAccompanyPersonAsync(ViewModel.AccompanyVisitorControl.ViewModel, person);
                return;
            }

            //2.存在证件号相同的陪同访客
            foreach (var item in ViewModel.AccompanyVisitorsWindowViewModel.VisitorControls)
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
            await SetAccompanyPersonAsync(ViewModel.AccompanyVisitorControl.ViewModel, person);
            //await SetAccompanyPersonAsync(ViewModel.AccompanyVisitorsWindowViewModel.VisitorControls.FirstOrDefault().ViewModel, person);
        }

        private async Task SetAccompanyPersonAsync(AccompanyVisitorControlViewModel controlViewModel, Person person)
        {
            var isBlack = await ViewModel.IsBacklistByIdCodeAsync(person.IdCode);
            if (isBlack)
            {
                return;
            }
            
            controlViewModel.VisitorInfo.CertificateType = person.CardType;
            controlViewModel.VisitorInfo.Name = person.Name;
            controlViewModel.VisitorInfo.CertificateNumber = person.IdCode;
            if (person.Portrait != null)
            {
                var portaitBitmap = new Bitmap(person.Portrait);
                controlViewModel.SetIdCardImage(portaitBitmap);
            }
            else
            {
                controlViewModel.SetIdCardImage(null);
            }
            controlViewModel.VisitorInfo.Gender = person.Gender;
        }
    }
}
