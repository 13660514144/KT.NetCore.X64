using System;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace KT.Elevator.Manage.Service.Devices.Hikvision.Models
{
    public partial class AcsDemoPublic
    {
        public static readonly string[] strDoorStatus = { "invalid", "sleep", "Normally open", "Normally close" };
        public static readonly string[] strVerify = {"invalid", "sleep", "card and password", "card", "card or password", "fingerprint", "fingerprint and password", "fingerprint or card",
                                                 "fingerprint and card", "fingerprint and card and password","face or fingerprint or card or password","face and fingerprint",
                                                 "face and password", "face and card", "face", "work number and password", "fingerprint or password", "work number and fingerprint",
                                                 "work number and fingerprint and password", "face and fingerprint and card", "face and password and fingerprint", "work number and face",
                                                 "face or face and swipe card"};
        public static readonly string[] strFingerType = { "Common fingerprint", "Stress fingerprint", "Patrol fingerprint", "Super fingerprint", "Dismissing fingerprint" };
        public static readonly string[] strCardType = { "unknown", "ordinary card", "disabled card", "black list card", "patrol card", "stress card", "super card", "guest card", "remove card",
                                                          "employee card", "emergency card","emergency management card"};

        public const int MAX_DEVICES = 512;//max device number
        //bmp status
        public const int TREE_ALL = 0;//device list	
        public const int DEVICE_LOGOUT = 1;//device not log in
        public const int DEVICE_LOGIN = 2;//device login
        public const int DEVICE_FORTIFY = 3;//on guard
        public const int DEVICE_ALARM = 4;//alarm on device
        public const int DEVICE_FORTIFY_ALARM = 5;//onguard & alarm on device
        public const int DOOR_COLSED = 6;
        public const int DOOR_OPEN = 7;
        public const int CARD_ON_LINE = 8;   //preview & record & alarm
        public const int CARD_OFF_LINE = 9;	 //channel off-line
        public const int DOOR_ALWAYSCOLSED = 6;
        public const int DOOR_ALWAYSOPEN = 7;

        public const int CHAN_ORIGINAL = 6;//no preview, no record
        public const int CHAN_PLAY = 7;   //preview
        public const int CHAN_RECORD = 8;   //record
        public const int CHAN_PLAY_RECORD = 9;   //preview and record
        public const int CHAN_ALARM = 10;   //no preview, no record, only alarm
        public const int CHAN_PLAY_ALARM = 11;   //review, no record, with alarm info
        public const int CHAN_PLAY_RECORD_ALARM = 12;   //preview & record & alarm
        public const int CHAN_OFF_LINE = 13;	 //channel off-line

        public const int ALARM_INFO_T = 0;
        public const int OPERATION_SUCC_T = 1;
        public const int OPERATION_FAIL_T = 2;
        public const int PLAY_SUCC_T = 3;
        public const int PLAY_FAIL_T = 4;


        public const int REGIONTYPE = 0;
        public const int DEVICETYPE = 2;
        public const int CHANNELTYPE = 3;
        public const int DOORTYPE = 4;
        public const int CARDREADERTYPE = 6;
        public const int USERTYPE = 5;

        //batch
        public const int ZERO_CHAN_INDEX = 500;
        public const int MIRROR_CHAN_INDEX = 400;

         
        public static bool CheckState(CHCNetSDK.NET_DVR_DATE struItem)
        {
            if (struItem.wYear < 1970 || struItem.byMonth > 12 || struItem.byDay > 31)
            {
                return false;
            }
            return true;
        }
        public static bool CheckDate(CHCNetSDK.NET_DVR_SIMPLE_DAYTIME struItem)
        {
            if (struItem.byHour > 24 || struItem.byMinute > 59 || struItem.bySecond > 59)
            {
                return false;
            }
            return true;
        }

        public static void WriteBytesToFile(byte[] bytes, int len, string strPath)
        {
            try
            {
                using (FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate))
                {
                    BinaryWriter objBinaryWrite = new BinaryWriter(fs);
                    fs.Write(bytes, 0, len);
                    fs.Close();
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("Write file exception details{}" + e.Data.ToString());
                return;
            }
        }
    }

}