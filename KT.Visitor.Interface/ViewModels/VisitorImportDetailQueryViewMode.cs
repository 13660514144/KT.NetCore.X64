using KT.Common.WpfApp.ViewModels;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Data.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace KT.Visitor.Interface.ViewModels
{
    public class VisitorImportDetailQueryViewModel : BindableBase
    {
        private string _name;
        private string _phone;
        private string _icCard;
        private string _status;
        private ObservableCollection<StatusEnum> _statuses;

        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
            }
        }
        public string Phone
        {
            get => _phone;
            set
            {
                SetProperty(ref _phone, value);
            }
        }
        public string IcCard
        {
            get => _icCard;
            set
            {
                SetProperty(ref _icCard, value);
            }
        }
        public string Status
        {
            get => _status;
            set
            {
                SetProperty(ref _status, value);
            }
        }
        public ObservableCollection<StatusEnum> Statuses
        {
            get => _statuses;
            set
            {
                SetProperty(ref _statuses, value);
            }
        }
    }
}
