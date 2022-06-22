using KT.Common.WpfApp.Utils;
using KT.Common.WpfApp.ViewModels;
using KT.Prowatch.Service.DllModels;
using KT.TestTool.TestApp.Apis;
using KT.TestTool.TestApp.Helpers;
using KT.TestTool.TestApp.HttpApis;
using log4net.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KT.TestTool.TestApp.Views.Prowatch
{
    public class ProwatchQrShowWindowViewModel : BindableBase
    {
        private BitmapImage _qrCodeImage;
        private string _message;
        private decimal _changeTimeSeconds;
        private decimal _showTimeSeconds;

        public ICommand ApplyCommand { get; private set; }

        private IProwatchApi _prowatchApi;
        private ProwatchPrintHelper _prowatchPrintHelper;
        private ILogger<ProwatchQrShowWindowViewModel> _logger;

        public ProwatchQrShowWindowViewModel(
            ProwatchPrintHelper prowatchPrintHelper,
            ProwatchWebApi prowatchWebApi,
            ILogger<ProwatchQrShowWindowViewModel> logger)
        {
            _prowatchApi = prowatchWebApi;
            _prowatchPrintHelper = prowatchPrintHelper;
            _logger = logger;

            ApplyCommand = new DelegateCommand(Apply);
        }

        private void Apply()
        {

        }

        public async Task StartShowAsync(int startCardNo, int endCardNo, string serverAddress,decimal changeTimeSeconds,decimal showTimeSeconds)
        {
            ChangeTimeSeconds = changeTimeSeconds;
            ShowTimeSeconds = showTimeSeconds;

            for (int i = startCardNo; i <= endCardNo; i++)
            {
                try
                {
                    await ShowCardNoAsync(i, serverAddress);
                    Message = i.ToString();
                    await Task.Delay((int)(ShowTimeSeconds * 1000));
                    QrCodeImage = null;
                    await Task.Delay((int)(ChangeTimeSeconds * 1000));
                }
                catch (Exception ex)
                {
                    _logger.LogError("显示二维码错误：", ex);
                    Message = ex.Message;
                }
            }
        }

        private async Task ShowCardNoAsync(int cardNo, string serverAddress)
        {
            var qrValue = _prowatchPrintHelper.GetCardNoValue(cardNo);

            var imageInfo = await _prowatchApi.GetQrAsync(serverAddress, qrValue);
            var bitmap = ImageConvert.BytesToBitmapImage(imageInfo.Bytes);
            QrCodeImage = bitmap;
        }

        private int _delayTimes = 10;
        //internal async Task StartAuthorizeAndShowAsync(int startCardNo, int endCardNo, string serverAddress, Func<string, string, int, Task> addAndAuthorize, Func<CardData, string, Task> cancelAuthorize)
        internal async Task StartAuthorizeAndShowAsync(int startCardNo, int endCardNo, string serverAddress, Func<string, string, int, Task> addAndAuthorize)
        {
            var activationDate = DateTime.Now.ToString("yyyy-MM-dd");
            var expirationDate = DateTime.Now.AddYears(5).ToString("yyyy-MM-dd");
            for (int i = startCardNo; i <= (endCardNo + DelayTimes); i++)
            {
                try
                {
                    var checkCardNo = i - DelayTimes;
                    Message = $"Authorize:{i}    Check:{checkCardNo}";
                    if (i <= endCardNo)
                    {
                        await addAndAuthorize.Invoke(activationDate, expirationDate, i);
                    }

                    //刷卡
                    if (checkCardNo >= startCardNo)
                    {
                        await ShowCardNoAsync(i, serverAddress);
                        await Task.Delay((int)(ShowTimeSeconds * 1000));
                        QrCodeImage = null;
                    }

                    await Task.Delay((int)(ChangeTimeSeconds * 1000));
                }
                catch (Exception ex)
                {
                    _logger.LogError("显示二维码错误：", ex);
                    Message = ex.Message;
                }
            }
        }

        public BitmapImage QrCodeImage
        {
            get
            {
                return _qrCodeImage;
            }

            set
            {
                SetProperty(ref _qrCodeImage, value);
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                SetProperty(ref _message, value);
            }
        }

        public decimal ChangeTimeSeconds
        {
            get
            {
                return _changeTimeSeconds;
            }

            set
            {
                SetProperty(ref _changeTimeSeconds, value);
            }
        }

        public decimal ShowTimeSeconds
        {
            get
            {
                return _showTimeSeconds;
            }

            set
            {
                SetProperty(ref _showTimeSeconds, value);
            }
        }

        public int DelayTimes
        {
            get
            {
                return _delayTimes;
            }

            set
            {
                SetProperty(ref _delayTimes, value);
            }
        }
    }
}
