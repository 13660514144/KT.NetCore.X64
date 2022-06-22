using System;
using System.Runtime.InteropServices;

namespace KT.Visitor.IdReader.SDK
{
    public partial class DSK5022SDK
    {
        #region HCUsbSDK.dll macro definition

        #region common use
        public const int MAX_MANUFACTURE_LEN = 32;
        public const int MAX_DEVICE_NAME_LEN = 32;
        public const int MAX_SERIAL_NUM_LEN = 48;////序列号的最大长度
        public const int MAX_USB_DEV_LEN = 64;
        public const int ERR_LEVEL = 1;//ListInfo里面的信息类型
        public const int DBG_LEVEL = 2;//ListInfo里面的信息类型
        public const int INF_LEVEL = 3;//ListInfo里面的信息类型
        public const int MAX_USERNAME_LEN = 32;//用户名的最大长度
        public const int MAX_PASSWORD_LEN = 16;//最大的密码长度
        public const int INVALID_USER_ID = -1;
        public const int USB_ERROR_BASE = 0;
        public const int USB_SUCCESS = (DSK5022SDK.USB_ERROR_BASE + 0);
        public const uint USB_SDK_GET_ACTIVATE_CARD = 0x0104;
        public const uint USB_SDK_GET_CERTIFICATE_INFO = 1000;
        #endregion

        #region
        public const int USB_SDK_SET_BEEP_AND_FLICKER = 0x0100;     //控制蜂鸣器及显示灯
        public const int USB_SDK_SET_CARD_PROTO = 0x0103;           //设置操作卡协议类型
        public const int USB_SDK_SET_M1_PWD_VERIFY = 0x0106;       //(M1卡) 验证扇区密码
        public const int USB_SDK_SET_M1_WRITE_BLOCK = 0x0108;       //(M1卡) 写卡指定块数据
        public const int USB_SDK_SET_M1_MODIFY_SCB = 0x0109;        //(M1卡) 修改指定扇区控制块
        public const int USB_SDK_SET_M1_BLOCK_ADD_VALUE = 0x010A;   //(M1卡) 块加值
        public const int USB_SDK_SET_M1_BLOCK_MINUS_VALUE = 0x010B; //(M1卡) 块减值
        public const int USB_SDK_SET_M1_MIFARE_PACK = 0x010E;       //(M1卡) Mifare数据包透传
        public const int USB_SDK_SET_CARD_PARAM = 0x0111;           //(M1卡) 设置卡参数
        public const int USB_SDK_SET_CPU_CARD_PACK = 0x0113;        //(M1卡) CPU卡数据包透传
        #endregion


        public enum LOG_LEVEL_ENUM
        {
            ENUM_ERROR_LEVEL = 1,
            ENUM_DEBUG_LEVEL = 2,
            ENUM_INFO_LEVEL = 3
        }
        #endregion

