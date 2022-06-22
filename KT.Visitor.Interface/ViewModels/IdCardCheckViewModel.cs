using KT.Common.WpfApp.ViewModels;
using System.Drawing;

namespace KT.Visitor.Interface.ViewModels
{
    /// <summary>
    /// 人证比对结果
    /// </summary>
    public class IdCardCheckViewModel : BindableBase
    {
        private string _name;
        private string _message;
        private int _similarity;
        private Bitmap _idCardImage;
        private Bitmap _photoImage;
        private Bitmap _face;

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                SetProperty(ref _name, value);
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

        public Bitmap IdCardImage
        {
            get
            {
                return _idCardImage;
            }

            set
            {
                SetProperty(ref _idCardImage, value);
            }
        }

        public Bitmap PhotoImage
        {
            get
            {
                return _photoImage;
            }

            set
            {
                SetProperty(ref _photoImage, value);
            }
        }
        public Bitmap Face
        {
            get
            {
                return _face;
            }

            set
            {
                SetProperty(ref _face, value);
            }
        }

        public int Similarity
        {
            get
            {
                return _similarity;
            }

            set
            {
                SetProperty(ref _similarity, value);
            }
        }
    }
}
