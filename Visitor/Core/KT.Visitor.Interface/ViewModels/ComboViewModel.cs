using Panuon.UI.Silver.Core;

namespace KT.Visitor.Interface.ViewModels
{
    public class ComboViewModel : PropertyChangedBase
    {
        private string id;
        private string name;
        private bool isChecked;

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsChecked
        {
            get
            {
                return isChecked;
            }

            set
            {
                isChecked = value;
                NotifyPropertyChanged();
            }
        }
    }
}
