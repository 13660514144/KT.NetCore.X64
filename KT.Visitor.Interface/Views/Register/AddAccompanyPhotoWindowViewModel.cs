using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Common.Core.Exceptions;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Helpers;
using Panuon.UI.Silver.Core;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace KT.Visitor.Interface.Views.Register
{
    public class AddAccompanyPhotoWindowViewModel : BindableBase
    {
        private ICommand _deleteAccompanyCommand;
        public ICommand DeleteAccompanyCommand => _deleteAccompanyCommand ??= new DelegateCommand<RegistVisitorViewModel>(DeleteAccompany);

        private ICommand _checkedPhotoCommand;
        public ICommand CheckedPhotoCommand => _checkedPhotoCommand ??= new DelegateCommand(CheckedPhoto);

        private ICommand _checkedCardCommand;
        public ICommand CheckCardCommand => _checkedCardCommand ??= new DelegateCommand(CheckCard);

        private ICommand _enterInputCommand;
        public ICommand EnterInputCommand => _enterInputCommand ??= new DelegateCommand(EnterInput);


        private ICommand _addIcCardCommand;
        public ICommand AddIcCardCommand => _addIcCardCommand ??= new DelegateCommand(AddIcCard);


        public Action FocusCardNumberAction { get; set; }
        public Action TakePhotoAction { get; set; }

        private void EnterInput()
        {
            if (InputPageHelepr.IsPageInput(InputPageHelepr.PHOTO_ACCOMPANY_INPUT))
            {
                AddIcCard();
            }
        }

        private void CheckCard()
        {
            if (!IsCard && !IsPhoto)
            {
                IsPhoto = true;
            }
            FocusCardNumberAction?.Invoke();
        }

        private void CheckedPhoto()
        {
            if (!IsCard && !IsPhoto)
            {
                IsCard = true;
            }
            FocusCardNumberAction?.Invoke();
        }

        private ObservableCollection<RegistVisitorViewModel> _accompanyInfos;
        private string _icCard;
        private int _accompanyOrder = 1;
        private int _accompanyNum;
        private bool _isCheckPhoto;

        private bool _isCard = false;
        private bool _isPhoto = true;

        private IEventAggregator _eventAggregator;

        public AddAccompanyPhotoWindowViewModel()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<RegistedSuccessEvent>().Subscribe(ClearAccompany);

            AccompanyInfos = new ObservableCollection<RegistVisitorViewModel>();
        }

        public List<VisitorInfoModel> GetVisitorTeams(string name)
        {
            //陪同访客
            var visitorTeams = new List<VisitorInfoModel>();
            foreach (var item in AccompanyInfos)
            {
                //设置陪同访客信息
                var visitor = new VisitorInfoModel
                {
                    IcCard = item.IcCard,
                    IdNumber = item.CertificateNumber,
                    Name = $"{ name }-{item.Name}",
                    FaceImg = item.ImageUrl,
                    Gender = item.Gender
                };
                visitorTeams.Add(visitor);
            }
            return visitorTeams;
        }

        internal void ClearAccompany()
        {
            AccompanyInfos.Clear();
            AccompanyNum = 0;
            AccompanyOrder = 1;
        }

        private void DeleteAccompany(RegistVisitorViewModel accmpany)
        {
            AccompanyInfos.Remove(accmpany);
            AccompanyOrder--;
            AccompanyNum = AccompanyOrder - 1;
            for (int i = accmpany.Order; i <= AccompanyInfos.Count; i++)
            {
                AccompanyInfos[i - 1].Order = i;
                AccompanyInfos[i - 1].Name = "陪同访客" + i;
            }
        }

        private void AddIcCard()
        {
            if (IsPhoto)
            {
                TakePhotoAction?.Invoke();
            }
            else
            {
                AddIcAccompany(string.Empty, null);
            }
        }


        public void AddIcAccompany(string imageUrl, BitmapImage bitmapImage)
        {
            if (IsCard)
            {
                if (string.IsNullOrEmpty(IcCard))
                {
                    throw CustomException.Run($"请输入卡号！");
                }
                else if (AccompanyInfos.FirstOrDefault(x => x.IcCard == IcCard) != null)
                {
                    throw CustomException.Run($"卡号已经存在！");
                }
            }

            var accompany = new RegistVisitorViewModel();
            accompany.IcCard = IcCard;
            accompany.ImageUrl = imageUrl;
            accompany.HeadImg = bitmapImage;
            accompany.Order = AccompanyOrder;
            accompany.Name = "陪同访客" + AccompanyOrder;
            AccompanyInfos.Add(accompany);
            AccompanyNum = AccompanyOrder;
            AccompanyOrder++;

            IcCard = string.Empty;
            FocusCardNumberAction?.Invoke();
        }

        public ObservableCollection<RegistVisitorViewModel> AccompanyInfos
        {
            get
            {
                return _accompanyInfos;
            }

            set
            {
                SetProperty(ref _accompanyInfos, value);
            }
        }

        public string IcCard
        {
            get
            {
                return _icCard;
            }

            set
            {
                SetProperty(ref _icCard, value);
            }
        }

        public int AccompanyOrder
        {
            get
            {
                return _accompanyOrder;
            }

            set
            {
                SetProperty(ref _accompanyOrder, value);
            }
        }

        public int AccompanyNum
        {
            get
            {
                return _accompanyNum;
            }

            set
            {
                SetProperty(ref _accompanyNum, value);
            }
        }

        public bool IsCheckPhoto
        {
            get
            {
                return _isCheckPhoto;
            }

            set
            {
                SetProperty(ref _isCheckPhoto, value);
            }
        }

        public bool IsCard
        {
            get
            {
                return _isCard;
            }

            set
            {
                SetProperty(ref _isCard, value);
            }
        }

        public bool IsPhoto
        {
            get
            {
                return _isPhoto;
            }

            set
            {
                SetProperty(ref _isPhoto, value);
            }
        }
    }
}
