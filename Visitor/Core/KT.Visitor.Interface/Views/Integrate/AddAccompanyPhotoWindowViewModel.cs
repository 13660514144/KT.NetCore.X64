using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Common.WpfApp.ViewModels;
using KT.Visitor.Interface.ViewModels;
using Panuon.UI.Silver.Core;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace KT.Visitor.Interface.Views.Integrate
{
    public class AddAccompanyPhotoWindowViewModel : PropertyChangedBase
    {
        public ICommand DeleteAccompanyCommand { get; private set; }

        private ObservableCollection<VisitorInfoViewModel> _accompanyInfos;
        private string _icCardNumber;
        private int _accompanyOrder = 1;
        private int _accompanyCount;

        public AddAccompanyPhotoWindowViewModel()
        {

            AccompanyInfos = new ObservableCollection<VisitorInfoViewModel>();

            DeleteAccompanyCommand = new DelegateCommand(DeleteAccompany);
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

        public void AddIcAccompany(string imageUrl, BitmapImage bitmapImage)
        {
            var accompany = new VisitorInfoViewModel();
            accompany.ImageUrl = imageUrl;
            accompany.HeadImg = bitmapImage;
            accompany.Order = AccompanyOrder;
            accompany.Name = "陪同访客" + AccompanyOrder;
            IcCardNumber = string.Empty;
            AccompanyInfos.Add(accompany);
            AccompanyOrder++;
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
