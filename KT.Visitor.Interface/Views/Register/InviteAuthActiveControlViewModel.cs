using KT.Common.Core.Exceptions;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Common.Views.Helper;
using KT.Visitor.IdReader.Common;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Views.Auth.Controls;
using KT.Visitor.Interface.Views.Register;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Auth
{
    public class InviteAuthActiveControlViewModel : KT.Common.WpfApp.ViewModels.BindableBase
    {
        private ObservableCollection<InviteAuthDetailControl> _appointAuthDetailControls;

        public ICommand AuthAllCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        private Person _person;
        private bool _isSubmiting;

        private VistitorConfigHelper _vistitorConfigHelper;
        private IVisitorApi _visitorAuthApi;
        private PrintHandler _printHandler;
        private IFunctionApi _functionApi;
        private DialogHelper _dialogHelper;
        private IEventAggregator _eventAggregator;

        public InviteAuthActiveControlViewModel()
        {
            _visitorAuthApi = ContainerHelper.Resolve<IVisitorApi>();
            _printHandler = ContainerHelper.Resolve<PrintHandler>();
            _functionApi = ContainerHelper.Resolve<IFunctionApi>();
            _vistitorConfigHelper = ContainerHelper.Resolve<VistitorConfigHelper>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            AuthAllCommand = new DelegateCommand(AuthAll);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Cancel()
        {
            //授权完成
            _eventAggregator.GetEvent<RegistedSuccessEvent>().Publish();
        }

        public Task SetValueAsync(List<VisitorInfoModel> records, Person person)
        {
            _person = person;

            AppointAuthDetailControls = new ObservableCollection<InviteAuthDetailControl>();
            int order = 1;
            foreach (var item in records)
            {
                var control = ContainerHelper.Resolve<InviteAuthDetailControl>();
                control.ViewModel.VisitorInfo = item;
                control.ViewModel.RegistVisitor = new RegistVisitorViewModel();
                control.ViewModel.RegistVisitor.Order = order;
                if (person.Portrait != null)
                {
                    var idCardBitmap = new Bitmap(person.Portrait);
                    control.ViewModel.TakePictureControl.Init(idCardBitmap);
                }
                AppointAuthDetailControls.Add(control);
                order++;
            }

            return Task.CompletedTask;
        }

        private void AuthAll()
        {
            IsSubmiting = true;
            AuthAllAsync();
        }


        private async void AuthAllAsync()
        {
            try
            {
                var results = await AuthSubmitAsync();

                // 异步 打印二维码 
                _printHandler.StartPrintAsync(results, true);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    //成功后跳转访客登记页面  
                    var integrateSuccessWindow = ContainerHelper.Resolve<SuccessWindow>();
                    integrateSuccessWindow.ViewModel.Init(results);
                    _dialogHelper.ShowDialog(integrateSuccessWindow);
                });

                //授权完成
                _eventAggregator.GetEvent<RegistedSuccessEvent>().Publish();
                _eventAggregator.GetEvent<InviteAuthSuccessEvent>().Publish();
            }
            catch (Exception ex)
            {
                IsSubmiting = false;
                throw ex;
            }
            finally
            {
                IsSubmiting = false;
            }
        }

        private async Task<List<RegisterResultModel>> AuthSubmitAsync()
        {
            var configParms = await _functionApi.GetConfigParmsAsync();

            var authData = new AuthInfoModel();

            foreach (var item in AppointAuthDetailControls)
            {
                var visitor = new AuthVisitorModel();
                visitor.Id = item.ViewModel.VisitorInfo.Id;
                visitor.Ic = item.ViewModel.RegistVisitor.IcCard;
                visitor.FaceImg = item.ViewModel.TakePictureControl.ViewModel.CaptureImage?.ImageUrl;

                if (configParms?.OpenVisitorCheck == true && string.IsNullOrEmpty(visitor.FaceImg))
                {
                    throw CustomException.Run("开启人证比对需要拍照！");
                }
                authData.Visitors.Add(visitor);
            }

            //提交数据
            var results = await _visitorAuthApi.AuthAsync(authData);
            return results;
        }

        public ObservableCollection<InviteAuthDetailControl> AppointAuthDetailControls
        {
            get
            {
                return _appointAuthDetailControls;
            }

            set
            {
                SetProperty(ref _appointAuthDetailControls, value);
            }
        }

        public bool IsSubmiting
        {
            get
            {
                return _isSubmiting;
            }

            set
            {
                SetProperty(ref _isSubmiting, value);
            }
        }
    }
}
