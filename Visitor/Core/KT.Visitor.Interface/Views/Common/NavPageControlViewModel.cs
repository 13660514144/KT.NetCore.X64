using KT.Proxy.WebApi.Backend.Models;
using Panuon.UI.Silver.Core;

namespace KT.Visitor.Interface.Views.Common
{
    public class NavPageControlViewModel : PropertyChangedBase
    {
        private int size = 20;
        private int page = 1;
        private int pages = 0;
        private int totals = 0;

        public int Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
                NotifyPropertyChanged();
            }
        }

        public int Page
        {
            get
            {
                return page;
            }

            set
            {
                page = value;
                NotifyPropertyChanged();
            }
        }

        public int Pages
        {
            get
            {
                return pages;
            }

            set
            {
                pages = value;
                NotifyPropertyChanged();
            }
        }

        public int Totals
        {
            get
            {
                return totals;
            }

            set
            {
                totals = value;
                NotifyPropertyChanged();
            }
        }

        internal void SetData(BasePageData pageInfo)
        {
            if (pageInfo != null)
            {
                this.Page = pageInfo.Page;
                this.Pages = pageInfo.Pages;
                this.Size = pageInfo.Size;
                this.Totals = pageInfo.Totals;
            }
        }
    }
}









