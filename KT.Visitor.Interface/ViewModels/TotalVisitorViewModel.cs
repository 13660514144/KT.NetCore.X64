using KT.Common.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Interface.ViewModels
{
    public class TotalVisitorViewModel : BindableBase
    {
        private int _todayVistorNum;
        private int _totalVisitorNum;

        public int TodayVistorNum
        {
            get
            {
                return _todayVistorNum;
            }

            set
            {
                SetProperty(ref _todayVistorNum, value);
            }
        }

        public int TotalVisitorNum
        {
            get
            {
                return _totalVisitorNum;
            }

            set
            {
                SetProperty(ref _totalVisitorNum, value);
            }
        }
    }
}
