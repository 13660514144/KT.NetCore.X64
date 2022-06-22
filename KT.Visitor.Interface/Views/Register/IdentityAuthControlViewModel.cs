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
    public class IdentityAuthControlViewModel : Prism.Mvvm.BindableBase
    {
        private IdentityAuthSearchControl _integrateIdentityAuthSearchControl;
        private ReadIdCardControl _readIdCardControl;
        private IdentityAuthActiveControl _integrateIdentityAuthActiveControl;

        private List<VisitorInfoModel> _records;
        private IRegion _contentRegion;

        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;
        private readonly ILogger _logger;

        public IdentityAuthControlViewModel()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            _integrateIdentityAuthSearchControl = ContainerHelper.Resolve<IdentityAuthSearchControl>();
            _readIdCardControl = ContainerHelper.Resolve<ReadIdCardControl>();
            _integrateIdentityAuthActiveControl = ContainerHelper.Resolve<IdentityAuthActiveControl>();
            _logger = ContainerHelper.Resolve<ILogger>();

            _eventAggregator.GetEvent<IdentityAuthSearchedEvent>().Subscribe(SearchEnd);
            _eventAggregator.GetEvent<ReadedPersonEvent>().Subscribe(EndReadIdAsync);
            _eventAggregator.GetEvent<IdentityAuthSuccessEvent>().Subscribe(IdentityAuthSuccess);
            _eventAggregator.GetEvent<IdentityAuthLinkEvent>().Subscribe(IdentityAuthSuccess);

            //Init();
        }


        private void IdentityAuthSuccess()
        {
            _contentRegion.Activate(_integrateIdentityAuthSearchControl);
        }

        internal void ViewLoadedInit(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _contentRegion = _regionManager.Regions[RegionNameHelper.IdentityAuthContentRegion];

            _contentRegion.Add(_integrateIdentityAuthSearchControl);
            _contentRegion.Add(_readIdCardControl);
            _contentRegion.Add(_integrateIdentityAuthActiveControl);

            _contentRegion.Activate(_integrateIdentityAuthSearchControl);

            _isPersonSeted = false;
        }

        private bool _isPersonSeted = false;
        private void EndReadIdAsync(Person person)
        {
            if (_records?.FirstOrDefault() == null)
            {
                return;
            }
            if (_isPersonSeted)
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
                        _contentRegion.Activate(_integrateIdentityAuthSearchControl);
                        _isPersonSeted = false;
                        return;
                    }

                    //人证比对
                    var firstRecord = _records.FirstOrDefault();
                    var isPassCheckIdCardFace = await _readIdCardControl.ViewModel.CheckIdCardFaceAsync(person.Portrait, firstRecord.FaceImg);
                    if (!isPassCheckIdCardFace)
                    {
                        _isPersonSeted = false;
                        return;
                    }

                    _contentRegion.Activate(_integrateIdentityAuthActiveControl);
                    await _integrateIdentityAuthActiveControl.ViewModel.SetValueAsync(_records, person);

                    _records = null;
                    _isPersonSeted = false;
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "身份认证错误！");
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

            //_readIdCardControl.Visibility = Visibility.Visible;
            //_integrateIdentityAuthSearchControl.Visibility = Visibility.Collapsed;

            _contentRegion.Activate(_readIdCardControl);
        }

        //public void Init()
        //{
        //_readIdCardControl.Visibility = Visibility.Collapsed;
        //_integrateIdentityAuthActiveControl.Visibility = Visibility.Collapsed;
        //}
    }
}
