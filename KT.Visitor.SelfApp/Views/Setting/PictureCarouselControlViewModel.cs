using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Visitor.SelfApp.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

namespace KT.Visitor.SelfApp.Views.Setting
{
    public class PictureCarouselControlViewModel : BindableBase
    {
        private SelfAppSettings _selfAppSettings;
        private CarouselPicturePageViewModel _pageData;
        private string[] _pictureUrls;
        private Timer _timer;

        private readonly ILogger _logger;

        public PictureCarouselControlViewModel()
        {
            _selfAppSettings = ContainerHelper.Resolve<SelfAppSettings>();
            _logger = ContainerHelper.Resolve<ILogger>();
        }

        private void PictureShowTimerCallback(object state)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
               // NextPageAsync();
            });
        }

        public void Init()
        {
            _timer = new Timer(PictureShowTimerCallback, null, 10 * 100, (int)(_selfAppSettings.PictureCarouselShowSecondTime * 1000));
            InitPicture();
        }

        private void InitPicture()
        {

            var baseUrl = string.Empty;
            if (_selfAppSettings.PictureCarouselPath.IndexOf(':') == 1)
            {
                baseUrl = _selfAppSettings.PictureCarouselPath;
            }
            else
            {
                baseUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _selfAppSettings.PictureCarouselPath);
            }

            if (!Directory.Exists(baseUrl))
            {
                _logger.LogWarning($"图片路径不存在：{baseUrl} ");
                return;
            }
            _pictureUrls = Directory.GetFiles(baseUrl);
            if (string.IsNullOrEmpty(_pictureUrls?.FirstOrDefault()))
            {
                _logger.LogWarning($"图片不存在：path:{baseUrl} ");
                return;
            }

            PageData = new CarouselPicturePageViewModel();
            PageData.Pages = _pictureUrls.Length;

            SetPageData(1);
        }

        private void SetPageData(int page)
        {
            if (page <= 0)
            {
                PageData.Page = PageData.Pages;
            }
            else if (page > PageData.Pages)
            {
                PageData.Page = 1;
            }
            else
            {
                PageData.Page = page;
            }

            PageData.Url = _pictureUrls[PageData.Page - 1];

            PageData.InitPageDetails();
        }

        private void PreviousPageAsync()
        {
            SetPageData(PageData.Page - 1);
        }

        private void NextPageAsync()
        {
            SetPageData(PageData.Page + 1);
        }

        public CarouselPicturePageViewModel PageData
        {
            get
            {
                return _pageData;
            }

            set
            {
                SetProperty(ref _pageData, value);
            }
        }
    }
}
