using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Interface.Views.Visitor.Controls;
using Prism.Ioc;
using Prism.Mvvm;
namespace KT.Visitor.Interface.Views.Register
{
    public class InviteAuthDetailControlViewModel : BindableBase
    {
        public ShowTakePictureControl TakePictureControl { get; set; }

        private VisitorInfoModel _visitorInfo;
        private RegistVisitorViewModel _registVisitor;

        public InviteAuthDetailControlViewModel()
        {
            TakePictureControl = ContainerHelper.Resolve<ShowTakePictureControl>();
        }

        public VisitorInfoModel VisitorInfo
        {
            get
            {
                return _visitorInfo;
            }

            set
            {
                SetProperty(ref _visitorInfo, value);
            }
        }

        public RegistVisitorViewModel RegistVisitor
        {
            get
            {
                return _registVisitor;
            }

            set
            {
                SetProperty(ref _registVisitor, value);
            }
        }
    }
}
