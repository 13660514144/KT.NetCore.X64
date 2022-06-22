namespace KT.Quanta.Model.Kone
{
    public class KoneSystemConfigModel
    {
        /// <summary>
        /// Standard call type (for GCAC) 
        /// 0 = Normal call(default)       
        /// 1 = Handicap call              
        /// 2 = Priority call              
        /// 3 = Empty car call             
        /// 4 = Space allocation call      
        /// </summary>                     
        public ushort StandardCallType { get; set; } = 0;

        /// <summary>
        /// ACS-Bypass call type (for RCGIF)
        /// 20 = Normal call(default)
        /// 21 = Handicap call
        /// 22 = Priority call
        /// 23 = Empty car call
        /// 24 = Space allocation call
        /// </summary>
        public ushort AcsBypassCallType { get; set; } = 20;

        /// <summary>
        /// Dop open access for dop message type
        /// 1:normal
        /// 2:with call type
        /// </summary>
        public ushort OpenAccessForDopMessageType { get; set; } = 1;
    }
}
