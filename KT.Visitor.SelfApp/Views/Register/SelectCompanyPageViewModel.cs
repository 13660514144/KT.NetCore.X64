using KT.Common.Data.Models;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.ViewModels;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.SelfApp.Views.Register
{
    public class SelectCompanyPageViewModel : BindableBase
    {
        private PageDataViewModel<CompanyViewModel> _floorPageData;
        private PageDataViewModel<CompanyViewModel> _companyPageData;
        //private List<CompanyViewModel> _bulidings;
        //private List<CompanyViewModel> _floors;
        //private List<CompanyViewModel> _companies;


        public SelectCompanyPageViewModel()
        {
            _floorPageData = new PageDataViewModel<CompanyViewModel>();
            _companyPageData = new PageDataViewModel<CompanyViewModel>();
            //_bulidings = new List<CompanyViewModel>();
            //_companies = new List<CompanyViewModel>();
            //_companies = new List<CompanyViewModel>();
        }


        //public List<CompanyViewModel> Bulidings
        //{
        //    get
        //    {
        //        return _bulidings;
        //    }

        //    set
        //    {
        //        SetProperty(ref _bulidings, value);
        //    }
        //}

        //public List<CompanyViewModel> Floors
        //{
        //    get
        //    {
        //        return _floors;
        //    }

        //    set
        //    {
        //        SetProperty(ref _floors, value);
        //    }
        //}

        //public List<CompanyViewModel> Companies
        //{
        //    get
        //    {
        //        return _companies;
        //    }

        //    set
        //    {
        //        SetProperty(ref _companies, value);
        //    }
        //}

        public PageDataViewModel<CompanyViewModel> FloorPageData
        {
            get
            {
                return _floorPageData;
            }

            set
            {
                SetProperty(ref _floorPageData, value);
            }
        }

        public PageDataViewModel<CompanyViewModel> CompanyPageData
        {
            get
            {
                return _companyPageData;
            }

            set
            {
                SetProperty(ref _companyPageData, value);
            }
        }
    }
}
