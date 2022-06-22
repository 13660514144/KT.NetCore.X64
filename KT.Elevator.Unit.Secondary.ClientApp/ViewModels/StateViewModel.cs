using KT.Common.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Secondary.ClientApp.ViewModels
{
    public class StateViewModel : BindableBase
    {
        private bool _isOnline;
        private bool _isLoadingData;

        public bool IsOnline
        {
            get => _isOnline;
            set
            {
                SetProperty(ref _isOnline, value);
            }
        }
        public bool IsLoadingData
        {
            get => _isLoadingData;
            set
            {
                SetProperty(ref _isLoadingData, value);
            }
        }
    }
}
