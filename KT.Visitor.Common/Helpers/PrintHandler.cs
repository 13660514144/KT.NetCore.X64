using KT.Common.WpfApp.Utils;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Tools.Printer.DocumentRenderer;
using KT.Visitor.Common.Tools.Printer.Models;
using KT.Visitor.Common.Tools.Printer.PrintOperator;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using KT.Common.WpfApp.Helpers;
using Microsoft.Extensions.Logging;
using KT.Visitor.Common.Settings;
using System.IO;
using System;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace KT.Visitor.Common.Helpers
{
    public class PrintHandler
    {
        private SmallTicketOperator _smallTicketOperator;
        private IFunctionApi _frontBaseApi;
        private VisitQrCodeRenderer _visitQrCodeRenderer;
        private ILogger _logger;
        private AppSettings _appSettings;

        public PrintHandler(SmallTicketOperator smallTicketOperator,
            IFunctionApi frontBaseApi,
            VisitQrCodeRenderer visitQrCodeRenderer,
            ILogger logger,
            AppSettings appSettings)
        {
            _smallTicketOperator = smallTicketOperator;
            _visitQrCodeRenderer = visitQrCodeRenderer;
            _frontBaseApi = frontBaseApi;
            _logger = logger;
            _appSettings = appSettings;
        }

        public async void StartPrintAsync(List<RegisterResultModel> registerRresults, bool isOnce)
        {
            
            if (registerRresults == null)
            {
                return;
            }
            var configParms = await _frontBaseApi.GetConfigParmsAsync();
            if (!configParms.PrintQrCode)
            {
                return;
            }

            var qrCodeResults = registerRresults.Where(x => !string.IsNullOrEmpty(x.Qr));
            
            foreach (var item in qrCodeResults)
            {
                //打印二维码//C764D517E93705E2B734F19D63C069718A615D6FA13E0A74564505566979A8922028D238747C5493347CDC96DFD9718F 
                //var imageInfo = await _frontBaseApi.GetQrAsync("C764D517E93705E2B734F19D63C069718A615D6FA13E0A74564505566979A8922028D238747C5493347CDC96DFD9718F");
                var imageInfo = await _frontBaseApi.GetQrAsync(item.Qr);
                var bitmap = ImageConvert.BytesToBitmap(imageInfo.Bytes);

                var printObj = VisitQRCodePrintModel.FromVisitorResult(item, bitmap, isOnce);
                await PrintApply(printObj);
            }
        }

        public Task PrintApply(VisitQRCodePrintModel printObj)
        {
            //开始打印
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tools/Printer/DocumentTemplate", _appSettings.QrCodePrintTemplate);

            Application.Current.Dispatcher.Invoke(() =>
            {
                _smallTicketOperator.Init(templatePath, printObj, _visitQrCodeRenderer);
                _smallTicketOperator.StartPrint();
            });

            return Task.CompletedTask;
        }
    }
}
