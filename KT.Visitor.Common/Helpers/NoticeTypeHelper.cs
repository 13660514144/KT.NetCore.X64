using KT.Proxy.BackendApi.Enums;
using KT.Visitor.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace KT.Visitor.Common.Helpers
{
    public class NoticeTypeHelper
    {
        public NoticeTypeHelper()
        {

        }

        public ObservableCollection<ItemsCheckViewModel> GetItemViewModels()
        {
            var results = new ObservableCollection<ItemsCheckViewModel>();
            foreach (var item in NoticeTypeEnum.Items)
            {
                var result = new ItemsCheckViewModel(item.Value, item.Text);

                results.Add(result);
            }
            return results;
        }
    }
}
