using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Common.WpfApp.ViewModels;
using KT.Visitor.Interface.ViewModels;
using Panuon.UI.Silver.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Integrate
{
    public class AddAccompanyCardWindowViewModel : PropertyChangedBase
    {
        public Action FocusCardNumber { get; set; }

        public ICommand AddIcCardCommand { get; private set; }
        public ICommand DeleteAccompanyCommand { get; private set; }
        public ICommand AppendNumCommand { get; private set; }
        public ICommand DeleteNumCommand { get; private set; }
        //public ICommand AppendNumConfirmCommand { get; private set; }
        //public ICommand EnterInputCommand { get; private set; }

        private ObservableCollection<VisitorInfoViewModel> _accompanyInfos;
        private string _icCardNumber;
        private int _accompanyOrder = 1;
        private int _accompanyCount;

        public AddAccompanyCardWindowViewModel()
        {

            AccompanyInfos = new ObservableCollection<VisitorInfoViewModel>();

            AddIcCardCommand = new DelegateCommand(AddIcCard);
            DeleteAccompanyCommand = new DelegateCommand(DeleteAccompany);
            AppendNumCommand = new DelegateCommand(AppendNum);
            DeleteNumCommand = new DelegateCommand(DeleteNum);
            //AppendNumConfirmCommand = new DelegateCommand(AddIcCard);
            //EnterInputCommand = new KT.Common.WpfApp.ViewModels.RelayCommand(AddIcCard);
        }

        public List<VisitorTeamModel> GetVisitorTeams()
        {
            //陪同访客
            var visitorTeams = new List<VisitorTeamModel>();
            foreach (var item in AccompanyInfos)
            {
                //设置陪同访客信息
                var visitor = new VisitorTeamModel
                {
                    IcCard = item.IcCardNumber,
                    IdNumber = item.CertificateNumber,
                    Name = item.Name,
                    FaceImg = item.ImageUrl,
                    Gender = item.Gender
                };
                visitorTeams.Add(visitor);
            }
            return visitorTeams;
        }

        private void DeleteNum()
        {
            if (IcCardNumber.Length > 0)
            {
                IcCardNumber = IcCardNumber[0..^1];
            }
        }

        private void AppendNum(string number)
        {
            IcCardNumber = IcCardNumber + number;
        }

        private void DeleteAccompany(object obj)
        {
            var accmpany = (VisitorInfoViewModel)obj;
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
            if (!string.IsNullOrEmpty(IcCardNumber)
                && AccompanyInfos.FirstOrDefault(x => x.IcCardNumber == IcCardNumber) == null)
            {
                var accompany = new VisitorInfoViewModel();
                accompany.IcCardNumber = IcCardNumber;
                accompany.Order = AccompanyOrder;
                accompany.Name = "陪同访客" + AccompanyOrder;
                IcCardNumber = string.Empty;
                AccompanyInfos.Add(accompany);
                AccompanyOrder++;
            }
            FocusCardNumber?.Invoke();
        }

        public ObservableCollection<VisitorInfoViewModel> AccompanyInfos
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

        public string IcCardNumber
        {
            get
            {
                return _icCardNumber;
            }

            set
            {
                _icCardNumber = value;
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
                NotifyPropertyChanged();
            }
        }

        public int AccompanyCount
        {
            get
            {
                return _accompanyCount;
            }

            set
            {
                _accompanyCount = value;
                NotifyPropertyChanged();
            }
        }
    }
}
