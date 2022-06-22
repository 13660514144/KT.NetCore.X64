using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Models;
using Microsoft.Win32;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace KT.Visitor.Interface.Views
{
    /// <summary>
    /// pblackAdd.xaml 的交互逻辑
    /// </summary>
    public partial class AddBlacklistControl : UserControl
    {
        public AddBlacklistControlViewModel ViewModel { get; private set; }

        private IBlacklistApi _blacklistApi;
        private IUploadImgApi _uploadImgApi;
        private ConfigHelper _configHelper;
        private DialogHelper _dialogHelper;
        private IEventAggregator _eventAggregator;
        public string Imgpara = "";
        public AddBlacklistControl()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Resolve<AddBlacklistControlViewModel>();
            _blacklistApi = ContainerHelper.Resolve<IBlacklistApi>();
            _uploadImgApi = ContainerHelper.Resolve<UploadImgApi>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            //新增黑名单
            _eventAggregator.GetEvent<AddBlacklistEvent>().Subscribe(AddBlacklist);
            //修改和名单
            _eventAggregator.GetEvent<EditFaceEvent>().Subscribe(EditFaceBlack);
            this.DataContext = ViewModel;
            //if (!string.IsNullOrEmpty(Imgpara))
            //{
            //    var imagesouce = new BitmapImage(new Uri(Imgpara));
            //    img_Head.Source = imagesouce.Clone();
            //}
        }
        private void EditFaceBlack(EditFace Face)
        {
            var imagesouce = new BitmapImage(new Uri(Face.UrlImg));
            img_Head.Source = imagesouce.Clone();
        }
        private void AddBlacklist()
        {
            //img_Head.Source = new BitmapImage(new Uri("pack://application:,,,/KT.Visitor.Interface;component/Resources/Images/certificateNot.png", UriKind.RelativeOrAbsolute));
            if (string.IsNullOrEmpty(ViewModel.Blacklist.SnapshotImg))
            {
                img_Head.Source = new BitmapImage(new Uri("pack://application:,,,/KT.Visitor.Interface;component/Resources/Images/certificateNot.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                var imagesouce = new BitmapImage(new Uri(ViewModel.Blacklist.SnapshotImg));
                img_Head.Source = imagesouce.Clone();
            }            
        }

        private void Btn_OK_Click(object sender, RoutedEventArgs e)
        {
            _ = ViewModel.SubmitAsync();
        }

        private void Btn_LoadImg_Click(object sender, RoutedEventArgs e)
        {
            _ = LoadImgAsync();
        }

        private async Task LoadImgAsync()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //注意这里写路径时要用c:\\而不是c:\TOODO不能用winodows路径
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "图片文件(*.jpg,*.png,*.jpeg,*.bmp)|*.jpg;*.png;*.jpeg;*.bmp|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;

            var result = openFileDialog.ShowDialog();
            if (result != true)
            {
                return;
            }

            string fName = openFileDialog.FileName;
            var file = openFileDialog.OpenFile();

            if (file.Length > _configHelper.LocalConfig.UploadFileSize)
            {
                ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("上传的图片不能大于100kb，请重新上传！");
                return;
            }
            // BitmapImage imagesouce = new BitmapImage();
            ViewModel.Blacklist.FaceImg = await _uploadImgApi.UploadFileAsync(fName);
            // Uri(“图片路径“)  
            var imagesouce = new BitmapImage(new Uri(fName));
            img_Head.Source = imagesouce.Clone();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            //跳转到黑名单页面
            _eventAggregator.GetEvent<NavLinkEvent>().Publish(NavLinkModel.Create(FontNavEnum.BLACKLIST));
        }

        private void btn_TreeCompanys_Click(object sender, RoutedEventArgs e)
        {
            pop_TreeCompanys.IsOpen = !pop_TreeCompanys.IsOpen;
        }
    }
}
