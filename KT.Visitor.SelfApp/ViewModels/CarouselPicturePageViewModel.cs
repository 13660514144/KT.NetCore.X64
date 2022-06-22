using KT.Common.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace KT.Visitor.SelfApp.ViewModels
{
    public class CarouselPicturePageViewModel : BindableBase
    { 
        private int _page;
        private int _pages;
        private string _url;
        private ObservableCollection<PageDetailViewModel> _pageDetails;

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

        public string Url
        {
            get
            {
                return _url;
            }

            set
            {
                SetProperty(ref _url, value);
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