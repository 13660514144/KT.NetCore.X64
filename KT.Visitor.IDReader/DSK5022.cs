using IDevices;
using KT.Common.Core.Utils;
using KT.Proxy.BackendApi.Enums;
using KT.Visitor.IdReader.Common;
using KT.Visitor.IdReader.SDK;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace KT.Visitor.IdReader
{
    public class DSK5022 : IDevice
    {
        private ILogger _logger;
        public DSK5022(ILogger logger)
        {
            _logger = logger;
        }

        public ReaderTypeEnum ReaderType => ReaderTypeEnum.DSK5022;
        public Action<object> ResultCallBack { get; set; }

        public int UserID { get; private set; }
        private DSK5022SDK.USB_SDK_USER_LOGIN_INFO StruCurUsbLoginInfo = new DSK5022SDK.USB_SDK_USER_LOGIN_INFO();

        private DSK5022SDK ProcessChineseCard = new DSK5022SDK();

        public bool InitComm()
        {
            var result = DSK5022SDK.USB_SDK_Init();
            if (result)
            {
                //写日志
                var logUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "hikvision");
                IntPtr logPathPtr = Marshal.StringToHGlobalAnsi(logUrl);
                DSK5022SDK.USB_SDK_SetLogToFile(3, logPathPtr, false);
            }
            return result;
        }

        public void StartSignalLamp()
        {
        }

        public bool Authenticate()
        {
            //枚举设备 
            var m_OnEnumDeviceCallBack = new DSK5022SDK.EnumDeviceCallBack(OnEnumDeviceCallBack);
            var result = DSK5022SDK.USB_SDK_EnumDevice(m_OnEnumDeviceCallBack, IntPtr.Zero);
            return result;
        }

        /// <summary>
        /// 回调函数用于在空间框中显示遍历到的设备的信息
        /// 枚举设备的回调函数
        /// </summary>
        /// <param name="pDevceInfo"></param>
        /// <param name="pUser"></param>
        /// <returns></returns>
        public void OnEnumDeviceCallBack(ref DSK5022SDK.USB_SDK_DEVICE_INFO pDevceInfo, IntPtr pUser)
        {
            var copyDeviceInfo = new DSK5022SDK.USB_SDK_DEVICE_INFO();
            copyDeviceInfo = pDevceInfo;
            copyDeviceInfo.dwSize = (uint)Marshal.SizeOf(copyDeviceInfo);//结构体本身的大小

            _logger.LogInformation($"Hikvision device :{JsonConvert.SerializeObject(copyDeviceInfo, JsonUtil.JsonPrintSettings)} ");
            ResultCallBack?.Invoke(JsonConvert.SerializeObject(copyDeviceInfo, JsonUtil.JsonSettings));
            if (copyDeviceInfo.szManufacturer != "HIKVISION")
            {
                return;
            }

            StruCurUsbLoginInfo.dwSize = (uint)Marshal.SizeOf(StruCurUsbLoginInfo);
            StruCurUsbLoginInfo.dwTimeout = 5000;//登录超时时间是5秒
            StruCurUsbLoginInfo.dwVID = copyDeviceInfo.dwVID;
            StruCurUsbLoginInfo.dwPID = copyDeviceInfo.dwPID;
            StruCurUsbLoginInfo.szSerialNumber = copyDeviceInfo.szSerialNumber;
            StruCurUsbLoginInfo.szUserName = "admin";
            StruCurUsbLoginInfo.szPassword = "12345";

            DSK5022SDK.USB_SDK_DEVICE_REG_RES StruDeviceRegRes = new DSK5022SDK.USB_SDK_DEVICE_REG_RES();
            StruDeviceRegRes.dwSize = (uint)Marshal.SizeOf(StruDeviceRegRes);
            int UserIDTemp = UserID;
            UserID = DSK5022SDK.USB_SDK_Login(ref StruCurUsbLoginInfo, ref StruDeviceRegRes);
            if (UserID == DSK5022SDK.INVALID_USER_ID)
            {
                ShowErrorMsg();
                //为了解决重复登录时的问题，但是这次只考虑了只能登录一个设备，两个设备同时能登录的话，ID会覆盖得继续解决ID的问题
                UserID = UserIDTemp;
            }
            else
            {
                _logger.LogInformation($"设备登录成功：{UserID} ");
            }
        }

        /// <summary>
        /// 转换USB_SDK_GetErrorMsg的结果成字符串显示到日志表格中
        /// </summary>
        public void ShowErrorMsg()
        {
            try
            {
                uint Error = DSK5022SDK.USB_SDK_GetLastError();
                IntPtr res = DSK5022SDK.USB_SDK_GetErrorMsg(Error);
                string sRes = Marshal.PtrToStringAnsi(res);
                _logger.LogError($"证件阅读器错误：{sRes} ");
            }
            catch
            {
                _logger.LogError($"证件阅读器错误信息读取失败：Fail To Show USB Error Message in Log Information");
            }
        }

        //private void LoginDevice(DSK5022SDK.USB_SDK_DEVICE_INFO device)
        //{
        //    _logger.LogInformation($"Hikvision device :{JsonConvert.SerializeObject(device, JsonUtil.JsonPrintSettings)} ");
        //    ResultCallBack?.Invoke(JsonConvert.SerializeObject(device, JsonUtil.JsonSettings));
        //    if (device.szManufacturer == "HIKVISION")
        //    {
        //        var userLoginInfo = new DSK5022SDK.USB_SDK_USER_LOGIN_INFO();
        //        userLoginInfo.Init();
        //        userLoginInfo.dwSize = (uint)Marshal.SizeOf(userLoginInfo);
        //        userLoginInfo.dwTimeout = 10 * 1000;
        //        userLoginInfo.dwVID = device.dwVID;
        //        userLoginInfo.dwPID = device.dwPID;
        //        userLoginInfo.szUserName = "admin";
        //        userLoginInfo.szPassword = "12345";
        //        userLoginInfo.szSerialNumber = device.szSerialNumber;

        //        var deviceRegRes = new DSK5022SDK.USB_SDK_DEVICE_REG_RES();
        //        deviceRegRes.Init();
        //        deviceRegRes.dwSize = (uint)Marshal.SizeOf(deviceRegRes);

        //        _logger.LogInformation($"Hikvision userLoginInfo :{JsonConvert.SerializeObject(userLoginInfo, JsonUtil.JsonPrintSettings)} ");
        //        _logger.LogInformation($"Hikvision deviceRegRes :{JsonConvert.SerializeObject(deviceRegRes, JsonUtil.JsonPrintSettings)} ");

        //        _userId = DSK5022SDK.USB_SDK_Login(ref userLoginInfo, ref deviceRegRes);

        //        _logger.LogInformation($"Hikvision logined :userId{_userId} ");
        //        _logger.LogInformation($"Hikvision userLoginInfo :{JsonConvert.SerializeObject(userLoginInfo, JsonUtil.JsonPrintSettings)} ");
        //        _logger.LogInformation($"Hikvision deviceRegRes :{JsonConvert.SerializeObject(deviceRegRes, JsonUtil.JsonPrintSettings)} ");

        //        if (_userId < 0)
        //        {
        //            var error = DSK5022SDK.USB_SDK_GetLastError();
        //            _logger.LogError($"Hikvision login error :userId{_userId} error:{error} ");

        //            ResultCallBack?.Invoke($"Hikvision login error :userId{_userId} error:{error} ");
        //        }
        //    }
        //}

        public bool CloseComm()
        {
            return DSK5022SDK.USB_SDK_Logout(UserID);
        }

        public Person ReadContent()
        {
            Person result = null;

            DSK5022SDK.USB_SDK_CERTIFICATE_INFO struCertificateInfo = new DSK5022SDK.USB_SDK_CERTIFICATE_INFO();
            struCertificateInfo.dwSize = (uint)Marshal.SizeOf(struCertificateInfo);

            DSK5022SDK.USB_CONFIG_OUTPUT_INFO struConfigOutputInfo = new DSK5022SDK.USB_CONFIG_OUTPUT_INFO();
            struConfigOutputInfo.dwOutBufferSize = struCertificateInfo.dwSize;
            IntPtr ptrstruCertificateInfo = Marshal.AllocHGlobal((int)struCertificateInfo.dwSize);
            Marshal.StructureToPtr(struCertificateInfo, ptrstruCertificateInfo, false);
            struConfigOutputInfo.lpOutBuffer = ptrstruCertificateInfo;

            DSK5022SDK.USB_CONFIG_INPUT_INFO strConfigInputConfig = new DSK5022SDK.USB_CONFIG_INPUT_INFO();
            if (DSK5022SDK.USB_SDK_GetDeviceConfig(UserID, DSK5022SDK.USB_SDK_GET_CERTIFICATE_INFO, ref strConfigInputConfig, ref struConfigOutputInfo))
            {
                struCertificateInfo = (DSK5022SDK.USB_SDK_CERTIFICATE_INFO)Marshal.PtrToStructure(struConfigOutputInfo.lpOutBuffer, typeof(DSK5022SDK.USB_SDK_CERTIFICATE_INFO));

                ProcessChineseCard.CertificateInfo = struCertificateInfo;
                result = ProcessChineseCard.ReadCertificateInfo();
                result.CardType = CertificateTypeEnum.ID_CARD.Value;

                Marshal.FreeHGlobal(ptrstruCertificateInfo);
            }
            else
            {
                Marshal.FreeHGlobal(ptrstruCertificateInfo);
                _logger.LogError("Fail to Read CardInfo", "ERROR");
            }

            _logger.LogInformation($"用户信息：{JsonConvert.SerializeObject(result, JsonUtil.JsonPrintSettings)} ");

            return result;

            //var struCertificateInfo = new DSK5022SDK.USB_SDK_CERTIFICATE_INFO();
            //struCertificateInfo.dwSize = (uint)Marshal.SizeOf(struCertificateInfo);

            //var struConfigOutputInfo = new DSK5022SDK.USB_CONFIG_OUTPUT_INFO();
            //struConfigOutputInfo.dwOutBufferSize = (uint)Marshal.SizeOf(struCertificateInfo);

            //IntPtr pStruCertificateInfo = Marshal.AllocHGlobal(Marshal.SizeOf<DSK5022SDK.USB_SDK_CERTIFICATE_INFO>());
            //Marshal.StructureToPtr(struCertificateInfo, pStruCertificateInfo, false);
            //struConfigOutputInfo.lpOutBuffer = pStruCertificateInfo;

            ////IntPtr pStruConfigOutputInfo = Marshal.AllocHGlobal(Marshal.SizeOf<CHCUsbSDK.TagUSB_CONFIG_OUTPUT_INFO>());
            ////Marshal.StructureToPtr(struConfigOutputInfo, pStruConfigOutputInfo, false);

            //var pInputInfo = new DSK5022SDK.USB_CONFIG_INPUT_INFO();

            //var result = DSK5022SDK.USB_SDK_GetDeviceConfig(_userId, DSK5022SDK.USB_SDK_GET_CERTIFICATE_INFO, ref pInputInfo, ref struConfigOutputInfo);

            //if (!result)
            //{
            //    var error = DSK5022SDK.USB_SDK_GetLastError();
            //    var message = DSK5022SDK.USB_SDK_GetErrorMsg(error);
            //    _logger.LogError($"读取身份证错误：error:{error} message:{message} ");
            //}

            //_logger.LogInformation($"身份证信息：{struCertificateInfo.byWordInfo} ");

            //return new Person();
        }

        public Person ScanContent(string operateIdType)
        {
            throw new NotImplementedException();
        }
    }
}
