using KT.Common.Core.Enums;
using KT.Elevator.Unit.Secondary.ClientApp.Service.Network.Helpers;
using System;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.Network.Controllers
{
    /// <summary>
    /// 自定义Netty控制器特性
    /// </summary>
    public class NettyModuleAttribute : Attribute
    {
        /// <summary>
        /// 路由值
        /// </summary>
        public int Route { get; private set; }

        /// <summary>
        /// 注入路由名称
        /// </summary>
        /// <param name="route">路由值</param> 
        public NettyModuleAttribute(int route)
        {
            Route = route;
        }
    }
}