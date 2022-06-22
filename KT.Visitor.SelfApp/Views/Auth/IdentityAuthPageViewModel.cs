using KT.Visitor.SelfApp.ViewModels;
using Prism.Mvvm;

namespace KT.Visitor.SelfApp.Views.Auth
{
    public class IdentityAuthPageViewModel : BindableBase
    {
        private PhoneViewModel phoneVM;

        public IdentityAuthPageViewModel()
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
