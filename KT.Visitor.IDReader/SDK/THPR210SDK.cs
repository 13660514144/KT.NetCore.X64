using KT.Proxy.BackendApi.Enums;
using KT.Visitor.IdReader.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace KT.Visitor.IdReader.SDK
{
    public partial class THPR210SDK
    {
        private ILogger _logger;

        #region 自定义成员变量
        //照片名称，原图:{{IMAGE_NAME}}.jpg  头像：{{IMAGE_NAME}}Head.jpg  灰度图：{{IMAGE_NAME}}IR.jpg
        private static string IMAGE_NAME_BASE = "Certificate";
        public static string IMAGE_NAME = IMAGE_NAME_BASE + ".jpg";
        public static string IMAGE_NAME_HEAD = IMAGE_NAME_BASE + "Head.jpg";
        public static string IMAGE_NAME_IR = IMAGE_NAME_BASE + "IR.jpg";
        #endregion

        ///// <summary>
        ///// 初始化识别核心
        ///// </summary>
        //InitIDCard pInitIDCard;
        //GetFieldNameEx pGetFieldNameEx;
        //GetCurrentDevice pGetCurrentDevice;
        //GetVersionInfo pGetVersionInfo;
        //CheckDeviceOnline pCheckDeviceOnline;
        //ResetIDCardID pResetIDCardID;
        //AddIDCardID pAddIDCardID;

        ///// <summary>
        ///// 检测是否有证件放入或拿出
        ///// </summary>
        //DetectDocument pDetectDocument;
        //SetRecogDG pSetRecogDG;
        //SetRecogVIZ pSetRecogVIZ;
        //SetConfigByFile pSetConfigByFile;
        //SetSaveImageType pSetSaveImageType;
        //FreeIDCard pFreeIDCard;
        //GetDeviceSN pGetDeviceSN;
        //SaveImageEx pSaveImageEx;
        //AutoProcessIDCard pAutoProcessIDCard;
        //GetRecogResultEx pGetRecogResultEx;

        //SID
        delegate int SDT_OpenPort(int iPort);
        delegate int SDT_ClosePort(int iPort);
        delegate int SDT_StartFindIDCard(int iPort, ref byte pRAPDU, int iIfOpen);
        delegate int SDT_SelectIDCard(int iPort, ref byte pRAPDU, int iIfOpen);
        delegate int SDT_ReadBaseMsg(int iPort, ref byte pucCHMsg, ref int puiCHMsgLen, ref byte pucPHMsg, ref int puiPHMsgLen, int iIfOpen);
        delegate int SDT_ReadNewAppMsg(int iPort, ref byte pucAppMsg, ref int puiAppMsgLen, int iIfOpen);
        delegate int GetBmp(string filename, int nType);
        SDT_OpenPort pSDT_OpenPort;
        SDT_ClosePort pSDT_ClosePort;
        SDT_StartFindIDCard pSDT_StartFindIDCard;
        SDT_SelectIDCard pSDT_SelectIDCard;
        SDT_ReadBaseMsg pSDT_ReadBaseMsg;
        SDT_ReadNewAppMsg pSDT_ReadNewAppMsg;
        GetBmp pGetBmp;

        #region 成员变量
        //SID
        // sdtapi.dll  loaded successfully flag     
        bool m_bLoad = false;
        // text info             
        byte[] pucCHMsg = new byte[512];
        // image info    
        byte[] pucPHMsg = new byte[1024];
        // text info data length
        int puiCHMsgLen = 512;
        // image info data length  
        int puiPHMsgLen = 1024;
        // 保存打开的端口号   
        int m_nOpenPort = 0;
        // 是否是二代证
        bool m_bSID = false;

        //IdCard.dll库是否加载成功
        bool m_bIsIDCardLoaded = false;
        //芯片数据分组
        List<int> _dgroup;
        //图片保存地址
        string _saveImagePath;

        //扫描结果
        public RecognResult Result { get; set; }

        #endregion


        #region 加载DLL函数
        /// <summary>
        /// 加载sdtapi.dll函数
        /// </summary>
        /// <param name="baseDllPath">dll库文件路径</param>
        public void LoadSdtapi(string baseDllPath)
        {
            string DllPath = Path.Combine(baseDllPath, "sdtapi.dll");
            int hModuleadi = MyDll.LoadLibrary(@DllPath);
            if (hModuleadi == 0)
            {
                throw IdReaderException.Run(string.Format("加载HTPR210函数失败：{0} url:{1} ", "加载sdtapi.dll失败, 请选择dll路径", DllPath));
            }
            IntPtr intPtr = (IntPtr)MyDll.GetProcAddress(hModuleadi, "SDT_OpenPort");
            pSDT_OpenPort = (SDT_OpenPort)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(SDT_OpenPort));
            intPtr = (IntPtr)MyDll.GetProcAddress(hModuleadi, "SDT_ClosePort");
            pSDT_ClosePort = (SDT_ClosePort)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(SDT_ClosePort));
            intPtr = (IntPtr)MyDll.GetProcAddress(hModuleadi, "SDT_StartFindIDCard");
            pSDT_StartFindIDCard = (SDT_StartFindIDCard)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(SDT_StartFindIDCard));
            intPtr = (IntPtr)MyDll.GetProcAddress(hModuleadi, "SDT_SelectIDCard");
            pSDT_SelectIDCard = (SDT_SelectIDCard)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(SDT_SelectIDCard));
            intPtr = (IntPtr)MyDll.GetProcAddress(hModuleadi, "SDT_ReadBaseMsg");
            pSDT_ReadBaseMsg = (SDT_ReadBaseMsg)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(SDT_ReadBaseMsg));
            intPtr = (IntPtr)MyDll.GetProcAddress(hModuleadi, "SDT_ReadNewAppMsg");
            pSDT_ReadNewAppMsg = (SDT_ReadNewAppMsg)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(SDT_ReadNewAppMsg));

            if (pSDT_OpenPort == null
                || pSDT_ClosePort == null
                || pSDT_StartFindIDCard == null
                || pSDT_SelectIDCard == null
                || pSDT_ReadBaseMsg == null
                || pSDT_ReadNewAppMsg == null)
            {
                MyDll.FreeLibrary(hModuleadi);
                throw IdReaderException.Run(string.Format("加载HTPR210函数失败：{0} url:{1} ", "加载sdtapi.dll失败, 请选择dll路径", DllPath));
            }
            _logger?.LogDebug("THPK210初始化sdtapi.dll库成功！");
        }

        /// <summary>
        /// 加载WltRS.dll函数
        /// </summary>
        /// <param name="baseDllPath">dll库文件路径</param>
        public void LoadWltRS(string baseDllPath)
        {
            string DllPath = Path.Combine(baseDllPath, "WltRS.dll");
            int hModuleWltRS = MyDll.LoadLibrary(@DllPath);
            if (hModuleWltRS == 0)
            {
                throw IdReaderException.Run(string.Format("加载HTPR210函数失败：{0} url:{1} ", "加载WltRS.dll失败, 请选择dll路径", DllPath));
            }
            IntPtr intPtr = (IntPtr)MyDll.GetProcAddress(hModuleWltRS, "GetBmp");
            pGetBmp = (GetBmp)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(GetBmp));
            if (pGetBmp == null)
            {
                MyDll.FreeLibrary(hModuleWltRS);
                throw IdReaderException.Run(string.Format("加载HTPR210函数失败：{0} url:{1} ", "加载WltRS.dll失败, 请选择dll路径", DllPath));
            }
            _logger?.LogDebug("THPK210初始化WltRS.dll库成功！");
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="isAuto"></param>
        /// <param name="dllPath"></param> 
        public THPR210SDK(ILogger logger, string userId, string dllPath)
        {
            _logger = logger;

            //初始化默认值
            Result = new RecognResult(_logger);

            //保存头像地址
            _saveImagePath = Path.Combine(AppContext.BaseDirectory, "Files\\Images\\Portraits");
            if (!Directory.Exists(_saveImagePath))
            {
                Directory.CreateDirectory(_saveImagePath);
            }
            _logger?.LogDebug("HTPR210图片保存地址： {0} ", _saveImagePath);

            _dgroup = new List<int> { 11, 12 };

            //加载库
            LoadKernal(userId, dllPath);
        }
        public static class MyDll
        {
            [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
            public static extern int LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);
            [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
            public static extern int GetProcAddress(int hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);
            [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
            public static extern bool FreeLibrary(int hModule);
        }

        #region 加载引用类模块

        /// <summary>
        /// 初始化所有库文件
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public void LoadKernal(string userId, string baseDllPath)
        {
            //加载IDCard.dll
            var idCardResult = IDCardApi.LoadLibrary("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll");
            if (idCardResult == 0)
            {
                throw IdReaderException.Run(string.Format("HTPR210加载加IDCard.dll失败：{0} ", idCardResult.ToString()));
            }
            _logger?.LogDebug("THPK210初始化IDCard.dll库成功！");
            //LoadIDCardDll(baseDllPath);

            //初始化识别核心文件信息 
            int nType = 1;
            _logger?.LogDebug("开始初始化HTPR210识别核心： userId:{0} nType:{1} dllPath:{1} ", userId, nType, baseDllPath);
            char[] cUserId = userId.ToCharArray();
            char[] cDllPath = baseDllPath.ToCharArray();
            int initIDCardResult = IDCardApi.InitIDCard(cUserId, nType, cDllPath);
            if (initIDCardResult != 0)
            {
                throw IdReaderException.Run(string.Format("初始化HTPR210识别核心失败：Failed to initialize the recognition engine,Return Value:{0} ", initIDCardResult.ToString()));
            }
            _logger?.LogDebug("初始化HTPR210识别核心成功！");

            //加载配置文件
            string configPath = Path.Combine(baseDllPath, "IDCardConfig.ini");
            _logger?.LogDebug("加载HTPR210配置文件： url:{0} ", configPath);
            char[] cConfigPath = configPath.ToCharArray();
            int setConfigResult = IDCardApi.SetConfigByFile(cConfigPath);
            if (setConfigResult != 0)
            {
                throw IdReaderException.Run(string.Format("加载HTPR210配置文件失败：Failed to initialize the recognition engine,Return Value:{0} ", setConfigResult.ToString()));
            }
            _logger?.LogDebug("初始化HTPR210配置文件成功！");

            //加载IDCard.dll成功
            m_bIsIDCardLoaded = true;

            //加载DLL与连接设备
            LoadDLLLinkDevice(baseDllPath);

            //检测加载设备,不在线时会中断操作
            CheckDevice();

            _logger?.LogDebug("加载HTPR210库信息成功 ");
        }

        /// <summary>
        /// 加载库与连接这设备
        /// </summary>
        private void LoadDLLLinkDevice(string baseDllPath)
        {
            //已加载
            if (m_bLoad)
            {
                _logger?.LogDebug("HTPR210 dll库已加载，无须再加载！");
                return;
            }

            //加载sdtapi.dll
            LoadSdtapi(baseDllPath);

            //加载WltRS.dll
            LoadWltRS(baseDllPath);

            //端口连接
            for (int iPort = 1001; iPort < 1017; iPort = iPort + 1)
            {
                if (pSDT_OpenPort(iPort) == 0x90)
                {
                    m_nOpenPort = iPort;
                    m_bLoad = true;
                    break;
                }
            }
            if (m_bLoad != true)
            {
                throw IdReaderException.Run("THPK210初始化库与连接这设备失败！");
            }
            _logger?.LogDebug("THPK210初始化库与连接这设备成功！");
        }

        #endregion


        #region 检测设备状态
        /// <summary>
        /// 检测设备信息
        /// </summary>
        private void CheckDevice()
        {
            btnCheckDeviceOnline();
            BtnGetDeviceSN();
            BtnCurrDeviceName();
            BtnGetSDKVersion();
            _logger?.LogDebug("检测加载HTPR210设备成功！");
        }
        /// <summary>
        /// 检测设备是否在线
        /// </summary>
        private void btnCheckDeviceOnline()
        {
            //检测加载设备
            var isDeviceOnline = IDCardApi.CheckDeviceOnline();
            if (!isDeviceOnline)
            {
                throw IdReaderException.Run("HTPR210设备离线！");
            }
        }
        /// <summary>
        /// 检测设备序列号
        /// </summary>
        private void BtnGetDeviceSN()
        {
            char[] cArrSN = new char[16];
            IDCardApi.GetDeviceSN(cArrSN, 16);
            _logger?.LogDebug("HTPR210设备序列号：{0} ", new String(cArrSN));
        }

        /// <summary>
        /// 检测设备名称
        /// </summary>
        private void BtnCurrDeviceName()
        {
            char[] cArrDeviceName = new char[128];
            IDCardApi.GetCurrentDevice(cArrDeviceName, 128);
            _logger?.LogDebug("HTPR210设备名称：{0} ", new String(cArrDeviceName));
        }
        /// <summary>
        /// SDK版本号
        /// </summary>
        private void BtnGetSDKVersion()
        {
            char[] cArrVersion = new char[128];
            IDCardApi.GetVersionInfo(cArrVersion, 128);
            _logger?.LogDebug("HTPR210 SDK版本号：{0} ", new String(cArrVersion));
        }

        #endregion



        /// <summary>
        /// 扫描证件
        /// </summary>
        public void AutoClassAndRecognize()
        {
            // 检测库是否加载
            if (!m_bIsIDCardLoaded)
            {
                throw IdReaderException.Run("THR210扫描终止：库未加载！ ");
            }

            // 检测设备是否在线
            IDCardApi.CheckDeviceOnline();

            // 检测是否有证件放入或拿出
            int nRet = IDCardApi.DetectDocument();
            if (nRet != 1)
            {
                throw IdReaderException.Run(string.Format("THR210扫描终止：未有证件放入或拿出:{0} ", nRet.ToString()));
            }

            //get param
            int nDG = 0;
            //groupBox4
            foreach (var c in _dgroup)
            {
                nDG |= (1 << c);
            }

            //设置设置是否读取芯片信息
            IDCardApi.SetRecogDG(nDG);
            //设置保存图片类型
            int nSaveImage = 31;
            IDCardApi.SetSaveImageType(nSaveImage);
            //设置是否进行版面识别
            IDCardApi.SetRecogVIZ(true);

            //读取证件
            int nCardType = 1;
            nRet = IDCardApi.AutoProcessIDCard(ref nCardType);
            this.Result.CardType = nRet;
            if (nRet <= 0)
            {
                throw IdReaderException.Run(string.Format("THR210扫描终止 pAutoProcessIDCard ：nRet:{0} nCardType:{1} ", nRet, nCardType));
            }
            _logger?.LogDebug("THR210扫描开始获取内容！");

            //获取证件内容
            GetContent();
            //show DG info                 
            if (nCardType == 1)
            {
                GetDGContent();
            }
            _logger?.LogDebug("THR210扫描结束获取内容！");

            //指定图像路径 
            var strImagePath = Path.Combine(_saveImagePath, IMAGE_NAME);
            char[] imagePathChars = strImagePath.ToCharArray();
            nRet = IDCardApi.SaveImageEx(imagePathChars, nSaveImage);
            _logger?.LogDebug("THR210图片保存成功：url{0}", strImagePath);
        }
        #region 获取证件内容

        /// <summary>
        /// 识别卡
        /// </summary>
        private void GetContent()
        {
            int MAX_CH_NUM = 128;
            int nBufLen = MAX_CH_NUM * sizeof(byte);

            for (int i = 0; ; i++)
            {
                String cArrFieldValue = new String('\0', MAX_CH_NUM);
                String cArrFieldName = new String('\0', MAX_CH_NUM);
                nBufLen = MAX_CH_NUM * sizeof(byte);
                int nRet = IDCardApi.GetRecogResultEx(1, i, cArrFieldValue, ref nBufLen);
                if (nRet == 3)
                {
                    break;
                }
                nBufLen = MAX_CH_NUM * sizeof(byte);
                IDCardApi.GetFieldNameEx(1, i, cArrFieldName, ref nBufLen);

                var key = cArrFieldName.Replace("\0", "");
                var val = cArrFieldValue.Replace("\0", "");
                GetContentByCardType(this.Result.CardType, key, val);
            }

            //int MAX_CH_NUM = 128;
            //int nBufLen = MAX_CH_NUM * sizeof(byte);

            //for (int i = 0; ; i++)
            //{

            //    char[] cArrFieldValue = new char[MAX_CH_NUM];
            //    char[] cArrFieldName = new char[MAX_CH_NUM];
            //    //String cArrFieldValue = new String('\0', MAX_CH_NUM);
            //    //String cArrFieldName = new String('\0', MAX_CH_NUM);
            //    int nRet = IDCardApi.GetRecogResultEx(1, i, cArrFieldValue, ref nBufLen);
            //    if (nRet == 3)
            //    {
            //        break;
            //    }
            //    nBufLen = MAX_CH_NUM * sizeof(byte);
            //    IDCardApi.GetFieldNameEx(1, i, cArrFieldName, ref nBufLen);

            //    var key = new String(cArrFieldName).Replace("\0", "");
            //    var val = new String(cArrFieldValue).Replace("\0", "");
            //    GetContentByCardType(this.Result.CardType, key, val);
            //}

            _logger?.LogDebug("THR210卡识别成功");
        }


        private void GetDGContent()
        {
            int MAX_CH_NUM = 128;
            int nBufLen = 42000;
            for (int j = 0; ; j++)
            {
                //char[] ArrFieldValue = new char[nBufLen];
                //char[] ArrFieldName = new char[MAX_CH_NUM];
                string ArrFieldValue = new string('\0', nBufLen);
                string ArrFieldName = new string('\0', MAX_CH_NUM);
                int nResu = IDCardApi.GetRecogResultEx(0, j, ArrFieldValue, ref nBufLen);
                if (nResu == 3)
                {
                    break;
                }
                nBufLen = MAX_CH_NUM * sizeof(byte);
                IDCardApi.GetFieldNameEx(0, j, ArrFieldName, ref nBufLen);
                Console.WriteLine(ArrFieldName);
                nBufLen = 42000;

                var key = ArrFieldName.Replace("\0", "");
                var val = ArrFieldValue.Replace("\0", "");
                GetContentByCardType(this.Result.CardType, key, val);
            }
        }
        #endregion


        #region 保存图片
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="nType"></param>
        /// <param name="strPath"></param>
        private void SaveImagePath(int nType, string strPath)
        {
            // imageList1.Images.Clear();
            if ((nType & 1) == 1)
            {
                if (System.IO.File.Exists(@strPath))
                {
                    FileStream fileStream = new FileStream(@strPath, FileMode.Open, FileAccess.Read);
                    var image = Image.FromStream(fileStream);
                    //  imageList1.Images.Add(Image.FromStream(fileStream)); //ting
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
            if ((nType & 2) == 2)
            {
                string strIR = @strPath.Insert(@strPath.Length - 4, "IR");
                if (System.IO.File.Exists(strIR))
                {
                    FileStream fileStream = new FileStream(@strIR, FileMode.Open, FileAccess.Read);
                    var image = Image.FromStream(fileStream);
                    // imageList1.Images.Add(Image.FromStream(fileStream)); //ting
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
            if ((nType & 4) == 4)
            {
                string strUV = @strPath.Insert(@strPath.Length - 4, "UV");
                if (System.IO.File.Exists(strUV))
                {
                    FileStream fileStream = new FileStream(@strUV, FileMode.Open, FileAccess.Read);
                    var image = Image.FromStream(fileStream);
                    //  imageList1.Images.Add(Image.FromStream(fileStream)); //ting
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
            if ((nType & 8) == 8)
            {
                string strHead = strPath.Insert(strPath.Length - 4, "Head");
                if (System.IO.File.Exists(strHead))
                {
                    FileStream fileStream = new FileStream(@strHead, FileMode.Open, FileAccess.Read);
                    var image = Image.FromStream(fileStream);
                    //  imageList1.Images.Add(Image.FromStream(fileStream)); //ting
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
            if ((nType & 16) == 16)
            {
                string strHeadEC = strPath.Insert(strPath.Length - 4, "HeadEC");
                if (System.IO.File.Exists(strHeadEC))
                {
                    FileStream fileStream = new FileStream(@strHeadEC, FileMode.Open, FileAccess.Read);
                    var image = Image.FromStream(fileStream);
                    //   imageList1.Images.Add(Image.FromStream(fileStream)); //ting
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
        }
        #endregion

        #region The Following Is SID
        /*-------------------------- The Following Is SID.-----------------------------*/
        // 读卡
        /* 0：成功
          -1: 加载sdtapi.dll失败
          -2：SDT_ReadNewAppMsg失败
          -3：SDT_StartFindIDCard失败
          -4：SDT_SelectIDCard失败
          -5: SDT_ReadBaseMsg失败*/

        /// <summary>
        /// 读取证件，支持 外国人永久居留证、二代身份证、港澳台居民居住证
        /// </summary>
        public void ReadCard()
        {
            if (m_bLoad != true)
            {
                throw IdReaderException.Run("THPR210读取卡：加载sdtapi.dll失败！");
            }
            byte[] pucAppMsg = new byte[320];
            int len = 320;
            int nRet = pSDT_ReadNewAppMsg(m_nOpenPort, ref pucAppMsg[0], ref len, 0);
            if (nRet == 0x91 || nRet == 0x90)
            {
                throw IdReaderException.Run("THPR210读取卡：SDT_ReadNewAppMsg失败！");
            }
            byte[] pRAPDU = new byte[30];
            nRet = pSDT_StartFindIDCard(m_nOpenPort, ref pRAPDU[0], 0);
            if (nRet != 0x9F)
            {
                throw IdReaderException.Run("THPR210读取卡：SDT_StartFindIDCard失败！");
            }
            if (pSDT_SelectIDCard(m_nOpenPort, ref pRAPDU[0], 0) != 0x90)
            {
                throw IdReaderException.Run("THPR210读取卡：SDT_SelectIDCard失败！");
            }
            nRet = pSDT_ReadBaseMsg(m_nOpenPort, ref pucCHMsg[0], ref puiCHMsgLen, ref pucPHMsg[0], ref puiPHMsgLen, 0);
            if (nRet != 0x90)
            {
                throw IdReaderException.Run("THPR210读取卡：SDT_ReadBaseMsg失败！");
            }

            string strSID = Encoding.Unicode.GetString(pucCHMsg, 248, 2);
            //外国人永久居留证
            if (strSID == "I")
            {
                m_bSID = false;
                ShowCHMsg_Prp();
            }
            //港澳台居民居住证
            else if (strSID == "J")
            {
                m_bSID = false;
                ShowCHMsg_Rrp();
            }
            //二代身份证
            else
            {
                m_bSID = true;
                ShowCHMsg_SID();
            }
            //显示图像//指定图像路径  
            char[] cImagePath = _saveImagePath.ToCharArray();
            string strImagePath = new string(cImagePath);
            SavePhoto(strImagePath, 2);

            _logger?.LogDebug("THPR210读取卡成功！");
        }

        /// <summary>
        /// 显示外国人永久居住证信息
        /// </summary>
        void ShowCHMsg_Prp()
        {
            Result.PaperName = "外国人永久居留证";
            Result.Name = GetName();
            Result.Gender = GetSex();
            Result.IDCode = GetIDCode();
            Result.PeopleCode = GetPeople();
            Result.PeopleChineseName = GetPeopleChineseName();
            Result.IssueDay = GetIssueDay();
            Result.Birthday = GetBirthday();
            Result.ExpityDay = GetExpityDay();
            Result.CardVersion = GetCardVersion();
            Result.Authority = GetAuthority();
            Result.Reverse = GetReverse();
            Result.CardType = 33;
        }

        /// <summary>
        /// 显示二代证信息
        /// </summary>
        void ShowCHMsg_SID()
        {
            Result.PaperName = "二代身份证";
            Result.Name = GetName();
            Result.Gender = GetSex();
            Result.IDCode = GetIDCode();
            Result.PeopleCode = GetPeople();
            Result.PeopleChineseName = GetPeopleChineseName();
            Result.Address = GetAddress();
            Result.IssueDay = GetIssueDay();
            Result.Birthday = GetBirthday();
            Result.ExpityDay = GetExpityDay();
            Result.CardVersion = GetCardVersion();
            Result.Authority = GetAuthority();
            Result.Reverse = GetReverse();
            Result.CardType = 2;
            if (GetFingerprintInfo().Length > 0)
            {
                Result.IsFingerprintInfo = true;
            }
            else
            {
                Result.IsFingerprintInfo = false;
            }
        }

        /// <summary>
        /// 显示港澳台居民居住证信息
        /// </summary>
        void ShowCHMsg_Rrp()
        {
            Result.PaperName = "港澳台居民居住证";
            Result.Name = GetName();
            Result.Gender = GetSex();
            Result.IDCode = GetIDCode();
            Result.PeopleCode = GetPeople();
            Result.PeopleChineseName = GetPeopleChineseName();
            Result.Address = GetAddress();
            Result.IssueDay = GetIssueDay();
            Result.Birthday = GetBirthday();
            Result.ExpityDay = GetExpityDay();
            Result.CardVersion = GetCardVersion();
            Result.Authority = GetAuthority();
            Result.Reverse = GetReverse(32, 4);
            Result.PassNum = GetPassNum();
            Result.IssueNum = GetIssueNum();
            Result.Reverse1 = GetReverse(242, 6);
            Result.Reverse2 = GetReverse(250, 6);
            Result.CardType = 31;
            if (GetFingerprintInfo().Length > 0)
            {
                Result.IsFingerprintInfo = true;
            }
            else
            {
                Result.IsFingerprintInfo = false;
            }
        }
        // 获取名字
        string GetName()
        {
            if (puiCHMsgLen == 0)
            {
                return "";
            }
            string str = "";
            if (m_bSID)
            {
                str = Encoding.Unicode.GetString(pucCHMsg, 0, 30);
            }
            else if (Encoding.Unicode.GetString(pucCHMsg, 248, 2) == "I")
            {
                str = Encoding.Unicode.GetString(pucCHMsg, 0, 120);
            }
            else
            {
                str = Encoding.Unicode.GetString(pucCHMsg, 0, 30);
            }
            str.TrimEnd();
            return str;
        }
        // 获取外国人中文姓名
        string GetPeopleChineseName()
        {
            if (puiCHMsgLen == 0)
            {
                return " ";
            }
            string str = Encoding.Unicode.GetString(pucCHMsg, 158, 30);
            return str;
        }
        // 获取证件版本号
        string GetCardVersion()
        {
            if (puiCHMsgLen == 0)
            {
                return " ";
            }
            string str = Encoding.Unicode.GetString(pucCHMsg, 236, 4);
            return str;
        }
        // 获取预留项
        string GetReverse()
        {
            if (puiCHMsgLen == 0)
            {
                return " ";
            }
            string str = Encoding.Unicode.GetString(pucCHMsg, 250, 6);
            return str;
        }
        string GetReverse(int nPosition, int nVal)
        {
            if (puiCHMsgLen == 0)
            {
                return " ";
            }
            string str = Encoding.Unicode.GetString(pucCHMsg, nPosition, nVal);
            return str;
        }
        // 获取性别
        string GetSex()
        {
            if (puiCHMsgLen == 0)
            {
                return " ";
            }
            byte sex = 0;
            if (Encoding.Unicode.GetString(pucCHMsg, 248, 2) == "I")
            {
                sex = pucCHMsg[120];
            }
            else
            {
                sex = pucCHMsg[30];
            }
            if (sex == '1')
            {
                return "男";
            }
            else
            {
                return "女";
            }
        }
        // 获取民族
        string GetPeople()
        {
            if (puiCHMsgLen == 0)
            {
                return " ";
            }
            if (m_bSID)
            {
                string str = Encoding.Unicode.GetString(pucCHMsg, 32, 4);
                switch (str)
                {
                    case "01": return "汉";
                    case "02": return "蒙古";
                    case "03": return "回";
                    case "04": return "藏";
                    case "05": return "维吾尔";
                    case "06": return "苗";
                    case "07": return "彝";
                    case "08": return "壮";
                    case "09": return "布依";
                    case "10": return "朝鲜";
                    case "11": return "满";
                    case "12": return "侗";
                    case "13": return "瑶";
                    case "14": return "白";
                    case "15": return "土家";
                    case "16": return "哈尼";
                    case "17": return "哈萨克";
                    case "18": return " 傣";
                    case "19": return " 黎";
                    case "20": return " 傈僳";
                    case "21": return " 佤";
                    case "22": return " 畲";
                    case "23": return " 高山";
                    case "24": return " 拉祜";
                    case "25": return " 水";
                    case "26": return " 东乡";
                    case "27": return " 纳西";
                    case "28": return " 景颇";
                    case "29": return " 柯尔克孜";
                    case "30": return " 土";
                    case "31": return " 达斡尔";
                    case "32": return " 仫佬";
                    case "33": return "羌";
                    case "34": return "布朗";
                    case "35": return "撒拉";
                    case "36": return "毛南";
                    case "37": return "仡佬";
                    case "38": return "锡伯";
                    case "39": return "阿昌";
                    case "40": return "普米";
                    case "41": return "塔吉克";
                    case "42": return "怒";
                    case "43": return "乌孜别克";
                    case "44": return "俄罗斯";
                    case "45": return "鄂温克";
                    case "46": return "德昂";
                    case "47": return "保安";
                    case "48": return "裕固";
                    case "49": return "京";
                    case "50": return "塔塔尔";
                    case "51": return "独龙";
                    case "52": return "鄂伦春";
                    case "53": return "赫哲";
                    case "54": return "门巴";
                    case "55": return "珞巴";
                    case "56": return "基诺";
                    case "97": return "其他";
                    case "98": return "外国血统中国籍人士";
                    default: return "";
                }
            }
            else
            {
                string str = Encoding.Unicode.GetString(pucCHMsg, 152, 6);
                str.TrimEnd();
                return str;
            }
        }
        // 获取出生日期
        string GetBirthday()
        {
            if (puiCHMsgLen == 0)
            {
                return " ";
            }
            string str = "";
            if (m_bSID)
            {
                str = Encoding.Unicode.GetString(pucCHMsg, 36, 16);
            }
            else if (Encoding.Unicode.GetString(pucCHMsg, 248, 2) == "I")
            {
                str = Encoding.Unicode.GetString(pucCHMsg, 220, 16);
            }
            else
            {
                str = Encoding.Unicode.GetString(pucCHMsg, 36, 16);
            }
            str.TrimEnd();
            return str;
        }
        // 获取住址
        string GetAddress()
        {
            if (puiCHMsgLen == 0)
            {
                return " ";
            }
            string str = Encoding.Unicode.GetString(pucCHMsg, 52, 70);
            return str;
        }
        // 获取签发机关
        string GetAuthority()
        {
            if (puiCHMsgLen == 0)
            {
                return " ";
            }
            string str = "";
            if (m_bSID)
            {
                str = Encoding.Unicode.GetString(pucCHMsg, 158, 30);
            }
            else if (Encoding.Unicode.GetString(pucCHMsg, 248, 2) == "I")
            {
                str = Encoding.Unicode.GetString(pucCHMsg, 240, 8);
            }
            else
            {
                str = Encoding.Unicode.GetString(pucCHMsg, 158, 30);
            }
            str.TrimEnd();
            return str;
        }
        // 身份证号
        string GetIDCode()
        {
            if (puiCHMsgLen == 0)
            {
                return " ";
            }
            string str = "";
            if (m_bSID)
            {
                str = Encoding.Unicode.GetString(pucCHMsg, 122, 36);
            }
            else if (Encoding.Unicode.GetString(pucCHMsg, 248, 2) == "I")
            {
                str = Encoding.Unicode.GetString(pucCHMsg, 122, 30);
            }
            else
            {
                str = Encoding.Unicode.GetString(pucCHMsg, 122, 36);
            }
            str.TrimEnd();
            return str;
        }
        // 证件有效期起始日期
        string GetIssueDay()
        {
            if (puiCHMsgLen == 0)
            {
                return " ";
            }
            string str = Encoding.Unicode.GetString(pucCHMsg, 188, 16);
            return str;
        }
        // 证件有效期终止日期
        string GetExpityDay()
        {
            if (puiCHMsgLen == 0)
            {
                return " ";
            }
            string str = Encoding.Unicode.GetString(pucCHMsg, 204, 16);
            return str;
        }
        //获取通行证号码
        string GetPassNum()
        {
            if (0 == puiCHMsgLen)
            {
                return "";
            }
            string str = Encoding.Unicode.GetString(pucCHMsg, 220, 18);
            return str;
        }
        //获取签发次数
        string GetIssueNum()
        {
            if (0 == puiCHMsgLen)
            {
                return "";
            }
            string str = Encoding.Unicode.GetString(pucCHMsg, 238, 4);
            return str;
        }
        //获取指纹信息
        string GetFingerprintInfo()
        {
            string str = "";
            if (puiPHMsgLen != 0)
            {
                str = "是";
            }
            else
            {
                str = "否";
            }
            return str;
        }
        // 保存头像
        int SavePhoto(string retFileName, int nType)
        {
            string savepath = Path.Combine(retFileName, "head.wlt");
            FileStream fs;
            fs = new FileStream(savepath, FileMode.Create, FileAccess.ReadWrite);
            fs.Write(pucPHMsg, 0, pucPHMsg.Length);
            fs.Close();
            pGetBmp(savepath, 2);
            return 0;
        }
        #endregion

        /// <summary>
        /// 释放资源
        /// </summary>
        public void FreeKenle()
        {
            if (m_bIsIDCardLoaded)
            {
                m_bIsIDCardLoaded = false;
                IDCardApi.FreeIDCard();
            }
        }

        ///// <summary>
        ///// 获取证件的字段
        ///// </summary>
        ///// <param name="cardType"></param>
        ///// <param name="key"></param>
        ///// <param name="val"></param>
        //private void GetContentByCardType(string cardType, string key, string val)
        //{


        //switch (cardType)
        //{
        //    case "2":  //身份证
        //        switch (key)
        //        {
        //            case "公民身份号码":
        //                this.Result.IDCode = val;
        //                break;
        //            case "姓名":
        //                this.Result.Name = val;
        //                break;
        //            case "性别":
        //                this.Result.Gender = val;
        //                break;
        //            case "住址":
        //                this.Result.Address = val;
        //                break;
        //            case "出生":
        //                this.Result.Birthday = val;
        //                break;
        //            case "签发日期":
        //                this.Result.IssueDay = val;
        //                break;
        //            case "民族":
        //                this.Result.Nation = val;
        //                break;
        //            case "有效期至":
        //                this.Result.IssueDay = val;
        //                break;

        //            default:
        //                break;
        //        }
        //        break;
        //    case "5": //驾照
        //        switch (key)
        //        {
        //            case "证号":
        //                this.Result.IDCode = val;
        //                break;
        //            case "姓名":
        //                this.Result.Name = val;
        //                break;
        //            case "性别":
        //                this.Result.Gender = val;
        //                break;
        //            case "住址":
        //                this.Result.Address = val;
        //                break;
        //            case "出生日期":
        //                this.Result.Birthday = val;
        //                break;
        //            case "初始领证日期":
        //                this.Result.IssueDay = val;
        //                break;
        //            case "准驾车型":
        //                this.Result.Reverse = val;
        //                break;
        //            case "有效起始日期":
        //                this.Result.IssueDay = val;
        //                break;
        //            case "有效截至日期":
        //                this.Result.ExpityDay = val;
        //                break;
        //            default:
        //                break;
        //        }
        //        break;
        //    case "13": //护照
        //        switch (key)
        //        {
        //            case "护照号码":
        //                this.Result.IDCode = val;
        //                break;
        //            case "本国姓名":
        //                this.Result.Name = val;
        //                break;
        //            case "性别":
        //                this.Result.Gender = val;
        //                break;
        //            case "住址":
        //                this.Result.Address = val;
        //                break;
        //            case "出生日期":
        //                this.Result.Birthday = val;
        //                break;
        //            case "初始领证日期":
        //                this.Result.IssueDay = val;
        //                break;
        //            case "签发国代码":
        //                this.Result.PeopleCode = val;
        //                break;
        //            case "英文姓名":
        //                this.Result.Reverse = val;
        //                break;
        //            case "有效期至":
        //                this.Result.ExpityDay = val;
        //                break;
        //            default:
        //                break;
        //        }
        //        break;
        //    case "9": //港澳通行证 2005
        //        switch (key)
        //        {
        //            case "证件号码":
        //                this.Result.IDCode = val;
        //                break;
        //            case "中文姓名":
        //                this.Result.Name = val;
        //                break;
        //            case "性别":
        //                this.Result.Gender = val;
        //                break;
        //            case "出生地":
        //                this.Result.Address = val;
        //                break;
        //            case "出生日期":
        //                this.Result.Birthday = val;
        //                break;
        //            case "签发日期":
        //                this.Result.IssueDay = val;
        //                break;
        //            case "持证人国籍代码":
        //                this.Result.Reverse = val;
        //                break;
        //            case "有效起始日期":
        //                this.Result.IssueDay = val;
        //                break;
        //            case "有效期至":
        //                this.Result.ExpityDay = val;
        //                break;
        //            default:
        //                break;
        //        }
        //        break;
        //    case "14": //港澳居民来往内地通行证
        //        switch (key)
        //        {
        //            case "证件号码":
        //                this.Result.IDCode = val;
        //                break;
        //            case "中文姓名":
        //                this.Result.Name = val;
        //                break;
        //            case "性别":
        //                this.Result.Gender = val;
        //                break;
        //            case "住址":
        //                this.Result.Address = val;
        //                break;
        //            case "出生日期":
        //                this.Result.Birthday = val;
        //                break;
        //            case "签发日期":
        //                this.Result.IssueDay = val;
        //                break;
        //            case "港澳证件号码":
        //                this.Result.Reverse = val;
        //                break;
        //            case "有效起始日期":
        //                this.Result.IssueDay = val;
        //                break;
        //            case "有效期限":
        //                this.Result.ExpityDay = val;
        //                break;
        //            default:
        //                break;
        //        }
        //        break;
        //    case "22": //港澳通行证 2014
        //        switch (key)
        //        {
        //            case "证件号码":
        //                this.Result.IDCode = val;
        //                break;
        //            case "中文姓名":
        //                this.Result.Name = val;
        //                break;
        //            case "性别":
        //                this.Result.Gender = val;
        //                break;
        //            case "签发地点":
        //                this.Result.Address = val;
        //                break;
        //            case "出生日期":
        //                this.Result.Birthday = val;
        //                break;
        //            case "初始领证日期":
        //                this.Result.IssueDay = val;
        //                break;
        //            case "准驾车型":
        //                this.Result.Reverse = val;
        //                break;
        //            case "有效起始日期":
        //                this.Result.IssueDay = val;
        //                break;
        //            case "有效期限":
        //                this.Result.ExpityDay = val;
        //                break;
        //            default:
        //                break;
        //        }
        //        break;
        //    default:
        //        break;
        //}
        //}


        /// <summary>
        /// 证件获取结果
        /// </summary>
        public class RecognResult
        {
            private ILogger _logger;
            public RecognResult(ILogger logger)
            {
                _logger = logger;
            }

            /// <summary>
            /// 获取照片，根据图片类型获取指定地址路径
            /// </summary>
            /// <param name="cardType">证件类型身份证与其它证件不同</param>
            /// <returns></returns>
            public Image HeadImg(string cardType)
            {
                Image image = null;
                string path = Path.Combine(AppContext.BaseDirectory, "Files\\Images\\Portraits");

                var defaultPath = string.Empty;

                if (cardType == CertificateTypeEnum.ID_CARD.Value)
                {
                    //身份证头像
                    defaultPath = Path.Combine(path, "head.bmp");
                }
                else
                {
                    defaultPath = Path.Combine(path, IMAGE_NAME_HEAD);
                }
                if (File.Exists(defaultPath))
                {
                    _logger?.LogDebug("获取证件头像：cardType:{0} url:{1} ", cardType, defaultPath);
                    image = ImageHelper.ReadImageFile(defaultPath);
                }
                else
                {
                    var path1 = Path.Combine(path, "head.bmp");
                    _logger?.LogDebug("获取证件头像：cardType:{0} url:{1} ", cardType, path1);
                    if (File.Exists(path1))
                    {
                        image = ImageHelper.ReadImageFile(path1);
                    }
                    else
                    {
                        var path2 = Path.Combine(path, IMAGE_NAME_HEAD);
                        _logger?.LogDebug("获取证件头像：cardType:{0} url:{1} ", cardType, path2);
                        if (File.Exists(path2))
                        {
                            image = ImageHelper.ReadImageFile(path2);
                        }
                    }
                }
                return image;
            }


            public string PaperName { get; set; }
            public string Name { get; set; }
            public string Gender { get; set; }
            public string IDCode { get; set; }
            public string PeopleCode { get; set; }
            public string PeopleChineseName { get; set; }
            /// <summary>
            /// 证件签发日期
            /// </summary>
            public string IssueDay { get; set; }
            /// <summary>
            /// 证件中止日期
            /// </summary>
            public string ExpityDay { get; set; }
            public string Birthday { get; set; }
            /// <summary>
            /// 证件版本号
            /// </summary>
            public string CardVersion { get; set; }
            /// <summary>
            /// 当次申请受理机关代码
            /// </summary>
            public string Authority { get; set; }
            /// <summary>
            /// 识别的证件类型
            /// </summary>
            public int CardType { get; set; }
            /// <summary>
            /// 预留项
            /// </summary>
            public string Reverse { get; set; }
            public string Address { get; set; }
            /// <summary>
            /// 是否有指纹信息
            /// </summary>
            public bool IsFingerprintInfo { get; set; }
            /// <summary>
            /// 通行证号码
            /// </summary>
            public string PassNum { get; set; }
            /// <summary>
            /// 签发次数
            /// </summary>
            public string IssueNum { get; set; }
            public string Reverse1 { get; set; }
            public string Reverse2 { get; set; }
            public string Nation { get; set; }
        }
    }
}
