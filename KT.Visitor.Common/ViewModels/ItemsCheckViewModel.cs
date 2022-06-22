using Panuon.UI.Silver.Core;

namespace KT.Visitor.Common.ViewModels
{
    public class ItemsCheckViewModel : PropertyChangedBase
    {
        private string id;
        private string name;
        private bool isChecked = false;

        public ItemsCheckViewModel(string name)
        {
            this.Id = name;
            this.Name = name;
        }
        public ItemsCheckViewModel(string name, bool isChecked) 
        {
            this.Id = name;
            this.Name = name;
            this.IsChecked = isChecked;
        }
        public ItemsCheckViewModel(string id, string name)  
        {
            this.Id = id;
            this.Name = name;
        }
        public ItemsCheckViewModel(string id, string name, bool isChecked) : this(id, name)
        {
            this.IsChecked = isChecked;
        }

        //private object data;

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

        //public object Data
        //{
        //    get
        //    {
        //        return data;
        //    }

        //    set
        //    {
        //        data = value;
        //    }
        //}
    }
}
