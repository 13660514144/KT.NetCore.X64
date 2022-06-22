using KT.Proxy.WebApi.Backend.Apis;
using KT.Visitor.Interface.ViewModels;
using KT.Proxy.WebApi.Backend.Models;
using Panuon.UI.Silver.Core;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Interface.Views.Setting
{
    public class NotifyPageViewModel : PropertyChangedBase
    {
        private NotifyApi _notifyApi;
        private CompanyApi _companyApi;

        // "紧急通知:#通知大楼#在#事件时间##通知事件#，请您#用户行为#，造成不便敬请谅解。"
        public NotifyPageViewModel(NotifyApi notifyApi, CompanyApi companyApi)
        {
            _notifyApi = notifyApi;
            _companyApi = companyApi;
        }

        //通知大楼
        private ObservableCollection<ItemsCheckViewModel> notifyFloors;

        private string sendText = "发送";

        //当天访客
        private bool isDayVisitor;
        //在楼访客
        private bool isVisitingVisitor;
        //在楼员工
        private bool isWorkingWorker;
        //所有员工
        private bool isAllWorker;

        // 通知事件
        private string notifyEvent;
        // 事件时间
        private string eventTime;
        // 用户行为
        private string userAction;

        // 通知短信
        private string notifyMessage;

        public async Task SendAsync()
        {
            NotifyModel model = new NotifyModel();

            //通知对象
            if (IsDayVisitor)
            {
                model.NoticeTypes.Add("VISITOR_ACCESS_TODAY");
            }
            if (IsVisitingVisitor)
            {
                model.NoticeTypes.Add("VISITOR_ON_EDIFICE");
            }
            if (IsWorkingWorker)
            {
                model.NoticeTypes.Add("STAFF_ON_EDIFICE");
            }
            if (IsAllWorker)
            {
                model.NoticeTypes.Add("ALL_STAFF");
            }

            //通知大楼
            foreach (var item in NotifyFloors)
            {
                if (item.IsChecked)
                {
                    model.EdificeIds.Add(item.Id);
                }
            }

            model.Event = NotifyEvent;
            model.Time = EventTime;
            model.Message = UserAction;

            await _notifyApi.SendNotifyAsync(model);
        }

        public async Task InitAsync()
        {
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
            str.Append(this.eventTime);
            str.Append(this.notifyEvent);
            str.Append("，请您");
            str.Append(this.userAction);
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

        public bool IsDayVisitor
        {
            get
            {
                return isDayVisitor;
            }

            set
            {
                isDayVisitor = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsVisitingVisitor
        {
            get
            {
                return isVisitingVisitor;
            }

            set
            {
                isVisitingVisitor = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsWorkingWorker
        {
            get
            {
                return isWorkingWorker;
            }

            set
            {
                isWorkingWorker = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsAllWorker
        {
            get
            {
                return isAllWorker;
            }

            set
            {
                isAllWorker = value;
                NotifyPropertyChanged();
            }
        }

        public string NotifyEvent
        {
            get
            {
                return notifyEvent;
            }

            set
            {
                notifyEvent = value;
                SetMessage();
                NotifyPropertyChanged();
            }
        }

        public string EventTime
        {
            get
            {
                return eventTime;
            }

            set
            {
                eventTime = value;
                SetMessage();
                NotifyPropertyChanged();
            }
        }

        public string UserAction
        {
            get
            {
                return userAction;
            }

            set
            {
                userAction = value;
                SetMessage();
                NotifyPropertyChanged();
            }
        }

        public string NotifyMessage
        {
            get
            {
                return notifyMessage;
            }

            set
            {
                notifyMessage = value;
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
    }
}
