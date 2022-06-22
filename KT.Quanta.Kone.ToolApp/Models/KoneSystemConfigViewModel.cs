using KT.Common.WpfApp.ViewModels;
using KT.Quanta.Kone.ToolApp.Enums;
using KT.Quanta.Model.Kone;

namespace KT.Quanta.Kone.ToolApp.Models
{
    public class KoneSystemConfigViewModel : BindableBase
    {
        private StandardCallTypeEnum standardCallType = StandardCallTypeEnum.NormalCall;
        private AcsBypassCallTypeEnum acsBypassCallType = AcsBypassCallTypeEnum.NormalCall;
        private OpenAccessForDopMessageTypeEnum openAccessForDopMessageType = OpenAccessForDopMessageTypeEnum.NORMAL;

        /// <summary>
        /// Standard call types (for GCAC) 
        /// 0 = Normal call(default)       
        /// 1 = Handicap call              
        /// 2 = Priority call              
        /// 3 = Empty car call             
        /// 4 = Space allocation call      
        /// </summary>                     
        public StandardCallTypeEnum StandardCallType
        {
            get => standardCallType;
            set
            {
                SetProperty(ref standardCallType, value);
            }
        }

        /// <summary>
        /// ACS-Bypass call types (for RCGIF)
        /// 20 = Normal call(default)
        /// 21 = Handicap call
        /// 22 = Priority call
        /// 23 = Empty car call
        /// 24 = Space allocation call
        /// </summary>
        public AcsBypassCallTypeEnum AcsBypassCallType
        {
            get => acsBypassCallType;
            set
            {
                SetProperty(ref acsBypassCallType, value);
            }
        }

        /// <summary>
        /// Dop open access for dop message type
        /// 1:normal
        /// 2:with call type
        /// </summary>
        public OpenAccessForDopMessageTypeEnum OpenAccessForDopMessageType
        {
            get => openAccessForDopMessageType;
            set
            {
                SetProperty(ref openAccessForDopMessageType, value);
            }
        }
    }
}
