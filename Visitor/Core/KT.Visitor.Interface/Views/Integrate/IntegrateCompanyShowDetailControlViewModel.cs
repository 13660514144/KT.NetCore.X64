using KT.Visitor.Interface.ViewModels;
using Panuon.UI.Silver.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Interface.Views.Integrate
{
    public class IntegrateCompanyShowDetailControlViewModel : PropertyChangedBase
    {
        private CompanyViewModel company;

        public CompanyViewModel Company
        {
            get
            {
                return company;
            }

            set
            {
                company = value;
                NotifyPropertyChanged();
            }
        }
    }
}
