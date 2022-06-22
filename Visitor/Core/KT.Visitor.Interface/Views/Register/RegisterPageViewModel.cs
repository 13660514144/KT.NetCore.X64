using KT.Proxy.WebApi.Backend.Apis;
using KT.Visitor.IdReader.Common;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Helper;
using KT.Visitor.Interface.Views.Integrate;
using KT.Visitor.Interface.Views.Visitor.Controls;
using Microsoft.Extensions.DependencyInjection;
using Panuon.UI.Silver.Core;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KT.Visitor.Interface.Views.Visitor
{
    public class RegisterPageViewModel : PropertyChangedBase
    {
        public IntegrateRegisterControl IntegrateRegisterControl { get; set; }

        private IContainerProvider _containerProvider;

        public RegisterPageViewModel(IntegrateRegisterControl integrateRegisterControl,
            IContainerProvider containerProvider)
        {
            IntegrateRegisterControl = integrateRegisterControl;
            _containerProvider = containerProvider;
        }
    }
}