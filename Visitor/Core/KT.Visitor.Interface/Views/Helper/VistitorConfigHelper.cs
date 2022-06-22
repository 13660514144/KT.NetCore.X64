using KT.Common.Core.Utils;
using KT.Proxy.WebApi.Backend.Apis;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Visitor.Interface.Views.Helper
{
    public class VistitorConfigHelper
    {
        private VistitorConfigApi _vistitorConfigApi;
        private ConfigHelper _configHelper;

        public VistitorConfigHelper(VistitorConfigApi vistitorConfigApi,
             ConfigHelper configHelper)
        {
            _vistitorConfigApi = vistitorConfigApi;
            _configHelper = configHelper;
        }

        /// <summary>
        /// 初始化授权方式数据
        /// </summary>
        /// <param name="authTypes"></param>
        /// <returns></returns>
        public ObservableCollection<ItemsCheckViewModel> SetAuthTypes(Dictionary<string, object> authTypes, string authModel)
        {
            var results = new ObservableCollection<ItemsCheckViewModel>();
            if (authTypes == null)
            {
                return results;
            }
            foreach (var item in authTypes.OrderBy(x => x.Value))
            {
                var cb = new ItemsCheckViewModel(item.Key, item.Value.IsNull());
                if (item.Key.ToString() == authModel)
                {
                    cb.IsChecked = true;
                }
                results.Add(cb);
            }
            return results;
        }

        /// <summary>
        /// 从服务器获取授权方式并初始化
        /// </summary>
        /// <returns></returns>
        public async Task<ObservableCollection<ItemsCheckViewModel>> InitAuthTypesAsync()
        {
            var settings = await _vistitorConfigApi.GetConfigParmsAsync();
            return SetAuthTypes(settings?.AuthTypes, _configHelper.LocalConfig.AuthModel);
        }

        /// <summary>
        /// 初始化来访事由
        /// </summary>
        /// <param name="visitReasons"></param>
        /// <returns></returns>
        public ObservableCollection<ItemsCheckViewModel> SetVisitReasons(List<string> visitReasons, string defaultChecked = "商务洽谈")
        {
            var results = new ObservableCollection<ItemsCheckViewModel>();
            if (visitReasons == null)
            {
                return results;
            }
            foreach (var item in visitReasons)
            {
                //设置默认选择
                if (item.IsNull() == "商务洽谈")
                {
                    results.Add(new ItemsCheckViewModel(item.IsNull(), true));
                }
                else
                {
                    results.Add(new ItemsCheckViewModel(item.IsNull()));
                }
            }
            return results;
        }
    }
}
