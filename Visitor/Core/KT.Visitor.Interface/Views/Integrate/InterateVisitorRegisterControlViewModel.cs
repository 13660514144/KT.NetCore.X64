using Panuon.UI.Silver.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace KT.Visitor.Interface.Views.Integrate
{
    public class InterateVisitorRegisterControlViewModel : PropertyChangedBase
    {
        public IntegrateCompanyControl IntegrateCompanyControl { get; set; }
        public IntegrateAuthModeControl IntegrateAuthModeControl { get; set; }
        public IntegrateVisitorControl IntegrateVisitorControl { get; set; }

        public InterateVisitorRegisterControlViewModel(IntegrateCompanyControl integrateCompanyControl,
            IntegrateAuthModeControl integrateAuthModeControl,
            IntegrateVisitorControl integrateVisitorControl)
        {
            IntegrateCompanyControl = integrateCompanyControl;
            IntegrateAuthModeControl = integrateAuthModeControl;
            IntegrateVisitorControl = integrateVisitorControl;

            IntegrateCompanyControl.Visibility = Visibility.Visible;
            IntegrateAuthModeControl.Visibility = Visibility.Collapsed;
            IntegrateVisitorControl.Visibility = Visibility.Collapsed;
        }
    }
}
