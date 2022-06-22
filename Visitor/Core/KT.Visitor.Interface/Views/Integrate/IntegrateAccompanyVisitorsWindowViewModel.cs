using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Common.Core.Exceptions;
using KT.Proxy.WebApi.Backend.Apis;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Visitor.Controls;
using Microsoft.Extensions.DependencyInjection;
using Panuon.UI.Silver.Core;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Integrate
{
    public class IntegrateAccompanyVisitorsWindowViewModel : PropertyChangedBase
    {

        public ICommand AddAccompanyCommand { get; private set; }

        //访客列表
        private ObservableCollection<IntegrateAccompanyVisitorControl> _visitorControls;

        private IContainerProvider _containerProvider;
        private BlacklistApi _blacklistApi;

        public IntegrateAccompanyVisitorsWindowViewModel(IContainerProvider containerProvider,
            BlacklistApi blacklistApi)
        {
            _containerProvider = containerProvider;
            _blacklistApi = blacklistApi;

            Init();
        }
        public void Init()
        {
            VisitorControls = new ObservableCollection<IntegrateAccompanyVisitorControl>();

            //增加默认访客 
            AddVistor();

            AddAccompanyCommand = new DelegateCommand(AddVistor);
        }

        private void AddVistor()
        {
            var control = _containerProvider.Resolve<IntegrateAccompanyVisitorControl>();
            control.ViewModel.ControlRemove = ControlRemove;
            VisitorControls.Add(control);
        }

        /// <summary>
        /// 删除控件
        /// </summary> 
        private void ControlRemove(object control)
        {
            var accompanyContorl = (IntegrateAccompanyVisitorControl)control;
            VisitorControls.Remove(accompanyContorl);
        }

        public List<VisitorTeamModel> GetVisitorTeams()
        {
            //陪同访客
            var visitorTeams = new List<VisitorTeamModel>();
            foreach (var item in VisitorControls)
            {
                //不校验主访客 
                if (string.IsNullOrEmpty(item.ViewModel.VisitorInfo.Name)
                    && string.IsNullOrEmpty(item.ViewModel.VisitorInfo.CertificateNumber))
                {
                    continue;
                }

                //设置陪同访客信息
                var visitor = new VisitorTeamModel
                {
                    IcCard = item.ViewModel.VisitorInfo.IcCardNumber,
                    IdNumber = item.ViewModel.VisitorInfo.CertificateNumber,
                    Name = item.ViewModel.VisitorInfo.Name,
                    FaceImg = item.ViewModel.TakePictureControl.ViewModel.ImageUrl,
                    Gender = item.ViewModel.VisitorInfo.Gender
                };
                visitorTeams.Add(visitor);
            }
            return visitorTeams;
        }


        public ObservableCollection<IntegrateAccompanyVisitorControl> VisitorControls
        {
            get
            {
                return _visitorControls;
            }

            set
            {
                _visitorControls = value;
                NotifyPropertyChanged();
            }
        }

    }
}

