using KT.Common.WpfApp.Utils;
using Panuon.UI.Silver.Core;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;

namespace KT.Visitor.Common.ViewModels
{
    public class CaptureImageViewModel : PropertyChangedBase
    {
        private string _imageUrl;
        private BitmapImage _image;

        public CaptureImageViewModel(string imageUrl, Image image)
        {
            this.ImageUrl = imageUrl;
            this.Image = ImageConvert.ImageToBitmapImage(image, ImageFormat.Png); 
        }

        public string ImageUrl
        {
            get
            {
                return _imageUrl;
            }

            set
            {
                _imageUrl = value;
                NotifyPropertyChanged();
            }
        }

        public BitmapImage Image
        {
            get
            {
                return _image;
            }

            set
            {
                _image = value;
                NotifyPropertyChanged();
            }
        }
    }
}
