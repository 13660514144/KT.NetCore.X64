namespace KT.Elevator.Manage.Service.Devices.Hikvision.Models
{
    public class UserInfo
    {
        public string MemberNo { get; set; }
        public string MemberName { get; set; }
        public string CardNum { get; set; }
        public string FingerPrintNum { get; set; }

        public CardInfo[] Cards { get; set; }
    }

}