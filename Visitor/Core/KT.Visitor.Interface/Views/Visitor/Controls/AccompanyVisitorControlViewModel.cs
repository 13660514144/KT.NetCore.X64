using KT.Visitor.IdReader.Common;
using KT.Visitor.Interface.ViewModels;
using Panuon.UI.Silver.Core;
using System.Collections.ObjectModel;

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    public class AccompanyVisitorControlViewModel : PropertyChangedBase
    {
        public VisitorInfoViewModel VisitorInfo { get; set; }
        public bool IsFocus { get; set; }

        private TakePictureControl _takePictureControl;

        //证件类型
        private ObservableCollection<CertificateTypeEnum> certificateTypes;

        /// <summary>
        /// 是否为主访客
        /// </summary>
        private bool _isMain;
        private string _nameTitle;

        public AccompanyVisitorControlViewModel(TakePictureControl takePictureControl)
        {
            _takePictureControl = takePictureControl;

            CertificateTypes = new ObservableCollection<CertificateTypeEnum>(CertificateTypeEnum.Items);
            VisitorInfo = new VisitorInfoViewModel();

            //默认非主访客，设置好名名称
            IsMain = false;
        }

        public ObservableCollection<CertificateTypeEnum> CertificateTypes
        {
            get => certificateTypes;
            set
            {
                certificateTypes = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsMain
        {
            get
            {
                return _isMain;
            }

            set
            {
                _isMain = value;
                NameTitle = value ? "主访客" : "陪同访客";
                NotifyPropertyChanged();
            }
        }


        public TakePictureControl TakePictureControl
        {
            get
            {
                return _takePictureControl;
            }

            set
            {
                _takePictureControl = value;
                NotifyPropertyChanged();
            }
        }

        public string NameTitle
        {
            get
            {
                return _nameTitle;
            }

            set
            {
                _nameTitle = value;
                NotifyPropertyChanged();
            }
        }
    }
}
