using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.Utils;
using KT.Common.WpfApp.ViewModels;
using KT.Front.WriteCard.Entity;
using KT.Proxy.BackendApi.Enums;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.IdReader.Common;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Views.Visitor.Controls;
using Microsoft.Extensions.Logging;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Register
{
    public class AccompanyVisitorControlViewModel : Prism.Mvvm.BindableBase
    {
        public object BindingControl { get; set; }
        public Action<string, AccompanyVisitorControlViewModel> ScanAndSetPersonAction { get; internal set; }

        public ICommand IdCardScanCommand { get; set; }
        public ICommand PassPortScanCommand { get; set; }
        public ICommand DriverLicenseCommand { get; set; }
        private ICommand _writeCardCommand;
        public ICommand WriteCardCommand => _writeCardCommand ??= new DelegateCommand(WriteCard);

        private RegistVisitorViewModel _visitorInfo;
        public bool IsFocus { get; set; }

        public ICommand RemoveVisitorCommand { get; private set; }

        private ShowTakePictureControl _takePictureControl;

        //证件类型
        private ObservableCollection<CertificateTypeEnum> _certificateTypes;

        /// <summary>
        /// 是否为主访客
        /// </summary>
        private bool _isMain;
        private string _nameTitle;
        private bool _isFirst;
        private string _writeCardResult;
        private bool _isWriting;
        private CompanyViewModel _company;

        private Visibility _scanVisibility = Visibility.Collapsed;
        private int _order;
        private Visibility _removeVisibility;
        private WriteRule _currentRule = new HitachiRule();

        private ConfigHelper _configHelper;
        private ILogger _logger;
        private IEventAggregator _eventAggregator;

        public AccompanyVisitorControlViewModel()
        {
            _takePictureControl = ContainerHelper.Resolve<ShowTakePictureControl>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            IdCardScanCommand = new DelegateCommand(IdCardScan);
            PassPortScanCommand = new DelegateCommand(PassPortScan);
            DriverLicenseCommand = new DelegateCommand(DriverLicense);

            RemoveVisitorCommand = new DelegateCommand(RemoveVisitor);

            _eventAggregator.GetEvent<RegistedSuccessEvent>().Subscribe(InitData);
            //选择公司所在楼层写入ic卡
            _eventAggregator.GetEvent<CompanyCheckedEvent>().Subscribe(CompanyChecked);

            InitData();
        }

        private void CompanyChecked(CompanyViewModel company)
        {
            _company = company;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitData()
        {
            CertificateTypes = new ObservableCollection<CertificateTypeEnum>(CertificateTypeEnum.Items);
            VisitorInfo = new RegistVisitorViewModel();

            //特殊组件包含扫描功能
            if (_configHelper.LocalConfig.Reader == ReaderTypeEnum.IDR210_FS531.Value ||
                _configHelper.LocalConfig.Reader == ReaderTypeEnum.FS531.Value )
            {
                ScanVisibility = Visibility.Visible;
                VisitorInfo.ChangedNameAndIdNumberAction = ChangedNameAndIdNumber;
            }

            //清空写卡提示
            ShowCardWriteWarnAsync(string.Empty);
        }

        /// <summary>
        /// 写卡
        /// </summary>
        private async void WriteCard()
        {
            try
            {
                IsWriting = true;
                //去掉前端0
                long Cno =Convert.ToInt64(VisitorInfo.IcCard);
                string Card = Cno.ToString();
                //
                //写卡
                var writeResult = await CurrentRule.WriteCardAsync(new List<int>() { (int)_company.FloorId }, Card);

                Application.Current.Dispatcher.Invoke(delegate
                {
                    IsWriting = false;
                    if (writeResult)
                    {
                        ShowCardWriteWarnAsync("写入卡成功");
                    }
                    else
                    {
                        ShowCardWriteWarnAsync("操作失败，连接失败");
                    }
                });
            }
            catch (Exception ex)
            {
                ShowCardWriteWarnAsync(ex.Message);
                IsWriting = false;
            }
        }

        private void ShowCardWriteWarnAsync(string warnText)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                WriteCardResult = warnText;
            });
        }

        /// <summary>
        /// 开始扫描身份证
        /// </summary>
        private void IdCardScan()
        {
            _logger.LogInformation("开始扫描身份证！");
            ScanAndSetPersonAction?.Invoke(OperateIdTypeEnum.ID_CARD.Value, this);
        }

        /// <summary>
        /// 开始扫描护照
        /// </summary>
        private void PassPortScan()
        {
            _logger.LogInformation("开始扫描护照！");
            ScanAndSetPersonAction?.Invoke(OperateIdTypeEnum.PASSPORT.Value, this);
        }

        /// <summary>
        /// 开始扫描驾照
        /// </summary>
        private void DriverLicense()
        {
            _logger.LogInformation("开始扫描驾照！");
            ScanAndSetPersonAction?.Invoke(OperateIdTypeEnum.DRIVER_LICENSE.Value, this);
        }

        private void ChangedNameAndIdNumber(string name, string idNumber)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(idNumber))
            {
                ScanVisibility = Visibility.Visible;
                VisitorInfo.HeadImg = null;
            }
            else
            {
                ScanVisibility = Visibility.Collapsed;
            }
        }

        //控件移除事件
        public Action<object> ControlRemove;
        private void RemoveVisitor()
        {
            ControlRemove?.Invoke(BindingControl);
        }

        /// <summary>
        /// 设置证件头像
        /// </summary>
        /// <param name="bitmapImage"></param>
        public void SetIdCardImage(Bitmap bitmapImage)
        {
            _takePictureControl.Init(bitmapImage);
            try
            {
                VisitorInfo.HeadImg = ImageConvert.BitmapToBitmapImage(bitmapImage, ImageFormat.Bmp);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "头像转换错误1！");
                try
                {
                    VisitorInfo.HeadImg = ImageConvert.BitmapToBitmapImage(bitmapImage);
                }
                catch (Exception ex1)
                {
                    _logger.LogError(ex1, "头像转换错误2！");
                }
            }

        }

        public ObservableCollection<CertificateTypeEnum> CertificateTypes
        {
            get => _certificateTypes;
            set
            {
                SetProperty(ref _certificateTypes, value);
            }
        }

        public bool IsMain
        {
            get
            {
                return _isMain;
            }

            set
            {
                SetProperty(ref _isMain, value);

                if (IsMain)
                {
                    NameTitle = "访客姓名";
                }
                else
                {
                    NameTitle = "陪同访客" + Order;
                }

                if (IsFirst || IsMain)
                {
                    RemoveVisibility = Visibility.Hidden;
                }
                else
                {
                    RemoveVisibility = Visibility.Visible;
                }
            }
        }

        public ShowTakePictureControl TakePictureControl
        {
            get
            {
                return _takePictureControl;
            }

            set
            {
                SetProperty(ref _takePictureControl, value);
            }
        }

        public string NameTitle
        {
            get
            {
                return _nameTitle;
            }

            set
            {
                SetProperty(ref _nameTitle, value);
            }
        }

        public bool IsFirst
        {
            get
            {
                return _isFirst;
            }

            set
            {
                SetProperty(ref _isFirst, value);

                if (IsFirst || IsMain)
                {
                    RemoveVisibility = Visibility.Hidden;
                }
                else
                {
                    RemoveVisibility = Visibility.Visible;
                }
            }
        }

        public Visibility RemoveVisibility
        {
            get
            {
                return _removeVisibility;
            }

            set
            {
                SetProperty(ref _removeVisibility, value);
            }
        }

        public int Order
        {
            get
            {
                return _order;
            }

            set
            {
                SetProperty(ref _order, value);

                if (IsMain)
                {
                    NameTitle = "访客姓名";
                }
                else
                {
                    NameTitle = "陪同访客" + Order;
                }
            }
        }

        public Visibility ScanVisibility
        {
            get
            {
                return _scanVisibility;
            }

            set
            {
                SetProperty(ref _scanVisibility, value);
            }
        }

        public RegistVisitorViewModel VisitorInfo
        {
            get
            {
                return _visitorInfo;
            }

            set
            {
                SetProperty(ref _visitorInfo, value);
            }
        }

        public string WriteCardResult
        {
            get
            {
                return _writeCardResult;
            }

            set
            {
                SetProperty(ref _writeCardResult, value);
            }
        }

        public bool IsWriting
        {
            get
            {
                return _isWriting;
            }

            set
            {
                SetProperty(ref _isWriting, value);
            }
        }

        public WriteRule CurrentRule
        {
            get { return _currentRule; }
            set
            {
                SetProperty(ref _currentRule, value);
            }
        }
    }
}
