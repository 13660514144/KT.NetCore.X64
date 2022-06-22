using KT.Common.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace KT.Quanta.Kone.ToolApp.Models
{
    public class EdificeViewModel : BindableBase
    {
        private string _id;
        private string _name;
        private ObservableCollection<FloorViewModel> _floors;
         
        private bool _isFrontAllChecked;
        private bool _isRearAllChecked;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public ObservableCollection<FloorViewModel> Floors
        {
            get
            {
                return _floors;
            }

            set
            {
                SetProperty(ref _floors, value);
            }
        }

        public bool IsFrontAllChecked
        {
            get
            {
                return _isFrontAllChecked;
            }

            set
            {
                SetProperty(ref _isFrontAllChecked, value);
            }
        }

        public bool IsRearAllChecked
        {
            get
            {
                return _isRearAllChecked;
            }

            set
            {
                SetProperty(ref _isRearAllChecked, value);
            }
        }

    }
}
