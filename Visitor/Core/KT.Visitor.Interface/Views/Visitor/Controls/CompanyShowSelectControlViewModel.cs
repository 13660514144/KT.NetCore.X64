using KT.Visitor.Interface.ViewModels;
using Panuon.UI.Silver.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    public class CompanyShowSelectControlViewModel : PropertyChangedBase
    {
        private ObservableCollection<CompanyViewModel> _companies;
        private CompanyViewModel _company;

        public ObservableCollection<CompanyViewModel> Companies
        {
            get
            {
                return _companies;
            }

            set
            {
                _companies = value;
                NotifyPropertyChanged();
            }
        }

        public CompanyViewModel Company
        {
            get
            {
                return _company;
            }

            set
            {
                _company = value;
                NotifyPropertyChanged();
            }
        }
    }
}
