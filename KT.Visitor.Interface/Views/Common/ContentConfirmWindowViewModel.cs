using KT.Common.WpfApp.ViewModels;
using Prism.Services.Dialogs;

namespace KT.Visitor.Interface.Views.Common
{
    public class ContentConfirmWindowViewModel : DialogViewModelBase
    {
        public ContentConfirmWindowViewModel()
        {
            Title = "温馨提示";
        }

        private object _contentControl;
        public object ContentControl
        {
            get { return _contentControl; }
            set { SetProperty(ref _contentControl, value); }
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            ContentControl = parameters.GetValue<object>("control");
        }
    }
}
