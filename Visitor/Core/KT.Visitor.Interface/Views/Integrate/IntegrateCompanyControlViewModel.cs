using KT.Common.WpfApp.ViewModels;
using KT.Visitor.Interface.Views.Visitor.Controls;
using Microsoft.Extensions.DependencyInjection;
using Panuon.UI.Silver.Core;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Integrate
{
    public class IntegrateCompanyControlViewModel : PropertyChangedBase
    {  
        public IntegrateCompanySelectListControl CompanySelectListControl { get; set; }
        public IntegrateCompanySearchSelectControl CompanySearchSelectControl { get; set; }
        public IntegrateCompanyShowDetailControl CompanyShowDetailControl { get; set; }

        public ICommand ShowSearchCommand { get; private set; }

        private IContainerProvider _containerProvider;

        public IntegrateCompanyControlViewModel(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;

            ShowSearchCommand = new DelegateCommand(ShowSearch);

            Init();
        }

        private void ShowSearch()
        {
            CompanySearchSelectControl.Visibility = Visibility.Visible;
        }

        internal void Init()
        {
            CompanySelectListControl = _containerProvider.Resolve<IntegrateCompanySelectListControl>();
            CompanySearchSelectControl = _containerProvider.Resolve<IntegrateCompanySearchSelectControl>();
            CompanyShowDetailControl = _containerProvider.Resolve<IntegrateCompanyShowDetailControl>();
        }
    }
}