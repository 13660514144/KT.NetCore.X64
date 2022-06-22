using KT.Common.WpfApp.Utils;
using KT.Visitor.Common.Tools.Printer.Models;
using System.Windows.Controls;
using System.Windows.Documents;

namespace KT.Visitor.Common.Tools.Printer.DocumentRenderer
{
    public class VisitQrCodeRenderer : IDocumentRenderer
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

            //TextBlock phone = doc.FindName("run_Phone") as TextBlock;
            //phone.Text = model.Phone;

            //TextBlock edificeName = doc.FindName("run_CompanyName") as TextBlock;
            //edificeName.Text = model.CompanyName;

            //TextBlock floorName = doc.FindName("run_FloorName") as TextBlock;
            //floorName.Text = model.FloorName;

            if (model.FloorCompanyName.Length <= 15)
            {
                TextBlock floorName1 = doc.FindName("run_FloorCompanyName1") as TextBlock;
                floorName1.Text = model.FloorCompanyName.Substring(0);
            }
            else if (model.FloorCompanyName.Length > 15)
            {
                TextBlock floorName1 = doc.FindName("run_FloorCompanyName1") as TextBlock;
                floorName1.Text = model.FloorCompanyName.Substring(0, 15);

                TextBlock floorName2 = doc.FindName("run_FloorCompanyName2") as TextBlock;
                floorName2.Text = model.FloorCompanyName.Substring(15);
            }

            TextBlock dateTime = doc.FindName("run_DateTime") as TextBlock;
            dateTime.Text = model.DateTime;

            TextBlock authMode = doc.FindName("run_AuthMode") as TextBlock;
            authMode.Text = model.AuthModeText;
        }
    }
}
