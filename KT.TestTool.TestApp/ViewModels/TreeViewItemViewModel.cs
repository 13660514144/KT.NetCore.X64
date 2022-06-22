using Panuon.UI.Silver.Core;
using System.Collections.ObjectModel;
using System.Windows;

namespace KT.TestTool.TestApp.Models
{
    public class TreeViewItemViewModel : PropertyChangedBase
    {
        public TreeViewItemViewModel(string header, string tag, string icon = null)
        {
            _visibility = Visibility.Visible;

            Header = header;
            Tag = tag;
            Icon = icon;
            MenuItems = new ObservableCollection<TreeViewItemViewModel>();
        }

        public string Icon { get; set; }

        public string Header { get; set; }

        public string Tag { get; set; }

        private Visibility _visibility;
        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isExpanded = true;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<TreeViewItemViewModel> _menuItems;
        public ObservableCollection<TreeViewItemViewModel> MenuItems
        {
            get => _menuItems;
            set
            {
                _menuItems = value;
                NotifyPropertyChanged();
            }
        }

    }
}
