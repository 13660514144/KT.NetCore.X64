using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Common.ViewModels
{
    public class BlacklistQueryViewModel : BindableBase
    {
        private string _name;
        private string _phone;
        private string _idNumber;
         

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                SetProperty(ref _name, value);
            }
        }

        public string Phone
        {
            get
            {
                return _phone;
            }

            set
            {
                SetProperty(ref _phone, value);
            }
        }

        public string IdNumber
        {
            get
            {
                return _idNumber;
            }

            set
            {
                SetProperty(ref _idNumber, value);
            }
        }
    }
}
