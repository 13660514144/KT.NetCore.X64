using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Enums;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Data.Enums;
using KT.Visitor.Interface.Controls.BaseWindows;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace KT.Visitor.Interface.Views
{
    public class VisitorDetailControlViewModel : BindableBase
    {
        private VisitorInfoModel _visitorInfo;
        private Visibility _cancelVisitVisibility;
        private Visibility _finishVisitVisibility;
        private bool _isBlackList;
        private string _blackReasonText;

        private IBlacklistApi _blacklistApi;
        private ConfigHelper _configHelper;
        private DialogHelper _dialogHelper;
        private IVisitorApi _visitorApi;

        public VisitorDetailControlViewModel()
        {
            _blacklistApi = ContainerHelper.Resolve<IBlacklistApi>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();
            _visitorApi = ContainerHelper.Resolve<IVisitorApi>();
        }

        internal void Init(VisitorInfoModel visitorInfo)
        {
            VisitorInfo = visitorInfo;

            IsBlackList = visitorInfo.BlackList;
            BlackReasonText = visitorInfo.BlackReason;

            if (visitorInfo.VisitorStatus == VisitStatusEnum.WAITING_AUDIT.Value
                || visitorInfo.VisitorStatus == VisitStatusEnum.WAITING_VERIFY.Value
                || visitorInfo.VisitorStatus == VisitStatusEnum.WAITING_VISIT.Value)
            {
                CancelVisitVisibility = Visibility.Visible;
            }
            else
            {
                CancelVisitVisibility = Visibility.Collapsed;
            }

            if (visitorInfo.VisitorStatus == VisitStatusEnum.VISITING.Value
                || visitorInfo.VisitorStatus == VisitStatusEnum.UNVISIT.Value
                || visitorInfo.VisitorStatus == VisitStatusEnum.OUTTIME_VISIT.Value
                || visitorInfo.VisitorStatus == VisitStatusEnum.OUTTIME_AUDIT.Value)
            {
                FinishVisitVisibility = Visibility.Visible;
            }
            else
            {
                FinishVisitVisibility = Visibility.Collapsed;
            }
        }

        public async Task AppendToBlackAsync()
        {
            //弹出确认页面操作
            var pullBlackConfirmWindow = ContainerHelper.Resolve<PullBlackConfirmWindow>();
            var pullBlackConfirmPageVM = pullBlackConfirmWindow.ViewModel;
            var pullResult = _dialogHelper.ShowDialog(pullBlackConfirmWindow);

            if (!pullResult.HasValue || !pullResult.Value)
            {
                return;
            }

            var black = new BlacklistModel();
            black.VisitorLogId = VisitorInfo.Id;
            black.Name = VisitorInfo.Name;
            black.IdType = VisitorInfo.IdType;
            black.IdNumber = VisitorInfo.IdNumber;
            black.Phone = VisitorInfo.Phone;
            black.Company = VisitorInfo.Company;
            black.FaceImg = VisitorInfo.FaceImg;
            black.SnapshotImg = VisitorInfo.SnapshotImg;

            //确认页面修改拉黑原因
            black.Reason = pullBlackConfirmPageVM.PullBlackReason;

            await _blacklistApi.AddAsync(black);

            ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("操作成功！", "提示");

            RefreshAction?.Invoke(VisitorInfo.Id);
        }

        public Func<long, Task> RefreshAction;

        internal async Task FinishVisitAsync()
        {
            var confirmWindow = ContainerHelper.Resolve<MessageWarnBox>();
            var confirmResult = confirmWindow.ShowMessage("确定要结束访问吗？", "温馨提示", "确定", "取消");
            if (confirmResult == true)
            {
                await _visitorApi.OperateAsync(VisitorInfo.Id, VisitOperateTypeEnum.FINISH);
                ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("操作成功！", "提示");
                RefreshAction?.Invoke(VisitorInfo.Id);
            }
        }

        internal async Task CancelVisitAsync()
        {
            var confirmWindow = ContainerHelper.Resolve<MessageWarnBox>();
            var confirmResult = confirmWindow.ShowMessage("确定要取消访问吗？", "温馨提示", "确定", "取消");
            if (confirmResult == true)
            {
                await _visitorApi.OperateAsync(VisitorInfo.Id, VisitOperateTypeEnum.CANCEL);
                ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("操作成功！", "提示");
                RefreshAction?.Invoke(VisitorInfo.Id);
            }
        }


        public Visibility CancelVisitVisibility
        {
            get
            {
                return _cancelVisitVisibility;
            }

            set
            {
                SetProperty(ref _cancelVisitVisibility, value);
            }
        }

        public Visibility FinishVisitVisibility
        {
            get
            {
                return _finishVisitVisibility;
            }

            set
            {
                SetProperty(ref _finishVisitVisibility, value);
            }
        }

        public VisitorInfoModel VisitorInfo
        {
            get
            {
                return _visitorInfo;
            }

            set
            {
                SetProperty(ref _visitorInfo, value);
            }
        }

        public bool IsBlackList
        {
            get
            {
                return _isBlackList;
            }

            set
            {
                SetProperty(ref _isBlackList, value);
            }
        }

        public string BlackReasonText
        {
            get
            {
                return _blackReasonText;
            }

            set
            {
                SetProperty(ref _blackReasonText, value);
            }
        }
    }
}
