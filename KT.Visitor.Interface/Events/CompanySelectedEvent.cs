using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Interface.Events
{
    /// <summary>
    /// 公司选中，未确认
    /// </summary>
    public class CompanyCheckedEvent : PubSubEvent<CompanyViewModel>
    {

    }
}
