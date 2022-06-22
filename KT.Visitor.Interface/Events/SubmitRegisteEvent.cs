using KT.Visitor.Interface.Models;
using Prism.Events;

namespace KT.Visitor.Interface.Events
{
    /// <summary>
    /// 提交访客登记
    /// </summary>
    public class SubmitRegisteEvent : PubSubEvent<SubmitParameterModel>
    {
    }
}
