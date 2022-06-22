using System;
using System.IO;

namespace KT.Visitor.Interface.Tools.Printer.Models
{
    public class PrintConfig
    {
        /// <summary>
        /// 打印模板基本路径
        /// </summary>
        private static string BASE_PRINTER_TEMPLATE_PATH => Path.Combine(AppContext.BaseDirectory, "Tools\\Printer\\DocumentTemplate");

        /// <summary>
        /// 访问二维码路径
        /// </summary>
        public static string VISIT_QR_CODE_DOCUMENT_PATH => Path.Combine(BASE_PRINTER_TEMPLATE_PATH, "VisitQRCodeDocument.xaml");


        public PrintConfig()
        {

        }
    }
}