        #region structure definition
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct USB_SDK_DEVICE_INFO
        {
            public uint dwSize;   //结构体大小
            public uint dwVID;   //设备VID
            public uint dwPID;   //设备PID
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_MANUFACTURE_LEN)]
            public string szManufacturer;//制造商（来自描述符)
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_DEVICE_NAME_LEN)]
            public string szDeviceName;//设备名称（来自描述符）
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_SERIAL_NUM_LEN)]
            public string szSerialNumber;//设备序列号（来自描述符）
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 68, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public void Init()
            {
                byRes = new byte[68];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct USB_SDK_USER_LOGIN_INFO//登录参数结构体
        {
            public uint dwSize; //结构体大小
            public uint dwTimeout; //登录超时时间（单位：毫秒）
            public uint dwVID;  //设备VID，枚举设备时得到
            public uint dwPID;  //设备PID，枚举设备时得到
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_USERNAME_LEN)]//用户名32
            public string szUserName;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_PASSWORD_LEN)]//密码16
            public string szPassword;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_SERIAL_NUM_LEN)]//序列号的最大长度48
            public string szSerialNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留的字段

            public void Init()
            {
                byRes = new byte[80];
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct USB_SDK_DEVICE_REG_RES//登录返回信息结构体
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_DEVICE_NAME_LEN)]//32
            public string szDeviceName;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_SERIAL_NUM_LEN)]//48
            public string szSerialNumber;
            uint dwSoftwareVersion;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 40, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                byRes = new byte[40];
            }
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct USB_SDK_M1_PWD_VERIFY_INFO    //卡扇区带密码验证结构体
        {
            public uint dwSize;
            public byte byPasswordType;
            public byte bySectionNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byPassword;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 18, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public void Init()
            {
                byRes1 = new byte[2];
                byPassword = new byte[6];
                byRes = new byte[18];
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct USB_SDK_M1_BLOCK_WRITE_DATA   //写卡指定块数据
        {
            public uint dwSize;
            public short wAddr;
            public byte byDataLen;
            public byte byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byData;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public void Init()
            {
                byData = new byte[16];
                byRes = new byte[8];
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct USB_SDK_M1_MODIFY_SCB   //(M1卡) 修改指定扇区控制块
        {
            public uint dwSize;
            public byte bySectionNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byPasswordA;
            public byte byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byCtrlBits;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byPasswordB;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public void Init()
            {
                byPasswordA = new byte[6];
                byCtrlBits = new byte[4];
                byPasswordB = new byte[6];
                byRes = new byte[10];
            }
        }




        [StructLayout(LayoutKind.Sequential)]
        public struct USB_SDK_ACTIVATE_CARD_RES
        {
            public uint dwSize;
            public byte byCardType;// 卡类型（0-TypeA m1卡，1-TypeA cpu卡,2-TypeB,3-125kHz Id卡）
            public byte bySerialLen; //卡物理序列号字节长度
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] bySerial;//卡物理序列号
            public byte bySelectVerifyLen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] bySelectVerify;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                bySerial = new byte[10];
                bySelectVerify = new byte[3];
                byRes = new byte[12];
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct USB_SDK_WAIT_SECOND
        {
            public uint dwSize;//结构体的大小
            public byte byWait;//1Byte操作等待时间（0-一直执行直到有卡响应，其他对应1S单位）
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 27, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                byRes = new byte[27];
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct USB_CONFIG_INPUT_INFO
        {
            public IntPtr lpCondBuffer;//指向条件缓冲区
            public uint dwCondBufferSize;//条件缓冲区大小
            public IntPtr lpInBuffer;//指向输出缓冲区
            public uint dwInBufferSize;//输入缓冲区大小
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                byRes = new byte[48];
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct USB_CONFIG_OUTPUT_INFO
        {
            public IntPtr lpOutBuffer;//指向输出缓冲区
            public uint dwOutBufferSize;//输出缓冲区大小
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//64字节
            public void Init()
            {
                byRes = new byte[56];
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct USB_SDK_CERTIFICATE_INFO
        {
            public uint dwSize; //结构体大小
            public ushort wWordInfoSize; //文字信息长度
            public ushort wPicInfoSize; //相片信息长度
            public ushort wFingerPrintInfoSize; //指纹信息长度
            public byte byCertificateType; //证件类型：0-身份证，1-中国绿卡
            public byte byRes2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256, ArraySubType = UnmanagedType.I1)]
            public byte[] byWordInfo;//文字信息
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024, ArraySubType = UnmanagedType.I1)]
            public byte[] byPicInfo;//相片信息
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024, ArraySubType = UnmanagedType.I1)]
            public byte[] byFingerPrintInfo;//指纹信息
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                byWordInfo = new byte[256];
                byPicInfo = new byte[1024];
                byFingerPrintInfo = new byte[1024];
                byRes = new byte[40];
            }
        }

        #endregion

        #region HCUsbSDK.dll function definition
        [DllImport("ReferenceFiles\\IdReaderSdks\\DSK5022\\HCUsbSDK.dll")]
        public static extern bool USB_SDK_Init();

        [DllImport("ReferenceFiles\\IdReaderSdks\\DSK5022\\HCUsbSDK.dll")]
        public static extern bool USB_SDK_Cleanup();

        [DllImport("ReferenceFiles\\IdReaderSdks\\DSK5022\\HCUsbSDK.dll")]
        public static extern uint USB_SDK_GetLastError();

        public delegate void EnumDeviceCallBack(ref USB_SDK_DEVICE_INFO pDevceInfo, IntPtr pUser);
        [DllImport("ReferenceFiles\\IdReaderSdks\\DSK5022\\HCUsbSDK.dll")]
        public static extern bool USB_SDK_EnumDevice(EnumDeviceCallBack cbEnumDeviceCallBack, IntPtr pUser);

        [DllImport("ReferenceFiles\\IdReaderSdks\\DSK5022\\HCUsbSDK.dll")]
        public static extern IntPtr USB_SDK_GetErrorMsg(uint dwErrorCode);

        [DllImport("ReferenceFiles\\IdReaderSdks\\DSK5022\\HCUsbSDK.dll")]
        public static extern bool USB_SDK_SetLogToFile(uint dwLogLevel, IntPtr strLogDir, bool bAutoDel);

        [DllImport("ReferenceFiles\\IdReaderSdks\\DSK5022\\HCUsbSDK.dll")]
        public static extern uint USB_SDK_GetSDKVersion();

        [DllImport("ReferenceFiles\\IdReaderSdks\\DSK5022\\HCUsbSDK.dll")]
        public static extern int USB_SDK_Login(ref USB_SDK_USER_LOGIN_INFO pUsbLoginInfo, ref USB_SDK_DEVICE_REG_RES pDevRegRes);

        [DllImport("ReferenceFiles\\IdReaderSdks\\DSK5022\\HCUsbSDK.dll")]
        public static extern bool USB_SDK_Logout(long lUserID);

        [DllImport("ReferenceFiles\\IdReaderSdks\\DSK5022\\HCUsbSDK.dll")]
        public static extern bool USB_SDK_GetDeviceConfig(int lUserID, uint dwCommand, ref USB_CONFIG_INPUT_INFO pInputInfo, ref USB_CONFIG_OUTPUT_INFO pOutputInfo);

        [DllImport("ReferenceFiles\\IdReaderSdks\\DSK5022\\HCUsbSDK.dll")]
        public static extern bool USB_SDK_SetDeviceConfig(int lUserID, uint dwCommand, ref USB_CONFIG_INPUT_INFO pInputInfo, ref USB_CONFIG_OUTPUT_INFO pOutputInfo);

        [DllImport("ReferenceFiles\\IdReaderSdks\\DSK5022\\Dewlt.dll")]
        public static extern int dewlt(IntPtr strPictureBin);
        #endregion

    }
}
