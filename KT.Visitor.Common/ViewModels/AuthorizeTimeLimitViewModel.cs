using Panuon.UI.Silver.Core;

namespace KT.Visitor.Common.ViewModels
{
    public class AuthorizeTimeLimitViewModel : PropertyChangedBase
    {
        private bool _isOne;
        private bool _isAuth;
        private int _days;

        //private string _face;
        //private string _ic;
        //private string _qr;
        //private string _def;
        public AuthorizeTimeLimitViewModel()
        {
            _isOne = true;
            _days = 1;
            //_face = "FACE";
            //_ic = "IC";
            //_qr = "QR";
        }
        //public string IsFace
        //{
        //    get
        //    {
        //        return _face;
        //    }

        //    set
        //    {
        //        _face = value;
        //        NotifyPropertyChanged();
        //    }
        //}
        //public string IsIc
        //{
        //    get
        //    {
        //        return _ic;
        //    }

        //    set
        //    {
        //        _ic = value;
        //        NotifyPropertyChanged();
        //    }
        //}
        //public string IsQr
        //{
        //    get
        //    {
        //        return _qr;
        //    }

        //    set
        //    {
        //        _qr = value;
        //        NotifyPropertyChanged();
        //    }
        //}
        //public string IsDef
        //{
        //    get
        //    {
        //        return _def;
        //    }

        //    set
        //    {
        //        _def = value;
        //        NotifyPropertyChanged();
        //    }
        //}
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
