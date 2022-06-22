using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Data.Enums;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Tools.Printer.DocumentRenderer;
using KT.Visitor.Interface.Views.Setting;
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

namespace KT.Visitor.Interface.Tools.Printer.PrintOperator
{
    public class SmallTicketOperator
    {
        private FlowDocument _flowDocument;

        private ConfigHelper _configHelper;
        private IContainerProvider _containerProvider;

        public SmallTicketOperator(ConfigHelper configHelper,
            IContainerProvider containerProvider)
        {
            _configHelper = configHelper;
            _containerProvider = containerProvider;
        }

        public void Init(string strTmplName, object data, IDocumentRenderer renderer)
        {
            _flowDocument = LoadDocumentAndRender(strTmplName, data, renderer);
        }

        /// <summary>
        /// 开始打印
        /// </summary>
        public async Task StartPrintAsync(string printer = "")
        {
            var config = _configHelper.LocalConfig;
            if (string.IsNullOrEmpty(printer))
            {
                if (string.IsNullOrEmpty(config.Printer))
                {
                    //默认打印机
                    printer = new PrintDocument().PrinterSettings.PrinterName;
                }
                else
                {
                    if (string.IsNullOrEmpty(config.Printer))
                    {
                        var result = MessageWarnBox.Show("您未设置打印机，是否以默认打印机打印？", "温馨提示", "确定", "取消");
                        if (result == true)
                        {
                            printer = new PrintDocument().PrinterSettings.PrinterName;
                        }
                        //else
                        //{
                        //    // 弹出操作窗口,先登录再设置/TOODO
                        //   var settingPage = 
                        //}
                    }
                    else
                    {
                        //配置打印机
                        printer = config.Printer;
                    }
                }
            }

            PrintDialog printDialog = new PrintDialog();

            printDialog.PrintQueue = new PrintQueue(new PrintServer(), printer);
            printDialog.PrintDocument(((IDocumentPaginatorSource)_flowDocument).DocumentPaginator, "访问二维码打印");
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
            if (renderer != null)
            {
                renderer.Render(doc, data);
            }
            return doc;
        }
    }
}
