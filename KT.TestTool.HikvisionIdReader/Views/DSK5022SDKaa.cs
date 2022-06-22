using Prism.Events;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace KT.TestTool.HikvisionIdReader.Views
{
    public class DSK5022SDKaa
    {
        private int MAX_MANUFACTURE_LEN = 32;
        private int MAX_DEVICE_NAME_LEN = 32;
        private int MAX_SERIAL_NUM_LEN = 48;

        private int MAX_USERNAME_LEN = 32;
        private int MAX_PASSWORD_LEN = 16;

        private int WORD_LEN = 256;
        private int PIC_LEN = 1024;
        private int FINGER_PRINT_LEN = 1024;
        private int ADDR_LEN = 128;
        private int MAC_LEN = 16;
        private int CARD_NO_LEN = 32;

        private int INVALID_USER_ID = -1;

        private int USB_UPGRADE_FAILED = 0;
        private int USB_UPGRADE_SUCCESS = 1;
        private int USB_UPGRADE_TRANS = 2;

        private int USB_INVALID_UPGRADE_HANDLE = -1;

        //-----------------------------------------------------------------------------------------
        private int USB_ERROR_BASE = 0;

        private int USB_SUCCESS = 0;   // Success (no error)

        //SDK内部错误码
        private int USB_ERROR_INIT_SDK = 1;
        private int USB_ERROR_SDK_NOT_INIT = 2;
        private int USB_ERROR_NO_DEVICE = 3;   // No such device (it may have been disconnected;
        private int USB_ERROR_ACCESS = 4;   // Access denied (insufficient permissions;

        private int USB_ERROR_OPEN = 5;
        private int USB_ERROR_DEV_NOT_READY = 6;

        private int USB_ERROR_IO = 7;   // Input/output error
        private int USB_ERROR_WRITE = 8;
        private int USB_ERROR_READ = 9;
        private int USB_ERROR_TIMEOUT = 10;   // Operation timed out
        private int USB_ERROR_WRITE_TIMEOUT = 11;
        private int USB_ERROR_READ_TIMEOUT = 12;

        private int USB_ERROR_BUSY = 13;   // Resource busy
        private int USB_ERROR_WRITE_BUSY = 14;
        private int USB_ERROR_READ_BUSY = 15;

        private int USB_ERROR_CLOSE = 16;  //

        private int USB_ERROR_OVERFLOW = 17;  // Overflow
        private int USB_ERROR_NO_MEM = 18;  // Insufficient memory
        private int USB_ERROR_PIPE = 19;  // Pipe error
        private int USB_ERROR_INTERRUPTED = 20;  // System call interrupted (perhaps due to signal;
        private int USB_ERROR_NOT_SUPPORTED = 21;  // Operation not supported or unimplemented on this platform
        private int USB_ERROR_WAIT_OBJ = 22;
        private int USB_ERROR_CREATE_OVERLAPPED = 23;
        private int USB_ERROR_OVERLAPPED = 24;
        private int USB_ERROR_RESET_EVENT = 25;

        private int USB_ERROR_SET_OUT_REPORT = 26;
        private int USB_ERROR_RECV_PACK_TIMEOUT = 27;
        private int USB_ERROR_RECV_DATA_LEN = 28;

        private int USB_ERROR_PARAM_INVALID = 29;
        private int USB_ERROR_PARAM_1_INVALID = 30;
        private int USB_ERROR_PARAM_2_INVALID = 31;
        private int USB_ERROR_PARAM_3_INVALID = 32;
        private int USB_ERROR_PARAM_4_INVALID = 33;
        private int USB_ERROR_PARAM_5_INVALID = 34;
        private int USB_ERROR_PARAM_6_INVALID = 35;
        private int USB_ERROR_PARAM_7_INVALID = 36;
        private int USB_ERROR_PARAM_8_INVALID = 37;
        private int USB_ERROR_INVALID_HANDLE = 38;

        private int USB_ERROR_INVALID_USER_ID = 39;
        private int USB_ERROR_INVALID_DEVICE_ID = 40;

        private int USB_ERROR_INVALID_SEESSION_ID = 41;
        private int USB_ERROR_CHECKSUM_FAILED = 42;

        private int USB_ERROR_INTER_STRUCT_SIZE = 43;
        private int USB_ERROR_EXTER_STRUCT_SIZE = 44;
        private int USB_ERROR_STRUCT_HEAD_VER = 45;

        private int USB_ERROR_REG_SEESION = 46;
        private int USB_ERROR_CONVERT_PARAM = 47;

        private int USB_ERROR_INTER_CMD_NOT_DEF = 48;
        private int USB_ERROR_EXTERNAL_CMD_NOT_DEF = 49;

        private int USB_ERROR_GET_DEV_ENCRY = 50;
        private int USB_ERROR_GET_DEV_AES = 51;
        private int USB_ERROR_DEV_NOT_SUPPOTR_AES = 52;
        private int USB_ERROR_DEV_NOT_SUPPOTR_CRC = 53;
        private int USB_ERROR_SDK_AES_MOD = 54;
        private int USB_ERROR_SDK_AES_KEY = 55;
        private int USB_ERROR_SDK_AES_KEY_LEN = 56;
        private int USB_ERROR_SDK_AES_KEY_TYPE = 57;
        private int USB_ERROR_SDK_AES_PROCESS = 58;
        private int USB_ERROR_AES_INPUT_DATA_LEN = 59;

        private int USB_ERROR_GET_DEV_AES_KEY = 60;
        private int USB_ERROR_DEV_REG = 61;
        private int USB_ERROR_LOGIN = 62;
        private int USB_ERROR_RELOGIN = 63;
        private int USB_ERROR_LOGOUT = 64;

        private int USB_ERROR_RET_HEAD_LENGTH = 65;
        private int USB_ERROR_RET_HEAD_VERSION = 66;
        private int USB_ERROR_RET_HEAD = 67;
        private int USB_ERROR_RET_DATA_LEN = 68;

        //设备相关错误码
        private int USB_ERROR_DEV_OPER_FAILED = 257;  // 操作失败
        private int USB_ERROR_DEV_OPER_NOT_SUPPORT = 258;  // 设备不支持该操作
        private int USB_ERROR_DEV_CHECK_SUM = 259;  // 校验和错误
        private int USB_ERROR_DEV_USER_ID = 260;  // 非法的UserID
        private int USB_ERROR_DEV_SESSION_ID = 261;  // 非法的SessionID
        private int USB_ERROR_DEV_OPER_TIMEOUT = 262;  // 设备操作超时

        private int USB_ERROR_DEV_UNKNOW_OPER_RES = 512;  // 未知的设备操作返回码

        private int USB_ERROR_NOT_DEFINED = 0x02FE;
        private int USB_ERROR_OTHER = 0x02FF;  // Other error
                                               //-----------------------------------------------------------------------------------------
        private int USB_SDK_OPERATION_CMD_START = 0x0100;

        private int USB_SDK_SET_BEEP_AND_FLICKER = 0x0100;
        private int USB_SDK_GET_HAIRPIN_VERSION = 0x0101;
        private int USB_SDK_CTRL_RESET_RFC = 0x0102;
        private int USB_SDK_SET_CARD_PROTO = 0x0103;
        private int USB_SDK_GET_ACTIVATE_CARD = 0x0104;
        private int USB_SDK_CTRL_STOP_CARD_OPER = 0x0105;
        private int USB_SDK_SET_M1_PWD_VERIFY = 0x0106;
        private int USB_SDK_GET_M1_READ_BLOCK = 0x0107;
        private int USB_SDK_SET_M1_WRITE_BLOCK = 0x0108;
        private int USB_SDK_SET_M1_MODIFY_SCB = 0x0109;
        private int USB_SDK_SET_M1_BLOCK_ADD_VALUE = 0x010A;
        private int USB_SDK_SET_M1_BLOCK_MINUS_VALUE = 0x010B;
        private int USB_SDK_CTRL_M1_BLOCK_TO_REG = 0x010C;
        private int USB_SDK_CTRL_M1_REG_TO_BLOCK = 0x010D;
        private int USB_SDK_SET_M1_MIFARE_PACK = 0x010E;
        //private int  USB_SDK_GET_M1_MIFARE_PACK     =          0x010F   ;
        private int USB_SDK_SET_PSAM_SEAT = 0x0110;
        private int USB_SDK_SET_CARD_PARAM = 0x0111;
        private int USB_SDK_GET_CPU_CARD_RESET = 0x0112;
        private int USB_SDK_SET_CPU_CARD_PACK = 0x0113;
        //private int  USB_SDK_GET_CPU_CARD_PACK      =          0x0114   ;

        private int USB_SDK_OPERATION_CMD_END = 0x0114;

        public static uint USB_SDK_GET_CERTIFICATE_INFO = 1000;
        private int USB_SDK_GET_CERTIFICATE_ADD_ADDR_INFO = 1001;
        private int USB_SDK_GET_CERTIFICATE_MAC = 1002;
        private int USB_SDK_GET_IC_CARD_NO = 1003;
        private int USB_SDK_DETECT_CARD = 1021;//检测卡片
        private int USB_SDK_SET_IDENTITY_INFO = 1023;   //身份信息下发

        private enum tagLOG_LEVEL_ENUM
        {
            ENUM_ERROR_LEVEL = 1,
            ENUM_DEBUG_LEVEL = 2,
            ENUM_INFO_LEVEL = 3
        }

        [DllImport(@"HCUsbSDK.dll")]
        public static extern bool USB_SDK_Init();//USB_SDK的初始化

        [DllImport(@"HCUsbSDK.dll")]
        public static extern bool USB_SDK_Cleanup();//USB_SDK的反初始化

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dwLogLevel">日志的级别（默认为0）：
        /// 0-表示关闭日志，
        /// 1-表示只输出ERROR错误日志，
        /// 2-输出ERROR错误信息和DEBUG调试信息，
        /// 3-输出ERROR错误信息、DEBUG调试信息和INFO普通信息等所有信息
        /// </param>
        /// <param name="strLogDir"></param>
        /// <param name="bAutoDel"></param>
        /// <returns></returns>
        [DllImport(@"HCUsbSDK.dll")]
        public static extern bool USB_SDK_SetLogToFile(uint dwLogLevel, string strLogDir, bool bAutoDel);

        [DllImport(@"HCUsbSDK.dll")]
        public static extern uint USB_SDK_GetLastError();//获取错误码

        [DllImport(@"HCUsbSDK.dll")]
        public static extern string USB_SDK_GetErrorMsg(uint dwErrorCode);//获取错误码对应的信息

        [DllImport(@"HCUsbSDK.dll")]
        public static extern bool USB_SDK_EnumDevice(EnumDeviceCallBack cbEnumDeviceCallBack);//枚举USB设备

        /// <summary>
        /// INVALID_USER_ID – 登录失败；否则为有效的用户ID，有效ID的范围为0-128
        /// </summary>
        [DllImport(@"HCUsbSDK.dll")]
        public static extern int USB_SDK_Login(ref USB_SDK_USER_LOGIN_INFO userLoginInfo, ref USB_SDK_DEVICE_REG_RES devRegRes);

        [DllImport(@"HCUsbSDK.dll")]
        public static extern bool USB_SDK_Logout(int userId);

        /// </summary>
        /// <param name="lUserID">USB_SDK_Login的返回值</param>
        /// <param name="dwCommand">USB_SDK_GET_CERTIFICATE_INFO</param>
        /// <param name="pInputInfo">pOutputInfo->lpOutBuffer = 一个USB_SDK_CERTIFICATE_INFO结构</param>
        /// <param name="pOutputInfo">pOutputInfo-> dwOutBufferSize =一个USB_SDK_CERTIFICATE_INFO结构的大小</param>
        /// <returns>
        /// TRUE-成功
        /// FALSE-失败
        /// </returns>
        [DllImport(@"HCUsbSDK.dll")]
        public static extern bool USB_SDK_GetDeviceConfig(int userID, uint command, ref USB_CONFIG_INPUT_INFO inputInfo, ref USB_CONFIG_OUTPUT_INFO outputInfo);


        public struct USB_SDK_DEVICE_INFO
        {
            public uint dwSize;   //结构体大小
            public uint dwVID;   //设备VID
            public uint dwPID;   //设备PID
            //char szManufacturer[MAX_MANUFACTURE_LEN/*32*/];//制造商（来自描述符）
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szManufacturer;
            //char szDeviceName[MAX_DEVICE_NAME_LEN/*32*/];//设备名称（来自描述符）
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szDeviceName;
            //char szSerialNumber[MAX_SERIAL_NUM_LEN/*48*/];//设备序列号（来自描述符）
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string szSerialNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 68)]
            public byte[] byRes;/*68*/
        }  //192字节

        //typedef void (CALLBACK* fEnumDeviceCallBack) (USB_SDK_DEVICE_INFO* pDevceInfo, void* pUser);

        public static bool ExecDevice()
        {
            var cbEnumDeviceCallBack = new EnumDeviceCallBack(CallBack);
            USB_SDK_EnumDevice(cbEnumDeviceCallBack);
            return true;
        }

        public delegate void EnumDeviceCallBack(IntPtr devceInfoPtr, IntPtr userPtr);

        public static void CallBack(IntPtr devceInfoPtr, IntPtr userPtr)
        {
            var deviceInfo = Marshal.PtrToStructure<USB_SDK_DEVICE_INFO>(devceInfoPtr);
            DeviceCallBack?.Invoke(deviceInfo);
        }

        public static Action<USB_SDK_DEVICE_INFO> DeviceCallBack;


        public struct USB_SDK_USER_LOGIN_INFO
        {
            public uint dwSize; //结构体大小
            public uint dwTimeout; //登录超时时间（单位：毫秒）
            public uint dwVID;  //设备VID，枚举设备时得到
            public uint dwPID;  //设备PID，枚举设备时得到
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szUserName; //用户名
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string szPassword; //密码  
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string szSerialNumber; //设备序列号，枚举设备时得到
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
            public byte[] byRes;

            public void Init()
            {
                byRes = new byte[80];
            }
        }  //192字节


        public struct USB_SDK_DEVICE_REG_RES
        {
            public uint dwSize;   //结构体大小
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szDeviceName; //设备名称
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string szSerialNumber; //设备序列号
            //软件版本号，格式为：24-32位为主版本号，16-24位为次版本号；0-16位为最小版本号；若版本号为1.2.3则返回0x01020003
            public uint dwSoftwareVersion;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public byte[] byRes;

            public void Init()
            {
                byRes = new byte[40];
            }
        }  //128字节


        public struct USB_SDK_CERTIFICATE_INFO
        {
            public uint dwSize; //结构体大小
            public ushort wWordInfoSize; //文字信息长度
            public ushort wPicInfoSize; //相片信息长度
            public ushort wFingerPrintInfoSize; //指纹信息长度            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byRes2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] byWordInfo; //文字信息
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public byte[] byPicInfo; //相片信息
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public byte[] byFingerPrintInfo; //指纹信息
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public byte[] byRes;

            public void Init()
            {
                byRes2 = new byte[2];
                byWordInfo = new byte[256];
                byPicInfo = new byte[1024];
                byFingerPrintInfo = new byte[1024];
                byRes = new byte[40];
            }
        }


        public struct USB_CONFIG_INPUT_INFO
        {
            public IntPtr lpCondBuffer;//void* 指向条件缓冲区
            public uint dwCondBufferSize;//条件缓冲区大小
            public IntPtr lpInBuffer;          //void* 指向输入缓冲区
            public uint dwInBufferSize;   //输入缓冲区大小
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
            public byte[] byRes;

            public void Init()
            {
                lpCondBuffer = IntPtr.Zero;
                lpInBuffer = IntPtr.Zero;
                byRes = new byte[48];
            }
        }  //64字节


        public struct USB_CONFIG_OUTPUT_INFO
        {
            public IntPtr lpOutBuffer;      //void* 指向输出缓冲区
            public uint dwOutBufferSize;  //输出缓冲区大小 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
            public byte[] byRes;

            public void Init()
            {
                lpOutBuffer = IntPtr.Zero;
                byRes = new byte[56];
            }
        } //64字节

        //public delegate void fEnumDeviceCallBack([MarshalAs(UnmanagedType.LPStruct, SizeConst = 192)] tagUSB_SDK_DEVICE_INFO pDevceInfo, IntPtr pUser);

        //public static void CallBack(tagUSB_SDK_DEVICE_INFO pDevceInfo, IntPtr pUser)
        //{
        //    DeviceCallBack?.Invoke(pDevceInfo);
        //}

        //public static Action<tagUSB_SDK_DEVICE_INFO> DeviceCallBack;

        //        //-----------------------------------------------------------------------------------------

        //        public struct tagUSB_SDK_USER_LOGIN_INFO
        //        {
        //            uint dwSize; //结构体大小
        //            uint dwTimeout; //登录超时时间（单位：毫秒）
        //            uint dwVID;  //设备VID，枚举设备时得到
        //            uint dwPID;  //设备PID，枚举设备时得到
        //            string szUserName;/*32*/ //用户名
        //            string szPassword;/*16*/ //密码
        //            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        //            string szSerialNumber; /*48*///设备序列号，枚举设备时得到
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
        //            byte[] byRes;/*80*/
        //        }
        //        //USB_SDK_USER_LOGIN_INFO, *LPUSB_SDK_USER_LOGIN_INFO; //192字节

        //        public struct tagUSB_SDK_DEVICE_INFO
        //        {
        //            uint dwSize;   //结构体大小
        //            uint dwVID;   //设备VID
        //            uint dwPID;   //设备PID
        //            char szManufacturer[MAX_MANUFACTURE_LEN/*32*/];//制造商（来自描述符）
        //            char szDeviceName[MAX_DEVICE_NAME_LEN/*32*/];//设备名称（来自描述符）
        //            char szSerialNumber[MAX_SERIAL_NUM_LEN/*48*/];//设备序列号（来自描述符）
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 68)]
        //            byte[] byRes;
        //        } //192字节

        //        public struct tagUSB_SDK_DEVICE_REG_RES
        //        {
        //            uint dwSize;   //结构体大小
        //            char szDeviceName[MAX_DEVICE_NAME_LEN /*32*/]; //设备名称
        //            char szSerialNumber[MAX_SERIAL_NUM_LEN /*48*/]; //设备序列号
        //            uint dwSoftwareVersion; //软件版本号,高16位是主版本,低16位是次版本
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        //            byte[] byRes;
        //        } //128字节

        //        public struct tagUSB_CONFIG_INPUT_INFO
        //        {
        //            void* lpCondBuffer;        //指向条件缓冲区
        //            uint dwCondBufferSize;//条件缓冲区大小
        //            void* lpInBuffer;          //指向输出缓冲区
        //            uint dwInBufferSize;   //输入缓冲区大小
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
        //            byte[] byRes;
        //        } //64字节

        //        public struct tagUSB_CONFIG_OUTPUT_INFO
        //        {
        //            void* lpOutBuffer;      //指向输出缓冲区
        //            uint dwOutBufferSize;  //输出缓冲区大小
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
        //            byte[] byRes;
        //        }  //64字节

        //        public struct tagCONFIG_INPUT_INFO
        //        {
        //            void* lpParamBuffer;    //输入参数缓冲区
        //            uint dwParamBufferSize;//输入参数缓冲区大小
        //            void* lpInBuffer;      //输入缓冲区
        //            uint dwInBufferSize;  //输入缓冲区大小
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
        //            byte[] byRes;
        //        } //64字节

        //        public struct tagUSB_UPGRADE_STATE
        //        {
        //            uint dwSize;
        //            byte byState;
        //            byte byProgress;
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        //            byte[] byRes;
        //        } //12字节

        //        public struct tagUSB_UPGRADE_COND
        //        {
        //            uint dwSize;
        //            byte byTimeout;
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        //            byte[] byRes;
        //        }//16字节

        //        public void (CALLBACK* fEnumDeviceCallBack) (USB_SDK_DEVICE_INFO* pDevceInfo, void* pUser);

        ////////////////////////////////////////////////////////////////////////////////

        //public struct tagUSB_SDK_BEEP_AND_FLICKER
        //        {
        //            uint dwSize;   //结构体大小
        //            byte byBeepType;// 蜂鸣类型 0无效，1连续，2慢鸣，3快鸣，4停止
        //            byte byBeepCount;// 鸣叫次数, （只对慢鸣、快鸣有效，且不能为0）
        //            byte byFlickerType;// 闪烁类型 0无效，1连续，2错误，3正确，4停止
        //            byte byFlickerCount;// 闪烁次数（只对错误、正确有效，且不能为0）
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
        //            byte[] byRes;
        //        }//32字节

        //        public struct tagUSB_SDK_HAIRPIN_VERSION
        //        {
        //            uint dwSize;   //结构体大小
        //            char szDeviceName[MAX_DEVICE_NAME_LEN /*32*/]; //设备名称
        //            char szSerialNumber[MAX_SERIAL_NUM_LEN /*48*/]; //设备序列号
        //            uint dwSoftwareVersion; //软件版本号,//软件版本号，格式为：24-32位为主版本号，16-24位为次版本号；0-16位为最小版本号；
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        //            byte[] byRes;
        //        } //128字节

        //        public struct tagUSB_SDK_CARD_PROTO
        //        {
        //            uint dwSize;   //结构体大小
        //            byte byProto; //卡协议类型（0-TypeA,1-TypeB,2-typeAB,3-125Khz,255所有）
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 27)]
        //            byte[] byRes;
        //        } //32字节

        //        public struct tagUSB_SDK_WAIT_SECOND
        //        {
        //            uint dwSize;   //结构体大小
        //            byte byWait; // 1Byte操作等待时间（0-一直执行直到有卡响应，其他对应1S单位）
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 27)]
        //            byte[] byRes;
        //        } //32字节

        //        public struct tagUSB_SDK_ACTIVATE_CARD_RES
        //        {
        //            uint dwSize;
        //            byte byCardType;// 卡类型（0-TypeA m1卡，1-TypeA cpu卡,2-TypeB,3-125kHz Id卡）
        //            byte bySerialLen; //卡物理序列号字节长度
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        //            byte[] bySerial;//卡物理序列号
        //            byte bySelectVerifyLen; //选择确认长度
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        //            byte[] bySelectVerify; //选择确认(3字节)
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        //            byte[] byRes;
        //        } //32字节

        //        public struct tagUSB_SDK_M1_PWD_VERIFY_INFO
        //        {
        //            uint dwSize;   //结构体大小
        //            byte byPasswordType; // 密码类别（0-KeyA, 1-KeyB）
        //            byte bySectionNum; // 要验证密码的扇区号
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        //            byte[] byRes1;  //保留字节
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        //            byte[] byPassword; // 6Byte密码
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
        //            byte[] byRes; //保留字节
        //        }  //32字节

        //        public struct tagUSB_SDK_M1_BLOCK_ADDR
        //        {
        //            uint dwSize;
        //            ushort wAddr; // 2Byte块地址
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
        //            byte[] byRes;
        //        }//32字节

        //        public struct tagUSB_SDK_M1_BLOCK_DATA
        //        {
        //            uint dwSize;   //结构体大小
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        //            byte[] byData;// 16Byte块数据
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        //            byte[] byRes;
        //        }//32字节

        //        public struct tagUSB_SDK_M1_BLOCK_WRITE_DATA
        //        {
        //            uint dwSize;   //结构体大小
        //            ushort wAddr;    // 2Byte块地址
        //            byte byDataLen; // 数据长度(0-16)
        //            byte byRes1;    //保留字节
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        //            byte[] byData; //16Byte BUFF(要写的块数据)
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        //            byte[] byRes;  //保留字节
        //        }//32字节

        //        public struct tagUSB_SDK_M1_MODIFY_SCB
        //        {
        //            uint dwSize;   //结构体大小
        //            byte bySectionNum;  //1Byte扇区号
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        //            byte[] byPasswordA;// 6Byte 密码A
        //            byte byRes1;    //保留字节
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        //            byte[] byCtrlBits;   // 4Byte控制位
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        //            byte[] byPasswordB;// 6Byte 
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        //            byte[] byRes; //保留字节
        //        }  //32字节

        //        public struct tagUSB_SDK_M1_BLOCK_OPER
        //        {
        //            uint dwSize;   //结构体大小
        //            ushort wAddr;    // 2Byte块地址
        //            ushort wValue;    // 2Byte要增加的值
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
        //            byte[] byRes;
        //        } //32字节

        //        public struct tagUSB_SDK_M1_BLOCK_OPER_RES
        //        {
        //            uint dwSize;
        //            ushort wSuccessNum;// 2Byte 实际成功次数
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
        //            byte[] byRes;
        //        } //32字节

        //        public struct tagUSB_SDK_M1_MIFARE_PACK
        //        {
        //            uint dwSize;
        //            byte byBufLen; //数据长度（0-255）
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        //            byte[] byRes1; //保留字节
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
        //            byte[] byBuf;//数据
        //            byte byRes2; //保留字节
        //            byte byDelay; //延迟时间（单位10ms）,0为默认值（2000ms）(由于SDK默认超时时间5秒，这个时间应不超过5秒)
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 55)]
        //            byte[] byRes;
        //        }  //320字节


        //        public struct tagUSB_SDK_PSAM_SEAT_INFO
        //        {
        //            uint dwSize;
        //            byte bySeat;// 1Byte PSAM卡座序号（0- 卡座1，1-卡座2）
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 27)]
        //            byte[] byRes;
        //        } //32字节

        //        public struct tagUSB_SDK_CARD_PARAM
        //        {
        //            uint dwSize;
        //            byte byCardType; // 1Byte卡类型(0-13.56射频CPU卡，1-PSAM卡座1,2-PSAM卡座2)
        //            byte byCardProto; // 1Byte卡协议类型（0为T=0，1为T=1）
        //                              //byte    byDelay; // 1Byte卡操作延时时间（0为默认，其他以10MS为单位）
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
        //            byte[] byRes;
        //        } //32字节

        //        public struct tagUSB_SDK_CPU_CARD_RESET_RES
        //        {
        //            uint dwSize;
        //            byte byBufLen;//byBuf中有效数据长度（0-60）
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        //            byte byRes1; //保留字节
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]
        //            byte byBuf;//（一般是厂商信息）
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
        //            byte byRes; //保留字节
        //        }  //96字节

        //        public struct tagUSB_SDK_CPU_CARD_PACK
        //        {
        //            uint dwSize;
        //            byte byBufLen; //0-255
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        //            byte[] byRes1; //保留字节
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
        //            byte[] byBuf;
        //            byte byDelay; //延迟时间（单位10ms），0为默认（200ms）
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
        //            byte[] byRes; //保留字节
        //        } //320字节

        //        public struct tagUSB_SDK_CERTIFICATE_INFO
        //        {
        //            uint dwSize; //结构体大小
        //            ushort wWordInfoSize; //文字信息长度
        //            ushort wPicInfoSize; //相片信息长度
        //            ushort wFingerPrintInfoSize; //指纹信息长度
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        //            byte[] byRes2;
        //            byte byWordInfo[WORD_LEN/*256*/]; //文字信息
        //            byte byPicInfo[PIC_LEN/*1024*/]; //相片信息
        //            byte byFingerPrintInfo[FINGER_PRINT_LEN/*1024*/]; //指纹信息
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        //            byte[] byRes;
        //        }

        //        public struct tagUSB_SDK_IDENTITY_INFO_CFG
        //        {
        //            uint dwSize; //结构体大小
        //            ushort wPicInfoSize; //相片信息长度
        //            ushort wFingerPrintInfoSize; //指纹信息长度
        //            byte byPicInfo[PIC_LEN/*1024*/]; //身份证图片信息
        //            byte byFingerPrintInfo[FINGER_PRINT_LEN/*1024*/]; //指纹信息
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        //            byte[] byRes;
        //        }

        //        public struct tagUSB_SDK_CERTIFICATE_ADD_ADDR_INFO
        //        {
        //            uint dwSize; //结构体大小
        //            ushort wAddrInfoSize; //追加住址信息长度
        //            byte byAddAddrInfo[ADDR_LEN/*128*/]; //追加住址信息
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        //            byte[] byRes;
        //        }

        //        public struct tagUSB_SDK_CERTIFICATE_MAC
        //        {
        //            uint dwSize; //结构体大小
        //            byte byMac[MAC_LEN/*16*/]; //物理序列号
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        //            byte[] byRes;
        //        }

        //        public struct tagUSB_SDK_IC_CARD_NO
        //        {
        //            uint dwSize; //结构体大小
        //            byte byCardNo[CARD_NO_LEN/*32*/]; //IC卡卡号
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        //            byte[] byRes;
        //        }

        //        public struct tagUSB_SDK_DETECT_CARD_COND
        //        {
        //            uint dwSize;   //结构体大小
        //            byte byWait; // 1Byte操作等待时间（0-一直执行直到有卡响应，其他对应1S单位）
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 27)]
        //            byte[] byRes;
        //        } //32字节

        //        public struct tagUSB_SDK_DETECT_CARD_CFG
        //        {
        //            uint dwSize; //结构体大小
        //            byte byCardStatus; //卡片状态：0-未检测到，1-检测到
        //            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 27)]
        //            byte[] byRes;
        //        } //32字节

        //        //-----------------------------------------------------------------------------------------

        //        [DllImport(@"HCUsbSDK.dll")]
        //        public static extern bool USB_SDK_Init();//USB_SDK的初始化

        //        [DllImport(@"HCUsbSDK.dll")]
        //        public static extern bool USB_SDK_Cleanup();//USB_SDK的反初始化

        //        [DllImport(@"HCUsbSDK.dll")]
        //        public static extern bool USB_SDK_SetLogToFile(uint dwLogLevel, string strLogDir, bool bAutoDel);

        //        [DllImport(@"HCUsbSDK.dll")]
        //        public static extern uint USB_SDK_GetLastError();//获取错误码

        //        [DllImport(@"HCUsbSDK.dll")]
        //        public static extern string USB_SDK_GetErrorMsg(uint dwErrorCode);//获取错误码对应的信息

        //        [DllImport(@"HCUsbSDK.dll")]
        //        public static extern bool USB_SDK_EnumDevice(fEnumDeviceCallBack cbEnumDeviceCallBack, void* pUser);//枚举USB设备

        //        //USB_SDK_API LONG  __stdcall USB_SDK_Login(LPUSB_SDK_USER_LOGIN_INFO pUsbLoginInfo, LPUSB_SDK_DEVICE_INFO pUsbDeviceInfo);//登录设备
        //        [DllImport(@"HCUsbSDK.dll")]
        //        public static extern int USB_SDK_Login(LPUSB_SDK_USER_LOGIN_INFO pUsbLoginInfo, LPUSB_SDK_DEVICE_REG_RES pDevRegRes);

        //        [DllImport(@"HCUsbSDK.dll")]
        //        public static extern bool USB_SDK_Logout(int lUserID);//关闭USB设备


        //        [DllImport(@"HCUsbSDK.dll")]
        //        public static extern bool USB_SDK_SetDeviceConfig(int lUserID,
        //                                                            uint dwCommand,
        //                                                            LPUSB_CONFIG_INPUT_INFO pConfigInputInfo,
        //                                                            LPUSB_CONFIG_OUTPUT_INFO pConfigOutputInfo);

        //        [DllImport(@"HCUsbSDK.dll")]
        //        public static extern bool USB_SDK_GetDeviceConfig(int lUserID,
        //                                                            uint dwCommand,
        //                                                            LPUSB_CONFIG_INPUT_INFO pConfigInputInfo,
        //                                                            LPUSB_CONFIG_OUTPUT_INFO pConfigOutputInfo);

        //        [DllImport(@"HCUsbSDK.dll")]
        //        public static extern bool USB_SDK_ControlDevice(int lUserID,
        //                                                          uint dwCommand,
        //                                                          LPUSB_CONTROL_INPUT_INFO pInputInfo);

        //        [DllImport(@"HCUsbSDK.dll")]
        //        public static extern uint   USB_SDK_GetSDKVersion(void);

        //        [DllImport(@"HCUsbSDK.dll")]
        //        public static extern int   USB_SDK_Upgrade(int lUserID, uint dwUpgradeType, char* sFileName, void* pInbuffer, uint dwBufferLen);

        //        [DllImport(@"HCUsbSDK.dll")]
        //        public static extern bool USB_SDK_GetUpgradeState(int lUpgradeHandle, LPUSB_UPGRADE_STATE_INFO pUpgradeState);

        //        [DllImport(@"HCUsbSDK.dll")]
        //        public static extern bool USB_SDK_CloseUpgradeHandle(int lUpgradeHandle);

        //        //-----------------------------------------------------------------------------------------


    }
}
