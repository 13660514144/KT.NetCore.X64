using KT.Common.WpfApp.ViewModels;
using KT.Visitor.Interface.Views.Auth;
using Panuon.UI.Silver.Core;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Integrate
{
    public class IntegrateMainWindowViewModel : PropertyChangedBase
    {
        public IntegrateRegisterControl IntegrateRegisterControl { get; set; }

        private IContainerProvider _containerProvider;

        public IntegrateMainWindowViewModel(IntegrateRegisterControl integrateRegisterControl,
            IContainerProvider containerProvider)
        {
            IntegrateRegisterControl = integrateRegisterControl;
            _containerProvider = containerProvider;
        }


    }
}
