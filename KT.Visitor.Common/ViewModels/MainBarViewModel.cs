using Panuon.UI.Silver.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Common.ViewModels
{
    public class MainBarInfoViewModel : PropertyChangedBase
    {
        private int _dayVisitorCount;
        private int _currentVisitorCount;

        public int DayVisitorCount
        {
            get
            {
                return _dayVisitorCount;
            }

            set
            {
                _dayVisitorCount = value;
                NotifyPropertyChanged();
            }
        }

        public int CurrentVisitorCount
        {
            get
            {
                return _currentVisitorCount;
            }

            set
            {
                _currentVisitorCount = value;
                NotifyPropertyChanged();
            }
        }
    }
}
