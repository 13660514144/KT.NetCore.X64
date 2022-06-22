using KT.Visitor.Interface.Tools.Printer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Controls;
using KT.Visitor.Interface.Tools.ImageHelper;
using CommonUtils;

namespace KT.Visitor.Interface.Tools.Printer.DocumentRenderer
{
    public class VisitQRCodeRenderer : IDocumentRenderer
    {
        public void Render(FlowDocument doc, object data)
        {
            var model = (VisitQRCodePrintModel)data;

            Image image = doc.FindName("image_QRCode") as Image;
            image.Source = ImageConvert.BitmapToImageSource(model.QrImage);

            TextBlock title = doc.FindName("run_EdificeName") as TextBlock;
            title.Text = model.EdificeName;

            TextBlock name = doc.FindName("run_Name") as TextBlock;
            name.Text = model.Name;

            TextBlock phone = doc.FindName("run_Phone") as TextBlock;
            phone.Text = model.Phone;

            TextBlock edificeName = doc.FindName("run_CompanyName") as TextBlock;
            edificeName.Text = model.CompanyName;

            TextBlock floorName = doc.FindName("run_FloorName") as TextBlock;
            floorName.Text = model.FloorName;

            TextBlock dateTime = doc.FindName("run_DateTime") as TextBlock;
            dateTime.Text = model.DateTime;
        }
    }
}
