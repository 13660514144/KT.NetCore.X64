using KT.Visitor.Interface.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    /// <summary>
    /// CompanySelectListControl.xaml 的交互逻辑
    /// </summary>
    public partial class CompanySelectListControl : UserControl
    {
        /// <summary>
        /// 选择公司事件
        /// </summary>
        public Action<CompanyViewModel> SelectedCompanyAction;

        private CompanySelectListControlViewModel _viewModel;

        public CompanySelectListControl(CompanySelectListControlViewModel viewModel)
        {
            InitializeComponent();

            this._viewModel = viewModel;
            this.DataContext = this._viewModel;
        }

        /// <summary>
        /// 初始化公司树型选择器
        /// </summary>
        public async Task InitTreeCompanysAsync()
        {
            await this._viewModel.InitTreeCompanysAsync();
        }

        private void BtnOperate_Click(object sender, RoutedEventArgs e)
        {
            //var cms = dtg_Show.SelectedItem as CompanyViewModel;
            //选择的公司传回操作页面
            SelectedCompanyAction?.Invoke(this._viewModel.Company);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._viewModel.Floor = (CompanyViewModel)((ListBox)sender).SelectedItem;
        }

        //private void lb_Floors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    //没有选择项隐藏公司列表数据
        //    var selectedItme = (CompanyViewModel)(((ListBox)sender).SelectedItem);
        //    if (selectedItme?.Children == null || selectedItme?.Children.Count <= 0)
        //    {
        //        dtg_Show.Visibility = Visibility.Collapsed;
        //        ctl_CompanyWarn.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        dtg_Show.ItemsSource = selectedItme.Children;
        //        dtg_Show.Visibility = Visibility.Visible;
        //        ctl_CompanyWarn.Visibility = Visibility.Collapsed;
        //    }
        //}

        //private void btn_SelectBuilding_Click(object sender, RoutedEventArgs e)
        //{
        //    //更改楼层选择
        //    var selectedId = ConvertUtil.ToInt32(((PUButton)sender).Tag);
        //    if (!selectedId.HasValue)
        //    {
        //        return;
        //    }
        //    foreach (var item in this.viewModel.TreeCompanys)
        //    {
        //        if (item.Id == selectedId.Value && !item.IsSelected)
        //        {
        //            item.IsSelected = true;
        //        }
        //        else if (item.Id != selectedId.Value && item.IsSelected)
        //        {
        //            item.IsSelected = false;
        //        }
        //    }
        //} 
    }
}
