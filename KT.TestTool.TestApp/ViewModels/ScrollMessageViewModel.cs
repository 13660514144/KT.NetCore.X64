using KT.Common.Core.Utils;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.TestTool.TestApp.ViewModels
{
    public class ScrollMessageViewModel : PropertyChangedBase
    {
        private ILogger<ScrollMessageViewModel> _logger;
        public ScrollMessageViewModel(ILogger<ScrollMessageViewModel> logger)
        {
            _logger = logger;
        }

        private int messageCount = 0;

        private void ClearMessage()
        {
            if (messageCount > 50)
            {
                Message = string.Empty;
                messageCount = 0;
            }
            messageCount++;
        }

        public void Clear()
        {
            Message = string.Empty;
            messageCount = 0;
        }

        /// <summary>
        /// 显示信息,最新增加的在上面
        /// </summary>
        public void AppendMessage(object newMessage, params object[] messages)
        {
            ClearMessage();
            if (messages != null && messages.Length > 0)
            {
                newMessage = string.Format(newMessage.ToString(), messages);
            }
            ExecAppendMessage("Start!--------------------------------------------");
            ExecAppendMessage(DateTimeUtil.NowSecondString().RightBlank() + newMessage);
            ExecAppendMessage("End!--------------------------------------------");
        }

        /// <summary>
        /// 显示信息,最新增加的在上面
        /// </summary>
        public void AppendMessageShape(object newMessage, params object[] messages)
        {
            ClearMessage();
            if (messages != null && messages.Length > 0)
            {
                newMessage = string.Format(newMessage.ToString(), messages);
            }
            ExecAppendMessage(DateTimeUtil.NowSecondString().RightBlank() + newMessage);
        }


        private void ExecAppendMessage(string newMessage)
        {
            _logger.LogInformation(newMessage);
            Message = newMessage.RightWrap() + Message;
        }

        private string _message;
        /// <summary>
        /// 显示消息
        /// </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyPropertyChanged();
            }
        }
    }
}
