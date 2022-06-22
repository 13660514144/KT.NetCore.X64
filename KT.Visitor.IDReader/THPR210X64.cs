using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDevices;
using KT.Visitor.IdReader.Common;
using System.Drawing;
using System.Diagnostics;
using System.Threading;

namespace KT.Visitor.IdReader
{
    public class THPR210X64: IDevice
    {
        private bool m_bIsIDCardLoaded = false;
        private string CardScanType = string.Empty;

        private int CodeType = 2;
        private ILogger _logger;
        public THPR210X64(ILogger logger)
        {
            _logger = logger;
        }
        public static string DllPath = string.Empty;//dll包路劲
        public static string ImgHeadPath = string.Empty;//头像保存路径
        public static string UserKeyID = "58795668242476138509";//授权ID
        public ReaderTypeEnum ReaderType { get; } = ReaderTypeEnum.THPR210;
        public Action<object> ResultCallBack { get; set; }
        #region  初始化
        public static class MyDll
        {
            [DllImport("Kernel32.dll")]
            public static extern IntPtr LoadLibrary(string path);

            [DllImport("Kernel32.dll")]
            public static extern bool FreeLibrary(IntPtr hModule);

            [DllImport("Kernel32.dll")]
            private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
            [DllImport("kernel32.dll")]
            public static extern uint GetLastError();
            public static Delegate LoadFunction<T>(IntPtr hModule, string functionName)
            {
                IntPtr functionAddress = GetProcAddress(hModule, functionName);
                if (functionAddress.ToInt64() == 0)
                {
                    return null;
                }
                return Marshal.GetDelegateForFunctionPointer(functionAddress, typeof(T));
            }
        }

        //初始化
        public delegate int InitIDCard([MarshalAs(UnmanagedType.LPWStr)] String strUserID, int nType, [MarshalAs(UnmanagedType.LPWStr)] String strDllPath);
        public delegate void FreeIDCard();

        //检测设备状态
        public delegate bool CheckDeviceOnlineEx();

        //图像采集
        public delegate int AcquireImage(int nCardType);
        public delegate bool SetAcquireImageType(int nLightType, int nImageType);
        public delegate bool SetUserDefinedImageSize(int nWidth, int nHeight);
        public delegate bool SetAcquireImageResolution(int nResolutionX, int nResolutionY);

        //识别并获取识别结果
        public delegate int ResetIDCardID();
        public delegate int AddIDCardID(int nMainID, int[] nSubID, int nSubIdCount);
        public delegate int SetIDCardRejectType(int nMainID, bool bFlag);
        public delegate int RecogIDCard();
        public delegate int RecogIDCardEX(int nMainID, int nSubID);
        public delegate int GetRecogResult(int nIndex, [MarshalAs(UnmanagedType.LPWStr)] String strBuffer, ref int nBufferLen);
        public delegate int GetFieldName(int nIndex, [MarshalAs(UnmanagedType.LPWStr)] String strBuffer, ref int nBufferLen);
        public delegate int GetResultType(int nIndex);
        public delegate int GetIDCardName([MarshalAs(UnmanagedType.LPWStr)] String strBuffer, ref int nBufferLen);
        public delegate int GetSubID();

        //图像保存
        public delegate int SaveImage([MarshalAs(UnmanagedType.LPWStr)] String strFileName);
        public delegate int SaveHeadImage([MarshalAs(UnmanagedType.LPWStr)] String strFileName);

        //其它接口
        public delegate int GetCurrentDevice([MarshalAs(UnmanagedType.LPWStr)] String strDeviceName, int nLength);
        public delegate void GetVersionInfo([MarshalAs(UnmanagedType.LPWStr)] String strVersion, int nLength);
        public delegate int GetDeviceSN([MarshalAs(UnmanagedType.LPWStr)] String strDevcieSn, int nLength);
        public delegate int WriteDeviceSI([MarshalAs(UnmanagedType.LPWStr)] String strDeviceSI, int nLength);
        public delegate int CheckDeviceSI([MarshalAs(UnmanagedType.LPWStr)] String strDeviceSI, int nLength);

