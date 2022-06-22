using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Proxy.BackendApi.Models;
using Panuon.UI.Silver.Core;
using Prism.Mvvm;

namespace KT.Visitor.Interface.Views.Controls
{
    public class AssistantVisitorControlViewModel : BindableBase
    {
        private VisitorInfoModel _visitorInfo;

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
    }
}
