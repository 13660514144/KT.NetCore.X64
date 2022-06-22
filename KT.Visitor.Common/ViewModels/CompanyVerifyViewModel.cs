using Panuon.UI.Silver.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Common.ViewModels
{
    public class CompanyVerifyViewModel : PropertyChangedBase
    {
        private string _companyUnit;
        private string _companyName;
        private string _userName;
        private string _telePhone;

        public string CompanyUnit
        {
            get
            {
                return _companyUnit;
            }

            set
            {
                _companyUnit = value;
                NotifyPropertyChanged();
            }
        }

        public string CompanyName
        {
            get
            {
                return _companyName;
            }

            set
            {
                _companyName = value;
                NotifyPropertyChanged();
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value;
                NotifyPropertyChanged();
            }
        }

        public string TelePhone
        {
            get
            {
                return _telePhone;
            }

            set
            {
                _telePhone = value;
                NotifyPropertyChanged();
            }
        }
    }
}