        //名片接口
        public delegate int RecogBusinessCard(int nType);
        public delegate int GetBusinessCardResultCount(int nID);
        public delegate int GetBusinessCardResult(int nID, int nIndex, [MarshalAs(UnmanagedType.LPWStr)] String strBuffer, ref int nBufferLen);
        public delegate int GetBusinessCardFieldName(int nID, [MarshalAs(UnmanagedType.LPWStr)] String strBuffer, ref int nBufferLen);
        public delegate int GetBusinessCardPosition(int nID, int nIndex, ref int nBufferLen, ref int nRight, ref int nTop, ref int nBottoom);
        //初始化
        InitIDCard lpInitIDCard;
        FreeIDCard lpFreeIDCard;
        //检测设备
        CheckDeviceOnlineEx lpCheckDeviceOnlineEx;
        //拍照
        AcquireImage lpAcquireImage;
        SetAcquireImageType lpSetAcquireImageType;
        SetAcquireImageResolution lpSetAcquireImageResolution;
        SetUserDefinedImageSize lpSetUserDefinedImageSize;
        //设置证件类型
        ResetIDCardID lpResetIDCardID;
        AddIDCardID lpAddIDCardID;
        SetIDCardRejectType lpSetIDCardRejectType;
        //识别并获取结果
        RecogIDCard lpRecogIDCard;
        RecogIDCardEX lpRecogIDCardEX;
        GetRecogResult lpGetRecogResult;
        GetResultType lpGetResultType;
        GetFieldName lpGetFieldName;
        GetSubID lpGetSubID;
        GetIDCardName lpGetIDCardName;
        //保存图片
        SaveImage lpSaveImage;
        SaveHeadImage lpSaveHeadImage;
        //其他接口
        GetVersionInfo lpGetVersionInfo;
        GetCurrentDevice lpGetCurrentDevice;
        GetDeviceSN lpGetDeviceSN;
        CheckDeviceSI lpCheckDeviceSI;
        WriteDeviceSI lpWriteDeviceSI;
        //名片接口
        RecogBusinessCard lpRecogBusinessCard;
        GetBusinessCardResultCount lpGetBusinessCardResultCount;
        GetBusinessCardResult lpGetBusinessCardResult;
        GetBusinessCardFieldName lpGetBusinessCardFieldName;
        GetBusinessCardPosition lpGetBusinessCardPosition;

