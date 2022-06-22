using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.SelfApp.Views.Register
{
    public class InviteReadIdCardPageViewModel : BindableBase
    {
        private readonly SelfAppSettings _selfAppSettings;
        public InviteReadIdCardPageViewModel(SelfAppSettings selfAppSettings)
        {
            _selfAppSettings = selfAppSettings; 
        }
         
        private string _takePhotoMessage;

        public string TakePhotoMessage
        {
            get => _takePhotoMessage;
            set
            {
                SetProperty(ref _takePhotoMessage, value);
            }
        } 
    }
}
