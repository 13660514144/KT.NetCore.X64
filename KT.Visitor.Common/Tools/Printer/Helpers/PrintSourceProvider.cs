using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Printing;
using System.Text;
using System.Windows.Controls;

namespace KT.Visitor.Common.Tools.Printer.Helpers
{
    /// <summary>
    /// 静态打印资源提供者
    /// </summary>
    public class PrintSourceProvider
    {
        private ILogger _logger;

        public PrintSourceProvider(ILogger logger)
        {
            _logger = logger;
        }

        private string _printerName;
        private PrintDialog _printDialog;
        private readonly object _locker = new object();

        public PrintDialog GetPrintDialog(string printerName)
        {
            if (_printerName == printerName)
            {
                if (_printDialog == null)
                {
                    _printDialog = GetPrintDialog();
                }
            }
            else
            {
                _printerName = printerName;
                _printDialog = GetPrintDialog();
            }

            return _printDialog;
        }
        private PrintDialog GetPrintDialog()
        {
            _logger.LogInformation($"创建打印机服务与队列：{_printerName} ");
            lock (_locker)
            {
                PrintDialog printDialog = new PrintDialog();
                printDialog.PrintQueue = new PrintQueue(new PrintServer(), _printerName);
                //printDialog.PrintQueue = new PrintQueue(new PrintServer(printer), printer);
                return printDialog;
            }
        }
    }
}
