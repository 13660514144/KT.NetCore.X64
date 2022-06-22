using KT.Visitor.Interface.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Panuon.UI.Silver.Core;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    public class VisitorsControlViewModel : PropertyChangedBase
    {
        //访客列表
        private ObservableCollection<AccompanyVisitorControl> _visitorControls;
        private IContainerProvider _containerProvider;

        public VisitorsControlViewModel(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;

            Init();
        }
        public void Init()
        {
            VisitorControls = new ObservableCollection<AccompanyVisitorControl>();

            //增加默认主访客
            var mainVisitor = _containerProvider.Resolve<AccompanyVisitorControl>();
            mainVisitor.ViewModel.IsMain = true;
            VisitorControls.Add(mainVisitor);
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
