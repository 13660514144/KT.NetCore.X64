using KT.Common.Core.Exceptions;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.ViewModels;
using Panuon.UI.Silver.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.FrontApp.Views
{
    public class NotifyControlViewModel : PropertyChangedBase
    {
        private IFunctionApi _notifyApi;
        private ICompanyApi _companyApi;
        private NoticeTypeHelper _noticeTypeHelper;

        // "紧急通知:#通知大楼#在#事件时间##通知事件#，请您#用户行为#，造成不便敬请谅解。"
        public NotifyControlViewModel()
        {
            _notifyApi = ContainerHelper.Resolve<IFunctionApi>();
            _companyApi = ContainerHelper.Resolve<ICompanyApi>();
            _noticeTypeHelper = ContainerHelper.Resolve<NoticeTypeHelper>();

            //_ = InitAsync();
        }

        //通知大楼
        private ObservableCollection<ItemsCheckViewModel> notifyFloors;

        private string sendText = "发送";

        ////当天访客
        //private bool isDayVisitor;
        ////在楼访客
        //private bool isVisitingVisitor;
        ////在楼员工
        //private bool isWorkingWorker;
        ////所有员工
        //private bool isAllWorker;

        // 通知事件
        private string _notifyEvent;
        // 事件时间
        private string _eventTime;
        // 用户行为
        private string _userAction;

        // 通知短信
        private string _notifyMessage;

        private ObservableCollection<ItemsCheckViewModel> noticeTypes;

        public async Task RefreshAsync()
        {
            await InitAsync();
             
            NotifyEvent = string.Empty;
            EventTime = string.Empty;
            UserAction = string.Empty;
            NotifyMessage = string.Empty;

        }

        public async Task SendAsync()
        {
            NotifyModel model = new NotifyModel();

            //通知对象 
            model.NoticeTypes = NoticeTypes.Where(x => x.IsChecked).Select(x => x.Id).ToList();

            //通知大楼
            model.EdificeIds = NotifyFloors.Where(x => x.IsChecked).Select(x => x.Id).ToList();

            model.Event = NotifyEvent;
            model.Time = EventTime;
            model.Message = UserAction;

            //较验数据 
            if (model.EdificeIds == null || model.EdificeIds.FirstOrDefault() == null)
            {
                throw CustomException.Run("请选择通知大楼");
            }
            if (model.NoticeTypes == null || model.NoticeTypes.FirstOrDefault() == null)
            {
                throw CustomException.Run("请选择通知对象");
            }
            if (string.IsNullOrEmpty(model.Event))
            {
                throw CustomException.Run("请输入通知事件");
            }
            if (string.IsNullOrEmpty(model.Time))
            {
                throw CustomException.Run("请输入事件时间");
            }
            if (string.IsNullOrEmpty(model.Message))
            {
                throw CustomException.Run("请输入用户行为");
            }

            await _notifyApi.SendNotifyAsync(model);
        }

        public async Task InitAsync()
        {
            NoticeTypes = _noticeTypeHelper.GetItemViewModels();

            this.NotifyFloors = new ObservableCollection<ItemsCheckViewModel>();
            var buls = await _companyApi.GetBuildingsAsync();
            if (buls != null && buls.Count > 0)
            {
                foreach (var item in buls)
                {
                    this.NotifyFloors.Add(new ItemsCheckViewModel(item.Id.ToString(), item.EdificeName));
                }
            }
        }
        public void SetMessage()
        {
            StringBuilder str = new StringBuilder();
            str.Append("紧急通知:");
            str.Append(GetNotifyFloorAllName());
            str.Append("在");
            str.Append(this._eventTime);
            str.Append(this._notifyEvent);
            str.Append("，请您");
            str.Append(this._userAction);
            str.Append("，造成不便敬请谅解。");

            this.NotifyMessage = str.ToString();
        }

        private string GetNotifyFloorAllName()
        {
            string floorName = string.Empty;
            foreach (var item in this.NotifyFloors)
            {
                if (!item.IsChecked)
                {
                    continue;
                }
                floorName += item.Name + "、";
            }
            if (floorName.Length > 0)
            {
                floorName = floorName.Substring(0, floorName.Length - 1);
            }
            return floorName;
        }


        public ObservableCollection<ItemsCheckViewModel> NotifyFloors
        {
            get
            {
                return notifyFloors;
            }

            set
            {
                notifyFloors = value;
                SetMessage();
                NotifyPropertyChanged();
            }
        }

        //public bool IsDayVisitor
        //{
        //    get
        //    {
        //        return isDayVisitor;
        //    }

        //    set
        //    {
        //        isDayVisitor = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        //public bool IsVisitingVisitor
        //{
        //    get
        //    {
        //        return isVisitingVisitor;
        //    }

        //    set
        //    {
        //        isVisitingVisitor = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        //public bool IsWorkingWorker
        //{
        //    get
        //    {
        //        return isWorkingWorker;
        //    }

        //    set
        //    {
        //        isWorkingWorker = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        //public bool IsAllWorker
        //{
        //    get
        //    {
        //        return isAllWorker;
        //    }

        //    set
        //    {
        //        isAllWorker = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        public string NotifyEvent
        {
            get
            {
                return _notifyEvent;
            }

            set
            {
                _notifyEvent = value;
                SetMessage();
                NotifyPropertyChanged();
            }
        }

        public string EventTime
        {
            get
            {
                return _eventTime;
            }

            set
            {
                _eventTime = value;
                SetMessage();
                NotifyPropertyChanged();
            }
        }

        public string UserAction
        {
            get
            {
                return _userAction;
            }

            set
            {
                _userAction = value;
                SetMessage();
                NotifyPropertyChanged();
            }
        }

        public string NotifyMessage
        {
            get
            {
                return _notifyMessage;
            }

            set
            {
                _notifyMessage = value;
                NotifyPropertyChanged();
            }
        }

        public string SendText
        {
            get
            {
                return sendText;
            }

            set
            {
                sendText = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<ItemsCheckViewModel> NoticeTypes
        {
            get
            {
                return noticeTypes;
            }

            set
            {
                noticeTypes = value;
                NotifyPropertyChanged();
            }
        }
    }
}
