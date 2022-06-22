using KT.Visitor.Interface.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Interface.Events
{
    /// <summary>
    /// 导航链接事件
    /// </summary>
    public class NavLinkEvent : PubSubEvent<NavLinkModel>
    {

    }

    /// <summary>
    /// 访客登记页面
    /// </summary>
    public class VisitorRegisteEvent : PubSubEvent
    {

    }

    /// <summary>
    /// 新增黑名单
    /// </summary>
    public class AddBlacklistEvent : PubSubEvent
    {

    }

    /// <summary>
    /// 身份验证
    /// </summary>
    public class IdentityAuthLinkEvent : PubSubEvent
    {

    }

    /// <summary>
    /// 邀约验证
    /// </summary>
    public class InviteAuthLinkEvent : PubSubEvent
    {

    }
}
