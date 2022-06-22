using Panuon.UI.Silver.Core;
using System.Drawing;

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    public class TakePictureControlViewModel : PropertyChangedBase
    { 
        private bool isTakedPicture = false;
        private Image picture;

        /// <summary>
        /// 上传到服务器后返回地址
        /// </summary>
        public string ImageUrl { get; set; }

        public void SetPicture(Image picture)
        {
            this.Picture = picture;
            this.IsTakedPicture = true;
        }

        /// <summary>
        /// 是否已经拍照完成
        /// </summary>
        public bool IsTakedPicture
        {
            get
            {
                return isTakedPicture;
            }

            set
            {
                isTakedPicture = value;
                NotifyPropertyChanged();
            }
        }

        public Image Picture
        {
            get
            {
                return picture;
            }

            set
            {
                picture = value;
                NotifyPropertyChanged();
            }
        }

    }
}
