using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Enums;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Data.Enums;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Register;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace KT.Visitor.Interface.Views.Register
{
    public class IdCardCheckWarnWindowViewModel : BindableBase
    {
        private IdCardCheckViewModel _idCardCheck;

        private IEventAggregator _eventAggregator;
        public IdCardCheckWarnWindowViewModel()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
        }

        public IdCardCheckViewModel IdCardCheck
        {
            get
            {
                return _idCardCheck;
            }

            set
            {
                SetProperty(ref _idCardCheck, value);
            }
        }
    }
}
