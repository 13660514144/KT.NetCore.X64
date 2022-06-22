using KT.Common.Core.Exceptions;
using KT.Common.WpfApp.Helpers;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.Tools.Printer.DocumentRenderer;
using KT.Visitor.Common.Tools.Printer.Helpers;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System;
using System.Drawing.Printing;
using System.IO;
using System.Printing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace KT.Visitor.Common.Tools.Printer.PrintOperator
{
    public class SmallTicketOperator
    {
        private FlowDocument _flowDocument;

        private ConfigHelper _configHelper;
        private DialogHelper _dialogHelper;
        private ILogger _logger;
        private PrintSourceProvider _printSourceProvider;

        public SmallTicketOperator(ConfigHelper configHelper,
            DialogHelper dialogHelper,
            ILogger logger,
            PrintSourceProvider printSourceProvider)
        {
            _configHelper = configHelper;
            _dialogHelper = dialogHelper;

            _logger = logger;
            _printSourceProvider = printSourceProvider;
        }

        public void Init(string strTmplName, object data, IDocumentRenderer renderer)
        {
            _logger.LogInformation("开始打印。");
            _flowDocument = LoadDocumentAndRender(strTmplName, data, renderer);
        }

        /// <summary>
        /// 开始打印
        /// </summary>
        public void StartPrint(string printer = "")
        {
            var configPrinter = _configHelper.LocalConfig?.Printer;
            _logger.LogInformation("打印设置打印机：{0} ", configPrinter);
            if (string.IsNullOrEmpty(printer))
            {
                if (string.IsNullOrEmpty(configPrinter))
                {
                    //默认打印机
                    printer = new PrintDocument().PrinterSettings.PrinterName;
                    _logger.LogInformation("打印默认打印机：{0} ", printer);
                }
                else
                {
                    if (string.IsNullOrEmpty(configPrinter))
                    {
                        throw CustomException.Run("您未设置打印机，请先设置打印机再打印。");
                    }
                    else
                    {
                        //配置打印机
                        printer = configPrinter;
                    }
                }
            }

            _logger.LogInformation("打印选择打印机：{0} ", printer);
            var printDialog = _printSourceProvider.GetPrintDialog(printer);

            _logger.LogInformation("开始初始化打印文档：{0} ", printer);
            var source = ((IDocumentPaginatorSource)_flowDocument).DocumentPaginator;

            printDialog.PrintDocument(source, "访问二维码打印");

            _logger.LogInformation("打印完成：{0} ", printer);
        }

        /// <summary>
        /// 加载并渲染文档
        /// </summary>
        /// <param name="strTmplName"></param>
        /// <param name="data"></param>
        /// <param name="renderer"></param>
        /// <returns></returns>
        private FlowDocument LoadDocumentAndRender(string strTmplName, Object data, IDocumentRenderer renderer)
        {
            FileStream xamlFile = new FileStream(strTmplName, FileMode.Open);
            FlowDocument doc = XamlReader.Load(xamlFile) as FlowDocument;
            xamlFile.Close();
            doc.PagePadding = new Thickness(5);
            doc.DataContext = data;
            doc.PageWidth = 300;
            doc.MaxPageHeight = 350;
            if (renderer != null)
            {
                renderer.Render(doc, data);
            }
            return doc;
        }
    }
}
