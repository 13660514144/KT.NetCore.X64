using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.ViewModels;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
namespace KT.Visitor.Interface.Views.Register
{
    public class IdentityAuthDetailControlViewModel : BindableBase
    {
        private VisitorInfoModel _visitorInfo;
        private RegistVisitorViewModel _registVisitor;
       
        public IdentityAuthDetailControlViewModel()
        {

        }

        public VisitorInfoModel VisitorInfo
        {
            get
            {
                return _visitorInfo;
            }

            set
            {
                SetProperty(ref _visitorInfo, value);
            }
        }

        public RegistVisitorViewModel RegistVisitor
        {
            get
            {
                return _registVisitor;
            }

            set
            {
                SetProperty(ref _registVisitor, value);
            }
        }
    }
}
