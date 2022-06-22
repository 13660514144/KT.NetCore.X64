using KT.Common.Core.Enums;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Enums;
using KT.Visitor.IdReader.Common;
using Newtonsoft.Json;
using Panuon.UI.Silver.Core;
using System;
using System.Windows.Media.Imaging;

namespace KT.Visitor.Common.ViewModels
{
    public class RegistVisitorViewModel : BindableBase
    {
        [JsonIgnore]
        public Action<string, string> ChangedNameAndIdNumberAction;

        //证件类型，默认身份证
        private string _certificateType;
        private string _name;
        private string _certificateNumber;
        private string _icCard;
        private string _phone;
        private string _gender;

        private int _order;
        private string _imageUrl;

        private BitmapImage _headImg;

        public RegistVisitorViewModel()
        {
            //身份证类型默认为2
            _certificateType = CertificateTypeEnum.ID_CARD.Value;
            _gender = GenderEnum.MALE.Value;

            _headImg = new BitmapImage(new Uri("pack://application:,,,/KT.Visitor.Interface;component/Resources/Images/certificateNot.png", UriKind.RelativeOrAbsolute));
        }

        public string CertificateType
        {
            get => _certificateType;
            set
            {
                SetProperty(ref _certificateType, value);
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
                SetProperty(ref _name, value?.Trim());

                ChangedNameAndIdNumberAction?.Invoke(Name, CertificateNumber);
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
                SetProperty(ref _certificateNumber, value);
                ChangedNameAndIdNumberAction?.Invoke(Name, CertificateNumber);
            }
        }

        public string IcCard
        {
            get
            {
                return _icCard;
            }

            set
            {
                SetProperty(ref _icCard, value?.Trim());
            }
        }

        [JsonIgnore]
        public BitmapImage HeadImg
        {
            get
            {
                return _headImg;
            }

            set
            {
                SetProperty(ref _headImg, value);
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
                SetProperty(ref _phone, value?.Trim());
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
                    SetProperty(ref _gender, "MALE");
                }
                else if (value == "女")
                {
                    SetProperty(ref _gender, "FEMALE");
                }
                else
                {
                    SetProperty(ref _gender, value);
                }
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
                SetProperty(ref _order, value);
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
                SetProperty(ref _imageUrl, value);
            }
        }
    }
}
