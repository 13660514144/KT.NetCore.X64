using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.IdReader.Common;
using KT.Visitor.IntegrateApp.Views;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Views.Auth;
using Microsoft.Extensions.Logging;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace KT.Visitor.Interface.Views.Register
{
    public class InviteAuthControlViewModel : Prism.Mvvm.BindableBase
    {
        public InviteAuthSearchControl _integrateInviteAuthSearchControl { get; set; }
        public ReadIdCardControl _readIdCardControl { get; set; }
        public InviteAuthActiveControl _integrateInviteAuthActiveControl { get; set; }

        private List<VisitorInfoModel> _records;
        private IRegion _contentRegion;

        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;
        private readonly ILogger _logger;

        public InviteAuthControlViewModel()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            _integrateInviteAuthSearchControl = ContainerHelper.Resolve<InviteAuthSearchControl>();
            _readIdCardControl = ContainerHelper.Resolve<ReadIdCardControl>();
            _integrateInviteAuthActiveControl = ContainerHelper.Resolve<InviteAuthActiveControl>();
            _logger = ContainerHelper.Resolve<ILogger>();

            _eventAggregator.GetEvent<InviteAuthSearchedEvent>().Subscribe(SearchEnd);
            _eventAggregator.GetEvent<ReadedPersonEvent>().Subscribe(EndReadIdAsync);
            _eventAggregator.GetEvent<InviteAuthSuccessEvent>().Subscribe(InviteAuthSuccess);
            _eventAggregator.GetEvent<InviteAuthLinkEvent>().Subscribe(InviteAuthSuccess);
        }

        private void InviteAuthSuccess()
        {
            _contentRegion.Activate(_integrateInviteAuthSearchControl);
        }

        internal void ViewLoadedInit(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _contentRegion = _regionManager.Regions[RegionNameHelper.InviteAuthContentRegion];

            _contentRegion.Add(_integrateInviteAuthSearchControl);
            _contentRegion.Add(_readIdCardControl);
            _contentRegion.Add(_integrateInviteAuthActiveControl);

            _contentRegion.Activate(_integrateInviteAuthSearchControl);
        }

        private bool _isPersonSeted = false;
        private void EndReadIdAsync(Person person)
        {
            if (_isPersonSeted || _records == null || _records.FirstOrDefault() == null)
            {
                return;
            }
            _isPersonSeted = true;

            try
            {
                _records = _records.Where(x => (string.IsNullOrEmpty(x.IdNumber) && x.Name == person.Name)
                                   || (!string.IsNullOrEmpty(x.IdNumber) && x.IdNumber == person.IdCode)).ToList();

                Application.Current.Dispatcher.Invoke(async () =>
                {
                    if (_records == null || _records.FirstOrDefault() == null)
                    {
                        ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("身份证姓名与访客姓名不一致！");
                        _contentRegion.Activate(_integrateInviteAuthSearchControl);
                        _isPersonSeted = false;
                        return;
                    }

                    _contentRegion.Activate(_integrateInviteAuthActiveControl);
                    await _integrateInviteAuthActiveControl.ViewModel.SetValueAsync(_records, person);

                    _records = null;
                    _isPersonSeted = false;
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "邀约认证错误！");
                ContainerHelper.Resolve<MessageWarnBox>().ShowMessage(ex.Message);
                //发布错误事件
                _eventAggregator?.GetEvent<ExceptionEvent>().Publish(ex);
            }
            finally
            {
                _isPersonSeted = false;
            }
        }

        private void SearchEnd(List<VisitorInfoModel> records)
        {
            _records = records;

            _contentRegion.Activate(_readIdCardControl);
        }
    }
}
