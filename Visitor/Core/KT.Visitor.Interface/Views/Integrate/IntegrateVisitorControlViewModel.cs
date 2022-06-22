using KT.Proxy.WebApi.Backend.Apis;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Helper;
using KT.Visitor.Interface.Views.Visitor.Controls;
using Panuon.UI.Silver.Core;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Integrate
{
    public class IntegrateVisitorControlViewModel : PropertyChangedBase
    {
        public IntegrateAccompanyVisitorControl AccompanyVisitorControl { get; private set; }
        public IntegrateAccompanyVisitorsWindowViewModel IntegrateAccompanyVisitorsWindowViewModel { get; private set; }

        public ICommand AddAccompanyCardCommand { get; private set; }
        public ICommand AddAccompanyPhotoCommand { get; private set; }
        public ICommand AddAccompanyCommand { get; private set; }
        public ICommand ChangeNormalAccompanyCommand { get; private set; }
        public ICommand ChangeSimpleAccompanyCommand { get; private set; }

        public AddAccompanyCardWindowViewModel AddAccompanyCardWindowViewModel { get; private set; }
        public AddAccompanyPhotoWindowViewModel AddAccompanyPhotoWindowViewModel { get; private set; }

        private string _authModel;
        private Visibility _visibilityNormalCompany = Visibility.Visible;

        //访问原因
        private ObservableCollection<ItemsCheckViewModel> visitReasons;
        private VistitorConfigApi _vistitorConfigApi;
        private VistitorConfigHelper _vistitorConfigHelper;
        private IContainerProvider _containerProvider;

        public IntegrateVisitorControlViewModel(IntegrateAccompanyVisitorControl accompanyVisitorControl,
                                                VistitorConfigApi vistitorConfigApi,
                                                VistitorConfigHelper vistitorConfigHelper,
                                                IContainerProvider containerProvider)
        {
            AccompanyVisitorControl = accompanyVisitorControl;
            AccompanyVisitorControl.ViewModel.IsMain = true;

            _vistitorConfigApi = vistitorConfigApi;
            _vistitorConfigHelper = vistitorConfigHelper;
            _containerProvider = containerProvider;

            AddAccompanyCardCommand = new DelegateCommand(AddAccompanyCard);
            AddAccompanyPhotoCommand = new DelegateCommand(AddAccompanyPhoto);
            AddAccompanyCommand = new DelegateCommand(AddAccompany);
            ChangeNormalAccompanyCommand = new DelegateCommand(ChangeNormalAccompany);
            ChangeSimpleAccompanyCommand = new DelegateCommand(ChangeSimpleAccompany);

            _ = InitAsync();
        }

        private void ChangeSimpleAccompany()
        {
            VisibilityNormalCompany = Visibility.Collapsed;
        }

        private void ChangeNormalAccompany()
        {
            VisibilityNormalCompany = Visibility.Visible;
        }

        private void AddAccompany()
        {
            var window = _containerProvider.Resolve<IntegrateAccompanyVisitorsWindow>();
            if (IntegrateAccompanyVisitorsWindowViewModel != null)
            {
                window.SetViewModel(IntegrateAccompanyVisitorsWindowViewModel);
            }
            else
            {
                IntegrateAccompanyVisitorsWindowViewModel = window.ViewModel;
            }
            window.ShowDialog();
        }

        private void AddAccompanyCard()
        {
            var window = _containerProvider.Resolve<AddAccompanyCardWindow>();
            if (AddAccompanyCardWindowViewModel != null)
            {
                window.SetViewModel(AddAccompanyCardWindowViewModel);
            }
            else
            {
                AddAccompanyCardWindowViewModel = window.ViewModel;
            }
            window.ShowDialog();
        }

        private void AddAccompanyPhoto()
        {
            var window = _containerProvider.Resolve<AddAccompanyPhotoWindow>();
            if (AddAccompanyPhotoWindowViewModel != null)
            {
                window.SetViewModel(AddAccompanyPhotoWindowViewModel);
            }
            else
            {
                AddAccompanyPhotoWindowViewModel = window.ViewModel;
            }
            window.ShowDialog();
        }

        public async Task InitAsync()
        {
            //初始化数据
            var visitorConfig = await _vistitorConfigApi.GetConfigParmsAsync();
            this.VisitReasons = _vistitorConfigHelper.SetVisitReasons(visitorConfig?.AccessReasons);
        }

        public ObservableCollection<ItemsCheckViewModel> VisitReasons
        {
            get
            {
                return visitReasons;
            }

            set
            {
                visitReasons = value;
                NotifyPropertyChanged();
            }
        }

        public string AuthModel
        {
            get
            {
                return _authModel;
            }

            set
            {
                _authModel = value;
                NotifyPropertyChanged();
            }
        }

        public Visibility VisibilityNormalCompany
        {
            get
            {
                return _visibilityNormalCompany;
            }

            set
            {
                _visibilityNormalCompany = value;
                NotifyPropertyChanged();
            }
        }
    }
}
