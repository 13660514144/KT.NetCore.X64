using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Visitor.Interface.Views.Visitor.Controls;
using log4net.Util;
using Microsoft.Extensions.DependencyInjection;
using Panuon.UI.Silver.Core;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Register
{
    public class CompanyControlViewModel : PropertyChangedBase
    {
        public CompanySelectListControl CompanySelectListControl { get; set; }
        public CompanySearchSelectControl CompanySearchSelectControl { get; set; }

        private bool _isShowSearch;

        public ICommand ShowSearchCommand { get; private set; }

        public CompanyControlViewModel()
        {
            ShowSearchCommand = new DelegateCommand(ShowSearch);

            Init();
        }

        public void ShowSearch()
        {
            if (IsShowSearch)
            {
                IsShowSearch = false;
                CompanySearchSelectControl.Visibility = Visibility.Collapsed;
            }
            else
            {
                CompanySearchSelectControl.Visibility = Visibility.Visible;
                IsShowSearch = true;
            }
        }

        internal void Init()
        {
            CompanySelectListControl = ContainerHelper.Resolve<CompanySelectListControl>();
            CompanySearchSelectControl = ContainerHelper.Resolve<CompanySearchSelectControl>();
        }

        public bool IsShowSearch
        {
            get
            {
                return _isShowSearch;
            }

            set
            {
                _isShowSearch = value;
                NotifyPropertyChanged();
            }
        }
    }
}