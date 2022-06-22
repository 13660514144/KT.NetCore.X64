using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Helper;
using Panuon.UI.Silver.Core;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KT.Visitor.Interface.Views.Auth
{
    public class IntegrateIdentityAuthActiveControlViewModel : PropertyChangedBase
    {
        private ObservableCollection<ItemsCheckViewModel> authTypes;
        private VistitorConfigHelper _vistitorConfigHelper;
        public IntegrateIdentityAuthActiveControlViewModel(VistitorConfigHelper vistitorConfigHelper)
        {
            _vistitorConfigHelper = vistitorConfigHelper;

            _ = InitAsync();
        }

        private async Task InitAsync()
        {
            var result = await _vistitorConfigHelper.InitAuthTypesAsync();
            this.AuthTypes = result;
        }

        public ObservableCollection<ItemsCheckViewModel> AuthTypes
        {
            get
            {
                return authTypes;
            }

            set
            {
                authTypes = value;
                NotifyPropertyChanged();
            }
        }
    }
}
