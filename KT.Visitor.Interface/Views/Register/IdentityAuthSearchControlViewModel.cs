using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Helpers;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Register
{
    public class IdentityAuthSearchControlViewModel : Prism.Mvvm.BindableBase
    {
        public Action FocusCardNumber { get; set; }

        private ICommand _searchCommand;
        public ICommand SearchCommand => _searchCommand ??= new DelegateCommand(Search);
        //private ICommand _enterInputCommand;
        //public ICommand EnterInputCommand => _enterInputCommand ??= new DelegateCommand(EnterInput);
        private ICommand _appendNumCommand;
        public ICommand AppendNumCommand => _appendNumCommand ??= new DelegateCommand<string>(AppendNum);
        private ICommand _deleteNumCommand;
        public ICommand DeleteNumCommand => _deleteNumCommand ??= new DelegateCommand(DeleteNum);

        private string _searchText;

        private IVisitorApi _visitorAuthApi;
        private DialogHelper _dialogHelper;
        private IEventAggregator _eventAggregator;

        public IdentityAuthSearchControlViewModel()
        {
            _visitorAuthApi = ContainerHelper.Resolve<IVisitorApi>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<IdentityAuthSuccessEvent>().Subscribe(IdentityAuthSuccess);
            _eventAggregator.GetEvent<IdentityAuthLinkEvent>().Subscribe(IdentityAuthSuccess);
        }

        public void EnterInput()
        {
            if (InputPageHelepr.IsPageInput(InputPageHelepr.IDENTITY_CHECK))
            {
                Search();
            }
        }

        private void IdentityAuthSuccess()
        {
            SearchText = string.Empty;
        }

        private void Search()
        {
            _ = SearchAsync();
        }

        private void DeleteNum()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                SearchText = SearchText[0..^1];
            }
        }

        private void AppendNum(string number)
        {
            SearchText = SearchText + number;
        }

        public Action<List<VisitorInfoModel>> SearchEndAction;
        private async Task SearchAsync()
        {
            if (string.IsNullOrEmpty(_searchText))
            {
                ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("请先输入访客手机号！");
                return;
            }
            var checkModel = new AuthCheckModel();
            checkModel.Phone = _searchText;

            var results = await _visitorAuthApi.CheckAsync(checkModel);
            if (results == null || results.FirstOrDefault() == null)
            {
                ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("未匹配到访问记录，请核实！");
                return;
            }

            InputPageHelepr.CurrentInputName = InputPageHelepr.IDENTITY_ACTIVE;
            _eventAggregator.GetEvent<IdentityAuthSearchedEvent>().Publish(results);
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
