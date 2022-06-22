using Panuon.UI.Silver.Core;
using System.Drawing;

namespace KT.Visitor.Interface.ViewModels
{
    public class CaptureImageViewModel : PropertyChangedBase
    {

        private string imageUrl;
        private Image image;

        public CaptureImageViewModel(string imageUrl, Image image)
        {
            this.ImageUrl = imageUrl;
            this.Image = image;
        }

        public string ImageUrl
        {
            get
            {
                return imageUrl;
            }

            set
            {
                imageUrl = value;
                NotifyPropertyChanged( );
            }
        }

        public Image Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
                NotifyPropertyChanged( );
            }
        }
    }
}
