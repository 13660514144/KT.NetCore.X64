using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Common.ViewModels
{
    public class TopPanelViewModel : BindableBase
    {
        private int _todyVisitorNum;
        private int _visitingNum;
        private string _userName;

        public int TodyVisitorNum
        {
            get
            {
                return _todyVisitorNum;
            }

            set
            {
                SetProperty(ref _todyVisitorNum, value);
            }
        }

        public int VisitingNum
        {
            get
            {
                return _visitingNum;
            }

            set
            {
                SetProperty(ref _visitingNum, value);
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                SetProperty(ref _userName, value);
            }
        }
    }
}
