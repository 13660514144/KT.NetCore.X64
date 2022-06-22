using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;//这是用到DllImport时候要引入的包

namespace KT.Visitor.IdReader.SDK
{
    public class CVR100XGSDK
    {
        [DllImport("ReferenceFiles/IdReaderSdks/CVR100XG/ScanDll.dll", EntryPoint = "InitComm", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int CVR_InitComm(int Port);//声明外部的标准动态库, 跟Win32API是一样的
        [DllImport("ReferenceFiles/IdReaderSdks/CVR100XG/ScanDll.dll", EntryPoint = "Authenticate", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int CVR_Authenticate();
        //IO_StartRFID 
        [DllImport("ReferenceFiles/IdReaderSdks/CVR100XG/ScanDll.dll", EntryPoint = "IO_StartRFID", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern bool IO_StartRFID();
        //IO_SearchCard  void long
        [DllImport("ReferenceFiles/IdReaderSdks/CVR100XG/ScanDll.dll", EntryPoint = "IO_SearchCard", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int IO_SearchCard();
        //IO_SelectCard  void long
        [DllImport("ReferenceFiles/IdReaderSdks/CVR100XG/ScanDll.dll", EntryPoint = "IO_SelectCard", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int IO_SelectCard();
        //IO_ReadCard  void long
        [DllImport("ReferenceFiles/IdReaderSdks/CVR100XG/ScanDll.dll", EntryPoint = "IO_ReadCard", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int IO_ReadCard(out Bar pstRst);//RFID读卡
                                                             //IO_StopRFID
        [DllImport("ReferenceFiles/IdReaderSdks/CVR100XG/ScanDll.dll", EntryPoint = "IO_StopRFID", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern bool IO_StopRFID();
        //：long IO_HasScanner(LPSTR IVS_600DS, HANDLE hPnpWnd); 
        [DllImport("ReferenceFiles/IdReaderSdks/CVR100XG/ScanDll.dll", EntryPoint = "IO_HasScanner", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int IO_HasScanner(string IDCapture, IntPtr hwndCurWindow);
        [DllImport("ReferenceFiles/IdReaderSdks/CVR100XG/ScanDll.dll", EntryPoint = "IO_CaptureImage", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int IO_CaptureImage(byte[] pSrcIr, int ir_Size, byte[] pSrcwh, int wh_Size);
        [DllImport("ReferenceFiles/IdReaderSdks/CVR100XG/ScanDll.dll", EntryPoint = "IO_NotifyBeep", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern void IO_NotifyBeep(int ms);
        // IO_Notify 有灯闪烁
        [DllImport("ReferenceFiles/IdReaderSdks/CVR100XG/ScanDll.dll", EntryPoint = "IO_Notify", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern void IO_Notify(int ms);
        //IO_NotifyLight 
        [DllImport("ReferenceFiles/IdReaderSdks/CVR100XG/ScanDll.dll", EntryPoint = "IO_NotifyLight", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern void IO_NotifyLight(int ms);
        /// <summary>
        // IO_GetVersion                                                                                           
        [DllImport("ReferenceFiles/IdReaderSdks/CVR100XG/ScanDll.dll", EntryPoint = "IO_GetVersion", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern void IO_GetVersion(byte[] pVersion, System.UInt32 nlen);
        //IO_SetFTLight 
        [DllImport("ReferenceFiles/IdReaderSdks/CVR100XG/ScanDll.dll", EntryPoint = "IO_SetFTLight", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern void IO_SetFTLight(int lType, int lSwitch);
        //IO_GetDevTopState 
        [DllImport("ReferenceFiles/IdReaderSdks/CVR100XG/ScanDll.dll", EntryPoint = "IO_GetDevTopState", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int IO_GetDevTopState();
        //IO_IdentifyCard_Buff 
        [DllImport("ReferenceFiles/IdReaderSdks/CVR100XG/ScanDll.dll", EntryPoint = "IO_IdentifyCard_Buff", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int IO_IdentifyCard_Buff(byte[] pImageBuff, int lWidth, int lHeight, int lImageType, int lCardType, out Bar pResult);

        /// C++自定义类 pVersion
        /// </summary>IO_GetDevTopState        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]


        public struct Bar
        {
            /// char*
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string name;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string SurnameCH;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string nameCH;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string sex;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string Gender;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string birthday;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string people;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string signdate;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string validterm;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 200)]
            public string address;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string number;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string organs;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string SurnameEN;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string nameEN;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string ENfullname;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 200)]
            public string Nationality;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string id;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string Leavetime;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string placeCH;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string placeEN;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string BirthplaceCH;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string BirthplaceEN;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szCodeOne;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szCodeTwo;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szCodeThree;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
            public string Permitnumber_number;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 200)]
            public string Vocational;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string DocumentsCategory;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValTStr, SizeConst = 200)]
            public string Other;
        }
    }
}
