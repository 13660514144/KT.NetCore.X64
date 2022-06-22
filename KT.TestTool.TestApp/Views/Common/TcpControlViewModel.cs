using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.TestTool.TestApp.ViewModels;
using Panuon.UI.Silver.Core;

namespace KT.TestTool.TestApp.Views
{
    public class TcpControlViewModel : PropertyChangedBase
    {
        /// <summary>
        /// 显示消息
        /// </summary>
        public ScrollMessageViewModel ScrollMessage { get; set; }

        /// <summary>
        /// 显示消息
        /// </summary>
        public SocketInfoViewModel SocketInfo { get; set; }

        public TcpControlViewModel()
        {
            ScrollMessage = ContainerHelper.Resolve<ScrollMessageViewModel>();
            SocketInfo = ContainerHelper.Resolve<SocketInfoViewModel>();
        }
 
    }
}