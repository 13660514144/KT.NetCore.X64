using KT.Proxy.WebApi.Backend.Models;
using Panuon.UI.Silver.Core;
using System.Collections.ObjectModel;

namespace KT.Visitor.Interface.ViewModels
{
    public class CompanyViewModel : PropertyChangedBase
    {
        //序号
        private int? order;
        private bool isExpand = true;
        //是否选中（多选）
        private bool isChecked;
        //是否选择（单选）
        private bool isSelected;

        // 公司Id
        private long id;
        //父Id
        private long parentId;
        //公司名称
        private string name;
        //公司单元
        private string unit;
        //大厦Id
        private long edificeId;
        //大厦名称
        private string edificeName;
        //楼层Id
        private long floorId;
        //楼层名称
        private string floorName;
        //预约审核人
        private string auditorName;
        //预约审核人电话
        private string auditorPhone;
        //是否开启预约审核功能
        private bool opening;
        //节点类型
        private string type;

        //子节点
        private ObservableCollection<CompanyViewModel> children;

        public CompanyViewModel( )
        {   
            this.children = new ObservableCollection<CompanyViewModel>();
        }
        public CompanyViewModel(CompanyModel company)
        {
            this.children = new ObservableCollection<CompanyViewModel>();
            if (company != null)
            {
                this.Id = company.Id;
                this.ParentId = company.ParentId;
                this.Name = company.Name;
                this.Unit = company.Unit;
                this.EdificeId = company.EdificeId;
                this.EdificeName = company.EdificeName;
                this.FloorId = company.FloorId;
                this.FloorName = company.FloorName;
                this.AuditorName = company.AuditorName;
                this.AuditorPhone = company.AuditorPhone;
                this.Opening = company.Opening;
                this.type = company.Type;
            }
        }


        public CompanyModel ToCompany()
        {
            CompanyModel company = new CompanyModel();
            company.Id = this.Id;
            company.ParentId = this.ParentId;
            company.Name = this.Name;
            company.Unit = this.Unit;
            company.EdificeId = this.EdificeId;
            company.EdificeName = this.EdificeName;
            company.FloorId = this.FloorId;
            company.FloorName = this.FloorName;
            company.AuditorName = this.AuditorName;
            company.AuditorPhone = this.AuditorPhone;
            company.Opening = this.Opening;
            company.Type = this.Type;

            return company;
        }

        /// <summary>
        /// 选择父类时改变子类选择
        /// </summary>
        /// <param name="parentVM"></param>
        /// <param name="isChecked"></param>
        public void ChangeCheckChildren(CompanyViewModel parentVM, bool isChecked)
        {
            //没有子类不操作
            if (parentVM?.children != null)
            {
                return;
            }

            foreach (var item in parentVM.children)
            {
                //改变子类选择状态
                if (item.IsChecked != isChecked)
                {
                    item.IsChecked = isChecked;
                }
            }
        }

        /// <summary>
        /// Id
        /// </summary>
        public long Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 子类
        /// </summary>
        public ObservableCollection<CompanyViewModel> Children
        {
            get
            {
                return children;
            }

            set
            {
                children = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpand
        {
            get
            {
                return isExpand;
            }

            set
            {
                isExpand = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 父Id
        /// </summary>
        public long ParentId
        {
            get
            {
                return parentId;
            }

            set
            {
                parentId = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 是否选中（多选）
        /// </summary>
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }

            set
            {
                isChecked = value;
                //选择或取消选择所有子类
                ChangeCheckChildren(this, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 是否选中（单选）
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                isSelected = value;
                NotifyPropertyChanged();
            }
        }

        public string Unit
        {
            get
            {
                return unit;
            }

            set
            {
                unit = value;
                NotifyPropertyChanged();
            }
        }

        public long EdificeId
        {
            get
            {
                return edificeId;
            }

            set
            {
                edificeId = value;
                NotifyPropertyChanged();
            }
        }

        public string EdificeName
        {
            get
            {
                return edificeName;
            }

            set
            {
                edificeName = value;
                NotifyPropertyChanged();
            }
        }

        public long FloorId
        {
            get
            {
                return floorId;
            }

            set
            {
                floorId = value;
                NotifyPropertyChanged();
            }
        }

        public string FloorName
        {
            get
            {
                return floorName;
            }

            set
            {
                floorName = value;
                NotifyPropertyChanged();
            }
        }

        public string AuditorName
        {
            get
            {
                return auditorName;
            }

            set
            {
                auditorName = value;
                NotifyPropertyChanged();
            }
        }

        public string AuditorPhone
        {
            get
            {
                return auditorPhone;
            }

            set
            {
                auditorPhone = value;
                NotifyPropertyChanged();
            }
        }

        public bool Opening
        {
            get
            {
                return opening;
            }

            set
            {
                opening = value;
                NotifyPropertyChanged();
            }
        }

        public int? Order
        {
            get
            {
                return order;
            }

            set
            {
                order = value;
                NotifyPropertyChanged();
            }
        }
    }
}