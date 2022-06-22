using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;//这是用到DllImport时候要引入的包

namespace KT.Visitor.IdReader.SDK
{
    public class HD900SDK
    {
        public const string dllpath = "HDstdapi_x64.dll";
        [DllImport(dllpath, EntryPoint = "HD_InitComm", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int HD_InitComm(int Port);//声明外部的标准动态库, 跟Win32API是一样的

        [DllImport(dllpath, EntryPoint = "HD_CloseComm", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int HD_CloseComm(int Port);//声明外部的标准动态库, 跟Win32API是一样的

        [DllImport(dllpath, EntryPoint = "HD_Authenticate", CharSet = CharSet.Ansi, SetLastError = false)]
        //public static extern int HD_Authenticate(int iType);//声明外部的标准动态库, 跟Win32API是一样的
        public static extern int HD_Authenticate(int iType);

        [DllImport(dllpath, EntryPoint = "HD_Read_IDInfo", CharSet = CharSet.Ansi, SetLastError = false)]
        // public static extern int HD_Read_IDInfo(IntPtr pBmpData, IntPtr BaseMsgData );
        public static extern int HD_Read_IDInfo(byte[] pBmpData, byte[] BaseMsgData);

        //HD_Read_ BaseMsg
        [DllImport(dllpath, EntryPoint = "HD_Read_BaseMsg", CharSet = CharSet.Ansi, SetLastError = false)]
        /*public static extern int HD_Read_BaseMsg(string path, 
            byte[] pName, 
            byte[] pSex, 
            byte[] pNation,
            byte[] pBirth,
            byte[] pAddress, 
            byte[] pCertNo, 
            byte[] pDepartment, 
            byte[] pEffectData, byte[] pExpire);//*/
        public static extern int HD_Read_BaseMsg(string path,
            string pName,
            string pSex,
            string pNation,
            string pBirth,
            string pAddress,
            string pCertNo,
            string pDepartment,
            string pEffectData, string pExpire);
        /// <summary>
        /// 支持指纹录入的
        /// </summary>
        /// <param name="pBmpData"></param>
        /// <param name="BaseMsgData"></param>
        /// <returns></returns>
        [DllImport(dllpath, EntryPoint = "HD_Read_IDFPInfo", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int HD_Read_IDFPInfo(out byte[] pFingerData, out byte[] pBmpData, out byte[] BaseMsgData);//pBmp maxsize 1024 basemesdata maxsize 256
        /// <summary>
        /// 读卡芯片
        /// </summary>
        /// <returns></returns>
        [DllImport(dllpath, EntryPoint = "HD_ReadCard", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern long HD_ReadCard();

        [DllImport(dllpath, EntryPoint = "GetName", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int GetName(byte[] pName);
        [DllImport(dllpath, EntryPoint = "GetSex", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int GetSex(byte[] pSex);
        [DllImport(dllpath, EntryPoint = "GetNation", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int GetNation(byte[] pNation);
        [DllImport(dllpath, EntryPoint = "GetBirth", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int GetBirth(byte[] pBirth);
        [DllImport(dllpath, EntryPoint = "GetAddress", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int GetAddress(byte[] pAddress);
        [DllImport(dllpath, EntryPoint = "GetCertNo", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int GetCertNo(byte[] pCertNo);

        [DllImport(dllpath, EntryPoint = "GetCardType", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int GetCardType();

        [DllImport(dllpath, EntryPoint = "GetEffectDate", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int GetEffectDate(byte[] pEffectDate);

        [DllImport(dllpath, EntryPoint = "GetExpireDate", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int GetExpireDate(byte[] pExpireDate);

        [DllImport(dllpath, EntryPoint = "GetBmpFile", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int GetBmpFile(string pBmpfilepath);
        [DllImport(dllpath, EntryPoint = "GetEnName", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int GetEnName(byte[] pEnName);

        [DllImport(dllpath, EntryPoint = "GetTXZHM", CharSet = CharSet.Ansi, SetLastError = false)]
        public static extern int GetTXZHM(byte[] pTXZHM);
    }
}