        public IntPtr m_handle;//指针或句柄
        #endregion
        /// <summary>
        /// 初始化设备
        /// </summary>
        /// <returns></returns>
        public  bool InitComm()
        {
            /*
            if (m_bIsIDCardLoaded)
            {
                //textBoxDisplayResult.Text = "核心已经加载成功";
                return true;
            }
            string Str = System.AppDomain.CurrentDomain.BaseDirectory;
            DllPath = Str + @"\\ReferenceFiles\\IdReaderSdks\\THPR210x64";
            ImgHeadPath = Str;
            String strTmp = "";
            strTmp += DllPath + "\\IDCard.dll";
            
            m_handle = MyDll.LoadLibrary(@strTmp);
            //m_handle = MyDll.LoadLibraryEx(@strTmp,null,8);
            if (m_handle.ToInt64() == 0)
            {
                uint nRes = MyDll.GetLastError();                
                _logger.LogInformation($"加载IDCard.dll失败：{ nRes.ToString()} ");
                return false;
            }
            //初始化
            lpInitIDCard = (InitIDCard)MyDll.LoadFunction<InitIDCard>(m_handle, "InitIDCard");
            lpFreeIDCard = (FreeIDCard)MyDll.LoadFunction<FreeIDCard>(m_handle, "FreeIDCard");
            //检测设备
            lpCheckDeviceOnlineEx = (CheckDeviceOnlineEx)MyDll.LoadFunction<CheckDeviceOnlineEx>(m_handle, "CheckDeviceOnlineEx");
            //拍照设置
            lpAcquireImage = (AcquireImage)MyDll.LoadFunction<AcquireImage>(m_handle, "AcquireImage");
            lpSetAcquireImageType = (SetAcquireImageType)MyDll.LoadFunction<SetAcquireImageType>(m_handle, "SetAcquireImageType");
            lpSetAcquireImageResolution = (SetAcquireImageResolution)MyDll.LoadFunction<SetAcquireImageResolution>(m_handle, "SetAcquireImageResolution");
            lpSetUserDefinedImageSize = (SetUserDefinedImageSize)MyDll.LoadFunction<SetUserDefinedImageSize>(m_handle, "SetUserDefinedImageSize");
            //识别并获取结果
            lpResetIDCardID = (ResetIDCardID)MyDll.LoadFunction<ResetIDCardID>(m_handle, "ResetIDCardID");
            lpAddIDCardID = (AddIDCardID)MyDll.LoadFunction<AddIDCardID>(m_handle, "AddIDCardID");
            lpSetIDCardRejectType = (SetIDCardRejectType)MyDll.LoadFunction<SetIDCardRejectType>(m_handle, "SetIDCardRejectType");
            lpRecogIDCard = (RecogIDCard)MyDll.LoadFunction<RecogIDCard>(m_handle, "RecogIDCard");
            lpRecogIDCardEX = (RecogIDCardEX)MyDll.LoadFunction<RecogIDCardEX>(m_handle, "RecogIDCardEX");
            lpGetRecogResult = (GetRecogResult)MyDll.LoadFunction<GetRecogResult>(m_handle, "GetRecogResult");
            lpGetResultType = (GetResultType)MyDll.LoadFunction<GetResultType>(m_handle, "GetResultType");
            lpGetFieldName = (GetFieldName)MyDll.LoadFunction<GetFieldName>(m_handle, "GetFieldName");
            lpGetSubID = (GetSubID)MyDll.LoadFunction<GetSubID>(m_handle, "GetSubID");
            lpGetIDCardName = (GetIDCardName)MyDll.LoadFunction<GetIDCardName>(m_handle, "GetIDCardName");
            //保存图片
            lpSaveImage = (SaveImage)MyDll.LoadFunction<SaveImage>(m_handle, "SaveImage");
            lpSaveHeadImage = (SaveHeadImage)MyDll.LoadFunction<SaveHeadImage>(m_handle, "SaveHeadImage");
            //其他接口
            lpGetVersionInfo = (GetVersionInfo)MyDll.LoadFunction<GetVersionInfo>(m_handle, "GetVersionInfo");
            lpGetCurrentDevice = (GetCurrentDevice)MyDll.LoadFunction<GetCurrentDevice>(m_handle, "GetCurrentDevice");
            lpGetDeviceSN = (GetDeviceSN)MyDll.LoadFunction<GetDeviceSN>(m_handle, "GetDeviceSN");
            lpCheckDeviceSI = (CheckDeviceSI)MyDll.LoadFunction<CheckDeviceSI>(m_handle, "CheckDeviceSI");
            lpWriteDeviceSI = (WriteDeviceSI)MyDll.LoadFunction<WriteDeviceSI>(m_handle, "WriteDeviceSI");
            //名片接口
            lpRecogBusinessCard = (RecogBusinessCard)MyDll.LoadFunction<RecogBusinessCard>(m_handle, "RecogBusinessCard");
            lpGetBusinessCardResult = (GetBusinessCardResult)MyDll.LoadFunction<GetBusinessCardResult>(m_handle, "GetBusinessCardResult");
            lpGetBusinessCardResultCount = (GetBusinessCardResultCount)MyDll.LoadFunction<GetBusinessCardResultCount>(m_handle, "GetBusinessCardResultCount");
            lpGetBusinessCardFieldName = (GetBusinessCardFieldName)MyDll.LoadFunction<GetBusinessCardFieldName>(m_handle, "GetBusinessCardFieldName");
            lpGetBusinessCardPosition = (GetBusinessCardPosition)MyDll.LoadFunction<GetBusinessCardPosition>(m_handle, "GetBusinessCardPosition");

            if (lpInitIDCard == null || lpGetCurrentDevice == null
                || lpCheckDeviceOnlineEx == null
                || lpAcquireImage == null || lpSetAcquireImageType == null
                || lpSetAcquireImageResolution == null || lpSetUserDefinedImageSize == null
                || lpResetIDCardID == null || lpAddIDCardID == null
                || lpRecogIDCard == null || lpRecogIDCardEX == null
                || lpGetRecogResult == null
                || lpGetFieldName == null || lpGetResultType == null || lpGetFieldName == null
                || lpGetIDCardName == null || lpGetSubID == null
                || lpGetVersionInfo == null || lpGetCurrentDevice == null || lpGetDeviceSN == null
                || lpCheckDeviceSI == null || lpWriteDeviceSI == null
                || lpRecogBusinessCard == null
                || lpGetBusinessCardResult == null || lpGetBusinessCardResultCount == null
                || lpGetBusinessCardFieldName == null || lpGetBusinessCardPosition == null)
            {
                MyDll.FreeLibrary(m_handle);
                //textBoxDisplayResult.Text = "初始化识别核心失败, 错误代码：-2\r\n";
                _logger.LogInformation($"初始化识别核心失败");
                return false;
            }

            //初始化识别核心
            int nRet;
            char[] arrUserID = UserKeyID.ToCharArray();
            if (DllPath.Length != 0)
            {
                char[] arrDllPath = DllPath.ToCharArray();
                nRet = lpInitIDCard(UserKeyID, 1, DllPath);
            }
            else
            {
                nRet = lpInitIDCard(UserKeyID, 1, null);
            }
            if (nRet != 0)
            {
                //textBoxDisplayResult.Text = "初始化识别核心失败,\r\n";
                String strtmp = nRet.ToString();
                //textBoxDisplayResult.Text += "返回值：" + strtmp;
                return false;
            }
            m_bIsIDCardLoaded = true;
            _logger.LogInformation($"加载识别核心成功->THPRX64");
            */
            return true;
        }
        /// <summary>
        /// 释放设备
        /// </summary>
        /// <returns></returns>
        public bool CloseComm()
        {
            //MyDll.FreeLibrary(m_handle);
            return true;
        }
        public void StartSignalLamp()
        { 
        }
        public Person ScanContent(string operateIdType)
        {
            //_logger.LogDebug($"operateIdType={operateIdType}");
            if (!string.IsNullOrEmpty(operateIdType))
            {
                switch (operateIdType)
                {
                    case "ID_CARD":
                        CodeType = 2;
                        break;
                    case "DRIVER_LICENSE":
                        CodeType = 5;
                        break;
                    case "PASSPORT":
                        CodeType = 13;
                        break;
                }
            }
            /*else
            {
                return null;
            }
            */
            return ReadContent();
        }
        public Person ReadContent()
        {
            //lock (this)
            //{
                string Msg = string.Empty;
                Person person = null;
                int nCardType = CodeType;

                Process process = new Process();
                process.StartInfo.FileName = "THPR210scan.exe";
                process.StartInfo.Arguments = $"{CodeType}";
                process.StartInfo.UseShellExecute = true;
                process.Start();
                process.WaitForExit();
                int exitCode = process.ExitCode;
            String strRunPath = System.Windows.Forms.Application.StartupPath;
            String file = strRunPath + @"\Files\Images\Portraits\";
            //Thread.Sleep(4000);
            if (exitCode == 1)
                {
                    using (FileStream fs = new FileStream("Scaninfo.txt", FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            String line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                if (line.Length < 3)
                                {
                                    return person;
                                }
                                string[] row = line.Split(':');
                                Msg += row[row.Length - 1] + "^";
                            }
                        }
                    }
                    if (Msg.Length > 0)
                    {                    
                        string[] Info = Msg.Split('^');
                        string CardType = string.Empty;
                        string Sex = string.Empty;
                        switch (nCardType)
                        {
                            case 2:
                                CardType = "ID_CARD";
                                Sex = (Info[1] == "男") ? "MALE" : "FEMALE";
                                break;
                            case 5:
                                CardType = "DRIVER_LICENSE";
                                Sex = (Info[2] == "男") ? "MALE" : "FEMALE";
                                break;
                            case 13:
                                CardType = "PASSPORT";
                                Sex = (Info[3] == "男") ? "MALE" : "FEMALE";
                                break;
                        }
                        person = new Person();
                        person.Name = (nCardType == 2) ? Info[0] : (nCardType == 5 ? Info[1] : Info[1]);
                        person.CardType = CardType;
                        person.IdCode = (nCardType == 2) ? Info[5] : (nCardType == 5 ? Info[0] : Info[0]);
                        person.Gender = Sex;
                        person.Nation = (nCardType == 2) ? Info[2] : null;// (nCardType == 5 ? Info[1] : Info[1]); 
                        person.Birthday = null;// (nCardType == 2) ? Info[3].Replace("-","") : (nCardType == 5 ? Info[4].Replace("-", "") : Info[4].Replace("-", "")); //_idCard.Result.Birthday;
                        person.Address = null;// (nCardType == 2) ? Info[4] : (nCardType == 5 ? Info[3] : string.Empty);
                        person.Agency = null;// "";// _idCard.Result.Authority;
                        person.ExpireStart = null;// (nCardType == 2) ? null : (nCardType == 5 ? Info[5].Replace("-", "") : Info[15].Replace("-", "")); //_idCard.Result.IssueDay;
                        person.ExpireEnd = null;// (nCardType == 2) ? null : (nCardType == 5 ? null : Info[5].Replace("-", "")); //_idCard.Result.ExpityDay;
                        person.Portrait = new Bitmap(@file+Info[Info.Length-2]);
                    GC.Collect();
                    //File.Delete("Scan_Head.jpg");
                }
                    return person;
                }
                else if (exitCode == 0)
                {
                    if (CodeType == 2)
                    {
                        CodeType = 5;
                        //_logger.LogInformation($"current CodeType=2 change CodeType={CodeType}");
                    }
                    else if (CodeType == 5)
                    {
                        CodeType = 13;
                        //_logger.LogInformation($"current CodeType=5 change CodeType={CodeType}");
                    }
                    else if (CodeType == 13)
                    {
                        CodeType = 2;
                        //_logger.LogInformation($"current CodeType=13 change CodeType={CodeType}");
                    }
                    return person;
                }
                else if (exitCode == 2 || exitCode == 3)
                {
                    return person;
                }
                //_logger.LogInformation($"person={JsonConvert.SerializeObject(person)}");
                return person;
            //}
            
        }
        public bool Authenticate()
        {
            return true;
        }
    }
}
