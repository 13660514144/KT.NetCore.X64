using KangTa.Visitor.Proxy.ServiceApi.Modes;
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

namespace KT.Visitor.Interface.Views.Register
{
    public class AddAccompanyCardWindowViewModel : PropertyChangedBase
    {
        public Action FocusCardNumber { get; set; }

        public ICommand AddIcCardCommand { get; private set; }
        public ICommand DeleteAccompanyCommand { get; private set; }
        public ICommand AppendNumCommand { get; private set; }
        public ICommand DeleteNumCommand { get; private set; }
        //public ICommand AppendNumConfirmCommand { get; private set; }
        public ICommand EnterInputCommand { get; private set; }

        private ObservableCollection<RegistVisitorViewModel> _accompanyInfos;
        private string _icCard;
        private int _accompanyOrder = 1;
        private int _accompanyNum = 0;

        private IEventAggregator _eventAggregator;

        public AddAccompanyCardWindowViewModel()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<RegistedSuccessEvent>().Subscribe(ClearAccompany);

            AccompanyInfos = new ObservableCollection<RegistVisitorViewModel>();

            AddIcCardCommand = new DelegateCommand(AddIcCard);
            DeleteAccompanyCommand = new DelegateCommand<RegistVisitorViewModel>(DeleteAccompany);
            AppendNumCommand = new DelegateCommand<string>(AppendNum);
            DeleteNumCommand = new DelegateCommand(DeleteNum);
            //AppendNumConfirmCommand = new DelegateCommand(AddIcCard);
            //EnterInputCommand = new RelayCommand (AddIcCard);
            EnterInputCommand = new DelegateCommand(EnterInput);
        }

        private void EnterInput()
        {
            if (InputPageHelepr.IsPageInput(InputPageHelepr.CARD_ACCOMPANY_INPUT))
            {
                AddIcCard();
            }
        }

        internal void ClearAccompany()
        {
            AccompanyInfos.Clear();
            AccompanyNum = 0;
            AccompanyOrder = 1;
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
                if (!string.IsNullOrEmpty(visitor.IcCard))
                {
                    long Crn = Convert.ToInt64(visitor.IcCard);
                    string Card = Crn.ToString();
                    visitor.IcCard = Card;
                }
                visitorTeams.Add(visitor);
            }
            return visitorTeams;
        }

        private void DeleteNum()
        {
            if (!string.IsNullOrEmpty(IcCard) && IcCard.Length > 0)
            {
                IcCard = IcCard[0..^1];
            }
        }

        private void AppendNum(string number)
        {
            IcCard = IcCard + number;
        }

        private void DeleteAccompany(RegistVisitorViewModel accmpany)
        {
            AccompanyInfos.Remove(accmpany);
            AccompanyOrder--;
            for (int i = accmpany.Order; i <= AccompanyInfos.Count; i++)
            {
                AccompanyInfos[i - 1].Order = i;
                AccompanyInfos[i - 1].Name = "陪同访客" + i;
            }
        }

        private void AddIcCard()
        {
            if (!string.IsNullOrEmpty(IcCard)
                && AccompanyInfos.FirstOrDefault(x => x.IcCard == IcCard) == null)
            {
                var accompany = new RegistVisitorViewModel();
                accompany.IcCard = IcCard;
                accompany.Order = AccompanyOrder;
                accompany.Name = "陪同访客" + AccompanyOrder;
                IcCard = string.Empty;
                AccompanyInfos.Add(accompany);
                AccompanyOrder++;
            }
            FocusCardNumber?.Invoke();
        }

        public ObservableCollection<RegistVisitorViewModel> AccompanyInfos
        {
            get
            {
                return _accompanyInfos;
            }

            set
            {
                _accompanyInfos = value;
                NotifyPropertyChanged();
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
                _icCard = value;
                NotifyPropertyChanged();
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
                _accompanyOrder = value;
                AccompanyNum = value - 1;
                NotifyPropertyChanged();
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
                _accompanyNum = value;
                NotifyPropertyChanged();
            }
        }
    }
}
