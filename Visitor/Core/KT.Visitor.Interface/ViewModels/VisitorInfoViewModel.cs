using KT.Visitor.IdReader.Common;
using Panuon.UI.Silver.Core;
using System;
using System.Windows.Media.Imaging;

namespace KT.Visitor.Interface.ViewModels
{
    public class VisitorInfoViewModel : PropertyChangedBase
    {
        //证件类型，默认身份证
        private string _certificateType;
        private string _name;
        private string _certificateNumber;
        private string _icCardNumber;
        private string _phone;
        private string _gender;

        private int _order;
        private string _imageUrl;

        private BitmapImage _headImg;

        public VisitorInfoViewModel()
        {
            //身份证类型默认为2
            _certificateType = "2";
            _gender = "MALE";

            _headImg = new BitmapImage(new Uri("/Resources/Images/certificateNot.png", UriKind.RelativeOrAbsolute));
        }

        public string CertificateType
        {
            get => _certificateType;
            set
            {
                //设置证件类型的时候选择结果可能会是中文名称
                _certificateType = CertificateTypeEnum.GetByValueOrText(value).Value;
                NotifyPropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value?.Trim();
                NotifyPropertyChanged();
            }
        }

        public string CertificateNumber
        {
            get
            {
                return _certificateNumber;
            }

            set
            {
                _certificateNumber = value;
                NotifyPropertyChanged();
            }
        }

        public string IcCardNumber
        {
            get
            {
                return _icCardNumber;
            }

            set
            {
                _icCardNumber = value?.Trim();
                NotifyPropertyChanged();
            }
        }

        public BitmapImage HeadImg
        {
            get
            {
                return _headImg;
            }

            set
            {
                _headImg = value;
                NotifyPropertyChanged();
            }
        }

        public string Phone
        {
            get
            {
                return _phone;
            }

            set
            {
                _phone = value?.Trim();
                NotifyPropertyChanged();
            }
        }

        public string Gender
        {
            get
            {
                return _gender;
            }

            set
            {
                if (value == "男")
                {
                    _gender = "MALE";
                }
                else if (value == "女")
                {
                    _gender = "FEMALE";
                }
                else
                {
                    _gender = value;
                }
                NotifyPropertyChanged();
            }
        }

        public int Order
        {
            get
            {
                return _order;
            }

            set
            {
                _order = value;
                NotifyPropertyChanged();
            }
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
    }
}
