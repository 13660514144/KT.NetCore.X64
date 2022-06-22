using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Visitor.Common.Helpers;
using KT.Visitor.SelfApp.Views.Setting;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System.Collections.Generic;
using System.Windows.Controls;

namespace KT.Visitor.SelfApp.Helpers
{
    /// <summary>
    /// 首页导航管理
    /// </summary>
    public class MainFrameHelper
    {
        private ILogger _logger;
        private IContainerProvider _containerProvider;
        private ConfigHelper _configHelper;

        public MainFrameHelper(ILogger logger,
            IContainerProvider containerProvider,
            ConfigHelper configHelper)
        {
            _logger = logger;
            _containerProvider = containerProvider;
            _configHelper = configHelper;
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
        public void PreHistory(object page)
        {
            if (HistoryPages.Count > 0)
            {
                if (HistoryPages[0].Equals(page))
                {
                    if (HistoryPages.Count > 1)
                    {
                        MainFrame.Content = HistoryPages[1];
                        HistoryPages.RemoveAt(0);
                    }
                    else
                    {
                        _logger.LogError("返回上一页错误，已经没有上一页了。");
                    }
                }
                else
                {
                    MainFrame.Content = HistoryPages[0];
                }
            }
            else
            {
                _logger.LogError("返回上一页错误，已经没有上一页了。");
            }
        }

        public void Init(Frame frame)
        {
            this.MainFrame = frame;
            if (string.IsNullOrEmpty(_configHelper.LocalConfig.ServerAddress))
            {
                var configPage = _containerProvider.Resolve<SettingPage>();
                Link(configPage);
                return;
            }
            var page = _containerProvider.Resolve<HomePage>();
            Link(page);
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
        /// 页面是否更改
        /// </summary>
        /// <param name="pageKey">页面Key</param>
        /// <returns></returns>
        public bool IsChangePage(Page page)
        {
            if (MainFrame.Content == page)
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
        public void Link(object page, bool isHistory = true)
        {
            //回到首页了所有数据重新来
            if (page is HomePage)
            {
                HistoryPages?.Clear();
            }

            if (isHistory)
            {
                this.PageKey = IdUtil.NewId();
                AddHistory(page);
            }

            MainFrame.Content = page;
        }

        /// <summary>
        /// 更改页面
        /// </summary>
        /// <param name="page"></param>
        public void Link<T>(bool isHistory = true)
        {
            var page = ContainerHelper.Resolve<T>();   
            if (isHistory)
            {
                this.PageKey = IdUtil.NewId();
                AddHistory(page);
            }
            Link(page);
        }

    }
}
