using KT.Common.Core.Utils;
using KT.Visitor.Interface.Views.Auth;
using KT.Visitor.Interface.Views.Blacklist;
using KT.Visitor.Interface.Views.Visitor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Helpers
{
    /// <summary>
    /// 首页导航管理,静态
    /// </summary>
    public class MainFrameHelper
    {
        private ILogger<MainFrameHelper> _logger;
        private IContainerProvider _containerProvider;

        public MainFrameHelper(ILogger<MainFrameHelper> logger,
            IContainerProvider containerProvider)
        {
            _logger = logger;
            _containerProvider = containerProvider;
        }

        /// <summary>
        /// 最多存储历史页面
        /// </summary>
        private int MaxHistoryPage = 6;
        /// <summary>
        /// 历史页面，记录当前页
        /// </summary>
        private List<object> HistoryPages;

        /// <summary>
        /// 增加历史记录
        /// </summary>
        /// <param name="obj"></param>
        private void AddHistory(object obj)
        {
            if (HistoryPages == null)
            {
                HistoryPages = new List<object>();
            }
            //数据条数过多先清除
            if (HistoryPages.Count > MaxHistoryPage)
            {
                HistoryPages.RemoveRange(MaxHistoryPage - 2, HistoryPages.Count - MaxHistoryPage + 1);
            }
            HistoryPages.Insert(0, obj);
        }

        /// <summary>
        /// 返回上一页
        /// </summary>
        public void PreHistory()
        {
            if (HistoryPages.Count > 0)
            {
                MainFrame.Content = HistoryPages[0];
                HistoryPages.RemoveAt(0);
            }
            else
            {
                _logger.LogInformation("返回上一页错误，已经没有上一页了。");
            }
        }

        public void Init(Frame frame)
        {
            this.MainFrame = frame;
            //默认跳转到访客登记页面
            LinkVisitorRegister();
        }

        /// <summary>
        /// 首页导航 Frame
        /// </summary>  
        public Frame MainFrame { get; set; }

        /// <summary>
        /// 检查是否页面与切换
        /// </summary>
        public string PageKey;
        /// <summary>
        /// 页面是否更改
        /// </summary>
        /// <param name="pageKey">页面Key</param>
        /// <returns></returns>
        public bool IsChangePage(string pageKey)
        {
            if (PageKey == pageKey && !string.IsNullOrEmpty(pageKey))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 更改页面
        /// </summary>
        /// <param name="page"></param>
        public void Link(object page, bool isHistory = true, string pageKey = "")
        {
            this.PageKey = string.IsNullOrEmpty(pageKey) ? IdUtil.NewId() : pageKey;
            if (isHistory)
            {
                AddHistory(page);
            }
            MainFrame.Content = page;
        }

        /// <summary>
        /// 更改页面
        /// </summary>
        /// <param name="page"></param>
        public void Link<T>(bool isHistory = true, string pageKey = "")
        {
            var page = _containerProvider.Resolve<T>();
            Link(page, isHistory, pageKey);
        }

        /// <summary>
        /// 跳转到访客登记页面
        /// </summary>
        public void LinkVisitorRegister()
        {
            var page = _containerProvider.Resolve<RegisterPage>();
            Link(page);
        }

        /// <summary>
        /// 跳转到身份认证
        /// </summary>
        public void LinkAuthIndex()
        {
            var page = _containerProvider.Resolve<IdentityAuthPage>();
            Link(page);
        }

        /// <summary>
        /// 跳转到访客记录
        /// </summary>
        public void LinkVistorRecord()
        {
            var page = _containerProvider.Resolve<VisitorRecordListPage>();
            Link(page);
        }

        /// <summary>
        /// 跳转到黑名单
        /// </summary>
        public void LinkBlacklist()
        {
            var page = _containerProvider.Resolve<VisitorBlacklist>();
            Link(page);
        }
    }
}
