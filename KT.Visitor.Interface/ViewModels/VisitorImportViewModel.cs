using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Data.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace KT.Visitor.Interface.ViewModels
{
    public class VisitorImportViewModel : BindableBase
    {
        private TreeSelectFloorViewModel _treeSelectFloor;
        private ObservableCollection<VisitStatusEnum> statusItems;
        private ObservableCollection<string> _visitorReasons;

        private bool _once = true;
        //private string _beVisitStaffId;
        //private string _beVisitFloorId;
        private string _updateTime;
        private string _createTime;
        private string _results;
        private string _status;
        private string _startVisitDate;
        private string _endVisitDate;
        private string _zip;
        private string _xls;
        //private string _beVisitStaffName;
        //private string _beVisitCompanyName;
        private string _reason;
        private string _id;
        //private long? _beVisitCompanyId;

        private string _companyStaffName;
        private CompanyStaffModel _companyStaff;
        private ObservableCollection<CompanyStaffModel> _companyStaffs;

        public VisitorImportViewModel()
        {
            _treeSelectFloor = new TreeSelectFloorViewModel();
            _ = _treeSelectFloor.InitTreeCheckAsync();
        }
        /// <summary>
        /// 大厦选择树
        /// </summary>
        public TreeSelectFloorViewModel TreeSelectFloor
        {
            get
            {
                return _treeSelectFloor;
            }

            set
            {
                SetProperty(ref _treeSelectFloor, value);
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
                SetProperty(ref statusItems, value);
            }
        }

        /// <summary>
        /// 访客事由
        /// </summary>
        public ObservableCollection<string> VisitorReasons
        {
            get
            {
                return _visitorReasons;
            }

            set
            {
                SetProperty(ref _visitorReasons, value);
            }
        }

        /// <summary>
        /// id
        /// </summary>
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        /// <summary>
        /// 事由
        /// </summary>
        public string Reason
        {
            get
            {
                return _reason;
            }
            set
            {
                SetProperty(ref _reason, value);
            }
        }

        ///// <summary>
        ///// 公司
        ///// </summary>
        //public string BeVisitCompanyName
        //{
        //    get
        //    {
        //        return _beVisitCompanyName;
        //    }
        //    set
        //    {
        //        SetProperty(ref _beVisitCompanyName, value);
        //    }
        //}

        ///// <summary>
        ///// 被访人
        ///// </summary>
        //public string BeVisitStaffName
        //{
        //    get
        //    {
        //        return _beVisitStaffName;
        //    }
        //    set
        //    {
        //        SetProperty(ref _beVisitStaffName, value);
        //    }
        //}

        /// <summary>
        /// xls
        /// </summary>
        public string Xls
        {
            get
            {
                return _xls;
            }
            set
            {
                SetProperty(ref _xls, value);
            }
        }

        /// <summary>
        /// zip头像
        /// </summary>
        public string Zip
        {
            get
            {
                return _zip;
            }
            set
            {
                SetProperty(ref _zip, value);
            }
        }

        /// <summary>
        ///  结束时间
        /// </summary>
        public string EndVisitDate
        {
            get
            {
                return _endVisitDate;
            }
            set
            {
                SetProperty(ref _endVisitDate, value);
            }
        }

        /// <summary>
        /// 开始访问时间
        /// </summary>
        public string StartVisitDate
        {
            get
            {
                return _startVisitDate;
            }
            set
            {
                SetProperty(ref _startVisitDate, value);
            }
        }

        /// <summary>
        /// 状态，DOING：处理中、SUCCESS：导入成功、FAIL：导入失败
        /// </summary>
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                SetProperty(ref _status, value);
            }
        }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string Results
        {
            get
            {
                return _results;
            }
            set
            {
                SetProperty(ref _results, value);
            }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string CreateTime
        {
            get
            {
                return _createTime;
            }
            set
            {
                SetProperty(ref _createTime, value);
            }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string UpdateTime
        {
            get
            {
                return _updateTime;
            }
            set
            {
                SetProperty(ref _updateTime, value);
            }
        }

        ///// <summary>
        ///// 访问楼层的Id
        ///// </summary>
        //public string BeVisitFloorId
        //{
        //    get
        //    {
        //        return _beVisitFloorId;
        //    }
        //    set
        //    {
        //        SetProperty(ref _beVisitFloorId, value);
        //    }
        //}

        ///// <summary>
        ///// 访问员工的Id
        ///// </summary>
        //public string BeVisitStaffId
        //{
        //    get
        //    {
        //        return _beVisitStaffId;
        //    }
        //    set
        //    {
        //        SetProperty(ref _beVisitStaffId, value);
        //    }
        //}

        /// <summary>
        /// 是否只允许进出一次，true：一进一出、false：不限
        /// </summary>
        public bool Once
        {
            get
            {
                return _once;
            }
            set
            {
                SetProperty(ref _once, value);
            }
        }

        ///// <summary>
        ///// 访问公司Id
        ///// </summary>
        //public long? BeVisitCompanyId
        //{
        //    get
        //    {
        //        return _beVisitCompanyId;
        //    }
        //    set
        //    {
        //        SetProperty(ref _beVisitCompanyId, value);
        //    }
        //}

        /// <summary>
        /// 访问对象
        /// </summary>
        public CompanyStaffModel CompanyStaff
        {
            get => _companyStaff;
            set
            {
                SetProperty(ref _companyStaff, value);
            }
        }

        /// <summary>
        /// 访问对象选项
        /// </summary>
        public ObservableCollection<CompanyStaffModel> CompanyStaffs
        {
            get => _companyStaffs;
            set
            {
                SetProperty(ref _companyStaffs, value);
            }
        }

        /// <summary>
        /// 访问对象名称
        /// </summary>
        public string CompanyStaffName
        {
            get => _companyStaffName;
            set
            {
                SetProperty(ref _companyStaffName, value);
            }
        }
    }
}
