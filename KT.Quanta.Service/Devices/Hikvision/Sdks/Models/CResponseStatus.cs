namespace KT.Quanta.Service.Devices.Hikvision.Models
{
    public class CResponseStatus
    {
        public string requestURL { get; set; }
        public int statusCode { get; set; }
        public string statusString { get; set; }
        public string subStatusCode { get; set; }
        public int errorCode { get; set; }
        public string errorMsg { get; set; }
    }

}