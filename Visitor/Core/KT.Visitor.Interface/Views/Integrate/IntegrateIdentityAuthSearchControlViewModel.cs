using KT.Common.WpfApp.ViewModels;
using KT.Proxy.WebApi.Backend.Apis;
using KT.Visitor.Interface.Controls.BaseWindows;
using Prism.Mvvm;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Integrate
{
    public class IntegrateIdentityAuthSearchControlViewModel : BindableBase
    {
        public Action FocusCardNumber { get; set; }

        public ICommand SearchCommand { get; private set; }
        public ICommand AppendNumCommand { get; private set; }
        public ICommand DeleteNumCommand { get; private set; }

        private VisitorAuthApi _visitorAuthApi;

        private string _searchText;

        public IntegrateIdentityAuthSearchControlViewModel(VisitorAuthApi visitorAuthApi)
        {
            _visitorAuthApi = visitorAuthApi;

            SearchCommand = new DelegateCommand(Search);
            AppendNumCommand = new DelegateCommand(AppendNum);
            DeleteNumCommand = new DelegateCommand(DeleteNum);
        }
        private void Search()
        {
            _ = SearchAsync();
        }

        private void DeleteNum()
        {
            if (SearchText.Length > 0)
            {
                SearchText = SearchText[0..^1];
            }
        }

        private void AppendNum(string number)
        {
            SearchText = SearchText + number;
        }


        private async Task SearchAsync()
        {
            var results = await _visitorAuthApi.AuthCheckAsync(new { phone = _searchText });
            if (results == null || results.FirstOrDefault() == null)
            {
                MessageWarnBox.Show("未匹配到访问记录，请核实");
                return;
            }
        }

        public string SearchText
        {
            get
            {
                return _searchText;
            }

            set
            {
                SetProperty(ref _searchText, value);
            }
        }


    }
}
