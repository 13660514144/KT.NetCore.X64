using KT.Common.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Kone.ToolApp.Models
{
    public class DopMaskRecordQueryViewModel : BindableBase
    {
        private string _elevatorServer;
        private string _type;
        private string _operate;
        private bool? _isSuccess;
        private int? _status;

        public string ElevatorServer
        {
            get => _elevatorServer;
            set
            {
                SetProperty(ref _elevatorServer, value);
            }
        }
        public string Type
        {
            get => _type;
            set
            {
                SetProperty(ref _type, value);
            }
        }
        public string Operate
        {
            get => _operate;
            set
            {
                SetProperty(ref _operate, value);
            }
        }
        public bool? IsSuccess
        {
            get => _isSuccess;
            set
            {
                SetProperty(ref _isSuccess, value);
            }
        }
        public int? Status
        {
            get => _status;
            set
            {
                SetProperty(ref _status, value);
            }
        }
    }
}
