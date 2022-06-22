using ArcFaceSDK.Mothed;
using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.Utils;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.Tools.ArcIdMatch.ArcIdSdk.Helpers;
using KT.Visitor.IdReader;
using KT.Visitor.IdReader.Common;
using KT.Visitor.SelfApp.Helpers;
using KT.Visitor.SelfApp.Views.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace KT.Visitor.SelfApp.Public
{
    /// <summary>
    /// IdentityReadIdCardPage.xaml 的交互逻辑
    /// </summary>
    public partial class IdentityReadIdCardPage : Page
    {
        private List<VisitorInfoModel> _visitorRecords;
        private IReader _reader;

        private MainFrameHelper _mainFrameHelper;
        private ConfigHelper _configHelper;
        private ReaderFactory _readerFactory;
        private IVisitorApi _visitorApi;
        private IFunctionApi _functionApi;
        private PrintHandler _printHandler;
        private ArcIdSdkHelper _arcIdCardHelper;
        private ILogger _logger;
        private FaceCheckMethod _FaceCheckMethod;
        public IdentityReadIdCardPage()
        {
            InitializeComponent();
            _FaceCheckMethod = ContainerHelper.Resolve<FaceCheckMethod>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _mainFrameHelper = ContainerHelper.Resolve<MainFrameHelper>();
            _readerFactory = ContainerHelper.Resolve<ReaderFactory>();
            _visitorApi = ContainerHelper.Resolve<IVisitorApi>();
            _printHandler = ContainerHelper.Resolve<PrintHandler>();
            _functionApi = ContainerHelper.Resolve<IFunctionApi>();
            _arcIdCardHelper = ContainerHelper.Resolve<ArcIdSdkHelper>();
            _logger = ContainerHelper.Resolve<ILogger>();


            //事件加载
            this.Loaded += Page_Loaded;
            this.Unloaded += Page_Unloaded;
        }

        private async void Img_exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            await _readerFactory.DisposeReaderAsync();
            var UI = ContainerHelper.Resolve<HomePage>();
            _mainFrameHelper.Link(UI);
        }

        public void Init(List<VisitorInfoModel> visitorRecords)
        {
            _visitorRecords = visitorRecords;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            #region 身份证阅读器动画效果
            var HeightAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(3),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
            };
            var opictyAn = new DoubleAnimation()
            {
                From = 0.1,
                To = 0.8,
                Duration = TimeSpan.FromSeconds(2),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            txb_am.BeginAnimation(HeightProperty, HeightAnimation);
            txb_am.BeginAnimation(OpacityProperty, opictyAn);
            #endregion

            InitReaderAsync();
        }

        /// <summary>
        /// 初始化身份证阅读器对象
        /// </summary>
        private async void InitReaderAsync()
        {
            //异步 初始化身份证阅读器对象 
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

                //初始化数据
                var visitorConfig = await _functionApi.GetConfigParmsAsync();
                if (visitorConfig?.OpenVisitorCheck == true)
                {
                    //提取图片特征值
                    // _arcIdCardHelper.IdCardDataFeatureExtraction(person.Portrait);//86
                    //X64
                    _FaceCheckMethod.ChooseMultiImg(person.Portrait);
                   //X64
                    foreach (var item in _visitorRecords)
                    {
                        //获取照片
                        if (string.IsNullOrEmpty(item.FaceImg))
                        {
                            throw CustomException.Run("访客预约未上传人脸图片，无法人证对比");
                        }
                        string[] http = item.FaceImg.Trim().Split('/');
                        if (http[3].Length < 1)
                        {
                            throw CustomException.Run("访客预约未上传人脸图片，无法人证对比");
                        }
                        var photoInfo = await _functionApi.GetPictureAsync(item.FaceImg);
                        var lenth = photoInfo.Bytes?.Length;
                        if (lenth.IfNullOrLessEqualZero())
                        {
                            throw CustomException.Run("人脸比对失败：照片为空");
                        }

                        //人证比对 
                        var photoBitmap = ImageConvert.BytesToBitmap(photoInfo.Bytes);
                        //X64
                        _FaceCheckMethod.ChooseImg(photoBitmap);
                        bool Flg = _FaceCheckMethod.CheckFaceFlg();
                        if (!Flg)
                        {
                            throw CustomException.Run($"人证核验失败，相似度：{_FaceCheckMethod.CheckValue}% ");
                        }
                        //X64
                        //X86
                        /*var compareResult = _arcIdCardHelper.FaceDataIdCardCompare(true, photoBitmap, false);
                        if (!compareResult.IsSuccess)
                        {
                            throw CustomException.Run($"{compareResult.Message}，相似度：{compareResult.Similarity}% ");
                        }*/
                    }
                }

                //开始授权
                Appoint();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "身份验证失败！");
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ContainerHelper.Resolve<OperateErrorPage>().ShowMessage(ex.Message);
                });
            }

        }
        private void Appoint()
        {
            var appoint = new AuthInfoModel();
            foreach (var item in _visitorRecords)
            {
                var visitor = new AuthVisitorModel();
                visitor.Id = item.Id;
                visitor.Ic = item.IcCard;
                visitor.FaceImg = item.FaceImg;
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
