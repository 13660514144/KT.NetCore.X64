using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace KT.Visitor.Common.ViewModels
{
    public class ExtendCollectionViewModel<T> : BindableBase
    {
        public ExtendCollectionViewModel()
        {
        }
        private int _order;
        private T _data;

        public int Order
        {
            get
            {
                return _order;
            }

            set
            {
                SetProperty(ref _order, value);
            }
        }

        public T Data
        {
            get
            {
                return _data;
            }

            set
            {
                SetProperty(ref _data, value);
            }
        }

    }
}
