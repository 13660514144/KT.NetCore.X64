using ArcFaceSDK.Mothed;
using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.Utils;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.Settings;
using KT.Visitor.Common.Tools.ArcIdMatch.ArcIdSdk.Helpers;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Common.Views.Helper;
using KT.Visitor.Data.Enums;
using KT.Visitor.IdReader;
using KT.Visitor.IdReader.Common;
using KT.Visitor.SelfApp.Helpers;
using KT.Visitor.SelfApp.Register;
using KT.Visitor.SelfApp.Views.Register;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFMediaKit.DirectShow.Controls;

namespace KT.Visitor.SelfApp.Public
{
    /// <summary>
    /// RegisterReadIdCardPage.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterReadIdCardPage : Page
    {
        public RegisterReadIdCardPageViewModel ViewModel { get; set; }
        public AppointmentModel Appointor { get; set; }
        private ObservableCollection<ItemsCheckViewModel> _visitReasons { get; set; }
        private string _authModelType { get; set; }
        private IReader _reader;
        public bool IsCheckPhoto { get; set; }

        private MainFrameHelper _mainFrameHelper;
        private ConfigHelper _configHelper;
        private ReaderFactory _readerFactory;
        private IFunctionApi _functionApi;
        private VistitorConfigHelper _vistitorConfigHelper;
        private ILogger _logger;
        private AppSettings _appSettings;
        private ArcIdSdkHelper _arcIdCardHelper;
        private UploadPhotoHelper _uploadPhotoHelper;
        private SelfAppSettings _selfAppSettings;
        private FaceCheckMethod _FaceCheckMethod;
        public RegisterReadIdCardPage()
        {
            InitializeComponent();

            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _mainFrameHelper = ContainerHelper.Resolve<MainFrameHelper>();
            _readerFactory = ContainerHelper.Resolve<ReaderFactory>();
            _functionApi = ContainerHelper.Resolve<IFunctionApi>();
            _vistitorConfigHelper = ContainerHelper.Resolve<VistitorConfigHelper>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _arcIdCardHelper = ContainerHelper.Resolve<ArcIdSdkHelper>();
            _uploadPhotoHelper = ContainerHelper.Resolve<UploadPhotoHelper>();
            _selfAppSettings = ContainerHelper.Resolve<SelfAppSettings>();
            _FaceCheckMethod = ContainerHelper.Resolve<FaceCheckMethod>();
            ViewModel = ContainerHelper.Resolve<RegisterReadIdCardPageViewModel>();

            _ = InitAsync();

            this.DataContext = ViewModel;

            //事件加载
            this.Loaded += Page_Loaded;
            this.Unloaded += RegisterReadIdCardPage_Unloaded;
        }

        private async Task InitAsync()
        {
            //初始化数据
            var visitorConfig = await _functionApi.GetConfigParmsAsync();
            _visitReasons = _vistitorConfigHelper.SetVisitReasons(visitorConfig?.AccessReasons);
            _authModelType = visitorConfig?.AuthType;

            IsCheckPhoto = _authModelType == AuthModelEnum.FACE.Value;
        }

        private async void Img_exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            await _readerFactory.DisposeReaderAsync();
            var UI = ContainerHelper.Resolve<HomePage>();
            _mainFrameHelper.Link(UI);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //相机
            InitCamera();

            ////身份证阅读器动画效果
            //InitAnimation();
        }

        //private void InitAnimation()
        //{
        //    var HeightAnimation = new DoubleAnimation()
        //    {
        //        From = 0,
        //        To = 360,
        //        Duration = TimeSpan.FromSeconds(3),
        //        AutoReverse = true,
        //        RepeatBehavior = RepeatBehavior.Forever,
        //    };
        //    var opictyAn = new DoubleAnimation()
        //    {
        //        From = 0.1,
        //        To = 0.8,
        //        Duration = TimeSpan.FromSeconds(2),
        //        AutoReverse = true,
        //        RepeatBehavior = RepeatBehavior.Forever
        //    };

        //    txb_am.BeginAnimation(HeightProperty, HeightAnimation);
        //    txb_am.BeginAnimation(OpacityProperty, opictyAn);
        //}

        private async void RegisterReadIdCardPage_Unloaded(object sender, RoutedEventArgs e)
        {
            await _readerFactory.DisposeReaderAsync();

            captureElement.MediaOpened -= CaptureElement_MediaOpened;
            captureElement.Stop();
            captureElement.VideoCaptureSource = null;
        }

        public void InitCamera()
        {
            _logger.LogInformation($"正在开启相机：index:{_appSettings.CameraIndex} names:{MultimediaUtil.VideoInputNames.ToList().RightDecorates("，")} ");
            if (MultimediaUtil.VideoInputNames.Length > _appSettings.CameraIndex)
            {
                // 第0个摄像头为默认摄像头
                captureElement.VideoCaptureSource = MultimediaUtil.VideoInputNames[_appSettings.CameraIndex];
                captureElement.Play();
                captureElement.MediaOpened += CaptureElement_MediaOpened;
            }
            else
            {
                MessageBox.Show($"获取可用摄像头出错：count:{MultimediaUtil.VideoInputNames.Length} index:{_appSettings.CameraIndex} ");
            }
        }

