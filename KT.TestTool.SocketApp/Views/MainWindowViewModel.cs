using KT.Common.WpfApp.ViewModels;
using Panuon.UI.Silver.Core;
using System.Collections.ObjectModel;

namespace KT.TestTool.SocketApp
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        private ObservableCollection<TreeViewItemViewModel> _menuItems;

        public MainWindowViewModel()
        {
            MenuItems = new ObservableCollection<TreeViewItemViewModel>()
            {
                new TreeViewItemViewModel("UDP数据接收", "UdpControl"),
                new TreeViewItemViewModel("TCP数据接收", "TcpControl")
            };
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                NotifyPropertyChanged();
                OnSearchTextChanged();
            }
        }

        public ObservableCollection<TreeViewItemViewModel> MenuItems
        {
            get
            {
                return _menuItems;
            }

            set
            {
                _menuItems = value;
                NotifyPropertyChanged();
            }
        }


        #region Event
        private void OnSearchTextChanged()
        {
            foreach (var item in MenuItems)
            {
                ChangeItemVisibility(item);
            }
        }

        private bool ChangeItemVisibility(TreeViewItemViewModel model)
        {
            var result = false;

            if (model.Header.ToLower().Contains(SearchText.ToLower()))
            {
                result = true;
            }

            if (model.MenuItems.Count != 0)
            {
                foreach (var item in model.MenuItems)
                {
                    var inner = ChangeItemVisibility(item);
                    result = result ? true : inner;
                }
            }

            model.Visibility = result ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            model.IsExpanded = result;
            return result;
        }

        #endregion

    }
}