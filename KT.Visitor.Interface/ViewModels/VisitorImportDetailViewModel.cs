using KT.Common.WpfApp.ViewModels;

namespace KT.Visitor.Interface.ViewModels
{
    public class VisitorImportDetailViewModel : BindableBase
    {
        private long _id;
        private string _name;
        private string _phone;
        private string _cardText;
        private string _beVisitStaffName;
        private string _beVisitCompanyName;
        private string _reason;
        private string _visitDate;
        private string _authMsg;
        private bool _status;
        private string _statusText;
        private string _failure;
        private string _fullVisitDate;

        private bool _isChecked;
        private string _serverFaceImg;

        /// <summary>
        /// Id主键
        /// </summary>
        public long Id
        {
            get
            {
                return _id;
            }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        /// <summary>
        /// 姓名
        /// </summary>
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

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                SetProperty(ref _phone, value);
            }
        }

        /// <summary>
        /// Ic卡显示文本
        /// </summary> 
        public string CardText
        {
            get
            {
                return _cardText;
            }
            set
            {
                SetProperty(ref _cardText, value);
            }
        }

        /// <summary>
        /// 被访员工
        /// </summary>
        public string BeVisitStaffName
        {
            get
            {
                return _beVisitStaffName;
            }
            set
            {
                SetProperty(ref _beVisitStaffName, value);
            }
        }

        /// <summary>
        /// 被访公司名称
        /// </summary>
        public string BeVisitCompanyName
        {
            get
            {
                return _beVisitCompanyName;
            }
            set
            {
                SetProperty(ref _beVisitCompanyName, value);
            }
        }

        /// <summary>
        /// 来访事由
        /// </summary>
        public string Reason
        {
            get
            {
                return _reason;
            }
            set
            {
                SetProperty(ref _reason, value);
            }
        }

        /// <summary>
        /// 来访时间
        /// </summary>
        public string VisitDate
        {
            get
            {
                return _visitDate;
            }
            set
            {
                SetProperty(ref _visitDate, value);
            }
        }

        /// <summary>
        /// 授权方式
        /// </summary>
        public string AuthMsg
        {
            get
            {
                return _authMsg;
            }
            set
            {
                SetProperty(ref _authMsg, value);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status
        {
            get
            {
                return _status;
            }
            set
            {
                SetProperty(ref _status, value);
            }
        }

        /// <summary>
        /// 状态显示
        /// </summary>
        public string StatusText
        {
            get
            {
                return _statusText;
            }
            set
            {
                SetProperty(ref _statusText, value);
            }
        }

        /// <summary>
        /// 失败原因
        /// </summary>
        public string Failure
        {
            get
            {
                return _failure;
            }
            set
            {
                SetProperty(ref _failure, value);
            }
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                SetProperty(ref _isChecked, value);
            }
        }

        public string ServerFaceImg
        {
            get => _serverFaceImg;
            set
            {
                SetProperty(ref _serverFaceImg, value);
            }
        }
        public string FullVisitDate
        {
            get => _fullVisitDate;
            set
            {
                SetProperty(ref _fullVisitDate, value);
            }
        }
    }
}
