using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.Utils;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Interface.Events;
using Panuon.UI.Silver.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Register
{
    public class AccompanyVisitorsWindowViewModel : PropertyChangedBase
    {
        public Action<string, AccompanyVisitorControlViewModel> ScanAndSetPersonAction { get; set; }

        public ICommand AddAccompanyCommand { get; private set; }


        //访客列表
        private ObservableCollection<AccompanyVisitorControl> _visitorControls;

        private IBlacklistApi _blacklistApi;

        private IEventAggregator _eventAggregator;

        public AccompanyVisitorsWindowViewModel()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _blacklistApi = ContainerHelper.Resolve<IBlacklistApi>();

            _eventAggregator.GetEvent<RegistedSuccessEvent>().Subscribe(ClearAccompany);

            AddAccompanyCommand = new DelegateCommand(AddVistor);

            Init();
        }

        public void Init()
        {
            VisitorControls = new ObservableCollection<AccompanyVisitorControl>();
        }

        internal void ClearAccompany()
        {
            VisitorControls.Clear();
        }

        public void AddVistor()
        {
            var control = ContainerHelper.Resolve<AccompanyVisitorControl>();
            control.ViewModel.ControlRemove = ControlRemove;
            control.ViewModel.ScanAndSetPersonAction = ScanAndSetPersonAction;
            if (VisitorControls == null || VisitorControls.FirstOrDefault() == null)
            {
                control.ViewModel.IsFirst = true;
                control.ViewModel.Order = 0;
            }
            else
            {
                control.ViewModel.Order = VisitorControls.Count + 1;
            }
            VisitorControls.Add(control);
        }

        /// <summary>
        /// 删除控件
        /// </summary> 
        private void ControlRemove(object control)
        {
            var accompanyContorl = (AccompanyVisitorControl)control;
            VisitorControls.Remove(accompanyContorl);
            if (VisitorControls.FirstOrDefault() == null)
            {
                AddVistor();
                return;
            }

            //重新排序
            for (int i = accompanyContorl.ViewModel.Order; i <= VisitorControls.Count; i++)
            {
                VisitorControls[i - 1].ViewModel.Order = i;
            }
        }

        public List<VisitorInfoModel> GetVisitorTeams()
        {
            //陪同访客
            var visitorTeams = new List<VisitorInfoModel>();
            foreach (var item in VisitorControls)
            {
                //设置陪同访客信息
                var visitor = new VisitorInfoModel
                {
                    IcCard = item.ViewModel.VisitorInfo.IcCard,
                    IdNumber = item.ViewModel.VisitorInfo.CertificateNumber,
                    Name = item.ViewModel.VisitorInfo.Name,
                    FaceImg = item.ViewModel.TakePictureControl.ViewModel.CaptureImage?.ImageUrl,
                    Gender = item.ViewModel.VisitorInfo.Gender,
                    PhotoImage = item.ViewModel.TakePictureControl.ViewModel.CaptureImage?.Image,
                    IdCardImage = item.ViewModel.VisitorInfo.HeadImg,
                    Phone=item.ViewModel.VisitorInfo.Phone
                };

                visitorTeams.Add(visitor);
            }
            return visitorTeams;
        }


        public ObservableCollection<AccompanyVisitorControl> VisitorControls
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

