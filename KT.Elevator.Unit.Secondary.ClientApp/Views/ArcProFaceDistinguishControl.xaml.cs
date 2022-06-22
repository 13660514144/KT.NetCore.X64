using AForge.Video.DirectShow;
using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.Utils;
using KT.Elevator.Unit.Entity.Models;
using KT.Elevator.Unit.Secondary.ClientApp.Events;
using KT.Elevator.Unit.Secondary.ClientApp.Service.FaceHelpers.ArcFacePro;
using KT.Elevator.Unit.Secondary.ClientApp.Service.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Quanta.Common.Enums;
using KT.Unit.Face.Arc.Pro.Entity;
using KT.Unit.Face.Arc.Pro.Models;
using KT.Unit.Face.Arc.Pro.SDKModels;
using KT.Unit.Face.Arc.Pro.Utils;
using KT.Unit.FaceRecognition.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace KT.Elevator.Unit.Secondary.ClientApp.Views
{
    /// <summary>
    /// ArcProFaceDistinguishControl.xaml 的交互逻辑
    /// </summary>
    public partial class ArcProFaceDistinguishControl : System.Windows.Controls.UserControl
    {
        #region 参数定义
        /// <summary>
        /// 图片最大大小
        /// </summary>
        private long maxSize = 1024 * 1024 * 2;

        /// <summary>
        /// 是否是双目摄像
        /// </summary>
        private bool _isDoubleShot = false;

        #region 视频模式下相关 
        /// <summary>
        /// 视频输入设备信息
        /// </summary>
        private FilterInfoCollection _filterInfoCollection;
        /// <summary>
        /// RGB摄像头设备
        /// </summary>
        private VideoCaptureDevice _rgbDeviceVideo;
        /// <summary>
        /// IR摄像头设备
        /// </summary>
        private VideoCaptureDevice _irDeviceVideo;
        /// <summary>
        /// 关闭FR线程开关
        /// </summary>
        private bool _exitVideoRGBFR = false;

        /// <summary>
        /// 关闭活体线程开关
        /// </summary>
        private bool _exitVideoRGBLiveness = false;
        /// <summary>
        /// 关闭IR活体和FR线程线程开关
        /// </summary>
        private bool _exitVideoIRFRLiveness = false;
        #endregion

        /// <summary>
        /// 是否已发送人脸输入事件
        /// </summary>
        private bool _isPublishFaceCome = false;
        #endregion

        #region 初始化
        public ArcProFaceDistinguishControlViewModel ViewModel { get; private set; }

        private IFaceProvider _faceProvider;

        private ILogger _logger;
        private IEventAggregator _eventAggregator;
        private ConfigHelper _configHelper;
        private KoalaFaceApi _koalaFaceApi;
        private AppSettings _appSettings;
        private readonly ArcFaceSettings _arcFaceSettings;
        private readonly FaceRecognitionAppSettings _faceRecognitionAppSettings;
        private readonly FaceFactory _faceFactory;
        private DateTime Trecord=DateTime.Now; //开始检车人脸 30秒没有人脸检测，关闭屏幕
        private string dir = AppDomain.CurrentDomain.BaseDirectory;
        Process process1;
        public ArcProFaceDistinguishControl()
        {
            InitializeComponent();

            _logger = ContainerHelper.Resolve<ILogger>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _koalaFaceApi = ContainerHelper.Resolve<KoalaFaceApi>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _faceRecognitionAppSettings = ContainerHelper.Resolve<FaceRecognitionAppSettings>();
            _arcFaceSettings = _faceRecognitionAppSettings.ArcProFaceSettings;
            _faceFactory = ContainerHelper.Resolve<FaceFactory>();
            Trecord = DateTime.Now;
            ViewModel = ContainerHelper.Resolve<ArcProFaceDistinguishControlViewModel>();

            this.DataContext = ViewModel;

            this.Loaded += ArcProFaceDistinguishControl_Loaded;
            this.Unloaded += ArcProFaceDistinguishControl_Unloaded;

            rgbVideoSource.Paint += RgbVideoSource_Paint;
            rgbVideoSource.PlayingFinished += RgbVideoSource_PlayingFinished;
            irVideoSource.Paint += IrVideoSource_Paint;

            System.Timers.Timer t = new System.Timers.Timer(120000);
            t.Elapsed += new System.Timers.ElapsedEventHandler(callbackStream);
            //设置是执行一次（false）还是一直执行(true)；   
            t.AutoReset = true;
            //是否执行System.Timers.Timer.Elapsed事件；  
            t.Enabled = true;
            t.Start();
        }

        public void Clear()
        {
            _isPublishFaceCome = false;
        }

        private void ArcProFaceDistinguishControl_Loaded(object sender, RoutedEventArgs e)
        {
            //摄像头初始化
            //InitVideo(); 2022-03-13
            //开启摄像头
            StartCamera();
        }
        public void callbackStream(object source, System.Timers.ElapsedEventArgs e)
        {
            System.TimeSpan nd = DateTime.Now - Trecord;//计算时间差，60秒内无人脸，重置一下摄像头，防止莫名奇妙黑屏
           
            if (nd.TotalSeconds > 60)
            {

                /*string strCheck = $"{dir}\\ScreenLight.exe";
                process1 = CreateProcessTasks(strCheck, "0", true);
                process1.Start();*/

                _logger.LogInformation($"摄像头：restart ");
                CloseCamera();
                Thread.Sleep(50);
                //InitVideo();
                StartCamera();
            }
            
        }
        /// <summary>
        /// 摄像头初始化
        /// </summary>
        private void InitVideo()
        {
            _filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (_filterInfoCollection.Count > 0)
            {
                for (int i = 0; i < _filterInfoCollection.Count; i++)
                {
                    _logger.LogInformation($"摄像头：index:{0} name:{_filterInfoCollection[i].Name} ");
                }
            }
        }
        #endregion

        #region 视频检测相关(<摄像头按钮点击事件、摄像头Paint事件、特征比对、摄像头播放完成事件>)
        /// <summary>
        /// 摄像头按钮点击事件
        /// </summary>
        private void StartCamera()
        {
            //在点击开始的时候再坐下初始化检测，防止程序启动时有摄像头，在点击摄像头按钮之前将摄像头拔掉的情况
            InitVideo();
            //必须保证有可用摄像头
            if (_filterInfoCollection.Count == 0)
            {
                _logger.LogError("未检测到摄像头，请确保已安装摄像头或驱动!");
                return;
            }

            //获取filterInfoCollection的总数
            int maxCameraCount = _filterInfoCollection.Count;

            //if (!rgbVideoSource.IsRunning)
            //{
            //    //仅打开RGB摄像头，IR摄像头控件隐藏
            //    rgbDeviceVideo = new VideoCaptureDevice(filterInfoCollection[_arcFaceSettings.RgbCameraIndex <= maxCameraCount ? _arcFaceSettings.RgbCameraIndex : 0].MonikerString);
            //    rgbDeviceVideo.VideoResolution = rgbDeviceVideo.VideoCapabilities[0];
            //    rgbVideoSource.VideoSource = rgbDeviceVideo;
            //    rgbVideoSource.Start();
            //}

            //如果配置了两个不同的摄像头索引
            if (_arcFaceSettings.RgbCameraIndex != _arcFaceSettings.IrCameraIndex && maxCameraCount >= 2)
            {
                //“选择识别图”、“开始匹配”按钮禁用，阈值控件可用，显示摄像头控件
                rgbVideoSource.Show();
                irVideoSource.Show();

                //RGB摄像头加载
                var rgbIndex = _filterInfoCollection[_arcFaceSettings.RgbCameraIndex < maxCameraCount ? _arcFaceSettings.RgbCameraIndex : 0].MonikerString;
                _logger.LogInformation($"打开RGB摄像头：index:{rgbIndex} ");

                _rgbDeviceVideo = new VideoCaptureDevice(rgbIndex);
                _rgbDeviceVideo.VideoResolution = _rgbDeviceVideo.VideoCapabilities[0];
                rgbVideoSource.VideoSource = _rgbDeviceVideo;
                rgbVideoSource.Start();

                //IR摄像头 
                var irIndex = _filterInfoCollection[_arcFaceSettings.IrCameraIndex < maxCameraCount ? _arcFaceSettings.IrCameraIndex : 0].MonikerString;
                _logger.LogInformation($"打开IR摄像头：index:{rgbIndex} ");

                _irDeviceVideo = new VideoCaptureDevice(irIndex);
                _irDeviceVideo.VideoResolution = _irDeviceVideo.VideoCapabilities[0];
                irVideoSource.VideoSource = _irDeviceVideo;
                irVideoSource.Start();
                //双摄标志设为true
                _isDoubleShot = true;
                //启动检测线程
                _exitVideoIRFRLiveness = false;
            }
            else
            {
                //“选择识别图”、“开始匹配”按钮禁用，阈值控件可用，显示摄像头控件
                rgbVideoSource.Show();

                //仅打开RGB摄像头，IR摄像头控件隐藏
                var rgbIndex = _filterInfoCollection[_arcFaceSettings.RgbCameraIndex <= maxCameraCount ? _arcFaceSettings.RgbCameraIndex : 0].MonikerString;
                _logger.LogInformation($"打开RGB摄像头：index:{rgbIndex} ");

                _rgbDeviceVideo = new VideoCaptureDevice(rgbIndex);
                _rgbDeviceVideo.VideoResolution = _rgbDeviceVideo.VideoCapabilities[0];
                rgbVideoSource.VideoSource = _rgbDeviceVideo;
                rgbVideoSource.Start();
                irVideoSource.Hide();
                irVideoHost.Visibility = Visibility.Hidden;
                //双摄标志设为false
                _isDoubleShot = false;
            }
            //启动两个检测线程
            _exitVideoRGBFR = false;
            _exitVideoRGBLiveness = false;
            //IsCanCheck = true;
        }

        private Font font = new Font(System.Drawing.FontFamily.GenericSerif, 10f, System.Drawing.FontStyle.Bold);
        private SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        private SolidBrush blueBrush = new SolidBrush(Color.Blue);
        private bool _isRGBLock = false;
        private MRECT _allRect = new MRECT();
        private object _rectLock = new object();
        private int _errorTimes = 0;

        private bool isIRLock = false;
        private FaceTrackUnit trackIRUnit = new FaceTrackUnit();
        /// <summary>
        /// 是否可检查人脸
        /// </summary>
        public bool IsCanCheck = true;

        /// <summary>
        /// RGB摄像头Paint事件，图像显示到窗体上，得到每一帧图像，并进行处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RgbVideoSource_Paint(object sender, PaintEventArgs e)
        {
            if (!rgbVideoSource.IsRunning)
            {
                return;
            }
            try
            {
                CheckFace(e);
            }
            catch (Exception ex)
            {
                IsCanCheck = true;
                _logger.LogError($"人脸识别失败：ex:{ex} ");
            }
        }
        private Process CreateProcessTasks(string exe_path, string str_arguments, bool b_create_win = true)
        {
            Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = exe_path;
            process.StartInfo.Arguments = str_arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = false;  //true
            process.StartInfo.RedirectStandardOutput = false;  //true
            process.StartInfo.RedirectStandardError = false;
            process.StartInfo.CreateNoWindow = b_create_win;
            return process;
        }
        private void CheckFace(PaintEventArgs e)
        {
            /*这里可以左屏幕开关*/
            //Trecord = DateTime.Now;//记录人脸当前检测时间
           
            if (_faceProvider == null)
            {
                _faceProvider = _faceFactory.GetRandomProvider();
                return;
            }

            //得到当前RGB摄像头下的图片
            Image image = rgbVideoSource.GetCurrentVideoFrame();
            if (image == null)
            {
                return;
            }
            //调整图像宽度，需要宽度为4的倍数
            if (image.Width % 4 != 0)
            {
                image = Common.Core.Utils.ImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
            }
            //检测人脸，得到Rect框
            MultiFaceInfo multiFaceInfo = FaceUtil.DetectFaceAndLandMark(_faceProvider.FaceEngineProvider.VideoEngine, image);
            //得到最大人脸
            SingleFaceInfo maxFace = FaceUtil.GetMaxFace(multiFaceInfo);
            //得到Rect
            MRECT rect = maxFace.faceRect;
            //检测RGB摄像头下最大人脸
            Graphics g = e.Graphics;
            float offsetX = rgbVideoSource.Width * 1f / image.Width;
            float offsetY = rgbVideoSource.Height * 1f / image.Height;
            float x = rect.left * offsetX;
            float width = rect.right * offsetX - x;
            float y = rect.top * offsetY;
            float height = rect.bottom * offsetY - y;

            if (!IsCanCheck)
            {
                return;
            }

            int area = (rect.right - rect.left) * (rect.bottom - rect.top);
            if (area > _arcFaceSettings.FaceMinArea)
            {
                //根据Rect进行画框
                g.DrawRectangle(Pens.Blue, x, y, width, height);
            }
            else
            {
                //根据Rect进行画框
                g.DrawRectangle(Pens.Red, x, y, width, height);

                lock (_rectLock)
                {
                    _allRect.left = (int)(rect.left * offsetX);
                    _allRect.top = (int)(rect.top * offsetY);
                    _allRect.right = (int)(rect.right * offsetX);
                    _allRect.bottom = (int)(rect.bottom * offsetY);
                }

                return;
            }

            //保证只检测一帧，防止页面卡顿以及出现其他内存被占用情况
            if (_isRGBLock)
            {
                return;
            }

            _isRGBLock = true;
            //异步处理提取特征值和比对，不然页面会比较卡
            Task.Run(async () =>
            {
                if (rect.left == 0 || rect.right == 0 || rect.top == 0 || rect.bottom == 0)
                {
                    lock (_rectLock)
                    {
                        _allRect.left = 0;
                        _allRect.top = 0;
                        _allRect.right = 0;
                        _allRect.bottom = 0;
                    }

                    _isRGBLock = false;

                    return;
                }

                _logger.LogInformation("人脸输入！");
                //人脸输入，显示刷脸页面
                if (!_isPublishFaceCome)
                {
                    _eventAggregator.GetEvent<FaceComeEvent>().Publish();
                    _isPublishFaceCome = true;
                }

                try
                {
                    lock (_rectLock)
                    {
                        _allRect.left = (int)(rect.left * offsetX);
                        _allRect.top = (int)(rect.top * offsetY);
                        _allRect.right = (int)(rect.right * offsetX);
                        _allRect.bottom = (int)(rect.bottom * offsetY);
                    }

                    if (_arcFaceSettings.IsCheckLiveness)
                    {

                        bool isLiveness = false;

                        ////调整图片数据，非常重要
                        //ImageInfo imageInfo = ArcImageUtil.ReadBMP(bitmap);
                        //if (imageInfo == null)
                        //{
                        //    _isRGBLock = false;

                        //    return;
                        //}

                        //调整图像宽度，需要宽度为4的倍数 
                        int retCode_Liveness = -1;
                        //RGB活体检测
                        LivenessInfo liveInfo = FaceUtil.LivenessInfo_RGB(_faceProvider.FaceEngineProvider.VideoRGBImageEngine,
                                                                               image,
                                                                               maxFace,
                                                                               out retCode_Liveness);
                        //判断检测结果
                        if (retCode_Liveness == 0 && liveInfo.num > 0)
                        {
                            int isLive = liveInfo.isLive[0];
                            isLiveness = (isLive == 1);
                        }
                        //if (imageInfo != null)
                        //{
                        //    MemoryUtil.Free(imageInfo.imgData);
                        //}

                        if (!isLiveness)
                        {
                            ////验证未通过 
                            _logger.LogInformation("验证未通过：RGB假体！");
                            _isRGBLock = false;

                            return;
                        }

                        //双目活体
                        if (_isDoubleShot && (_lastLiveTime + (_arcFaceSettings.LiveDetectAbleSecondTime * 1000)) < DateTimeUtil.UtcNowMillis())
                        {
                            _logger.LogInformation("验证未通过：IR假体！");
                            _isRGBLock = false;

                            return;
                        }
                    }

                    var passRightResult = new PassRightFaceCheckResultModel();
                    if (_faceRecognitionAppSettings.FaceRecognitionType == FaceRecognitionTypeEnum.Koala.Value)
                    {
                        //在线匹对KoalaFaceApi
                        var bitmap = new Bitmap(image);
                        var result = await _koalaFaceApi.PushRecordAsync(_faceRecognitionAppSettings.KoalaFaceSettings.CheckUrl, bitmap, rect.left, rect.top, rect.right, rect.bottom);

                        passRightResult.IsSuccess = result?.Data?.PersonId > 0;
                        passRightResult.Sign = result?.Data?.PersonId.ToString();
                    }
                    else if (_faceRecognitionAppSettings.FaceRecognitionType == FaceRecognitionTypeEnum.ArcPro40.Value)
                    {
                        //提取人脸特征
                        var faceFullFeature = _faceProvider.GetFaceFeatureBytes(image);
                        if (faceFullFeature == null || faceFullFeature.FaceFeature == null || faceFullFeature.FaceFeature.featureSize == 0)
                        {
                            _logger.LogWarning("图片未取得到特征值！");
                            return;
                        }
                        var feature = faceFullFeature.FaceFeature;
                        //得到比对结果
                        var result = await _faceFactory.CompareFeatureAsync(feature, faceFullFeature.IsMask);

                        passRightResult.IsSuccess = result != null;
                        passRightResult.Id = result?.FacePassRight.Id;
                        passRightResult.Sign = result?.FacePassRight.Sign;
                    }
                    else
                    {
                        _logger.LogError($"人脸识别类型出错：{_faceRecognitionAppSettings.FaceRecognitionType} {_faceRecognitionAppSettings.FaceAuxiliaryType}");
                        return;
                    }
                    Trecord = DateTime.Now;//记录人脸当前检测时间
                    /* //打开显示器
                     string strCheck = $"{dir}\\ScreenLight.exe";
                    process1 = CreateProcessTasks(strCheck, "1", true);
                    process1.Start();*/
                    //事件上传数据
                    var handleElevator = new UnitHandleElevatorModel();
                    handleElevator.AccessType = AccessTypeEnum.FACE.Value;
                    handleElevator.DeviceType = CardDeviceTypeEnum.FACE_CAMERA.Value;
                    handleElevator.HandleElevatorDeviceId = _configHelper.LocalConfig.HandleElevatorDeviceId;
                    //handleElevator.SourceFloorId = _configHelper.LocalConfig.DeviceFloorId;
                    //派梯设备Id不存二次派梯一体机，上传数据时从服务端获取
                    handleElevator.DeviceId = string.Empty;
                    handleElevator.PassTime = DateTimeUtil.UtcNowMillis();

                    //获取通行抓拍人脸图片  
                    if (image.Width > 1536 || image.Height > 1536)
                    {
                        image = ArcImageUtil.ScaleImage(image, 1536, 1536);
                    }
                    handleElevator.FaceImage = ImageConvert.ImageToBytes(image);
                    handleElevator.FaceImageSize = handleElevator.FaceImage.Length;

                    if (passRightResult.IsSuccess)
                    {
                        _logger.LogInformation($"人脸对比结果：result:{ JsonConvert.SerializeObject(passRightResult, JsonUtil.JsonPrintSettings) } ");

                        //人脸权限  
                        handleElevator.HandleElevatorRight.PassRightId = passRightResult.Id;
                        handleElevator.HandleElevatorRight.PassRightSign = passRightResult.Sign;
                        handleElevator.Sign = passRightResult.Sign;

                        Dispatcher.Invoke(() =>
                        {
                            IsCanCheck = false;
                            //人脸授权成功
                            _eventAggregator.GetEvent<CardInputedEvent>().Publish(handleElevator);
                        });
                    }
                    else
                    {
                        _logger.LogInformation($"人脸对比结果：result:{ JsonConvert.SerializeObject(passRightResult, JsonUtil.JsonPrintSettings) } ");
                        _logger.LogInformation("验证未通过：找不到匹配人脸的结果，请求陌生人权限！");
                        //在线鉴权中，考拉没有人脸，依然做陌生人请求权限
                        //人脸授权未匹配
                        handleElevator.HandleElevatorRight.PassRightId = "0";
                        handleElevator.Sign = "000000";
                        handleElevator.HandleElevatorRight.PassRightSign = "000000";
                        Dispatcher.Invoke(() =>
                        {
                            IsCanCheck = false;
                            _eventAggregator.GetEvent<CardInputedEvent>().Publish(handleElevator);
                        });
                        #region
                        /*if (_errorTimes < _arcFaceSettings.FaceErrorRecheckTimes)
                        {
                            await Task.Delay((int)(_arcFaceSettings.FaceErrorRetryDelaySecondTime * 1000));
                            _errorTimes++;
                        }
                        else
                        {
                            _errorTimes = 0;
                            Dispatcher.Invoke(() =>
                            {
                                IsCanCheck = false;
                                //人脸授权未匹配
                                handleElevator.Sign = "0000";
                                handleElevator.HandleElevatorRight.PassRightSign ="0000";
                                _eventAggregator.GetEvent<FaceNoPassEvent>().Publish(handleElevator);
                            });
                        }*/
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                finally
                {
                    if (image != null)
                    {
                        image.Dispose();
                    }
                    _isRGBLock = false;
                }

                _isRGBLock = false;
            });
        }

        /// <summary>
        /// RGB摄像头Paint事件,同步RGB人脸框，对比人脸框后进行IR活体检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IrVideoSource_Paint(object sender, PaintEventArgs e)
        {
            if (!_isDoubleShot || !irVideoSource.IsRunning)
            {
                return;
            }
            try
            {
                LiveDetect(e);
            }
            catch (Exception ex)
            {
                _logger.LogError($"活体检测失败：ex:{ex} ");
            }
        }

        /// <summary>
        /// 最后活体时间
        /// </summary>
        private long _lastLiveTime;
        private object _irVideoImageLocker = new object();

        private void LiveDetect(PaintEventArgs e)
        {
            //如果双摄，且IR摄像头工作，获取IR摄像头图片
            Bitmap irBitmap = irVideoSource.GetCurrentVideoFrame();
            if (irBitmap == null)
            {
                return;
            }

            //得到Rect
            MRECT rect = new MRECT();
            lock (_rectLock)
            {
                rect = _allRect;
            }
            float irOffsetX = irVideoSource.Width * 1f / irBitmap.Width;
            float irOffsetY = irVideoSource.Height * 1f / irBitmap.Height;
            float offsetX = irVideoSource.Width * 1f / rgbVideoSource.Width;
            float offsetY = irVideoSource.Height * 1f / rgbVideoSource.Height;
            //检测IR摄像头下最大人脸
            Graphics g = e.Graphics;

            float x = rect.left * offsetX;
            float width = rect.right * offsetX - x;
            float y = rect.top * offsetY;
            float height = rect.bottom * offsetY - y;

            //根据Rect进行画框
            g.DrawRectangle(Pens.Red, x, y, width, height);
            //if (trackIRUnit.GetIrLivenessMessage != "" && x > 0 && y > 0)
            //{
            //    //将上一帧检测结果显示到页面上
            //    g.DrawString(trackIRUnit.message, font, trackIRUnit.message.Contains("活体") ? blueBrush : yellowBrush, x, y - 15);
            //}

            //保证只检测一帧，防止页面卡顿以及出现其他内存被占用情况
            if (isIRLock)
            {
                return;
            }
            isIRLock = true;

            //异步处理提取特征值和比对，不然页面会比较卡
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                if (rect.left == 0 || rect.right == 0 || rect.top == 0 || rect.bottom == 0)
                {
                    //trackIRUnit.message = string.Empty;
                    isIRLock = false;
                    return;
                }

                bool isLiveness = false;
                try
                {
                    isLiveness = LiveDetect(irBitmap, rect, irOffsetX, irOffsetY, offsetX, offsetY);
                    if (isLiveness)
                    {
                        //记录最后活体时间，与人脸检测结果匹配
                        _lastLiveTime = DateTimeUtil.UtcNowMillis();
                    }
                    if (irBitmap != null)
                    {
                        irBitmap.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Featch face feature error:ex:{ex} ");
                }
                finally
                {
                    //trackIRUnit.message = string.Format("IR{0}", isLiveness ? "活体" : "假体");
                    if (!isLiveness)
                    {
                        _logger.LogInformation("假体!");
                    }
                    isIRLock = false;
                }
            }));
        }

        private bool LiveDetect(Bitmap irBitmap, MRECT rect, float irOffsetX, float irOffsetY, float offsetX, float offsetY)
        {
            //得到当前摄像头下的图片
            if (irBitmap == null)
            {
                _logger.LogError("Camera picture is null!");
                return false;
            }


            Bitmap irBmpClone = null;
            lock (_irVideoImageLocker)
            {
                irBmpClone = (Bitmap)irBitmap.Clone();
            }
            if (irBmpClone == null)
            {
                _logger.LogError("Camera picture clone is null!");
                return false;
            }

            //检测人脸，得到Rect框
            MultiFaceInfo multiFaceInfo = FaceUtil.DetectFaceIR(_faceProvider.FaceEngineProvider.VideoIRImageEngine, irBmpClone);
            if (multiFaceInfo.faceNum <= 0)
            {
                return false;
            }

            //得到最大人脸
            SingleFaceInfo irMaxFace = FaceUtil.GetMaxFace(multiFaceInfo);
            //得到Rect
            MRECT irRect = irMaxFace.faceRect;
            if (irRect.left == 0 || irRect.right == 0 || irRect.top == 0 || irRect.bottom == 0)
            {
                return false;
            }

            //判断RGB图片检测的人脸框与IR摄像头检测的人脸框偏移量是否在误差允许范围内
            if (!IsInAllowErrorRange(rect.left * (offsetX / irOffsetX), irRect.left)
                || !IsInAllowErrorRange(rect.right * (offsetX / irOffsetX), irRect.right)
                || !IsInAllowErrorRange(rect.top * (offsetY / irOffsetY), irRect.top)
                || !IsInAllowErrorRange(rect.bottom * (offsetY / irOffsetY), irRect.bottom))
            {
                _logger.LogInformation("活体检测未通过：范围超出界限！");
                return false;
            }

            //将图片进行灰度转换，然后获取图片数据
            //ImageInfo irImageInfo = ArcImageUtil.ReadBMP_IR(irBitmap);
            //if (irImageInfo == null)
            //{
            //    _logger.LogInformation("活体检测未通过：图片精灰度转化失败！");
            //    return false;
            //}

            //IR活体检测
            LivenessInfo liveInfo = FaceUtil.LivenessInfo_IR(_faceProvider.FaceEngineProvider.VideoIRImageEngine,
                                                                   irBitmap,
                                                                   irMaxFace,
                                                                   out int retCode_Liveness);
            //if (irImageInfo != null)
            //{
            //    MemoryUtil.Free(irImageInfo.imgData);
            //}

            //判断检测结果
            if (retCode_Liveness == 0 && liveInfo.num > 0)
            {
                if (liveInfo.isLive[0] == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        #endregion

        #region 窗体关闭
        private void RgbVideoSource_PlayingFinished(object sender, AForge.Video.ReasonToFinishPlaying reason)
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                _exitVideoRGBFR = true;
                _exitVideoRGBLiveness = true;
                _exitVideoIRFRLiveness = true;
            }
            catch (Exception ex)
            {
                LogUtil.LogInfo(GetType(), ex);
            }
        }
        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        private void ArcProFaceDistinguishControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (rgbVideoSource.IsRunning || irVideoSource.IsRunning)
            {
                //关闭摄像头
                CloseCamera();
            }
        }

        /// <summary>
        /// 关闭摄像头
        /// </summary>
        private void CloseCamera()
        {
            //关闭摄像头
            if (rgbVideoSource.IsRunning)
            {
                rgbVideoSource.SignalToStop();
            }
            rgbVideoSource.Hide();
            //关闭摄像头
            if (irVideoSource.IsRunning)
            {
                irVideoSource.SignalToStop();
            }
            irVideoSource.Hide();
        }

        #endregion

        #region 公用方法

        /// <summary>
        /// 校验图片
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        private bool CheckImage(string imagePath)
        {
            if (imagePath == null)
            {
                _logger.LogInformation("图片不存在，请确认后再导入\r\n");
                return false;
            }
            try
            {
                //判断图片是否正常，如将其他文件把后缀改为.jpg，这样就会报错
                System.Drawing.Image image = ArcImageUtil.ReadFromFile(imagePath);
                if (image == null)
                {
                    throw new Exception();
                }
                else
                {
                    image.Dispose();
                }
            }
            catch
            {
                _logger.LogInformation(string.Format("{0} 图片格式有问题，请确认后再导入\r\n", imagePath));
                return false;
            }
            FileInfo fileCheck = new FileInfo(imagePath);
            if (fileCheck.Exists == false)
            {
                _logger.LogInformation(string.Format("{0} 不存在\r\n", fileCheck.Name));
                return false;
            }
            else if (fileCheck.Length > maxSize)
            {
                _logger.LogInformation(string.Format("{0} 图片大小超过2M，请压缩后再导入\r\n", fileCheck.Name));
                return false;
            }
            else if (fileCheck.Length < 2)
            {
                _logger.LogInformation(string.Format("{0} 图像质量太小，请重新选择\r\n", fileCheck.Name));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断参数0与参数1是否在误差允许范围内
        /// </summary>
        /// <param name="allRect">参数0</param>
        /// <param name="liveRect">参数1</param>
        /// <returns></returns>
        private bool IsInAllowErrorRange(float allRect, float liveRect)
        {
            bool rel = false;
            if (Math.Abs(allRect - liveRect) < _arcFaceSettings.AllowAbleErrorRange)
            {
                rel = true;
            }
            else
            {
                _logger.LogInformation($"活体检测未通过：范围超出界限:allRect:{allRect} liveRect:{liveRect} allRect-liveRect:{allRect - liveRect} Range:{_arcFaceSettings.AllowAbleErrorRange}");
            }
            return rel;
        }
        #endregion
    }
}
