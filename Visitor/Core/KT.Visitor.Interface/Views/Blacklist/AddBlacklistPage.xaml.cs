using KT.Proxy.WebApi.Backend.Apis;
using KT.Proxy.WebApi.Backend.Models;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.ViewModels;
using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace KT.Visitor.Interface.Views.Blacklist
{
    /// <summary>
    /// pblackAdd.xaml 的交互逻辑
    /// </summary>
    public partial class AddBlacklistPage : Page
    {
        public AddBlacklistPageViewModel ViewModel { get; private set; }

        private BlacklistApi _blacklistApi;
        private MainFrameHelper _mainFrameHelper;
        private UploadImgApi _uploadImgApi;
        private ConfigHelper _configHelper;

        public AddBlacklistPage(BlacklistApi blacklistApi,
            AddBlacklistPageViewModel viewModel,
            MainFrameHelper mainFrameHelper,
            UploadImgApi uploadImgApi,
            ConfigHelper configHelper)
        {
            InitializeComponent();

            _blacklistApi = blacklistApi;
            ViewModel = viewModel;
            _mainFrameHelper = mainFrameHelper;
            _uploadImgApi = uploadImgApi;
            _configHelper = configHelper;

            this.DataContext = ViewModel;

            MouseDown += AddBlacklistPage_MouseDown;
        }

        private void AddBlacklistPage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //点击树型选择器不关闭  
            if (IsNotClose(Grid_CheckTreeCompanys, e.Source))
            {
                return;
            }
            else
            {
                pop_TreeCompanys.IsOpen = false;
            }
        }

        /// <summary>
        /// 检查是否关闭窗口
        /// </summary>
        /// <param name="control"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private bool IsNotClose(UIElement control, object source)
        {
            if (object.ReferenceEquals(control, source))
            {
                return true;
            }
            if (control is System.Windows.Controls.Panel)
            {
                var panel = control as System.Windows.Controls.Panel;
                foreach (UIElement item in panel.Children)
                {
                    if (IsNotClose(item, source))
                    {
                        return true;
                    }
                }
            }

            return false;
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
            openFileDialog.Filter = "文本文件|*.*|所有文件|*.*";
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
                MessageWarnBox.Show("上传的图片不能大于100kb，请重新上传！");
                return;
            }
            // BitmapImage imagesouce = new BitmapImage();
            ViewModel.Blacklist.FaceImg = await _uploadImgApi.UploadPortraitAsync(fName);
            //Uri(“图片路径“)  
            var imagesouce = new BitmapImage(new Uri(fName));
            img_Head.Source = imagesouce.Clone();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            _mainFrameHelper.LinkBlacklist();
        }

        private void btn_TreeCompanys_Click(object sender, RoutedEventArgs e)
        {
            pop_TreeCompanys.IsOpen = !pop_TreeCompanys.IsOpen;
        }

        private void But_Backlist_Click(object sender, RoutedEventArgs e)
        {
            _mainFrameHelper.LinkBlacklist();
        }
    }
}
