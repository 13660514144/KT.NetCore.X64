using KT.Common.WpfApp.ViewModels;
using KT.Visitor.Interface.Views.Auth;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Integrate
{
    public class IntegrateIdentityAuthControlViewModel : BindableBase
    {
        public IntegrateIdentityAuthSearchControl IntegrateIdentityAuthSearchControl { get; set; }
        public IntegrateIdentityAuthActiveControl IntegrateIdentityAuthActiveControl { get; set; }

        public IntegrateIdentityAuthControlViewModel(IntegrateIdentityAuthSearchControl integrateIdentityAuthSearchControl,
            IntegrateIdentityAuthActiveControl integrateIdentityAuthActiveControl)
        {
            IntegrateIdentityAuthSearchControl = integrateIdentityAuthSearchControl;
            IntegrateIdentityAuthActiveControl = integrateIdentityAuthActiveControl;

            Init();
        }


        public void Init()
        {
            IntegrateIdentityAuthActiveControl.Visibility = Visibility.Collapsed;
        }

    }
}
