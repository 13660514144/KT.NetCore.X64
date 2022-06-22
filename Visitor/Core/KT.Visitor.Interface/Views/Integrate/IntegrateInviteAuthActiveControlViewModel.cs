using KT.Proxy.WebApi.Backend.Models;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Helper;
using KT.Visitor.Interface.Views.Visitor.Controls;
using Panuon.UI.Silver.Core;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KT.Visitor.Interface.Views.Auth
{
    public class IntegrateInviteAuthActiveControlViewModel : PropertyChangedBase
    {
        public TakePictureControl TakePictureControl { get; set; }
        private VisitorInfoModel record;
        private ObservableCollection<ItemsCheckViewModel> authTypes;
        private VistitorConfigHelper _vistitorConfigHelper;

        public IntegrateInviteAuthActiveControlViewModel(VistitorConfigHelper vistitorConfigHelper,
            TakePictureControl takePictureControl)
        {
            _vistitorConfigHelper = vistitorConfigHelper;
            TakePictureControl = takePictureControl;

            _ = InitAsync();
        }
        public async Task InitAsync()
        {
            this.AuthTypes = await _vistitorConfigHelper.InitAuthTypesAsync();
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

        public VisitorInfoModel Record
        {
            get
            {
                return record;
            }

            set
            {
                record = value;
                NotifyPropertyChanged();
            }
        }
    }
}