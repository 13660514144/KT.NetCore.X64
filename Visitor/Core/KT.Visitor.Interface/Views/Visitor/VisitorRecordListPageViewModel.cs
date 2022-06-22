using KT.Common.Core.Utils;
using KT.Visitor.Data.Enums;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Helper;
using Panuon.UI.Silver.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KT.Visitor.Interface.Views.Visitor
{
    public class VisitorRecordListPageViewModel : PropertyChangedBase
    {
        private ObservableCollection<CompanyViewModel> treeCompanys;
        private ObservableCollection<VisitStatusEnum> statusItems;

        private CompanyTreeCheckHelper _companyTreeCheckHelper;

        private string status;
        private string startTime;
        private string endTime;

        public VisitorRecordListPageViewModel(CompanyTreeCheckHelper companyTreeCheckHelper)
        {
            _companyTreeCheckHelper = companyTreeCheckHelper;

            InitAsync();
        }

        public async Task InitAsync()
        {
            this.status = string.Empty;
            this.StartTime = DateTimeUtil.DayStartMilliString();
            this.EndTime = DateTimeUtil.DayEndMilliString();

            //初始化对你 
            this.StatusItems = VisitStatusEnum.GetVMs(true);

            //初始化数据
            this.TreeCompanys = await _companyTreeCheckHelper.InitTreeAsync(false);
        }

        /// <summary>
        /// 获取树型选择的公司ID列表
        /// </summary>
        /// <returns></returns>
        internal async Task<List<long>> GetSelectdCommpanyIdsAsync()
        {
            return await Task.Run(() =>
            {
                return _companyTreeCheckHelper.GetSelectdCommpanyIds(this.TreeCompanys);
            });
        }

        /// <summary>
        /// 大厦选择树
        /// </summary>
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

        /// <summary>
        /// 状态下拉选择
        /// </summary>
        public ObservableCollection<VisitStatusEnum> StatusItems
        {
            get
            {
                return statusItems;
            }

            set
            {
                statusItems = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 当前选择的状态
        /// </summary>
        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
                NotifyPropertyChanged();
            }
        }

        public string StartTime
        {
            get
            {
                return startTime;
            }

            set
            {
                startTime = value;
                NotifyPropertyChanged();
            }
        }

        public string EndTime
        {
            get
            {
                return endTime;
            }

            set
            {
                endTime = value;
                NotifyPropertyChanged();
            }
        }

    }
}
