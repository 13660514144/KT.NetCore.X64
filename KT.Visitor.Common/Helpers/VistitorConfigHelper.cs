using KT.Common.Core.Utils;
using KT.Proxy.BackendApi.Apis;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Visitor.Common.Views.Helper
{
    public class VistitorConfigHelper
    {
        private IFunctionApi _vistitorConfigApi;
        private ConfigHelper _configHelper;

        public VistitorConfigHelper(IFunctionApi vistitorConfigApi,
             ConfigHelper configHelper)
        {
            _vistitorConfigApi = vistitorConfigApi;
            _configHelper = configHelper;
        }
 
        /// <summary>
        /// 初始化来访事由
        /// </summary>
        /// <param name="visitReasons"></param>
        /// <returns></returns>
        public ObservableCollection<ItemsCheckViewModel> SetVisitReasons(List<string> visitReasons)
        {
            var results = new ObservableCollection<ItemsCheckViewModel>();
            if (visitReasons == null)
            {
                return results;
            }
            foreach (var item in visitReasons)
            {
                results.Add(new ItemsCheckViewModel(item.IsNull()));
            }
            //设置默认选择第一个
            var first = results.FirstOrDefault();
            first.IsChecked = true;

            return results;
        }
    }
}
