namespace KT.Quanta.Service.Devices.Hikvision.Models
{
    public class CDevice
    {
        public int iDeviceIndex { get; set; }  	//device index
        public string chLocalNodeName { get; set; }       //local device node
        public string chDeviceName { get; set; }          //device name
        public string chDeviceIP { get; set; }           //device IP: IP,pppoe address, or network gate address, etc
        public string chLoginUserName { get; set; }      //login user name
        public string chLoginPwd { get; set; }            //password
        public string chSerialNumber { get; set; }       //SN
        public int iDeviceType { get; set; }  	//device type
        public int lDevicePort { get; set; }   	//port number
        public byte byCharaterEncodeType { get; set; }
        public int iDoorNum { get; set; }
        //public int[] iDoorStatus { get; set; }
        //public string[] sDoorName { get; set; }  // door name
        public int iDeviceChanNum { get; set; }
    }

}