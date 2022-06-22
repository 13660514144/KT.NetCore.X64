using Prism.Mvvm;

namespace KT.Visitor.SelfApp.Views.Common
{
    public class OperateErrorPageViewModel : BindableBase
    {
        private string _title;
        private string _errorMsg;

        public OperateErrorPageViewModel()
        {
            Title = "温馨提示";
        }

        public string ErrorMsg
        {
            get
            {
                return _errorMsg;
            }

            set
            {
                SetProperty(ref _errorMsg, value);
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                SetProperty(ref _title, value);
            }
        }
    }
}
