using KT.Common.Core.Exceptions;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Apis;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KT.Visitor.SelfApp
{
    public class HomePageViewModel : BindableBase
    {
        public SystemInfoViewModel _systemInfo;

        public ICommand EnglishWarnCommand { get; private set; }

        private ConfigHelper _configHelper;
        private IFunctionApi _functionApi;
        private SelfAppSettings _selfAppSettings;

        public HomePageViewModel()
        {
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _functionApi = ContainerHelper.Resolve<IFunctionApi>();
            _selfAppSettings = ContainerHelper.Resolve<SelfAppSettings>();

            SystemInfo = ContainerHelper.Resolve<SystemInfoViewModel>();

            EnglishWarnCommand = new DelegateCommand(EnglishWarn);
        }

        private void EnglishWarn()
        {
            throw CustomException.RunTitle(_selfAppSettings.EnglishWarnTitle, _selfAppSettings.EnglishWarnText);
        }

        public SystemInfoViewModel SystemInfo
        {
            get
            {
                return _systemInfo;
            }

            set
            {
                _systemInfo = value;
            }
        }
    }
}
