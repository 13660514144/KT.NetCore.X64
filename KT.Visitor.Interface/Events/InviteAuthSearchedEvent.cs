using KT.Proxy.BackendApi.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Interface.Events
{
    /// <summary>
    /// 身份验证查询完毕
    /// </summary>
    public class InviteAuthSearchedEvent : PubSubEvent<List<VisitorInfoModel>>
    {
    }
}
