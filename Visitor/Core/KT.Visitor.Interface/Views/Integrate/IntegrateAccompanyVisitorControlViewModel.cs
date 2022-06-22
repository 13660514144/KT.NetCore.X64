using KT.Common.WpfApp.ViewModels;
using KT.Visitor.IdReader.Common;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Visitor.Controls;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Integrate
{
    public class IntegrateAccompanyVisitorControlViewModel : BindableBase
    {
        public object BindingControl { get; set; }

        public VisitorInfoViewModel VisitorInfo { get; set; }
        public bool IsFocus { get; set; }

        public ICommand RemoveVisitorCommand { get; private set; }

        private TakePictureControl _takePictureControl;

        //证件类型
        private ObservableCollection<CertificateTypeEnum> _certificateTypes;

        /// <summary>
        /// 是否为主访客
        /// </summary>
        private bool _isMain;
        private string _nameTitle;

        public IntegrateAccompanyVisitorControlViewModel(TakePictureControl takePictureControl)
        {
            _takePictureControl = takePictureControl;

            CertificateTypes = new ObservableCollection<CertificateTypeEnum>(CertificateTypeEnum.Items);
            VisitorInfo = new VisitorInfoViewModel();

            //默认非主访客，设置好名名称
            IsMain = false;

            RemoveVisitorCommand = new DelegateCommand(RemoveVisitor);
        }

        //控件移除事件
        public Action<object> ControlRemove;
        private void RemoveVisitor()
        {
            ControlRemove?.Invoke(BindingControl);
        }

        public ObservableCollection<CertificateTypeEnum> CertificateTypes
        {
            get => _certificateTypes;
            set
            {
                SetProperty(ref _certificateTypes, value);
            }
        }

        public bool IsMain
        {
            get
            {
                return _isMain;
            }

            set
            {
                NameTitle = value ? "主访客" : "陪同访客";
                SetProperty(ref _isMain, value);
            }
        }


        public TakePictureControl TakePictureControl
        {
            get
            {
                return _takePictureControl;
            }

            set
            {
                SetProperty(ref _takePictureControl, value);
            }
        }

        public string NameTitle
        {
            get
            {
                return _nameTitle;
            }

            set
            {
                SetProperty(ref _nameTitle, value);
            }
        }
    }
}
