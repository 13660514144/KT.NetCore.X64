using Panuon.UI.Silver.Core;

namespace KT.Visitor.Common.ViewModels
{
    public class NavPageViewModel : PropertyChangedBase
    {
        private int page;
        private bool isSelected;

        public int Page
        {
            get
            {
                return page;
            }

            set
            {
                page = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                isSelected = value;
                NotifyPropertyChanged();
            }
        }
    }
}
