using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace KT.Common.Data.Models
{
    /// <summary>
    /// 分页数据结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageDataViewModel<T> : BindableBase
    {
        public PageDataViewModel()
        {
            List = new ObservableCollection<T>();
        }

        public PageDataViewModel(PageData<T> pageData)
        {
            List = new ObservableCollection<T>(pageData.List);
            Page = pageData.Page;
            Pages = pageData.Pages;
            Size = pageData.Size;
            Totals = pageData.Totals;

            InitPageDetails();
        }

        public void InitPageDetails()
        {
            //初始化显示页面
            PageDetails = new ObservableCollection<PageDetailViewModel>();
            for (var i = 1; i <= Pages; i++)
            {
                PageDetails.Add(CreatePage(i));
            }

            //默认有一页
            if (PageDetails.FirstOrDefault() == null)
            {
                PageDetails.Add(CreatePage(1));
            }
        }

        private PageDetailViewModel CreatePage(int page)
        {
            var pageVM = new PageDetailViewModel();
            pageVM.Page = page;
            pageVM.Name = page.ToString();
            pageVM.IsCurrent = page == Page;

            return pageVM;
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        private int _page;

        /// <summary>
        /// 总页数
        /// </summary>
        private int _pages;

        /// <summary>
        /// 每页条数
        /// </summary>
        private int _size;

        /// <summary>
        /// 总条数
        /// </summary>
        private int _totals;

        /// <summary>
        /// 分页列表
        /// </summary>
        private ObservableCollection<T> _list;

        /// <summary>
        /// 显示操作的页码
        /// </summary>
        private ObservableCollection<PageDetailViewModel> _pageDetails;

        public int Page
        {
            get
            {
                return _page;
            }

            set
            {
                SetProperty(ref _page, value);
            }
        }

        public int Pages
        {
            get
            {
                return _pages;
            }

            set
            {
                SetProperty(ref _pages, value);
            }
        }

        public int Size
        {
            get
            {
                return _size;
            }

            set
            {
                SetProperty(ref _size, value);
            }
        }

        public int Totals
        {
            get
            {
                return _totals;
            }

            set
            {
                SetProperty(ref _totals, value);
            }
        }

        public ObservableCollection<T> List
        {
            get
            {
                return _list;
            }

            set
            {
                SetProperty(ref _list, value);
            }
        }

        public ObservableCollection<PageDetailViewModel> PageDetails
        {
            get
            {
                return _pageDetails;
            }

            set
            {
                SetProperty(ref _pageDetails, value);
            }
        }
    }
    public class PageDetailViewModel : BindableBase
    {
        private int _page;
        private string _name;
        private bool _isCurrent;

        public int Page
        {
            get
            {
                return _page;
            }

            set
            {
                SetProperty(ref _page, value);
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                SetProperty(ref _name, value);
            }
        }

        public bool IsCurrent
        {
            get
            {
                return _isCurrent;
            }

            set
            {
                SetProperty(ref _isCurrent, value);
            }
        }
    }
}