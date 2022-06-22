using ArcFaceSDK;
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
using KT.Visitor.SelfApp.Views.Common;
using KT.Visitor.SelfApp.Views.Register;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
    /// InviteReadIdCardPage.xaml 的交互逻辑
    /// </summary>
    public partial class InviteReadIdCardPage : Page
    {
        public RegisterReadIdCardPageViewModel ViewModel { get; set; }
        public bool IsCheckPhoto { get; set; }
        //public AppointmentModel Appointor { get; set; }

        private Person _person { get; set; }
        private List<VisitorInfoModel> _visitorRecords;
        private ObservableCollection<ItemsCheckViewModel> _visitReasons { get; set; }
        private string _authModelType { get; set; }
        private IReader _reader;

        private MainFrameHelper _mainFrameHelper;
        private ConfigHelper _configHelper;
        private ReaderFactory _readerFactory;
        private IVisitorApi _visitorApi;
        private IFunctionApi _functionApi;
        private PrintHandler _printHandler;
        private VistitorConfigHelper _vistitorConfigHelper;
        private ILogger _logger;
        private AppSettings _appSettings;
        private ArcIdSdkHelper _arcIdCardHelper;
        private UploadPhotoHelper _uploadPhotoHelper;
        private SelfAppSettings _selfAppSettings;

        private FaceEngine _FaceEngine;
        private FaceCheckMethod _FaceCheckMethod;

        public InviteReadIdCardPage()
        {
            InitializeComponent();

            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _mainFrameHelper = ContainerHelper.Resolve<MainFrameHelper>();
            _readerFactory = ContainerHelper.Resolve<ReaderFactory>();
            _visitorApi = ContainerHelper.Resolve<IVisitorApi>();
            _printHandler = ContainerHelper.Resolve<PrintHandler>();
            _functionApi = ContainerHelper.Resolve<IFunctionApi>();
            _vistitorConfigHelper = ContainerHelper.Resolve<VistitorConfigHelper>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _arcIdCardHelper = ContainerHelper.Resolve<ArcIdSdkHelper>();
            _uploadPhotoHelper = ContainerHelper.Resolve<UploadPhotoHelper>();
            _selfAppSettings = ContainerHelper.Resolve<SelfAppSettings>();

            ViewModel = ContainerHelper.Resolve<RegisterReadIdCardPageViewModel>();
            _FaceEngine = ContainerHelper.Resolve<FaceEngine>();
            _FaceCheckMethod = ContainerHelper.Resolve<FaceCheckMethod>();
            this.DataContext = ViewModel;

            //事件加载
            this.Loaded += Page_Loaded;
            this.Unloaded += RegisterReadIdCardPage_Unloaded;
        }

        public async Task InitAsync(List<VisitorInfoModel> visitorRecords)
        {
            //初始化数据
            var visitorConfig = await _functionApi.GetConfigParmsAsync();
            _visitReasons = _vistitorConfigHelper.SetVisitReasons(visitorConfig?.AccessReasons);

            _authModelType = visitorConfig?.AuthType;
            IsCheckPhoto = _authModelType == AuthModelEnum.FACE.Value;

            _visitorRecords = visitorRecords;
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
        }

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
            _reader = await _readerFactory.StartAsync(_configHelper.LocalConfig.Reader, SetPersonAsync, false);
        }

        private async Task SetPersonAsync(Person person)
        {
            try
            {
                await _readerFactory.DisposeReaderAsync();

                _visitorRecords = _visitorRecords.Where(x => x.Name == person.Name).ToList();
                if (_visitorRecords == null || _visitorRecords.FirstOrDefault() == null)
                {
                    throw CustomException.Run("身份证姓名与访客姓名不一致！");
                }

                string faceImage;
                //初始化数据
                var visitorConfig = await _functionApi.GetConfigParmsAsync();
                if (visitorConfig?.OpenVisitorCheck == true)
                {
                    //提取图片特征值
                    //_arcIdCardHelper.IdCardDataFeatureExtraction(person.Portrait);  //X86
                    //X64
                    _FaceCheckMethod.ChooseMultiImg(person.Portrait);  //  证件照
                    //X64
                    //拍照
                    faceImage = await TakePictureAsync(true);
                }
                else
                {
                    //拍照 
                    faceImage = await TakePictureAsync(false);
                }

                Appoint(faceImage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "邀约验证失败！");
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ContainerHelper.Resolve<OperateErrorPage>().ShowMessage(ex.Message);
                });
            }
        }

        private int _takePictureRetimes = 0;
        private async Task<string> TakePictureAsync(bool openVisitorCheck)
        {
            try
            {
                //拍照
                var bitmap = TakePictureOperation();
                //X64
                _FaceCheckMethod.ChooseImg(bitmap);
                bool Flg = _FaceCheckMethod.CheckFaceFlg();
                if (openVisitorCheck)
                {
                    if (!Flg)
                    {
                        throw CustomException.Run($"人证核验失败，相似度：{_FaceCheckMethod.CheckValue}% ");
                    }
                    bitmap = new Bitmap(_FaceCheckMethod.byteArrayToImage(_FaceCheckMethod.Rightimg.feature));
                }
                /*else
                {
                    bitmap = new Bitmap(_FaceCheckMethod.byteArrayToImage(_FaceCheckMethod.Rightimg.feature)); 
                }*/
                //X64
                /*
                if (openVisitorCheck)
                {
                    //人证比对
                    var compareResult = _arcIdCardHelper.FaceDataIdCardCompare(true, bitmap, true);
                    if (!compareResult.IsSuccess)
                    {
                        throw CustomException.Run($"{compareResult.Message}，相似度：{compareResult.Similarity}% ");
                    }
                    if (compareResult.Face != null)
                    {
                        bitmap = compareResult.Face;
                    }
                }
                else
                {
                    var cutFace = _arcIdCardHelper.GetCutFace(bitmap);
                    if (cutFace.Face != null)
                    {
                        bitmap = cutFace.Face;
                    }
                }
                */
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
                    return await TakePictureAsync(openVisitorCheck);
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

                captureElement.Measure(captureElement.RenderSize);
                captureElement.Arrange(new Rect(captureElement.RenderSize));
                bmp.Render(captureElement);

                //captureElement.Pause();

                var imageSource = BitmapFrame.Create(bmp);
                return ImageConvert.ImageSourceToBitmap(imageSource);
            });
        }

        //开始授权 
        private void Appoint(string imageUrl)
        {
            var appoint = new AuthInfoModel();
            foreach (var item in _visitorRecords)
            {
                var visitor = new AuthVisitorModel();
                visitor.Id = item.Id;
                visitor.Ic = item.IcCard;
                visitor.FaceImg = imageUrl;

                //visitor.Name = person.Name;
                //visitor.IdType = person.CardType;
                //visitor.IdNumber = person.IdCode;
                //visitor.Gender = person.Gender;

                appoint.Visitors.Add(visitor);
            }

            MaskTipBox.Run(appoint, RunSubmitAsync, SuccessSubmit, ErrorSubmit);
        }

        private void ErrorSubmit(Exception ex)
        {
            _logger.LogError($"预约失败：{ex} ");
            var exception = ex.GetInner();
            Dispatcher.Invoke(() =>
            {
                var UI = ContainerHelper.Resolve<OperateErrorPage>();
                UI.ViewModel.ErrorMsg = $"预约失败，请重新操作：{exception.Message}";
                UI.ViewModel.Title = "操作提示";
                _mainFrameHelper.Link(UI, false);
            });
        }

        private void SuccessSubmit(List<RegisterResultModel> results, AuthInfoModel appoint)
        {
            Dispatcher.Invoke(() =>
            {
                //跳转到成功页面
                var successPage = ContainerHelper.Resolve<OperateSucessPage>();
                successPage.ViewModel.Init(results);
                successPage.LoadedAction += () =>
                {
                    // 异步 打印二维码 
                    _printHandler.StartPrintAsync(results, true);
                };
                _mainFrameHelper.Link(successPage, false);
            });
        }

        private async Task<List<RegisterResultModel>> RunSubmitAsync(AuthInfoModel appoint)
        {
            //后台提交预约登录
            var results = await _visitorApi.AuthAsync(appoint);
            return results;
        }

        private async void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            await _readerFactory.DisposeReaderAsync();
        }
    }
}
