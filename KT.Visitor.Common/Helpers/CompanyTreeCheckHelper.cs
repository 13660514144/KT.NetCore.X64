using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Visitor.Common.Views.Helper
{
    public class CompanyTreeCheckHelper
    {
        private ICompanyApi _companyApi;

        public CompanyTreeCheckHelper()
        {
            _companyApi = ContainerHelper.Resolve<ICompanyApi>();
        }

        /// <summary>
        /// 初始化树型公司数据，数据自动从服务器获取
        /// </summary>
        /// <param name="isSelectedFirst">是否默认选择第一家公司，包括子公司</param>
        /// <returns></returns>
        public async Task<ObservableCollection<CompanyViewModel>> InitTreeAsync(bool isSelectedFirst)
        {
            //服务器获取大厦、楼层、公司数据
            var buildes = await _companyApi.GetMapsAsync();
            if (buildes == null)
            {
                return new ObservableCollection<CompanyViewModel>();
            }
            return SetTreeChildren(buildes, null, isSelectedFirst);
        }

        /// <summary>
        /// 初始化树型公司数据，数据自动从服务器获取
        /// </summary>
        /// <param name="isSelectedFirst">是否默认选择第一家公司，包括子公司</param>
        /// <returns></returns>
        public async Task<ObservableCollection<CompanyViewModel>> InitTreeFloorAsync(bool isSelectedFirst, Action<CompanyViewModel> selectedAction)
        {
            //服务器获取大厦、楼层、公司数据
            var buildes = await _companyApi.GetMapsAsync();
            if (buildes == null)
            {
                return new ObservableCollection<CompanyViewModel>();
            }
            return SetTreeFloorChildren(buildes, null, isSelectedFirst, Guid.NewGuid().ToString(), selectedAction);
        }

        /// <summary>
        /// 初始化树型公司数据，数据自动从服务器获取
        /// </summary>
        /// <param name="selectedCompanies">选择的公司</param>
        /// <returns></returns>
        public async Task<ObservableCollection<CompanyViewModel>> InitTreeAsync(List<CompanyModel> selectedCompanies,
            Action<CompanyViewModel> checkedAction,
            bool isContainNullCompanyFloor = true)
        {
            //服务器获取大厦、楼层、公司数据
            var buildes = await _companyApi.GetMapsAsync();
            if (buildes == null)
            {
                return new ObservableCollection<CompanyViewModel>();
            }
            return SetTreeChildren(buildes, null, selectedCompanies, checkedAction, isContainNullCompanyFloor);
        }

        /// <summary>
        /// 获取公司Id列表
        /// </summary>
        /// <param name="vms"></param>
        /// <returns></returns>
        public List<long> GetSelectdCommpanyIds(ObservableCollection<CompanyViewModel> vms, List<long> list = null)
        {
            if (list == null)
            {
                list = new List<long>();
            }

            if (vms == null || vms.Count <= 0)
            {
                return list;
            }

            foreach (var item in vms)
            {
                if (item.Type == "company" && item.IsChecked)
                {
                    int? id = ConvertUtil.ToInt32(item.Id);
                    if (id.HasValue)
                    {
                        list.Add(id.Value);
                    }
                }
                //添加子类
                list = GetSelectdCommpanyIds(item.Children, list);
            }
            return list;
        }

        /// <summary>
        /// 获取公司列表
        /// </summary>
        /// <param name="list">返回的结果列表</param>
        /// <param name="vms"></param>
        /// <returns></returns>
        public List<CompanyModel> GetSelectdCommpany(ObservableCollection<CompanyViewModel> vms, List<CompanyModel> list = null)
        {
            if (list == null)
            {
                list = new List<CompanyModel>();
            }

            if (vms == null || vms.Count <= 0)
            {
                return list;
            }

            foreach (var item in vms)
            {
                if (item.Type == "company" && item.IsChecked)
                {
                    list.Add(item.ToCompany());
                }
                //添加子类
                list = GetSelectdCommpany(item.Children, list);
            }
            return list;
        }

        /// <summary>
        /// 获取公司列表
        /// </summary>
        /// <param name="list">返回的结果列表</param>
        /// <param name="vms"></param>
        /// <returns></returns>
        public List<CompanyModel> GetSelectdFloorsIsSelected(ObservableCollection<CompanyViewModel> vms, List<CompanyModel> list = null)
        {
            if (list == null)
            {
                list = new List<CompanyModel>();
            }

            if (vms == null || vms.Count <= 0)
            {
                return list;
            }

            foreach (var item in vms)
            {
                if (item.Type == "floor" && item.IsSelected)
                {
                    list.Add(item.ToCompany());
                }
                //添加子类
                list = GetSelectdFloorsIsSelected(item.Children, list);
            }
            return list;
        }

        /// <summary>
        /// 获取公司列表
        /// </summary>
        /// <param name="list">返回的结果列表</param>
        /// <param name="vms"></param>
        /// <returns></returns>
        public CompanyModel GetSelectdFloorIsSelected(ObservableCollection<CompanyViewModel> vms, List<CompanyModel> list = null)
        {
            if (list == null)
            {
                list = new List<CompanyModel>();
            }

            if (vms == null || vms.Count <= 0)
            {
                return null;
            }

            foreach (var item in vms)
            {
                if (item.Type == "floor" && item.IsSelected)
                {
                    var childFloor = item.ToCompany();
                    if (childFloor != null)
                    {
                        return childFloor;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 根据公司生成楼层与大厦
        /// </summary>
        /// <param name="buildes"></param>
        /// <param name="isSelectedFirst"></param>
        /// <returns></returns>
        public ObservableCollection<CompanyViewModel> SetTreeParentCompany(List<CompanyModel> buildes, bool isSelectedFirst)
        {
            int order = 1;
            bool isSelected = false;
            var edifices = new ObservableCollection<CompanyViewModel>();
            foreach (var item in buildes)
            {
                if (order == 1 && isSelectedFirst)
                {
                    isSelected = true;
                }
                else
                {
                    isSelected = false;
                }

                var oldEdifice = edifices.FirstOrDefault(x => x.Id == item.EdificeId);
                if (oldEdifice == null)
                {
                    var edifice = new CompanyViewModel();
                    edifice.Id = item.EdificeId;
                    edifice.Name = item.EdificeName;
                    edifice.IsSelected = isSelected;
                    edifices.Add(edifice);

                    edifice.Children = new ObservableCollection<CompanyViewModel>();
                    var floor = new CompanyViewModel();
                    floor.ParentId = edifice.Id;
                    floor.Id = item.FloorId;
                    floor.Name = item.FloorName;
                    floor.IsSelected = isSelected;
                    edifice.Children.Add(floor);

                    var company = new CompanyViewModel(item);
                    company.ParentId = floor.Id;
                    company.IsSelected = isSelected;
                    company.Order = order;
                    floor.Children.Add(company);

                    order++;
                    continue;
                }

                if (oldEdifice.Children == null)
                {
                    oldEdifice.Children = new ObservableCollection<CompanyViewModel>();
                }
                var oldNode = oldEdifice.Children?.FirstOrDefault(x => x.Id == item.FloorId);
                if (oldNode == null)
                {
                    var floor = new CompanyViewModel();
                    floor.ParentId = oldEdifice.Id;
                    floor.Id = item.FloorId;
                    floor.Name = item.FloorName;
                    floor.IsSelected = isSelected;
                    oldEdifice.Children.Add(floor);

                    var company = new CompanyViewModel(item);
                    company.ParentId = floor.Id;
                    company.IsSelected = isSelected;
                    company.Order = order;
                    floor.Children.Add(company);

                    order++;
                    continue;
                }
                else
                {
                    var company = new CompanyViewModel(item);
                    company.ParentId = oldNode.Id;
                    company.IsSelected = isSelected;
                    company.Order = order;
                    oldNode.Children.Add(company);

                    order++;
                }
            }
            return edifices;
        }

        /// <summary>
        /// 转换楼层数据成ViewModel
        /// </summary>
        /// <param name="buildes">树型原始数据</param>
        /// <param name="parent">上级公司</param>
        /// <param name="isSelectedFirst">是否默认选择第一条数据</param>
        /// <returns></returns>
        public ObservableCollection<CompanyViewModel> SetTreeChildren(List<CompanyModel> buildes, CompanyModel parent, bool isSelectedFirst)
        {
            var companyVMs = new ObservableCollection<CompanyViewModel>();
            //排序序号
            int order = 1;
            foreach (var item in buildes)
            {
                var tcvm = new CompanyViewModel(item);
                //排序序号
                tcvm.Order = order++;
                //父Id
                if (parent != null)
                {
                    tcvm.ParentId = parent.Id;
                }
                //默认选择第一家公司
                if (tcvm.Order == 1)
                {
                    tcvm.IsSelected = isSelectedFirst;
                }
                //公司不展开
                if (item.Type == "floor")
                {
                    tcvm.IsExpand = false;
                }
                //子公司
                if (item.Nodes != null && item.Nodes.Count > 0)
                {
                    //只选择第一个公司下的第一个公司
                    if (tcvm.Order == 1)
                    {
                        tcvm.Children = SetTreeChildren(item.Nodes, item, isSelectedFirst);
                    }
                    else
                    {
                        tcvm.Children = SetTreeChildren(item.Nodes, item, false);
                    }
                }

                companyVMs.Add(tcvm);
            }
            return companyVMs;
        }

        /// <summary>
        /// 转换楼层数据成ViewModel
        /// </summary>
        /// <param name="buildes">树型原始数据</param>
        /// <param name="parent">上级公司</param>
        /// <param name="isSelectedFirst">是否默认选择第一条数据</param>
        /// <returns></returns>
        public ObservableCollection<CompanyViewModel> SetTreeFloorChildren(List<CompanyModel> buildes, CompanyModel parent, bool isSelectedFirst, string groupName, Action<CompanyViewModel> selectedAction)
        {
            var companyVMs = new ObservableCollection<CompanyViewModel>();
            //排序序号
            int order = 1;
            foreach (var item in buildes)
            {
                var tcvm = new CompanyViewModel(item);
                //排序序号
                tcvm.Order = order++;
                tcvm.GroupName = groupName;
                tcvm.SelectedAction = selectedAction;
                //父Id
                if (parent != null)
                {
                    tcvm.ParentId = parent.Id;
                }
                //默认选择第一家公司
                if (tcvm.Order == 1)
                {
                    tcvm.IsSelected = isSelectedFirst;
                }
                //公司不展开
                if (item.Type == "floor")
                {
                    tcvm.IsExpand = false;
                }
                if (item.Type == "edifice")
                {
                    tcvm.IsEnable = false;
                    //子公司
                    if (item.Nodes != null && item.Nodes.Count > 0)
                    {
                        //只选择第一个公司下的第一个公司
                        if (tcvm.Order == 1)
                        {
                            tcvm.Children = SetTreeFloorChildren(item.Nodes, item, isSelectedFirst, groupName, selectedAction);
                        }
                        else
                        {
                            tcvm.Children = SetTreeFloorChildren(item.Nodes, item, false, groupName, selectedAction);
                        }
                    }
                }

                companyVMs.Add(tcvm);
            }
            return companyVMs;
        }

        /// <summary>
        /// 转换楼层数据成ViewModel
        /// </summary>
        /// <param name="buildes">树型原始数据</param>
        /// <param name="parent">上级公司</param>
        /// <param name="isSelectedFirst">是否默认选择第一条数据</param>
        /// <returns></returns>
        public ObservableCollection<CompanyViewModel> SetTreeChildren(List<CompanyModel> buildes,
            CompanyViewModel parent,
            List<CompanyModel> selectedCompanies,
            Action<CompanyViewModel> selectedAction,
            bool isContainNullCompanyFloor = true)
        {
            var companyVMs = new ObservableCollection<CompanyViewModel>();
            //排序序号 
            foreach (var item in buildes)
            {
                var tcvm = new CompanyViewModel(item);

                tcvm.CheckedAction = selectedAction;
                //公司不展开
                if (item.Type == "floor")
                {
                    tcvm.IsExpand = false;
                }
                //父Id
                if (parent != null)
                {
                    tcvm.ParentId = parent.Id;
                    tcvm.Parent = parent;
                }
                if (selectedCompanies != null && selectedCompanies.FirstOrDefault() != null)
                {
                    if (selectedCompanies.FirstOrDefault(x => x.Id == tcvm.Id && x.ParentId == tcvm.FloorId) != null)
                    {
                        tcvm.IsChecked = true;
                    }
                }
                else
                {
                    tcvm.IsChecked = false;
                }

                //子公司
                if (item.Nodes != null && item.Nodes.Count > 0)
                {
                    tcvm.Children = SetTreeChildren(item.Nodes, tcvm, selectedCompanies, selectedAction, isContainNullCompanyFloor);
                }

                if (isContainNullCompanyFloor)
                {
                    companyVMs.Add(tcvm);
                    tcvm.ChangeCheckParnent();
                }
                else
                {
                    //不添加空公司楼层
                    if ((tcvm.Type != "floor" && tcvm.Type != "edifice") || tcvm.Children?.FirstOrDefault() != null)
                    {
                        companyVMs.Add(tcvm);
                        tcvm.ChangeCheckParnent();
                    }
                }
            }
            return companyVMs;
        }
    }
}
