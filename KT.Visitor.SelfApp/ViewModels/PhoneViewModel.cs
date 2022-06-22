using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Mvvm;

namespace KT.Visitor.SelfApp.ViewModels
{
    public class PhoneViewModel : BindableBase
    {
        private string phone;

        private Visibility isError;
        private string errorMsg;

        public PhoneViewModel()
        {
            this.IsError = Visibility.Hidden;
        }

        public string Phone
        {
            get
            {
                return phone;
            }

            set
            {
                SetProperty(ref phone, value);
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
