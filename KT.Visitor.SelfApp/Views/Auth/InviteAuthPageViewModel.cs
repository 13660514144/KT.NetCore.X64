using KT.Visitor.SelfApp.ViewModels;
using Prism.Mvvm;

namespace KT.Visitor.SelfApp.Views.Auth
{
    public class InviteAuthPageViewModel : BindableBase
    {
        private PhoneViewModel phoneVM;

        public InviteAuthPageViewModel()
        {
            this.phoneVM = new PhoneViewModel();
        }

        public PhoneViewModel PhoneVM
        {
            get
            {
                return phoneVM;
            }

            set
            {
                SetProperty(ref phoneVM, value);
            }
        }
    }
}
