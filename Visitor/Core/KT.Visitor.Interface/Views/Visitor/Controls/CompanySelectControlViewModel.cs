using KT.Visitor.Interface.Views.Helper;
using Microsoft.Extensions.DependencyInjection;
using Panuon.UI.Silver.Core;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    public class CompanySelectControlViewModel : PropertyChangedBase
    {
        //公司选择局部界面
        public CompanyShowDetailControl CompanyShowDetailControl { get; set; }
        public CompanySelectListControl CompanySelectListControl { get; set; }
        public CompanyWarnControl CompanyWarnControl { get; set; }
        public CompanyShowSelectControl CompanyShowSelectControl { get; set; }
        public CompanySearchSelectControl CompanySearchSelectControl { get; set; }

        //private Visibility _companyShowDetailControlVisibility;
        //private Visibility _companySelectListControlVisibility;
        //private Visibility _companyWarnControlVisibility;
        //private Visibility _companyShowSelectControlVisibility;
        //private Visibility _companySearchSelectControlVisibility;

        private VistitorConfigHelper _vistitorConfigHelper;
        private IContainerProvider _containerProvider;

        public CompanySelectControlViewModel(VistitorConfigHelper vistitorConfigHelper,
            IContainerProvider containerProvider)
        {
            _vistitorConfigHelper = vistitorConfigHelper;
            _containerProvider = containerProvider;

            Init();
        }
        internal void Init()
        {
            CompanySelectListControl = _containerProvider.Resolve<CompanySelectListControl>();
            CompanyShowDetailControl = _containerProvider.Resolve<CompanyShowDetailControl>();
            CompanyWarnControl = _containerProvider.Resolve<CompanyWarnControl>();
            CompanyShowSelectControl = _containerProvider.Resolve<CompanyShowSelectControl>();
            CompanySearchSelectControl = _containerProvider.Resolve<CompanySearchSelectControl>();
        }

        //public Visibility CompanyShowDetailControlVisibility
        //{
        //    get
        //    {
        //        return _companyShowDetailControlVisibility;
        //    }

        //    set
        //    {
        //        _companyShowDetailControlVisibility = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        //public Visibility CompanySelectListControlVisibility
        //{
        //    get
        //    {
        //        return _companySelectListControlVisibility;
        //    }

        //    set
        //    {
        //        _companySelectListControlVisibility = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        //public Visibility CompanyWarnControlVisibility
        //{
        //    get
        //    {
        //        return _companyWarnControlVisibility;
        //    }

        //    set
        //    {
        //        _companyWarnControlVisibility = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        //public Visibility CompanyShowSelectControlVisibility
        //{
        //    get
        //    {
        //        return _companyShowSelectControlVisibility;
        //    }

        //    set
        //    {
        //        _companyShowSelectControlVisibility = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        //public Visibility CompanySearchSelectControlVisibility
        //{
        //    get
        //    {
        //        return _companySearchSelectControlVisibility;
        //    }

        //    set
        //    {
        //        _companySearchSelectControlVisibility = value;
        //        NotifyPropertyChanged();
        //    }
        //}

    }
}
