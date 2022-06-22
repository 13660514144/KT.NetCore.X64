using KT.Common.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace KT.Quanta.Kone.ToolApp.Models
{
    public class PassRightViewModel : BindableBase
    {
        private string _name;
        private string _sign;
        private ObservableCollection<FloorViewModel> _floors;
        private string _floorNames;

        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
            }
        }
        public string Sign
        {
            get => _sign;
            set
            {
                SetProperty(ref _sign, value);
            }
        }
        public ObservableCollection<FloorViewModel> Floors
        {
            get => _floors;
            set
            {
                SetProperty(ref _floors, value);
            }
        }
        public string FloorNames
        {
            get => _floorNames;
            set
            {
                SetProperty(ref _floorNames, value);
            }
        }
    }
}
