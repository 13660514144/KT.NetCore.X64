using Panuon.UI.Silver.Core;

namespace KT.Visitor.Interface.Controls.BaseWindows
{
    public class MessageWarnBoxViewModel : PropertyChangedBase
    {        
        public MessageWarnBoxViewModel()
        {

        }

        public void SetValue(string mesg, string title, string confirmName, string cancelName)
        {
            Message = mesg;
            Title = title;
            ConfirmName = confirmName;
            CancelName = cancelName;
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                NotifyPropertyChanged();
            }
        }
        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                NotifyPropertyChanged();
            }
        }
        private string warnType;
        public string WarnType
        {
            get
            {
                return warnType;
            }
            set
            {
                warnType = value;
                NotifyPropertyChanged();
            }
        }

        private string confirmName;
        public string ConfirmName
        {
            get
            {
                return confirmName;
            }

            set
            {
                confirmName = value;
                NotifyPropertyChanged();
            }
        }

        private string cancelName;
        public string CancelName
        {
            get
            {
                return cancelName;
            }

            set
            {
                cancelName = value;
                NotifyPropertyChanged();
            }
        }


    }
}
