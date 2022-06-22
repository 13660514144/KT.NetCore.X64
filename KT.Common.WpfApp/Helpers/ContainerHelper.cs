using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.WpfApp.Helpers
{
    public static class ContainerHelper
    {
        private static ILogger _logger;
        private static IContainerProvider provider;

        public static T Resolve<T>()
        {
            //_logger?.LogInformation("开始获取Container对象：{0} ", typeof(T).FullName);
            var result = provider.Resolve<T>();
            //_logger?.LogInformation("结束获取Container对象：{0} ", typeof(T).FullName);
            return result;
        }

        public static T Resolve<T>(this T source)
        {
            if (source != null)
            {
               // _logger.LogInformation("使用原有Container对象：{0} ", typeof(T).FullName);
                return source;
            }
            return Resolve<T>();
        }
 
        public static void SetProvider(ILogger logger, IContainerProvider value)
        {
            _logger = logger;
            provider = value;
        }

        //public static IContainerProvider GetProvider()
        //{
        //    return provider;
        //}
    }
}
