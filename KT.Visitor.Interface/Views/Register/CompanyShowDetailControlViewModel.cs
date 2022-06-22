using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Interface.Events;
using Panuon.UI.Silver.Core;
using Prism.Events;
using System;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Register
{
    public class CompanyShowDetailControlViewModel : PropertyChangedBase
    {
        public ICommand ConfirmCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        private IEventAggregator _eventAggregator;
        public CompanyShowDetailControlViewModel()
        {
            ConfirmCommand = new DelegateCommand(Confirm);
            CancelCommand = new DelegateCommand(Cancel);

            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
        }

        private void Cancel()
        {
            Company = null;
            _eventAggregator.GetEvent<CompanySelectedEvent>().Publish(null);
        }

        private void Confirm()
        {
            _eventAggregator.GetEvent<CompanySelectedEvent>().Publish(Company);
        }

        private CompanyViewModel company;

        public CompanyViewModel Company
        {
            get
            {
                return company;
            }

            set
            {
                company = value;
                NotifyPropertyChanged();
            }
        }
    }
}
