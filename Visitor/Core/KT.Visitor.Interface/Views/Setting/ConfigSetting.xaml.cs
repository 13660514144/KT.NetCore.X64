using KT.Visitor.Data.Enums;
using KT.Visitor.Data.IServices;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace KT.Visitor.Interface.Views.Setting
{
    /// <summary>
    /// pConfig.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigSetting : Page
    {
        private ISystemConfigDataService _systemConfigDataService;
        private ConfigHelper _configHelper;
        private MainFrameHelper _mainFrameHelper;

        public ConfigSetting(ISystemConfigDataService systemConfigDataService,
            ConfigHelper configHelper,
           MainFrameHelper mainFrameHelper)
        {
            InitializeComponent();
            _systemConfigDataService = systemConfigDataService;
            _configHelper = configHelper;
            _mainFrameHelper = mainFrameHelper;
        }

        private void Btn_LoadImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";//注意这里写路径时要用c:\\而不是c:\
            openFileDialog.Filter = "文本文件|*.*|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            string fName = "";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            fName = openFileDialog.FileName;
            var file = openFileDialog.OpenFile();
            var fileSize = Math.Ceiling(file.Length / 1024.0);

            if (fileSize > _configHelper.LocalConfig.UploadFileSize)
            {
                MessageWarnBox.Show("上传的图片不能大于100kb，请重新上传！");
                return;
            }

            //Uri(“图片路径“)    
            if (File.Exists(fName))
            {
                var imagesouce = new BitmapImage(new Uri(fName));
                img_logo.Source = imagesouce;
                string destimg = AppDomain.CurrentDomain.BaseDirectory + "Files/Images/logoWhite.png";
                File.Copy(fName, destimg, true);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txt_name.Text = _configHelper.LocalConfig.SystemName;
            //Uri(“图片路径“)
            string fileUrl = AppDomain.CurrentDomain.BaseDirectory + "Files/Images/logoWhite.png";
            string fileTempUrl = AppDomain.CurrentDomain.BaseDirectory + "Files/Images/temp_logo.png";
            if (File.Exists(fileUrl))
            {
                //复制临时文件打开
                File.Copy(fileUrl, fileTempUrl, true);
                img_logo.Source = new BitmapImage(new Uri(fileTempUrl));
            }
        }
        public BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage bit3 = new BitmapImage();
            bit3.BeginInit();
            bit3.StreamSource = ms;
            bit3.EndInit();
            ms.Close();
            return bit3;
        }

        public static System.Drawing.Bitmap Base64ToImage(string photo)
        {
            //将Base64String转为图片并保存
            byte[] arr2 = Convert.FromBase64String(photo);
            using (MemoryStream ms2 = new MemoryStream(arr2))
            {
                System.Drawing.Bitmap bmp2 = new System.Drawing.Bitmap(ms2);
                return bmp2;
            }
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            _ = SaveAsync();
        }

        private async Task SaveAsync()
        {
            if (txt_name.Text.Length == 0)
            {
                MessageWarnBox.Show("系统名不能为空");
                return;
            }
            _configHelper.LocalConfig.SystemName = txt_name.Text;
            await _systemConfigDataService.AddOrUpdateAsync(SystemConfigEnum.SYSTEM_NAME, _configHelper.LocalConfig.SystemName);

            MessageWarnBox.Show("设置成功");
        }

        private void btn_def_Click(object sender, RoutedEventArgs e)
        {
            _ = ResetAsync();
        }

        private async Task ResetAsync()
        {
            //框中使用默认图片 
            string def_path = AppDomain.CurrentDomain.BaseDirectory + "Files/Images/def_logo.png";
            var imagesouce = new BitmapImage(new Uri(def_path));
            img_logo.Source = imagesouce;

            //删除上传的图片
            string destimg = AppDomain.CurrentDomain.BaseDirectory + "Files/Images/logoWhite.png";
            if (File.Exists(destimg))
            {
                File.Delete(destimg);
            }

            _configHelper.LocalConfig.SystemName = "前台访客登记系统";
            await _systemConfigDataService.AddOrUpdateAsync(SystemConfigEnum.SYSTEM_NAME, _configHelper.LocalConfig.SystemName);
            txt_name.Text = _configHelper.LocalConfig.SystemName;

            MessageWarnBox.Show("设置成功");
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            _mainFrameHelper.LinkVisitorRegister();
        }
    }

}