        private void CaptureElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            InitReaderAsync();
        }

        /// <summary>
        /// 异步 初始化读卡器对像
        /// </summary>
        private async void InitReaderAsync()
        {
            // 开启证件阅读器延迟，等摄像头初始化完成后才能开启，否则阅读证件后无法正常拍照
            await Task.Delay((int)(_selfAppSettings.IdReaderStartDelaySecondTime * 1000));
            //初始化身份证阅读器对象 
            try
            {
                _reader = await _readerFactory.StartAsync(_configHelper.LocalConfig.Reader, SetPersonAsync, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化身份证阅读器失败!!!");
                _logger.LogInformation($"初始化身份证阅读器失败:{ex.Message}");
            }
        }

        private bool _faceChecking = false;
        private async Task SetPersonAsync(Person person)
        {
            if (_faceChecking)
            {
                return;
            }
            _faceChecking = true;

            try
            {
                Appointor.Name = person.Name;
                Appointor.IdType = person.CardType;
                Appointor.IdNumber = person.IdCode;
                Appointor.Gender = person.Gender;

                //初始化数据
                var visitorConfig = await _functionApi.GetConfigParmsAsync();
                if (visitorConfig?.OpenVisitorCheck == true)
                {
                    //拍照
                    Appointor.FaceImg = await TakePictureAsync(person.Portrait);
                }
                else
                {
                    //拍照
                    Appointor.FaceImg = await TakePictureAsync(null);
                }

                await _readerFactory.DisposeReaderAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"人脸检测对比失败：{ex} ");
                throw;
            }
            finally
            {
                _faceChecking = false;
            }

            //跳转到下一步
            Dispatcher.Invoke(() =>
            {
                var page = ContainerHelper.Resolve<VisitorRegisterPage>();
                page.Appointor = Appointor;
                page.ViewModel.AuthModelType = _authModelType;
                page.ViewModel.VisitReasons = _visitReasons;

                _mainFrameHelper.Link(page);
            });
        }

        private int _takePictureRetimes = 0;
        private async Task<string> TakePictureAsync(System.Drawing.Image portrait)
        {
            try
            {
                //拍照
                var bitmap = TakePictureOperation();

                if (portrait != null)
                {
                    //提取图片特征值
                    //_arcIdCardHelper.IdCardDataFeatureExtraction(portrait); //x86
                    //X64
                    _FaceCheckMethod.ChooseMultiImg(portrait);//身份证
                    //X64
                    //人证比对
                    //X64
                    _FaceCheckMethod.ChooseImg(bitmap);//拍照
                    bool Flg = _FaceCheckMethod.CheckFaceFlg();
                    if (!Flg)
                    {
                        throw CustomException.Run($"人证核验失败，相似度：{_FaceCheckMethod.CheckValue}% ");
                    }
                    bitmap = new Bitmap("PhotoFace.jpg");
                    //x64
                    //X86
                    /*
                    var compareResult = _arcIdCardHelper.FaceDataIdCardCompare(false, bitmap, true);
                    if (!compareResult.IsSuccess)
                    {
                        throw CustomException.Run($"{compareResult.Message}，相似度：{compareResult.Similarity}% ");
                    }
                    if (compareResult.Face != null)
                    {
                        bitmap = compareResult.Face;
                    }
                    */
                }
                else
                {
                    /*var cutFace = _arcIdCardHelper.GetCutFace(bitmap);
                    if (cutFace.Face != null)
                    {
                        bitmap = cutFace.Face;
                    }*/
                }

                //上传图头像服务器
                /* is face check true */
                var imageUrl = await _uploadPhotoHelper.UploadPortraitAsync(bitmap, true);

                ViewModel.TakePhotoMessage = string.Empty;
                return imageUrl;
            }
            catch (Exception ex)
            {
                _takePictureRetimes++;
                if (_takePictureRetimes <= 20)
                {
                    ViewModel.TakePhotoMessage = $"{ex.Message} ({_takePictureRetimes}次)";

                    await Task.Delay(3 * 1000);
                    return await TakePictureAsync(portrait);
                }
                else
                {
                    _logger.LogInformation("获取头像结束：ex:{0} times:{1} ", ex.Message, _takePictureRetimes);

                    await _readerFactory.DisposeReaderAsync();
                    throw ex;
                }
            }
        }

        private Bitmap TakePictureOperation()
        {
            return Dispatcher.Invoke(() =>
            {
                //怎么抓取高清的原始图像
                RenderTargetBitmap bmp = new RenderTargetBitmap(
                     (int)captureElement.ActualWidth,
                     (int)captureElement.ActualHeight,
                     96, 96, PixelFormats.Default);

                //RenderTargetBitmap bmp = new RenderTargetBitmap(
                //     (int)captureElement.ActualWidth,
                //     (int)captureElement.ActualHeight,
                //     128, 128, PixelFormats.Default);
                captureElement.Measure(captureElement.RenderSize);
                captureElement.Arrange(new Rect(captureElement.RenderSize));
                bmp.Render(captureElement);

                //captureElement.Pause();

                var imageSource = BitmapFrame.Create(bmp);
                return ImageConvert.ImageSourceToBitmap(imageSource);
            });
        }
    }
}
