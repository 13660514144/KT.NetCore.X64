using System;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Service.Network
{
    /// <summary>
    /// 自定义Netty控制器Action特性
    /// </summary>
    public class NettyActionAttribute : Attribute
    {
        /// <summary>
        /// 路由值
        /// </summary>
        public int Route { get; private set; }

        /// <summary>
        /// 注入路由名称
        /// </summary>
        /// <param name="route">路由值</param> 
        public NettyActionAttribute(int route)
        {
            Route = route;
        }
    }
}