using Panuon.UI.Silver.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    public class CompanySearchSelectControlViewModel : PropertyChangedBase
    {
        private string _searchText;

        public string SearchText
        {
            get
            {
                return _searchText;
            }

            set
            {
                _searchText = value;
                NotifyPropertyChanged();
            }
        }
    }
}
