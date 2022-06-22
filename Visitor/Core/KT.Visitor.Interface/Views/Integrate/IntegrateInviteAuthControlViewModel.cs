using KT.Visitor.Interface.Views.Auth;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace KT.Visitor.Interface.Views.Integrate
{
    public class IntegrateInviteAuthControlViewModel : BindableBase
    {
        public IntegrateInviteAuthSearchControl IntegrateInviteAuthSearchControl { get; set; }
        public IntegrateInviteAuthActiveControl IntegrateInviteAuthActiveControl { get; set; }

        public IntegrateInviteAuthControlViewModel(IntegrateInviteAuthSearchControl integrateInviteAuthSearchControl,
            IntegrateInviteAuthActiveControl integrateInviteAuthActiveControl)
        {
            IntegrateInviteAuthSearchControl = integrateInviteAuthSearchControl;
            IntegrateInviteAuthActiveControl = integrateInviteAuthActiveControl;

            Init();
        }
        public void Init()
        {
            IntegrateInviteAuthActiveControl.Visibility = Visibility.Collapsed;
        }
    }
}
