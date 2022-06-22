using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Interface.Events
{
    /// <summary>
    /// 公司选择完成
    /// </summary>
    public class CompanySelectedEvent : PubSubEvent<CompanyViewModel>
    {

    }
}
