using KT.Common.Core.Utils;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace KT.Common.WpfApp.ViewModels
{
    public class ScrollMessageViewModel : BindableBase
    {
        private ObservableCollection<string> _messageList;

        private ILogger<ScrollMessageViewModel> _logger;

        public ScrollMessageViewModel(ILogger<ScrollMessageViewModel> logger)
        {
            _logger = logger;

            _messageList = new ObservableCollection<string>();
        }

        private void CheckClearMessage()
        {
            if (MessageList.Count > 200)
            {
                MessageList.RemoveAt(200);
                CheckClearMessage();
            }
        }

        public void Clear()
        {
            MessageList = new ObservableCollection<string>();
        }

        /// <summary>
        /// 显示信息,最新增加的在上面
        /// </summary>
        public void InsertTop(object newMessage, params object[] messages)
        {
            if (messages != null && messages.Length > 0)
            {
                newMessage = string.Format(newMessage.ToString(), messages);
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                CheckClearMessage();
                InsertTopMessage("Start!--------------------------------------------");
                InsertTopMessage(DateTimeUtil.NowSecondString().RightBlank() + newMessage);
                InsertTopMessage("End!--------------------------------------------");
            });
        }

        /// <summary>
        /// 显示信息,最新增加的在上面
        /// </summary>
        public void InsertTopShape(object newMessage, params object[] messages)
        {
            if (messages != null && messages.Length > 0)
            {
                newMessage = string.Format(newMessage.ToString(), messages);
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                CheckClearMessage();
                InsertTopMessage(DateTimeUtil.NowSecondString().RightBlank() + newMessage);
            });
        }

        private void InsertTopMessage(string newMessage)
        {
            _logger.LogInformation(newMessage);
            MessageList.Insert(0, newMessage);
        }

        public ObservableCollection<string> MessageList
        {
            get
            {
                return _messageList;
            }

            set
            {
                SetProperty(ref _messageList, value);
            }
        }
    }
}
