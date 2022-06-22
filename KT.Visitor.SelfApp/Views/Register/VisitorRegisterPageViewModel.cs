using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Common.Views.Helper;
using KT.Visitor.SelfApp.ViewModels;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KT.Visitor.SelfApp.Views.Auth
{
    public class VisitorRegisterPageViewModel : BindableBase
    {
        public string AuthModelType { get; set; }

        private PhoneViewModel _phoneVM;
        private ObservableCollection<ItemsCheckViewModel> _visitReasons;

        private VistitorConfigHelper _vistitorConfigHelper;
        private IFunctionApi _functionApi;

        public VisitorRegisterPageViewModel()
        {
            _phoneVM = ContainerHelper.Resolve<PhoneViewModel>();
            _vistitorConfigHelper = ContainerHelper.Resolve<VistitorConfigHelper>();
            _functionApi = ContainerHelper.Resolve<IFunctionApi>(); 
        }
         
        public PhoneViewModel PhoneVM
        {
            get
            {
                return _phoneVM;
            }

            set
            {
                SetProperty(ref _phoneVM, value);
            }
        }

        public ObservableCollection<ItemsCheckViewModel> VisitReasons
        {
            get
            {
                return _visitReasons;
            }

            set
            {
                SetProperty(ref _visitReasons, value);
            }
        }
    }
}
