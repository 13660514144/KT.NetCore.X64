using Prism.Mvvm;
using System.Windows;

namespace KT.Visitor.SelfApp.ViewModels
{
    public class PasswordViewModel : BindableBase
    {
        private bool isLogined = false;
        private string password;

        private Visibility isError;
        private string errorMsg;

        public PasswordViewModel()
        {
            this.IsError = Visibility.Hidden;
        }

        public bool IsLogined
        {
            get
            {
                return isLogined;
            }

            set
            {
                SetProperty(ref isLogined, value);
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                SetProperty(ref password, value);
                IsError = Visibility.Hidden;
                ErrorMsg = string.Empty;
            }
        }

        public Visibility IsError
        {
            get
            {
                return isError;
            }

            set
            {
                SetProperty(ref isError, value);
            }
        }

        public string ErrorMsg
        {
            get
            {
                return errorMsg;
            }

            set
            {
                SetProperty(ref errorMsg, value);
            }
        }
    }
}
