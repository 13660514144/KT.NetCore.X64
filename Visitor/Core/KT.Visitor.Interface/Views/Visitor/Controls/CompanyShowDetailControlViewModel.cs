using KT.Visitor.Interface.ViewModels;
using Panuon.UI.Silver.Core;

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    public class CompanyShowDetailControlViewModel : PropertyChangedBase
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
