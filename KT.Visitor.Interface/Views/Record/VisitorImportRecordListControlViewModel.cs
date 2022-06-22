using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Views.Helper;
using KT.Visitor.Data.Enums;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Models;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Common;
using Microsoft.Win32;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views
{
    public class VisitorImportRecordListControlViewModel : BindableBase
    {
        public NavPageControl NavPageControl { get; private set; }

        //导入详情
        private ICommand _detailCommand;
        public ICommand DetailCommand => _detailCommand ??= new DelegateCommand<VisitorImportModel>(DetailAsync);

        //访客信息选择
        private ICommand _selectVisitorImportCommand;
        public ICommand SelectVisitorImportCommand => _selectVisitorImportCommand ??= new DelegateCommand(SelectVisitorImport);

        //下载访客信息模板
        private ICommand _downloadVisitorImportTemplateCommand;
        public ICommand DownloadVisitorImportTemplateCommand => _downloadVisitorImportTemplateCommand ??= new DelegateCommand(DownloadVisitorImportTemplate);

        //访客头像选择
        private ICommand _selectVisitorImportPortraitCommand;
        public ICommand SelectVisitorImportPortraitCommand => _selectVisitorImportPortraitCommand ??= new DelegateCommand(SelectVisitorImportPortrait);

        //来访事由
        private ICommand _selectedCompanyStaffCommand;
        public ICommand SelectedCompanyStaffCommand => _selectedCompanyStaffCommand ??= new DelegateCommand<CompanyStaffModel>(SelectedCompanyStaff);

        ////来访事由
        //private ICommand _companyStaffEnterInputCommand;
        //public ICommand CompanyStaffEnterInputCommand => _companyStaffEnterInputCommand ??= new DelegateCommand<string>(CompanyStaffEnterInputAsync);

        //来访事由
        private ICommand _comboBoxTextChangedCommand;
        public ICommand ComboBoxTextChangedCommand => _comboBoxTextChangedCommand ??= new DelegateCommand<string>(CompanyStaffEnterInputAsync);

        //导入访客
        private ICommand _linkVisitorRecordListCommand;
        public ICommand LinkVisitorRecordListCommand => _linkVisitorRecordListCommand ??= new DelegateCommand(LinkVisitorRecordList);

        private ICommand _linkVisitorImportDoubtCommand;
        public ICommand LinkVisitorImportDoubtCommand => _linkVisitorImportDoubtCommand ??= new DelegateCommand(LinkVisitorImportDoubt);

        private CompanyTreeCheckHelper _companyTreeCheckHelper;
        private VisitorImportViewModel _visitorImport;
        private bool _isCompanyStaffDropDownOpen;

        private bool _isSelected = false;

        private IEventAggregator _eventAggregator;
        private IFunctionApi _functionApi;
        private IVisitorApi _visitorApi;

        public VisitorImportRecordListControlViewModel()
        {
            _companyTreeCheckHelper = ContainerHelper.Resolve<CompanyTreeCheckHelper>();
            NavPageControl = ContainerHelper.Resolve<NavPageControl>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _functionApi = ContainerHelper.Resolve<IFunctionApi>();
            _visitorApi = ContainerHelper.Resolve<IVisitorApi>();

            VisitorImport = new VisitorImportViewModel();

            _ = InitAsync();
        }

        /// <summary>
        /// 访客信息选择
        /// </summary>
        private void SelectVisitorImport()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "请选择访客信息文件";
            openFileDialog.Filter = "Excel文件(*.xls;*.xlsx)|*.xls;*.xlsx|所有文件|*.*";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            openFileDialog.RestoreDirectory = false;

            var dialogResultl = openFileDialog.ShowDialog();
            if (dialogResultl == true)
            {
                if (!openFileDialog.FileName.ToUpper().EndsWith(".XLS") && !openFileDialog.FileName.ToUpper().EndsWith(".XLSX"))
                {
                    throw new Exception("导入文件格式出错，只能导入（.xls）或（.xlsx）格式文件！");
                }

                VisitorImport.Xls = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// 下载访客信息模板
        /// </summary>
        private void DownloadVisitorImportTemplate()
        {
            System.Windows.Forms.SaveFileDialog saveDg = new System.Windows.Forms.SaveFileDialog();
            saveDg.Filter = "(*.xls)|*.xls|(*.xlsx)|*.xlsx";
            saveDg.FileName = "批量导入访客模板.xls";
            saveDg.AddExtension = true;
            saveDg.RestoreDirectory = true;
            if (saveDg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //此处做你想做的事
                var filePath = saveDg.FileName;

                var name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".Files.Documents.VisitorImprotTemplate.xls";
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                var stream = assembly.GetManifestResourceStream(name);

                var buffer = new Byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Seek(0, SeekOrigin.Begin);

                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// 访客头像选择
        /// </summary>
        private void SelectVisitorImportPortrait()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "请选择访客头像文件";
            openFileDialog.Filter = "zip文件|*.zip|所有文件|*.*";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            openFileDialog.RestoreDirectory = false;

            var dialogResultl = openFileDialog.ShowDialog();
            if (dialogResultl == true)
            {
                if (!openFileDialog.FileName.ToUpper().EndsWith(".ZIP"))
                {
                    throw new Exception("导入文件格式出错，只能导入（.zip）格式文件！");
                }
                VisitorImport.Zip = openFileDialog.FileName;
            }
        }

        private void SelectedCompanyStaff(CompanyStaffModel companyStaff)
        {
            VisitorImport.CompanyStaffName = companyStaff?.DisplayName;
            _isSelected = true;
        }
        private void LinkVisitorRecordList()
        {
            _eventAggregator.GetEvent<NavLinkEvent>().Publish(NavLinkModel.Create(FontNavEnum.VISITOR_RECORD));
        }
        private void LinkVisitorImportDoubt()
        {
            ContainerHelper.Resolve<MessageInfoBox>().ShowMessage("1.请先填写来访信息和授权方式，下载并按模板填写上传访客信息文件;\r\n2.二维码授权可先开启二维码凭证短信，通行凭证将通过短信发送给访客; 如需打印纸质 二维码凭证请到前台打印;\r\n3.人脸授权需上传访客头像：将访客头像以“姓名”或“姓名 + 手机号”命名并放至在同个文 件夹内，头像要求面部清晰，人脸有效区域在100px * 100px以 上，jpg或png格式，收集 完成后将所有图片压缩为zip格式文件上传。", "温馨提示");
        }
        private async Task CompanyStaffEnterInputAsync(string companyStaffName)
        {
            if (_isSelected)
            {
                _isSelected = false;
                return;
            }

            var query = new CompanyStaffQuery();
            query.Search = companyStaffName;

            var companyStaffs = await _visitorApi.GetCompanyOrStaff(query);
            if (companyStaffs?.FirstOrDefault() == null)
            {
                VisitorImport.CompanyStaffs = new ObservableCollection<CompanyStaffModel>();
                _isSelected = true;
                VisitorImport.CompanyStaffName = companyStaffName;
            }
            else
            {
                VisitorImport.CompanyStaffs = new ObservableCollection<CompanyStaffModel>(companyStaffs);
            }

            IsCompanyStaffDropDownOpen = true;
        }
        private void DetailAsync(VisitorImportModel item)
        {
            //跳转到黑名单页面
            _eventAggregator.GetEvent<NavLinkEvent>().Publish(NavLinkModel.Create(FontNavEnum.VISITOR_IMPORT_DETAIL, data: item));
        }

        public async Task InitAsync()
        {
            VisitorImport.Status = string.Empty;

            //初始化对你 
            VisitorImport.StatusItems = VisitStatusEnum.GetVMs(true);

            var configParms = await _functionApi.GetConfigParmsAsync();
            VisitorImport.VisitorReasons = new ObservableCollection<string>(configParms.AccessReasons);
        }

        public VisitorImportViewModel VisitorImport
        {
            get => _visitorImport;
            set
            {
                SetProperty(ref _visitorImport, value);
            }
        }

        public bool IsCompanyStaffDropDownOpen
        {
            get => _isCompanyStaffDropDownOpen;
            set
            {
                SetProperty(ref _isCompanyStaffDropDownOpen, value);
            }
        }
    }
}
