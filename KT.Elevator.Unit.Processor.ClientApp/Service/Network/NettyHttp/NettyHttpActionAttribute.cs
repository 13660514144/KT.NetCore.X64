using KT.Common.Core.Enums;
using KT.Elevator.Unit.Processor.ClientApp.Service.Network.Helpers;
using System;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.Network.Controllers
{
    /// <summary>
    /// 自定义Netty控制器Action特性
    /// </summary>
    public class NettyHttpActionAttribute : Attribute
    {
        /// <summary>
        /// 路由值
        /// </summary>
        public string Route { get; private set; }

        /// <summary>
        /// 注入路由名称
        /// </summary>
        /// <param name="route">路由值</param> 
        public NettyHttpActionAttribute(string route)
        {
            Route = route;
        }
    }
}