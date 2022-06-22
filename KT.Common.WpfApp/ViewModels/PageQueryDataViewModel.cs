using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.WpfApp.ViewModels
{
    public class PageQueryDataViewModel<TResult, TQuery> : PageDataViewModel<TResult> where TQuery : new()
    {
        private TQuery _query;

        public PageQueryDataViewModel()
        {
            _query = new TQuery();
        }

        public TQuery Query
        {
            get => _query;
            set
            {
                SetProperty(ref _query, value);
            }
        }
    }
}
