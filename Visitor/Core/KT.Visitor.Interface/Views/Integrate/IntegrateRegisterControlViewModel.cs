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
    public class IntegrateRegisterControlViewModel : PropertyChangedBase
    {
        public InterateVisitorRegisterControl InterateVisitorRegisterControl { get; set; }
        public IntegrateIdentityAuthControl IntegrateIdentityAuthControl { get; set; }
        public IntegrateInviteAuthControl IntegrateInviteAuthControl { get; set; }

        private string _operatingName;

        public ICommand VisitorRegisteCommand { get; private set; }
        public ICommand IdentityAuthCommand { get; private set; }
        public ICommand InviteAuthCommand { get; private set; }

        private IContainerProvider _containerProvider;
        public IntegrateRegisterControlViewModel(InterateVisitorRegisterControl interateVisitorRegisterControl,
            IntegrateIdentityAuthControl integrateIdentityAuthControl,
            IntegrateInviteAuthControl integrateInviteAuthControl,
            IContainerProvider containerProvider)
        {
            InterateVisitorRegisterControl = interateVisitorRegisterControl;
            IntegrateIdentityAuthControl = integrateIdentityAuthControl;
            IntegrateInviteAuthControl = integrateInviteAuthControl;
            _containerProvider = containerProvider;

            VisitorRegisteCommand = new DelegateCommand(VisitorRegiste);
            IdentityAuthCommand = new DelegateCommand(IdentityAuth);
            InviteAuthCommand = new DelegateCommand(InviteAuth);

            InterateVisitorRegisterControl.EndRegistedAction += EndRegisted;

            //默认访客登记
            VisitorRegiste();
        }

        /// <summary>
        /// 完成注册
        /// </summary>
        private void EndRegisted()
        {
            InterateVisitorRegisterControl = _containerProvider.Resolve<InterateVisitorRegisterControl>();
        }

        private void VisitorRegiste()
        {
            OperatingName = "访客登记";
            InterateVisitorRegisterControl.Visibility = Visibility.Visible;
            IntegrateIdentityAuthControl.Visibility = Visibility.Collapsed;
            IntegrateInviteAuthControl.Visibility = Visibility.Collapsed;
        }

        private void IdentityAuth()
        {
            OperatingName = "身份验证";
            InterateVisitorRegisterControl.Visibility = Visibility.Collapsed;
            IntegrateIdentityAuthControl.Visibility = Visibility.Visible;
            IntegrateInviteAuthControl.Visibility = Visibility.Collapsed;
        }

        private void InviteAuth()
        {
            OperatingName = "邀约验证";
            InterateVisitorRegisterControl.Visibility = Visibility.Collapsed;
            IntegrateIdentityAuthControl.Visibility = Visibility.Collapsed;
            IntegrateInviteAuthControl.Visibility = Visibility.Visible;
        }

        public string OperatingName
        {
            get
            {
                return _operatingName;
            }

            set
            {
                _operatingName = value;
                NotifyPropertyChanged();
            }
        }

    }
}
