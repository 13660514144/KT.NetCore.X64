using KT.Common.Core.Exceptions;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Common.Views.Helper;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Views.Controls;
using KT.Visitor.Interface.Views.Visitor.Controls;
using Microsoft.EntityFrameworkCore.Internal;
using Panuon.UI.Silver.Core;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Register
{
    public class CompanySelectListControlViewModel : PropertyChangedBase
    {
        public ICommand SelectedCompanyCommand { get; private set; }
        public ICommand _selectedBulidingCommand;
        public ICommand SelectedBulidingCommand => _selectedBulidingCommand ??= new DelegateCommand(SelectedBuliding);

        public CompanyWarnControl CompanyWarnControl { get; private set; }

        private CompanyTreeCheckHelper _companyTreeCheckHelper;
        private IEventAggregator _eventAggregator;
        private readonly ICompanyApi _companyApi;

        public CompanySelectListControlViewModel()
        {
            _companyTreeCheckHelper = ContainerHelper.Resolve<CompanyTreeCheckHelper>();
            CompanyWarnControl = ContainerHelper.Resolve<CompanyWarnControl>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _companyApi = ContainerHelper.Resolve<ICompanyApi>();

            SelectedCompanyCommand = new DelegateCommand(SelectedCompany);
        }

        private async void SelectedCompany()
        {
            //判断公司能否访问指定楼层
            var query = new CompanyCheckVisitQuery();
            query.CompanyId = Company.Id;
            query.FloorId = _floor.Id;
            var isFloor = await _companyApi.CheckVisitAsync(query);
            if (isFloor.Code != 200)
            {
                throw CustomException.RunTitle("温馨提示", isFloor.Message); // $"该公司指定接待楼层为{Company.FloorName}，其他楼层暂不支持来访，请访问接待楼层");
            }
            _eventAggregator.GetEvent<CompanyCheckedEvent>().Publish(Company);
        }

        private void SelectedBuliding()
        {
            Task.Run(() =>
            {
                if (Buliding?.Children?.FirstOrDefault() == null)
                {
                    return;
                }
                if (Buliding.Children.Any(x => x.IsSelected))
                {
                    return;
                }
                var currentFloor = Buliding.Children.FirstOrDefault();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    currentFloor.IsSelected = true;
                });
            });
        }

        /// <summary>
        /// 初始化公司树型数据
        /// </summary>
        public async Task InitTreeCompanysAsync()
        {
            //初始化树形公司选择，默认选择第一家公司
            this.TreeCompanys = await _companyTreeCheckHelper.InitTreeAsync(true);
        }

        //选择的大厦
        private CompanyViewModel _buliding;
        //选择的楼层
        private CompanyViewModel _floor;
        //选择的公司
        private CompanyViewModel _company;
        //公司树形列表数据
        private ObservableCollection<CompanyViewModel> treeCompanys;

        public ObservableCollection<CompanyViewModel> TreeCompanys
        {
            get
            {
                return treeCompanys;
            }

            set
            {
                treeCompanys = value;
                NotifyPropertyChanged();
            }
        }

        public CompanyViewModel Company
        {
            get
            {
                return _company;
            }

            set
            {
                _company = value;
                NotifyPropertyChanged();
            }
        }

        public CompanyViewModel Floor
        {
            get
            {
                return _floor;
            }

            set
            {
                _floor = value;
                NotifyPropertyChanged();
            }
        }

        public CompanyViewModel Buliding
        {
            get
            {
                return _buliding;
            }

            set
            {
                _buliding = value;
                NotifyPropertyChanged();
            }
        }
    }
}
