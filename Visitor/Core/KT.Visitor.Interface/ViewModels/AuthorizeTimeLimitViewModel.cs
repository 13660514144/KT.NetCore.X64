using KT.Common.WpfApp.ViewModels;
using Panuon.UI.Silver.Core;

namespace KT.Visitor.Interface.ViewModels
{
    public class AuthorizeTimeLimitViewModel : PropertyChangedBase
    {
        private bool _isOne;
        private bool _isAuth;
        private int _days;

        public AuthorizeTimeLimitViewModel()
        {
            _isOne = true;
            _days = 1;
        }
        public bool IsOne
        {
            get
            {
                return _isOne;
            }

            set
            {
                _isOne = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsAuth
        {
            get
            {
                return _isAuth;
            }

            set
            {
                _isAuth = value;
                NotifyPropertyChanged();
            }
        }

        public int Days
        {
            get
            {
                return _days;
            }

            set
            {
                _days = value;
                NotifyPropertyChanged();
            }
        }
    }
}
