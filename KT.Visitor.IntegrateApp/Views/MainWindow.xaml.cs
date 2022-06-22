using KT.Common.Core.Helpers;
using KT.Common.WpfApp.Helpers;

using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.Settings;

using KT.Visitor.Common.Tools.ArcIdMatch.ArcIdSdk.Helpers;
using KT.Visitor.IdReader.Common;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Helpers;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver;
using Prism.Events;
using System;
using System.Windows;
using System.Windows.Input;

using ArcFaceSDK;
using ArcFaceSDK.Entity;
using ArcFaceSDK.SDKModels;
using ArcFaceSDK.Utils;
using ArcSoftFace.Entity;

using System.Drawing;
using System.IO;
using System.Collections.Generic;
using ArcFaceSDK.Mothed;
using KT.Proxy.BackendApi.Apis;

namespace KT.Visitor.IntegrateApp.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : WindowX
    {
        public MainWindowViewModel ViewModel;
        private DialogHelper _dialogHelper;
        private IEventAggregator _eventAggregator;
        private ReaderFactory _readerFactory;
        private ConfigHelper _configHelper;
        private ArcIdSdkHelper _arcIdCardHelper;
        private IFunctionApi _functionApi;
        //X64
        //private readonly FaceProFactoryProxy _faceFactoryProxy;
        //X64
        private AppSettings _appSettings;
        private ILogger _logger;
        private readonly FaceRecognitionAppSettings _faceRecognitionAppSettings;
        #region 参数定义
        /// <summary>
        /// 图像处理引擎对象
        /// </summary>
        public FaceEngine imageEngine ;

        /// <summary>
        /// 保存右侧图片路径
        /// </summary>
        public string image1Path;

       

        /// <summary>
        /// 比对人脸图片人脸特征
        /// </summary>
        public List<FaceFeature> rightImageFeatureList = new List<FaceFeature>();

        /// <summary>
        /// 保存对比图片的列表
        /// </summary>
        public List<string> imagePathList = new List<string>();

        /// <summary>
        /// 人脸库人脸特征列表
        /// </summary>
        public List<FaceFeature> leftImageFeatureList = new List<FaceFeature>();

        /// <summary>
        /// 人脸比对阈值
        /// </summary>
        public float threshold = 0.8f;

        /// <summary>
        /// 红外（IR）活体阈值
        /// </summary>
        public float thresholdIr = 0.7f;

        /// <summary>
        /// 可见光（RGB）活体阈值
        /// </summary>
        public float thresholdRgb = 0.5f;

        /// <summary>
        /// 图像质量注册阈值
        /// </summary>
        public float thresholdImgRegister = 0.63f;

        /// <summary>
        /// 图像质量识别戴口罩阈值
        /// </summary>
        public float thresholdImgMask = 0.29f;

        /// <summary>
        /// 图像质量识别未戴口罩阈值
        /// </summary>
        public float thresholdImgNoMask = 0.49f;
     
        //视频模式下相关
        /// <summary>
        /// 视频引擎对象
        /// </summary>
        public FaceEngine videoEngine = new FaceEngine();

        /// <summary>
        /// RGB视频引擎对象
        /// </summary>
        public FaceEngine videoRGBImageEngine = new FaceEngine();

        /// <summary>
        /// IR视频引擎对象
        /// </summary>
        public FaceEngine videoIRImageEngine = new FaceEngine();

       
        /// <summary>
        /// 是否是双目摄像
        /// </summary>
        public bool isDoubleShot = false;

        /// <summary>
        /// RGB 摄像头索引
        /// </summary>
        public int rgbCameraIndex = 0;

        /// <summary>
        /// IR 摄像头索引
        /// </summary>
        public int irCameraIndex = 0;

        /// <summary>
        /// 人员库图片选择 锁对象
        /// </summary>
        public object chooseImgLocker = new object();

        /// <summary>
        /// RGB视频帧图像使用锁
        /// </summary>
        public object rgbVideoImageLocker = new object();
        /// <summary>
        /// IR视频帧图像使用锁
        /// </summary>
        public object irVideoImageLocker = new object();
        /// <summary>
        /// RGB视频帧图像
        /// </summary>
        public Bitmap rgbVideoBitmap = null;
        /// <summary>
        /// IR视频帧图像
        /// </summary>
        public Bitmap irVideoBitmap = null;
        /// <summary>
        /// RGB 摄像头视频人脸追踪检测结果
        /// </summary>
        public DictionaryUnit<int, FaceTrackUnit> trackRGBUnitDict = new DictionaryUnit<int, FaceTrackUnit>();

        /// <summary>
        /// RGB 特征搜索尝试次数字典
        /// </summary>
        public DictionaryUnit<int, int> rgbFeatureTryDict = new DictionaryUnit<int, int>();

        /// <summary>
        /// RGB 活体检测尝试次数字典
        /// </summary>
        public DictionaryUnit<int, int> rgbLivenessTryDict = new DictionaryUnit<int, int>();

        /// <summary>
        /// IR 视频最大人脸追踪检测结果
        /// </summary>
        public FaceTrackUnit trackIRUnit = new FaceTrackUnit();
        

        /// <summary>
        /// 红色画笔
        /// </summary>
        public SolidBrush redBrush = new SolidBrush(Color.Red);

        /// <summary>
        /// 绿色画笔
        /// </summary>
        public SolidBrush greenBrush = new SolidBrush(Color.Green);

        /// <summary>
        /// 关闭FR线程开关
        /// </summary>
        public bool exitVideoRGBFR = false;

        /// <summary>
        /// 关闭活体线程开关
        /// </summary>
        public bool exitVideoRGBLiveness = false;
        /// <summary>
        /// 关闭IR活体和FR线程线程开关
        /// </summary>
        public bool exitVideoIRFRLiveness = false;
        /// <summary>
        /// FR失败重试次数
        /// </summary>
        public int frMatchTime = 30;

        /// <summary>
        /// 活体检测失败重试次数
        /// </summary>
        public int liveMatchTime = 30;
        #endregion
        public MainWindow()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Resolve<MainWindowViewModel>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _readerFactory = ContainerHelper.Resolve<ReaderFactory>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _functionApi = ContainerHelper.Resolve<IFunctionApi>();
            imageEngine = ContainerHelper.Resolve<FaceEngine>();
            //x86
            _arcIdCardHelper = ContainerHelper.Resolve<ArcIdSdkHelper>();
            //x86
            //x64
            //_faceFactoryProxy = ContainerHelper.Resolve<FaceProFactoryProxy>();
            //x64
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _faceRecognitionAppSettings = ContainerHelper.Resolve<FaceRecognitionAppSettings>();
            //初始化要加载数据，先登录再加载
            this.DataContext = ViewModel;

            this.KeyDown += MainWindow_KeyDown;

            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;
        }

        private async void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                try
                {
                    //关闭证件阅读器
                    await _readerFactory.DisposeReaderAsync();

                    //关闭人证比对
                    //_arcIdCardHelper.Close();
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Escape关闭程序出错：{ex} ");
                }
                finally
                {
                    Btn_ExitLogin_Click(sender, null);
                }
                return;
            }
            KeyDownHelper.ShowSeachAction?.Invoke(sender, e);
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 设置全屏
            WindowHelper.ReleaseFullWindow(this, _appSettings.IsFullScreen);

            //加载证件阅读器
            // test hidden
            try
            {
                ReaderFactory.Reader = await _readerFactory.StartAsync(_configHelper.LocalConfig?.Reader, SetPersonAsync, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            // test hidden
            //初始化人证比对
            //_arcIdCardHelper.Init();  //X86

            //X64

            var visitorConfig = await _functionApi.GetConfigParmsAsync();
            if (visitorConfig?.OpenVisitorCheck == true)
            {
                _arcIdCardHelper.Init();  //X86
                #region x64
                /*if (_faceRecognitionAppSettings.FaceAuxiliaryType != FaceRecognitionTypeEnum.ArcPro40.Value)
                {
                    
                    MessageBox.Show($"找不到人脸识别类型：人证比对需要人脸类型配置，程序将会退出！！ ");
                    System.Environment.Exit(0);
                }
                InitEngines();*/
                #endregion
            }
            //X64
        }
        /// <summary>
        /// X64
        /// </summary>
        private async void InitOrderAsync()
        {
            //await _faceFactoryProxy.InitAsync();
        }
        /// <summary>
        /// 初始化引擎
        /// </summary>
        private void InitEngines()
        {
            try
            {
                //读取配置文件
                bool IsOnlineActive= _faceRecognitionAppSettings.ArcProFaceSettings.IsOnlineActive;
                frMatchTime = _faceRecognitionAppSettings.ArcProFaceSettings.FrMatchTime;
                string offlineActiveFilePath = _faceRecognitionAppSettings.ArcProFaceSettings.OfflineActiveFilePath;
                int retCode = 0;
                //bool isOnlineActive = false;//true(在线激活) or false(离线激活)
                try
                {
                    #region 读取离线激活配置信息
                    if (IsOnlineActive)
                    {
                        #region 读取在线激活配置信息
                        string appId = _faceRecognitionAppSettings.ArcProFaceSettings.AppId;
                        string sdkKey64 = _faceRecognitionAppSettings.ArcProFaceSettings.SdkKey64;
                        string sdkKey32 = _faceRecognitionAppSettings.ArcProFaceSettings.SdkKey32;
                        string activeKey64 = _faceRecognitionAppSettings.ArcProFaceSettings.ActiveKey64;
                        string activeKey32 = _faceRecognitionAppSettings.ArcProFaceSettings.ActiveKey32;
                        //判断CPU位数
                        var is64CPU = Environment.Is64BitProcess;
                        if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(is64CPU ? sdkKey64 : sdkKey32) || string.IsNullOrWhiteSpace(is64CPU ? activeKey64 : activeKey32))
                        {
                            MessageBox.Show(string.Format("请在App.config配置文件中先配置APP_ID和SDKKEY{0}、ACTIVEKEY{0}!", is64CPU ? "64" : "32"));
                            System.Environment.Exit(0);
                        }
                        #endregion
                        //在线激活引擎    如出现错误，1.请先确认从官网下载的sdk库已放到对应的bin中，2.当前选择的CPU为x86或者x64
                        retCode = imageEngine.ASFOnlineActivation(appId, is64CPU ? sdkKey64 : sdkKey32, is64CPU ? activeKey64 : activeKey32);
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(offlineActiveFilePath) || !File.Exists(offlineActiveFilePath))
                        {
                            string deviceInfo;
                            retCode = imageEngine.ASFGetActiveDeviceInfo(out deviceInfo);
                            if (retCode != 0)
                            {
                                MessageBox.Show("获取设备信息失败，错误码:" + retCode);
                            }
                            else
                            {
                                File.WriteAllText("ActiveDeviceInfo.txt", deviceInfo);
                                MessageBox.Show("获取设备信息成功，已保存到运行根目录ActiveDeviceInfo.txt文件，请在官网执行离线激活操作，将生成的离线授权文件路径在App.config里配置后再重新运行");
                            }
                            //System.Environment.Exit(0);
                            //离线激活                            
                        }
                        retCode = imageEngine.ASFOfflineActivation(offlineActiveFilePath);
                        if (retCode != 0 && retCode != 90114)
                        {
                            MessageBox.Show("激活SDK失败,错误码:" + retCode);
                            _logger.LogInformation($"激活SDK失败,错误码:{retCode}");
                            System.Environment.Exit(0);
                        }
                    }
                    
                    #endregion
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("无法加载 DLL"))
                    {
                        MessageBox.Show("请将SDK相关DLL放入bin对应的x86或x64下的文件夹中!");
                        
                    }
                    else
                    {
                        MessageBox.Show("激活SDK失败,请先检查依赖环境及SDK的平台、版本是否正确!");
                    }
                    System.Environment.Exit(0);
                }

                //初始化引擎
                DetectionMode detectMode = DetectionMode.ASF_DETECT_MODE_IMAGE;
                //Video模式下检测脸部的角度优先值
                ASF_OrientPriority videoDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_ALL_OUT;
                //Image模式下检测脸部的角度优先值
                ASF_OrientPriority imageDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_ALL_OUT;
                //最大需要检测的人脸个数
                int detectFaceMaxNum = 6;
                //引擎初始化时需要初始化的检测功能组合
                int combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER | FaceEngineMask.ASF_FACE3DANGLE | FaceEngineMask.ASF_IMAGEQUALITY | FaceEngineMask.ASF_MASKDETECT;
                //初始化引擎，正常值为0，其他返回值请参考http://ai.arcsoft.com.cn/bbs/forum.php?mod=viewthread&tid=19&_dsign=dbad527e
                retCode = imageEngine.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceMaxNum, combinedMask);
                         
                if (retCode == 0)
                {
                    _logger.LogInformation($"图片引擎初始化成功");
                }
                else
                {
                    _logger.LogInformation(string.Format("图片引擎初始化失败!错误码为:{0}", retCode));
                    MessageBox.Show(string.Format("图片引擎初始化失败!错误码为:{0}", retCode));
                    System.Environment.Exit(0);
                }

                //初始化视频模式下人脸检测引擎
                DetectionMode detectModeVideo = DetectionMode.ASF_DETECT_MODE_VIDEO;
                int combinedMaskVideo = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_FACELANDMARK;
                retCode = videoEngine.ASFInitEngine(detectModeVideo, videoDetectFaceOrientPriority, detectFaceMaxNum, combinedMaskVideo);
                
                if (retCode == 0)
                {
                    _logger.LogInformation($"视频引擎初始化成功");
                }
                else
                {
                    _logger.LogInformation(string.Format("视频引擎初始化失败!错误码为:{0}", retCode));
                    MessageBox.Show(string.Format("视频引擎初始化失败!错误码为:{0}", retCode));
                    System.Environment.Exit(0);
                }

                //RGB视频专用FR引擎
                combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_LIVENESS | FaceEngineMask.ASF_MASKDETECT;
                retCode = videoRGBImageEngine.ASFInitEngine(detectMode, videoDetectFaceOrientPriority, detectFaceMaxNum, combinedMask);
                
                if (retCode == 0)
                {
                    _logger.LogInformation($"RGB处理引擎初始化成功");
                }
                else
                {
                    _logger.LogInformation(string.Format("RGB处理引擎初始化失败!错误码为:{0}", retCode));
                    MessageBox.Show(string.Format("RGB处理引擎初始化失败!错误码为:{0}", retCode));
                    System.Environment.Exit(0);
                }
                //设置活体阈值
                videoRGBImageEngine.ASFSetLivenessParam(_faceRecognitionAppSettings.ArcProFaceSettings.ThresholdRgb);
                
                //IR视频专用FR引擎
                combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_IR_LIVENESS;
                retCode = videoIRImageEngine.ASFInitEngine(detectModeVideo, videoDetectFaceOrientPriority, detectFaceMaxNum, combinedMask);
                
                if (retCode == 0)
                {
                    _logger.LogInformation($"IR处理引擎初始化成功");
                }
                else
                {
                    _logger.LogInformation(string.Format("IR处理引擎初始化失败!错误码为:{0}\r\n", retCode));
                    MessageBox.Show(string.Format("IR处理引擎初始化失败!错误码为:{0}\r\n", retCode));
                    System.Environment.Exit(0);
                }
                //设置活体阈值 thresholdRgb  thresholdIr
                videoIRImageEngine.ASFSetLivenessParam(_faceRecognitionAppSettings.ArcProFaceSettings.ThresholdRgb,
                    _faceRecognitionAppSettings.ArcProFaceSettings.ThresholdIr);
                
                //initVideo();
            }
            catch (Exception ex)
            {                
                _logger.LogError($"程序初始化异常{ex}");
                MessageBox.Show("程序初始化异常,请在App.config中修改日志配置,根据日志查找原因!");
                System.Environment.Exit(0);
            }
            
        }
        private void SetPersonAsync(Person person)
        {
            _eventAggregator.GetEvent<ReadedPersonEvent>().Publish(person);
        }

        private void Btn_ExitLogin_Click(object sender, RoutedEventArgs e)
        {
            //_dialogHelper.RemoveFirst();
            //var loginWindow = ContainerHelper.Resolve<LoginWindow>();
            //_dialogHelper.Show(loginWindow);
            //this.Close();

            this.Close();
        }

        private async void MainWindow_Closed(object sender, EventArgs e)
        {
            EnvironmentExitHelper.Start();

            //销毁
            try
            {
                //关闭证件阅读器
                await _readerFactory.DisposeReaderAsync();

                //关闭人证比对
                //_arcIdCardHelper.Close();
                //X64
                //_faceFactoryProxy.Close();
                //X64
            }
            catch (Exception ex)
            {
                _logger.LogError($"关闭程序销毁数据失败：ex:{ex} ");
            }
            finally
            {
                Environment.Exit(-1);
            }
        }
    }
}
