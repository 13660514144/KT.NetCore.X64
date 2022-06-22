using Panuon.UI.Silver.Core;

namespace KT.Visitor.Interface.Views.Blacklist
{
    public class PullBlackConfirmWindowViewModel : PropertyChangedBase
    {
        private string pullBlackReason;

        public string PullBlackReason
        {
            get
            {
                return pullBlackReason;
            }

            set
            {
                pullBlackReason = value;
                NotifyPropertyChanged();
            }
        }
    }
}
