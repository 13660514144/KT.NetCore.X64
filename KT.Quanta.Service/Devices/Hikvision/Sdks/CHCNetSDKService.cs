using KT.Common.Core.Enums;
using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Hikvision.Models;
using KT.Quanta.Service.Devices.Hikvision.Sdks.Models;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace KT.Quanta.Service.Devices.Hikvision.Sdks
{
    public class CHCNetSDKService
    {
        private ILogger _logger;

        //是否初始化，初始化只操作一次，所以设置成静态
        public bool _isInit = false;

        public Func<PassRecordModel, Task> ExecuteUploadPassRecordAsync;

        private const uint XML_ABILITY_OUT_LEN = 3 * 1024 * 1024;
        //private CHCNetSDKCard.NET_DVR_SETUPALARM_PARAM struSetupAlarmParam;
        private CHCNetSDKEvent.EXCEPYIONCALLBACK m_fExceptionCB;
        private CHCNetSDKEvent.MSGCallBack_V31 m_falarmData_V31;
        private int m_lAlarmHandle;
        private CfgHandleModel _cfgHandle;

        public CHCNetSDKService()
        {
            _cfgHandle = new CfgHandleModel();
        }

        public void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        public Task<bool> ExecuteInitAsync()
        {
            if (_isInit)
            {
                return Task.FromResult(true);
            }
            //初始化Sdk
            if (CHCNetSDKCard.NET_DVR_Init() == false)
            {
                _logger.LogError($"userId:?? Hikvision Error:SDK NET_DVR_Init error!");
                return Task.FromResult(false);
            }

            //保存SDK日志 To save the SDK log
            var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            CHCNetSDKCard.NET_DVR_SetLogToFile(3, logPath, false);

            //设置透传报警信息类型
            CHCNetSDKEvent.NET_DVR_LOCAL_GENERAL_CFG struLocalCfg = new CHCNetSDKEvent.NET_DVR_LOCAL_GENERAL_CFG();
            //控制JSON透传报警数据和图片是否分离，0-不分离(COMM_VCA_ALARM返回)，1-分离（分离后走COMM_ISAPI_ALARM回调返回）
            struLocalCfg.byAlarmJsonPictureSeparate = 1;

            Int32 nSize = Marshal.SizeOf(struLocalCfg);
            IntPtr ptrLocalCfg = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(struLocalCfg, ptrLocalCfg, false);

            //NET_DVR_LOCAL_CFG_TYPE_GENERAL
            if (!CHCNetSDKEvent.NET_DVR_SetSDKLocalCfg(17, ptrLocalCfg))
            {
                var iLastErr = CHCNetSDKEvent.NET_DVR_GetLastError();
                _logger.LogError($"userId:?? NET_DVR_SetSDKLocalCfg failed, error code= {iLastErr} ");

            }
            Marshal.FreeHGlobal(ptrLocalCfg);

            //设置异常消息回调函数
            if (m_fExceptionCB == null)
            {
                m_fExceptionCB = new CHCNetSDKEvent.EXCEPYIONCALLBACK(cbExceptionCB);
            }
            CHCNetSDKEvent.NET_DVR_SetExceptionCallBack_V30(0, IntPtr.Zero, m_fExceptionCB, IntPtr.Zero);

            //设置报警回调函数
            if (m_falarmData_V31 == null)
            {
                m_falarmData_V31 = new CHCNetSDKEvent.MSGCallBack_V31(MsgCallback_V31);
            }
            CHCNetSDKEvent.NET_DVR_SetDVRMessageCallBack_V31(m_falarmData_V31, IntPtr.Zero);

            _isInit = true;
            return Task.FromResult(_isInit);
        }

        private bool MsgCallback_V31(int lCommand, ref CHCNetSDKEvent.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        {
            //通过lCommand来判断接收到的报警信息类型，不同的lCommand对应不同的pAlarmInfo内容
            AlarmMessageHandle(lCommand, ref pAlarmer, pAlarmInfo, dwBufLen, pUser);

            return true; //回调函数需要有返回，表示正常接收到数据
        }

        public void MsgCallback(int lCommand, ref CHCNetSDKEvent.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        {
            //通过lCommand来判断接收到的报警信息类型，不同的lCommand对应不同的pAlarmInfo内容
            AlarmMessageHandle(lCommand, ref pAlarmer, pAlarmInfo, dwBufLen, pUser);
        }

        public void AlarmMessageHandle(int lCommand, ref CHCNetSDKEvent.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        {
            //通过lCommand来判断接收到的报警信息类型，不同的lCommand对应不同的pAlarmInfo内容
            switch (lCommand)
            {
                //门禁主机报警上传
                case CHCNetSDKCard.COMM_ALARM_ACS:
                    ExecuteProcessCommAlarmACS(ref pAlarmer, pAlarmInfo, dwBufLen, pUser);
                    break;
                default:
                    _logger.LogInformation($"Hikvision event command:{lCommand} 报警信息不支持 ");
                    break;
            }
        }

        private void cbExceptionCB(uint dwType, int lUserID, int lHandle, IntPtr pUser)
        {
            //异常消息信息类型
            string stringAlarm = "异常消息回调，信息类型：0x" + Convert.ToString(dwType, 16) + ", lUserID:" + lUserID + ", lHandle:" + lHandle;
            _logger.LogError($"userId::{lUserID} Hikvision {stringAlarm} ");
        }

        public Task<(int UserId, CHCNetSDKCard.NET_DVR_USER_LOGIN_INFO LoginInfo, CHCNetSDKCard.NET_DVR_DEVICEINFO_V40 DeviceInfo)>
            ExecuteLoginAsync(int userId, string account, string password, string ip, int port, string logTip)
        {
            //已登录，退出登录
            if (userId >= 0)
            {
                CHCNetSDKCard.NET_DVR_Logout_V30(userId);
                userId = -1;
            }

            CHCNetSDKCard.NET_DVR_USER_LOGIN_INFO struLoginInfo = new CHCNetSDKCard.NET_DVR_USER_LOGIN_INFO();
            CHCNetSDKCard.NET_DVR_DEVICEINFO_V40 struDeviceInfoV40 = new CHCNetSDKCard.NET_DVR_DEVICEINFO_V40();
            struDeviceInfoV40.struDeviceV30.sSerialNumber = new byte[CHCNetSDKCard.SERIALNO_LEN];

            struLoginInfo.sDeviceAddress = ip;
            struLoginInfo.wPort = (ushort)port;
            struLoginInfo.sUserName = account;
            struLoginInfo.sPassword = password;

            userId = CHCNetSDKCard.NET_DVR_Login_V40(ref struLoginInfo, ref struDeviceInfoV40);
            if (userId >= 0)
            {
                //登录成功
                _logger.LogInformation($"{logTip} Hikvision Success:Login Successful");

                return Task.FromResult((userId, struLoginInfo, struDeviceInfoV40));
            }

            uint nErr = CHCNetSDKCard.NET_DVR_GetLastError();
            if (nErr == CHCNetSDKCard.NET_DVR_PASSWORD_ERROR)
            {
                _logger.LogError($"{logTip} Hikvision Error:user name or password error!");
                if (1 == struDeviceInfoV40.bySupportLock)
                {
                    _logger.LogError($"{logTip} Hikvision Error:Left {struDeviceInfoV40.byRetryLoginTime} try opportunity");
                }
            }
            else if (nErr == CHCNetSDKCard.NET_DVR_USER_LOCKED)
            {
                if (1 == struDeviceInfoV40.bySupportLock)
                {
                    _logger.LogError($"{logTip} Hikvision Error:user is locked, the remaining lock time is {struDeviceInfoV40.dwSurplusLockTime} ");
                }
            }
            else
            {
                _logger.LogError($"{logTip} Hikvision Error:login erro:{nErr} !");
            }

            return Task.FromResult((userId, struLoginInfo, struDeviceInfoV40));
        }

        public Task<HikvisionResultModel<string>> ExecuteSetCardAsync(Encoding encoding, int userId, HikvisionPersonCardQuery personCard, string logTip)
        {
            IntPtr ptrStruCond;
            var result = new HikvisionResultModel<string>();

            StopRemoteConfig(ref _cfgHandle.m_lSetCardCfgHandle);

            CHCNetSDKCard.NET_DVR_CARD_COND struCond = new CHCNetSDKCard.NET_DVR_CARD_COND();
            struCond.Init();
            struCond.dwSize = (uint)Marshal.SizeOf(struCond);
            struCond.dwCardNum = 1;
            ptrStruCond = Marshal.AllocHGlobal((int)struCond.dwSize);
            Marshal.StructureToPtr(struCond, ptrStruCond, false);

            _cfgHandle.m_lSetCardCfgHandle = CHCNetSDKCard.NET_DVR_StartRemoteConfig(userId, CHCNetSDKCard.NET_DVR_SET_CARD, ptrStruCond, (int)struCond.dwSize, null, IntPtr.Zero);
            if (_cfgHandle.m_lSetCardCfgHandle < 0)
            {
                var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                result.Message += $"NET_DVR_SET_CARD error:code:{errorCode} message:{errorMessage} m_lSetCardCfgHandle:{_cfgHandle.m_lSetCardCfgHandle} ";
                _logger.LogError($"{logTip} {result.Message}");

                Marshal.FreeHGlobal(ptrStruCond);
                StopRemoteConfig(ref _cfgHandle.m_lSetCardCfgHandle);

                result.IsSuccess = false;
                return Task.FromResult(result);
            }

            result = SendCardData(encoding, userId, personCard, logTip);

            StopRemoteConfig(ref _cfgHandle.m_lSetCardCfgHandle);
            Marshal.FreeHGlobal(ptrStruCond);

            return Task.FromResult(result);
        }

        private HikvisionResultModel<string> SendCardData(Encoding encoding, int userId, HikvisionPersonCardQuery personCard, string logTip)
        {
            var result = new HikvisionResultModel<string>();

            CHCNetSDKCard.NET_DVR_CARD_RECORD struData = new CHCNetSDKCard.NET_DVR_CARD_RECORD();
            struData.Init();
            struData.dwSize = (uint)Marshal.SizeOf(struData);
            struData.byCardType = 1;
            byte[] byTempCardNo = new byte[CHCNetSDKCard.ACS_CARD_NO_LEN];
            byTempCardNo = Encoding.UTF8.GetBytes(personCard.CardNo);
            for (int i = 0; i < byTempCardNo.Length; i++)
            {
                struData.byCardNo[i] = byTempCardNo[i];
            }
            struData.wCardRightPlan[0] = personCard.CardRightPlan;
            struData.dwEmployeeNo = personCard.EmployeeNo;

            byte[] byTempName = new byte[CHCNetSDKCard.NAME_LEN];
            byTempName = encoding.GetBytes(personCard.EmployeeName);
            for (int i = 0; i < byTempName.Length; i++)
            {
                struData.byName[i] = byTempName[i];
            }
            struData.struValid.byEnable = 1;
            struData.struValid.struBeginTime.wYear = 2000;
            struData.struValid.struBeginTime.byMonth = 1;
            struData.struValid.struBeginTime.byDay = 1;
            struData.struValid.struBeginTime.byHour = 11;
            struData.struValid.struBeginTime.byMinute = 11;
            struData.struValid.struBeginTime.bySecond = 11;
            struData.struValid.struEndTime.wYear = 2030;
            struData.struValid.struEndTime.byMonth = 1;
            struData.struValid.struEndTime.byDay = 1;
            struData.struValid.struEndTime.byHour = 11;
            struData.struValid.struEndTime.byMinute = 11;
            struData.struValid.struEndTime.bySecond = 11;
            //struData.byDoorRight[0] = 1;
            struData.byDoorRight = personCard.DoorRights;
            struData.wCardRightPlan[0] = 1;
            IntPtr ptrStruData = Marshal.AllocHGlobal((int)struData.dwSize);
            Marshal.StructureToPtr(struData, ptrStruData, false);

            _logger.LogError($"{logTip} Hikvision SetCard: cardNo:{personCard.CardNo} " +
                $"doorRight:{struData.byDoorRight.ToCommaString()} " +
                $"personCard:{JsonConvert.SerializeObject(personCard, JsonUtil.JsonPrintSettings)} " +
                $"struData:{JsonConvert.SerializeObject(struData, JsonUtil.JsonPrintSettings)} ");

            CHCNetSDKCard.NET_DVR_CARD_STATUS struStatus = new CHCNetSDKCard.NET_DVR_CARD_STATUS();
            struStatus.Init();
            struStatus.dwSize = (uint)Marshal.SizeOf(struStatus);
            IntPtr ptrdwState = Marshal.AllocHGlobal((int)struStatus.dwSize);
            Marshal.StructureToPtr(struStatus, ptrdwState, false);

            int dwState = (int)CHCNetSDKCard.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS;
            uint dwReturned = 0;
            while (true)
            {
                dwState = CHCNetSDKCard.NET_DVR_SendWithRecvRemoteConfig(_cfgHandle.m_lSetCardCfgHandle, ptrStruData, struData.dwSize, ptrdwState, struStatus.dwSize, ref dwReturned);
                struStatus = (CHCNetSDKCard.NET_DVR_CARD_STATUS)Marshal.PtrToStructure(ptrdwState, typeof(CHCNetSDKCard.NET_DVR_CARD_STATUS));
                if (dwState == (int)CHCNetSDKCard.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_NEEDWAIT)
                {
                    Thread.Sleep(10);
                    continue;
                }
                else if (dwState == (int)CHCNetSDKCard.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FAILED)
                {
                    var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                    var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                    result.Message += $"NET_DVR_SET_CARD failed:code:{errorCode} message:{errorMessage} ";
                    _logger.LogError($"{logTip} {result.Message}");
                }
                else if (dwState == (int)CHCNetSDKCard.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS)
                {
                    if (struStatus.dwErrorCode != 0)
                    {
                        result.Message += $"NET_DVR_SET_CARD success dwErrorCode:{struStatus.dwErrorCode} ";
                        _logger.LogWarning($"{logTip} {result.Message}");
                    }
                    else
                    {
                        _logger.LogInformation($"{logTip} Hikvision Success:NET_DVR_SET_CARD success");
                        result.IsSuccess = true;
                    }
                }
                else if (dwState == (int)CHCNetSDKCard.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FINISH)
                {
                    _logger.LogInformation($"{logTip} Hikvision Success:NET_DVR_SET_CARD finish");
                    break;
                }
                else if (dwState == (int)CHCNetSDKCard.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_EXCEPTION)
                {
                    var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                    var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                    result.Message += $"NET_DVR_SET_CARD exception:code:{errorCode} message:{errorMessage} ";
                    _logger.LogError($"{logTip} {result.Message}");
                    break;
                }
                else
                {
                    var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                    var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                    result.Message += $"NET_DVR_SET_CARD unknown:code:{errorCode} message:{errorMessage} ";
                    _logger.LogError($"{logTip} {result.Message}");
                    break;
                }
            }
            Marshal.FreeHGlobal(ptrStruData);
            Marshal.FreeHGlobal(ptrdwState);

            return result;
        }

        public Task<HikvisionResultModel<CHCNetSDKCard.NET_DVR_CARD_RECORD>> ExecuteGetCardAsync(Encoding encoding, int userId, string cardNo, string logTip)
        {
            var result = new HikvisionResultModel<CHCNetSDKCard.NET_DVR_CARD_RECORD>();
            HikvisionPersonCardQuery personCard = null;

            StopRemoteConfig(ref _cfgHandle.m_lGetCardCfgHandle);

            CHCNetSDKCard.NET_DVR_CARD_COND struCond = new CHCNetSDKCard.NET_DVR_CARD_COND();
            struCond.Init();
            struCond.dwSize = (uint)Marshal.SizeOf(struCond);
            struCond.dwCardNum = 1;
            IntPtr ptrStruCond = Marshal.AllocHGlobal((int)struCond.dwSize);
            Marshal.StructureToPtr(struCond, ptrStruCond, false);

            CHCNetSDKCard.NET_DVR_CARD_RECORD struData = new CHCNetSDKCard.NET_DVR_CARD_RECORD();
            struData.Init();
            struData.dwSize = (uint)Marshal.SizeOf(struData);
            byte[] byTempCardNo = new byte[CHCNetSDKCard.ACS_CARD_NO_LEN];
            byTempCardNo = Encoding.UTF8.GetBytes(cardNo);
            for (int i = 0; i < byTempCardNo.Length; i++)
            {
                struData.byCardNo[i] = byTempCardNo[i];
            }
            IntPtr ptrStruData = Marshal.AllocHGlobal((int)struData.dwSize);
            Marshal.StructureToPtr(struData, ptrStruData, false);

            CHCNetSDKCard.NET_DVR_CARD_SEND_DATA struSendData = new CHCNetSDKCard.NET_DVR_CARD_SEND_DATA();
            struSendData.Init();
            struSendData.dwSize = (uint)Marshal.SizeOf(struSendData);
            for (int i = 0; i < byTempCardNo.Length; i++)
            {
                struSendData.byCardNo[i] = byTempCardNo[i];
            }

            IntPtr ptrStruSendData = Marshal.AllocHGlobal((int)struSendData.dwSize);
            Marshal.StructureToPtr(struSendData, ptrStruSendData, false);
            _cfgHandle.m_lGetCardCfgHandle = CHCNetSDKCard.NET_DVR_StartRemoteConfig(userId, CHCNetSDKCard.NET_DVR_GET_CARD, ptrStruCond, (int)struCond.dwSize, null, IntPtr.Zero);
            if (_cfgHandle.m_lGetCardCfgHandle < 0)
            {
                var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                result.Message += $"NET_DVR_GET_CARD error:code:{errorCode} message:{errorMessage} m_lGetCardCfgHandle:{_cfgHandle.m_lGetCardCfgHandle} ";
                _logger.LogError($"{logTip} {result.Message}");

                Marshal.FreeHGlobal(ptrStruCond);
                StopRemoteConfig(ref _cfgHandle.m_lGetCardCfgHandle);

                result.IsSuccess = false;
                return Task.FromResult(result);
            }
            else
            {
                int dwState = (int)CHCNetSDKCard.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS;
                uint dwReturned = 0;
                while (true)
                {
                    dwState = CHCNetSDKCard.NET_DVR_SendWithRecvRemoteConfig(_cfgHandle.m_lGetCardCfgHandle, ptrStruSendData, struSendData.dwSize, ptrStruData, struData.dwSize, ref dwReturned);
                    struData = (CHCNetSDKCard.NET_DVR_CARD_RECORD)Marshal.PtrToStructure(ptrStruData, typeof(CHCNetSDKCard.NET_DVR_CARD_RECORD));
                    if (dwState == (int)CHCNetSDKCard.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_NEEDWAIT)
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                    else if (dwState == (int)CHCNetSDKCard.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FAILED)
                    {
                        var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                        var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                        result.Message += $"NET_DVR_GET_CARD failed:code:{errorCode} message:{errorMessage} ";
                        _logger.LogError($"{logTip} {result.Message}");
                    }
                    else if (dwState == (int)CHCNetSDKCard.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS)
                    {
                        personCard = new HikvisionPersonCardQuery();
                        personCard.CardNo = Encoding.UTF8.GetString(struData.byCardNo);
                        personCard.CardRightPlan = struData.wCardRightPlan[0];
                        personCard.EmployeeNo = struData.dwEmployeeNo;
                        personCard.EmployeeName = encoding.GetString(struData.byName);

                        _logger.LogInformation($"{logTip} Hikvision Success:NET_DVR_GET_CARD success");
                        result.IsSuccess = true;
                    }
                    else if (dwState == (int)CHCNetSDKCard.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FINISH)
                    {
                        _logger.LogInformation($"{logTip} Hikvision Success:NET_DVR_GET_CARD finish");
                        break;
                    }
                    else if (dwState == (int)CHCNetSDKCard.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_EXCEPTION)
                    {
                        var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                        var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                        result.Message += $"NET_DVR_GET_CARD exception:code:{errorCode} message:{errorMessage} ";
                        _logger.LogError($"{logTip} {result.Message}");
                        result.IsSuccess = false;
                        break;
                    }
                    else
                    {
                        var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                        var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                        result.Message += $"NET_DVR_GET_CARD unknown:code:{errorCode} message:{errorMessage} ";
                        _logger.LogError($"{logTip} {result.Message}");
                        result.IsSuccess = false;
                        break;
                    }
                }
            }
            StopRemoteConfig(ref _cfgHandle.m_lGetCardCfgHandle);
            Marshal.FreeHGlobal(ptrStruSendData);
            Marshal.FreeHGlobal(ptrStruData);

            result.Data = struData;
            return Task.FromResult(result);
        }

        public Task<HikvisionResultModel<string>> ExecuteDeleteCardAsync(int userId, string cardNo, string logTip)
        {
            var result = new HikvisionResultModel<string>();

            StopRemoteConfig(ref _cfgHandle.m_lDelCardCfgHandle);

            CHCNetSDKCard.NET_DVR_CARD_COND struCond = new CHCNetSDKCard.NET_DVR_CARD_COND();
            struCond.Init();
            struCond.dwSize = (uint)Marshal.SizeOf(struCond);
            struCond.dwCardNum = 1;
            IntPtr ptrStruCond = Marshal.AllocHGlobal((int)struCond.dwSize);
            Marshal.StructureToPtr(struCond, ptrStruCond, false);

            CHCNetSDKCard.NET_DVR_CARD_SEND_DATA struSendData = new CHCNetSDKCard.NET_DVR_CARD_SEND_DATA();
            struSendData.Init();
            struSendData.dwSize = (uint)Marshal.SizeOf(struSendData);
            byte[] byTempCardNo = new byte[CHCNetSDKCard.ACS_CARD_NO_LEN];
            byTempCardNo = Encoding.UTF8.GetBytes(cardNo);
            for (int i = 0; i < byTempCardNo.Length; i++)
            {
                struSendData.byCardNo[i] = byTempCardNo[i];
            }
            IntPtr ptrStruSendData = Marshal.AllocHGlobal((int)struSendData.dwSize);
            Marshal.StructureToPtr(struSendData, ptrStruSendData, false);

            CHCNetSDKCard.NET_DVR_CARD_STATUS struStatus = new CHCNetSDKCard.NET_DVR_CARD_STATUS();
            struStatus.Init();
            struStatus.dwSize = (uint)Marshal.SizeOf(struStatus);
            IntPtr ptrdwState = Marshal.AllocHGlobal((int)struStatus.dwSize);
            Marshal.StructureToPtr(struStatus, ptrdwState, false);

            _cfgHandle.m_lDelCardCfgHandle = CHCNetSDKCard.NET_DVR_StartRemoteConfig(userId, CHCNetSDKCard.NET_DVR_DEL_CARD, ptrStruCond, (int)struCond.dwSize, null, IntPtr.Zero);
            if (_cfgHandle.m_lDelCardCfgHandle < 0)
            {
                var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                result.Message += $"NET_DVR_DEL_CARD error:code:{errorCode} message:{errorMessage} m_lDelCardCfgHandle:{_cfgHandle.m_lDelCardCfgHandle}";
                _logger.LogError($"{logTip} {result.Message}");

                Marshal.FreeHGlobal(ptrStruCond);
                StopRemoteConfig(ref _cfgHandle.m_lDelCardCfgHandle);

                result.IsSuccess = false;
                return Task.FromResult(result);
            }
            else
            {
                int dwState = (int)CHCNetSDKCard.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS;
                uint dwReturned = 0;
                while (true)
                {
                    dwState = CHCNetSDKCard.NET_DVR_SendWithRecvRemoteConfig(_cfgHandle.m_lDelCardCfgHandle, ptrStruSendData, struSendData.dwSize, ptrdwState, struStatus.dwSize, ref dwReturned);
                    if (dwState == (int)CHCNetSDKCard.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_NEEDWAIT)
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                    else if (dwState == (int)CHCNetSDKCard.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FAILED)
                    {
                        var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                        var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                        result.Message += $"NET_DVR_DEL_CARD failed:code:{errorCode} message:{errorMessage}";
                        _logger.LogError($"{logTip} {result.Message}");
                    }
                    else if (dwState == (int)CHCNetSDKCard.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS)
                    {
                        _logger.LogInformation($"{logTip} Hikvision Success:NET_DVR_DEL_CARD success");
                        result.IsSuccess = true;
                    }
                    else if (dwState == (int)CHCNetSDKCard.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FINISH)
                    {
                        _logger.LogInformation($"{logTip} Hikvision Success:NET_DVR_DEL_CARD finish");
                        break;
                    }
                    else if (dwState == (int)CHCNetSDKCard.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_EXCEPTION)
                    {
                        var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                        var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                        result.Message += $"NET_DVR_DEL_CARD exception:code:{errorCode} message:{errorMessage}";
                        _logger.LogError($"{logTip} {result.Message}");
                        result.IsSuccess = false;
                        break;
                    }
                    else
                    {
                        var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                        var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                        result.Message += $"NET_DVR_DEL_CARD unknown:code:{errorCode} message:{errorMessage}";
                        _logger.LogError($"{logTip} {result.Message}");
                        result.IsSuccess = false;
                        break;
                    }
                }
            }
            StopRemoteConfig(ref _cfgHandle.m_lDelCardCfgHandle);

            Marshal.FreeHGlobal(ptrStruSendData);
            Marshal.FreeHGlobal(ptrdwState);

            return Task.FromResult(result);
        }

        public Task<HikvisionResultModel<string>> ExecuteSetFaceAsync(int userId, string cardNo, string facePath, string logTip, int cardReaderNo = 1)
        {
            var result = new HikvisionResultModel<string>();
            if (string.IsNullOrEmpty(facePath))
            {
                result.Message += $"human Face not exists!";
                _logger.LogError($"{logTip} {result.Message}");
                result.IsSuccess = false;
                return Task.FromResult(result);
            }

            StopRemoteConfig(ref _cfgHandle.m_lSetFaceCfgHandle);

            CHCNetSDKCard.NET_DVR_FACE_COND struCond = new CHCNetSDKCard.NET_DVR_FACE_COND();
            struCond.init();
            struCond.dwSize = Marshal.SizeOf(struCond);
            struCond.dwFaceNum = 1;
            struCond.dwEnableReaderNo = cardReaderNo;
            byte[] byTemp = Encoding.UTF8.GetBytes(cardNo);
            for (int i = 0; i < byTemp.Length; i++)
            {
                struCond.byCardNo[i] = byTemp[i];
            }

            int dwInBufferSize = struCond.dwSize;
            IntPtr ptrstruCond = Marshal.AllocHGlobal(dwInBufferSize);
            Marshal.StructureToPtr(struCond, ptrstruCond, false);
            _cfgHandle.m_lSetFaceCfgHandle = CHCNetSDKCard.NET_DVR_StartRemoteConfig(userId, CHCNetSDKCard.NET_DVR_SET_FACE, ptrstruCond, dwInBufferSize, null, IntPtr.Zero);
            if (_cfgHandle.m_lSetFaceCfgHandle < 0)
            {
                var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                result.Message += $"NET_DVR_SET_FACE error:code:{errorCode} message:{errorMessage} m_lSetFaceCfgHandle:{_cfgHandle.m_lSetFaceCfgHandle} ";
                _logger.LogError($"{logTip} {result.Message}");

                Marshal.FreeHGlobal(ptrstruCond);
                StopRemoteConfig(ref _cfgHandle.m_lSetFaceCfgHandle);

                result.IsSuccess = false;
                return Task.FromResult(result);
            }

            CHCNetSDKCard.NET_DVR_FACE_RECORD struRecord = new CHCNetSDKCard.NET_DVR_FACE_RECORD();
            struRecord.init();
            struRecord.dwSize = Marshal.SizeOf(struRecord);

            byte[] byRecordNo = Encoding.UTF8.GetBytes(cardNo);
            for (int i = 0; i < byRecordNo.Length; i++)
            {
                struRecord.byCardNo[i] = byRecordNo[i];
            }

            var setFaceMessage = ExecuteReadFaceData(userId, facePath, ref struRecord, logTip);
            result.Message += setFaceMessage;

            int dwInBuffSize = Marshal.SizeOf(struRecord);
            int dwStatus = 0;

            CHCNetSDKCard.NET_DVR_FACE_STATUS struStatus = new CHCNetSDKCard.NET_DVR_FACE_STATUS();
            struStatus.init();
            struStatus.dwSize = Marshal.SizeOf(struStatus);
            int dwOutBuffSize = struStatus.dwSize;
            IntPtr ptrOutDataLen = Marshal.AllocHGlobal(sizeof(int));
            bool flag = true;
            while (flag)
            {
                dwStatus = CHCNetSDKCard.NET_DVR_SendWithRecvRemoteConfig(_cfgHandle.m_lSetFaceCfgHandle, ref struRecord, dwInBuffSize, ref struStatus, dwOutBuffSize, ptrOutDataLen);

                //成功读取到数据，处理完本次数据后需调用next
                if (dwStatus == CHCNetSDKCard.NET_SDK_GET_NEXT_STATUS_SUCCESS)
                {
                    var setFaceResult = ProcessSetFaceData(userId, ref struStatus, ref flag, logTip);
                    result.IsSuccess = setFaceResult.IsSuccess;
                    result.Message += setFaceResult.Message;
                }
                else if (dwStatus == CHCNetSDKCard.NET_SDK_GET_NEXT_STATUS_NEED_WAIT)
                {
                }
                else if (dwStatus == CHCNetSDKCard.NET_SDK_GET_NEXT_STATUS_FAILED)
                {
                    var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                    var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                    result.Message += $"NET_DVR_SET_FACE failed:code:{errorCode} message:{errorMessage} ";
                    _logger.LogError($"{logTip} {result.Message}");
                    flag = false;
                }
                else if (dwStatus == CHCNetSDKCard.NET_SDK_GET_NEXT_STATUS_FINISH)
                {
                    _logger.LogInformation($"{logTip} Hikvision Error:NET_DVR_SET_FACE_FINISH!");
                    flag = false;
                }
                else
                {
                    var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                    var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                    result.Message += $"NET_DVR_SET_FACE unkown:code:{errorCode} message:{errorMessage} ";
                    _logger.LogError($"{logTip} {result.Message}");
                    flag = false;
                }
            }
            StopRemoteConfig(ref _cfgHandle.m_lSetFaceCfgHandle);

            Marshal.FreeHGlobal(ptrstruCond);
            Marshal.FreeHGlobal(ptrOutDataLen);

            return Task.FromResult(result);
        }

        public Task<HikvisionResultModel<string>> ExecuteGetFaceAsync(int userId, string cardNo, string logTip, int cardReaderNo = 1)
        {
            var result = new HikvisionResultModel<string>();

            StopRemoteConfig(ref _cfgHandle.m_lGetFaceCfgHandle);

            CHCNetSDKCard.NET_DVR_FACE_COND struCond = new CHCNetSDKCard.NET_DVR_FACE_COND();
            struCond.init();
            struCond.dwSize = Marshal.SizeOf(struCond);
            int dwSize = struCond.dwSize;
            struCond.dwEnableReaderNo = cardReaderNo;
            //人脸数量是1
            struCond.dwFaceNum = 1;
            byte[] byTemp = Encoding.UTF8.GetBytes(cardNo);
            for (int i = 0; i < byTemp.Length; i++)
            {
                struCond.byCardNo[i] = byTemp[i];
            }

            IntPtr ptrStruCond = Marshal.AllocHGlobal(dwSize);
            Marshal.StructureToPtr(struCond, ptrStruCond, false);

            _cfgHandle.m_lGetFaceCfgHandle = CHCNetSDKCard.NET_DVR_StartRemoteConfig(userId, CHCNetSDKCard.NET_DVR_GET_FACE, ptrStruCond, dwSize, null, IntPtr.Zero);
            if (_cfgHandle.m_lGetFaceCfgHandle == -1)
            {
                var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                result.Message += $"NET_DVR_GET_FACE error:code:{errorCode} message:{errorMessage} m_lGetFaceCfgHandle:{_cfgHandle.m_lGetFaceCfgHandle} ";
                _logger.LogError($"{logTip} {result.Message}");

                Marshal.FreeHGlobal(ptrStruCond);
                StopRemoteConfig(ref _cfgHandle.m_lGetFaceCfgHandle);

                result.IsSuccess = false;
                return Task.FromResult(result);
            }

            bool Flag = true;
            int dwStatus = 0;

            CHCNetSDKCard.NET_DVR_FACE_RECORD struRecord = new CHCNetSDKCard.NET_DVR_FACE_RECORD();
            struRecord.init();
            struRecord.dwSize = Marshal.SizeOf(struRecord);
            int dwOutBuffSize = struRecord.dwSize;
            while (Flag)
            {
                dwStatus = CHCNetSDKCard.NET_DVR_GetNextRemoteConfig(_cfgHandle.m_lGetFaceCfgHandle, ref struRecord, dwOutBuffSize);

                //成功读取到数据，处理完本次数据后需调用next
                if (dwStatus == CHCNetSDKCard.NET_SDK_GET_NEXT_STATUS_SUCCESS)
                {
                    result.Data = ProcessFaceData(userId, cardNo, ref struRecord, ref Flag, logTip);
                }
                else if (dwStatus == CHCNetSDKCard.NET_SDK_GET_NEXT_STATUS_NEED_WAIT)
                {

                }
                else if (dwStatus == CHCNetSDKCard.NET_SDK_GET_NEXT_STATUS_FAILED)
                {
                    var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                    var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                    result.Message += $"NET_DVR_GET_FACE failed:code:{errorCode} message:{errorMessage} ";
                    _logger.LogError($"{logTip} {result.Message}");
                    Flag = false;
                }
                else if (dwStatus == CHCNetSDKCard.NET_SDK_GET_NEXT_STATUS_FINISH)
                {
                    _logger.LogInformation($"{logTip} Hikvision Success:NET_SDK_GET_NEXT_STATUS_FINISH!");
                    Flag = false;
                }
                else
                {
                    var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                    var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                    result.Message += $"NET_DVR_GET_FACE unkown:code:{errorCode} message:{errorMessage} ";
                    _logger.LogError($"{logTip} {result.Message}");
                    Flag = false;
                }
            }
            StopRemoteConfig(ref _cfgHandle.m_lGetFaceCfgHandle);

            Marshal.FreeHGlobal(ptrStruCond);

            result.IsSuccess = !string.IsNullOrEmpty(result.Data);
            return Task.FromResult(result);
        }

        public Task<HikvisionResultModel<string>> ExecuteDeleteFaceAsync(int userId, string cardNo, int cardReaderNo, string logTip)
        {
            var result = new HikvisionResultModel<string>();
            CHCNetSDKCard.NET_DVR_FACE_PARAM_CTRL_CARDNO struCardNo = new CHCNetSDKCard.NET_DVR_FACE_PARAM_CTRL_CARDNO();
            struCardNo.init();
            struCardNo.dwSize = Marshal.SizeOf(struCardNo);
            struCardNo.byMode = 0;
            int dwSize = struCardNo.dwSize;
            byte[] byCardNo = Encoding.UTF8.GetBytes(cardNo);
            for (int i = 0; i < byCardNo.Length; i++)
            {
                struCardNo.struByCard.byCardNo[i] = byCardNo[i];
            }

            struCardNo.struByCard.byEnableCardReader[cardReaderNo - 1] = 1;

            for (int i = 0; i < CHCNetSDKCard.MAX_FACE_NUM; ++i)
            {
                //全部写1删除人脸
                struCardNo.struByCard.byFaceID[i] = 1;
            }
            var isDeleteFace = CHCNetSDKCard.NET_DVR_RemoteControl(userId, CHCNetSDKCard.NET_DVR_DEL_FACE_PARAM_CFG, ref struCardNo, dwSize);
            if (isDeleteFace)
            {
                _logger.LogInformation($"{logTip} Hikvision Success:NET_SDK_DEL_FACE_SUCCEED 删除卡成功");
                result.IsSuccess = true;
                return Task.FromResult(result);
            }
            else
            {
                var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                result.Message += $"NET_SDK_DEL_FACE failed:code:{errorCode} message:{errorMessage} ";
                _logger.LogError($"{logTip} {result.Message}");
                result.IsSuccess = false;
                return Task.FromResult(result);
            }
        }

        public Task<int> ExecuteGetCurACSDeviceDoorNumAsync(int userId, string logTip)
        {
            //get device ability for door num ,read carder num is door num*2 
            int doorNum = 0;

            string pOutXMLBuf = GetACSDeviceAbility(userId, CHCNetSDKCard.ACS_ABILITY, "<AcsAbility version=\"2.0\">\r\n</AcsAbility>", logTip);
            if (pOutXMLBuf != null)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();

                    // wait to determine
                    doc.LoadXml(pOutXMLBuf);
                    XmlNode root = doc.DocumentElement;
                    XmlNode node = root.SelectSingleNode("doorNo");

                    if (node != null)
                    {
                        XmlNode doorNumAtttibuteNode = node.SelectSingleNode("@max");
                        doorNum = int.Parse(doorNumAtttibuteNode.InnerText);
                    }
                    else
                    {
                        doorNum = 0;
                    }
                }
                catch (XmlException ex)
                {
                    _logger.LogError($"{logTip} Hikvision Error:GetCurACSDeviceDoorNumAsync 解析xml错误，ex:{ex} ");
                }
            }
            return Task.FromResult(doorNum);
        }

        private string ExecuteReadFaceData(int userId, string facePath, ref CHCNetSDKCard.NET_DVR_FACE_RECORD struRecord, string logTip)
        {
            using (var fs = File.OpenRead(facePath))
            {
                if (0 == fs.Length)
                {
                    _logger.LogError($"{logTip} Hikvision Error:The face picture is 0k,please input another picture!");
                    return $"The face picture is 0k,please input another picture!";
                }
                if (200 * 1024 < fs.Length)
                {
                    _logger.LogError($"{logTip} Hikvision Error:The face picture is larger than 200k,please input another picture!");
                    return $"The face picture is larger than 200k,please input another picture!";
                }
                try
                {
                    int.TryParse(fs.Length.ToString(), out struRecord.dwFaceLen);
                    int iLen = struRecord.dwFaceLen;
                    byte[] by = new byte[iLen];
                    struRecord.pFaceBuffer = Marshal.AllocHGlobal(iLen);
                    fs.Read(by, 0, iLen);
                    Marshal.Copy(by, 0, struRecord.pFaceBuffer, iLen);
                    fs.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{logTip} Hikvision Error:Read Face Data failed:ex:{ex}");
                    fs.Close();
                    return $"Read Face Data failed:ex:{ex}";
                }
            }
            return string.Empty;
        }

        private HikvisionResultModel<string> ProcessSetFaceData(int userId, ref CHCNetSDKCard.NET_DVR_FACE_STATUS struStatus, ref bool flag, string logTip)
        {
            var result = new HikvisionResultModel<string>();
            switch (struStatus.byRecvStatus)
            {
                case 1:
                    _logger.LogInformation($"{logTip} Hikvision Success:SetFaceDataSuccessful 下发人脸成功！");
                    result.IsSuccess = true;
                    break;
                default:
                    flag = false;
                    result.IsSuccess = false;
                    var errorCode = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                    var errorMessage = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref errorCode));
                    result.Message += $"NET_SDK_SET_FACE_DATA failed:code:{errorCode} message:{errorMessage} " +
                        $"status:{struStatus.byRecvStatus} statusMessage:{(Encoding.UTF8.GetString(struStatus.byErrorMsg))?.TrimEnd('\0')} ";
                    _logger.LogError($"{logTip} {result.Message}");
                    break;
            }
            return result;
        }

        private string ProcessFaceData(int userId, string cardNo, ref CHCNetSDKCard.NET_DVR_FACE_RECORD struRecord, ref bool flag, string logTip)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "uploads", "hikvision", "faces");
            Directory.CreateDirectory(path);
            var facePath = Path.Combine(path, $"{cardNo}.jpg");

            if (0 == struRecord.dwFaceLen)
            {
                return string.Empty;
            }

            try
            {
                using (FileStream fs = new FileStream(facePath, FileMode.OpenOrCreate))
                {
                    int faceLen = struRecord.dwFaceLen;
                    byte[] by = new byte[faceLen];
                    Marshal.Copy(struRecord.pFaceBuffer, by, 0, faceLen);
                    fs.Write(by, 0, faceLen);
                    fs.Close();
                }

                return facePath;
            }
            catch (Exception ex)
            {
                flag = false;
                _logger.LogError($"{logTip} Hikvision Error:ProcessFingerData failed:ex:{ex}");
            }
            return string.Empty;
        }

        private string GetACSDeviceAbility(int userId, int iAbilityType, string inParam, string logTip)
        {
            string exceptionInfo = null;
            string errorinfo = null;
            int dwLastError = 0;

            IntPtr ptrCfgVer;
            IntPtr m_pOutBuf;
            bool dwReturnValue = false;
            string pOutXMLBuf = null;
            string strInputParam = inParam;

            m_pOutBuf = Marshal.AllocHGlobal((int)XML_ABILITY_OUT_LEN);
            try
            {
                uint dwInSize = (uint)strInputParam.Length;
                ptrCfgVer = Marshal.StringToCoTaskMemAnsi(strInputParam);
                dwReturnValue = CHCNetSDKCard.NET_DVR_GetDeviceAbility(userId, (uint)iAbilityType, ptrCfgVer, dwInSize, m_pOutBuf, XML_ABILITY_OUT_LEN);
                Marshal.FreeHGlobal(ptrCfgVer);

                if (dwReturnValue)
                {
                    pOutXMLBuf = Marshal.PtrToStringAnsi(m_pOutBuf);
                }

                dwLastError = (int)CHCNetSDKCard.NET_DVR_GetLastError();
                errorinfo = Marshal.PtrToStringAnsi(CHCNetSDKCard.NET_DVR_GetErrorMsg(ref dwLastError));
            }
            catch (Exception ex)
            {
                exceptionInfo = ex.Message;
            }
            finally
            {
                Marshal.FreeHGlobal(m_pOutBuf);

                if (exceptionInfo != null)
                {
                    _logger.LogError($"{logTip} Hikvision Error:operation exception, please check the exception information{exceptionInfo}");
                }
                else
                {
                    if (dwReturnValue)
                    {
                        _logger.LogInformation($"Hikvision Error:ACS_ABILITY:Get ability success!OPERATION_SUCC_T:{AcsDemoPublic.OPERATION_SUCC_T}");
                    }
                    else
                    {
                        _logger.LogError($"{logTip} Hikvision Error:Get ability failed! error code:{dwLastError},error message:{errorinfo}");
                    }
                }
            }
            return pOutXMLBuf;
        }

        public Task<bool> ExecuteDeployEventAsync(int userId, HikvisionTypeParameterModel deployType, string logTip)
        {
            CHCNetSDKEvent.NET_DVR_SETUPALARM_PARAM struAlarmParam = new CHCNetSDKEvent.NET_DVR_SETUPALARM_PARAM();
            struAlarmParam.dwSize = (uint)Marshal.SizeOf(struAlarmParam);
            struAlarmParam.byLevel = deployType.DeployLevel; //0- 一级布防,1- 二级布防
            struAlarmParam.byAlarmInfoType = 1;//智能交通设备有效，新报警信息类型
            struAlarmParam.byFaceAlarmDetection = 1;//1-人脸侦测
            struAlarmParam.byDeployType = deployType.DeployType;//0-客户端布防,1-实时布防

            _logger.LogInformation($"{logTip} Hikvision布防：data:{JsonConvert.SerializeObject(struAlarmParam, JsonUtil.JsonPrintSettings)} ");

            m_lAlarmHandle = CHCNetSDKEvent.NET_DVR_SetupAlarmChan_V41(userId, ref struAlarmParam);
            if (m_lAlarmHandle < 0)
            {
                var iLastErr = CHCNetSDKEvent.NET_DVR_GetLastError();
                _logger.LogError($"{logTip} 布防失败，错误号：{iLastErr} ");

                _logger.LogError($"{logTip} Hikvision Error:NET_DVR_SetupAlarmChan_V41 Setup alarm channel failed " +
                    $"布防失败，错误号：{iLastErr} " +
                    $"Data:{JsonConvert.SerializeObject(struAlarmParam, JsonUtil.JsonPrintSettings)} ");

                return Task.FromResult(false);
            }
            else
            {
                _logger.LogInformation($"{logTip} Hikvision Success:NET_DVR_SetupAlarmChan_V41 布防成功!");
                return Task.FromResult(true);
            }
        }

        public void ExecuteProcessCommAlarmACS(ref CHCNetSDKEvent.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        {
            CHCNetSDKEvent.NET_DVR_ACS_ALARM_INFO struAcsAlarm = new CHCNetSDKEvent.NET_DVR_ACS_ALARM_INFO();
            uint dwSize = (uint)Marshal.SizeOf(struAcsAlarm);
            struAcsAlarm = (CHCNetSDKEvent.NET_DVR_ACS_ALARM_INFO)Marshal.PtrToStructure(pAlarmInfo, typeof(CHCNetSDKEvent.NET_DVR_ACS_ALARM_INFO));

            /**************************************************/
            _logger.LogInformation($"Hikvision 接收数据1：struAcsAlarm: {JsonConvert.SerializeObject(struAcsAlarm, JsonUtil.JsonPrintSettings)} " +
                $"pAlarmInfo:{JsonConvert.SerializeObject(struAcsAlarm, JsonUtil.JsonPrintSettings)} ");

            //报警设备IP地址
            string strIP = Encoding.UTF8.GetString(pAlarmer.sDeviceIP).TrimEnd('\0');

            var passRecord = new PassRecordModel();
            //事件不能通过当前实例化对象设备信息获取，需从NET_DVR_ALARMER数据中获取 
            passRecord.DeviceId = $"{strIP}:{pAlarmer.wLinkPort}";

            passRecord.PassLocalTime = string.Format("{0}-{1:D2}-{2} {3:D2}:{4:D2}:{5:D2}",
                struAcsAlarm.struTime.dwYear,
                struAcsAlarm.struTime.dwMonth,
                struAcsAlarm.struTime.dwDay,
                struAcsAlarm.struTime.dwHour,
                struAcsAlarm.struTime.dwMinute,
                struAcsAlarm.struTime.dwSecond);

            if (struAcsAlarm.struAcsEventInfo.byCardNo?.Length > 0)
            {
                passRecord.PassRightSign = Encoding.UTF8.GetString(struAcsAlarm.struAcsEventInfo.byCardNo).TrimEnd('\0');
                if (string.IsNullOrEmpty(passRecord.PassRightSign))
                {
                    _logger.LogInformation($"{passRecord.DeviceId} 状态不符合的上传数据：卡号为空！time:{passRecord.PassLocalTime} ");
                    return;
                }
            }
            else
            {
                _logger.LogInformation($"{passRecord.DeviceId} 状态不符合的上传数据：卡号为空！！time:{passRecord.PassLocalTime} ");
                return;
            }

            //string[] szCardType = { "normal card", "disabled card", "blacklist card", "night watch card", "stress card", "super card", "guest card" };
            byte byCardType = struAcsAlarm.struAcsEventInfo.byCardType;
            if (byCardType == 3)
            {
                _logger.LogInformation($"{passRecord.DeviceId} 状态不符合的上传数据：time:{passRecord.PassLocalTime}" +
                    $" hivisoion card number:{passRecord.PassRightSign} " +
                    $"card type:{byCardType} ");
                return;
            }

            _logger.LogInformation($"{passRecord.DeviceId} 符合的上传的数据：card nubmer:{passRecord.PassRightSign} time:{passRecord.PassLocalTime} ");

            //门编号
            if (struAcsAlarm.struAcsEventInfo.dwDoorNo != 0)
            {
                passRecord.Remark = struAcsAlarm.struAcsEventInfo.dwDoorNo.ToString();
            }

            //口罩
            if (struAcsAlarm.struAcsEventInfo.byMask == 2)
            {
                passRecord.IsMask = false;
            }
            else if (struAcsAlarm.struAcsEventInfo.byMask == 3)
            {
                passRecord.IsMask = true;
            }

            if (struAcsAlarm.dwPicDataLen != 0 && struAcsAlarm.pPicData != IntPtr.Zero)
            {
                var fileName = $"{IdUtil.NewId()}.jpg";
                var fileFullName = PathUtil.GetFileRandomFullName(PathEnum.RECORD_IMAGE_PATH, fileName);
                using (FileStream fs = new FileStream(fileFullName, FileMode.Create))
                {
                    int iLen = (int)struAcsAlarm.dwPicDataLen;
                    byte[] by = new byte[iLen];
                    Marshal.Copy(struAcsAlarm.pPicData, by, 0, iLen);

                    passRecord.FaceImage = by;
                    passRecord.FaceImageSize = iLen;

                    fs.Write(by, 0, iLen);
                    fs.Close();
                }

                passRecord.AccessType = AccessTypeEnum.FACE.Value;
            }
            else
            {
                passRecord.AccessType = AccessTypeEnum.IC_CARD.Value;
            }

            _logger.LogInformation($"{passRecord.DeviceId} Hikvision 接收数据2：{JsonConvert.SerializeObject(passRecord, JsonUtil.JsonPrintSettings)} ");

            //过滤不符合条件数据
            if (passRecord.AccessType == AccessTypeEnum.IC_CARD.Value
                && (string.IsNullOrEmpty(passRecord.PassRightSign) || passRecord.PassRightSign == 0.ToString()))
            {
                return;
            }

            //异步 上传事件
            ExecuteUploadPassRecordAsync?.Invoke(passRecord);
        }


        public Task<bool> ExecuteLogoutAsync(int userId, string logTip)
        {
            bool isResult = false;
            //已登录，退出登录
            if (userId >= 0)
            {
                isResult = CHCNetSDKCard.NET_DVR_Logout_V30(userId);
                if (!isResult)
                {
                    var iLastErr = CHCNetSDKCard.NET_DVR_GetLastError();
                    _logger.LogError($"{logTip} 退出登录失败，错误号：{iLastErr}");
                }
                userId = -1;
            }
            return Task.FromResult(isResult);
        }

        public Task<bool> CloseAlarmAsync(bool isDeployEvent, int userId, string logTip)
        {
            if (!isDeployEvent)
            {
                _logger.LogWarning("未布防");
                return Task.FromResult(false);
            }

            //已登录，退出登录
            if (userId >= 0)
            {
                if (!CHCNetSDKCard.NET_DVR_CloseAlarmChan_V30(userId))
                {
                    var iLastErr = CHCNetSDKCard.NET_DVR_GetLastError();
                    _logger.LogError($"{logTip} 撤防失败，错误号：{iLastErr}"); //撤防失败，输出错误号 
                }
                else
                {
                    isDeployEvent = false;
                }
            }
            else
            {
                _logger.LogWarning("未登录");
            }

            return Task.FromResult(true);
        }


        public void StopRemoteConfig(ref int m_lCfgHandle)
        {
            _logger.LogInformation($"停止长连接：m_lCfgHandle:{m_lCfgHandle} ");
            if (m_lCfgHandle >= 0)
            {
                var result = CHCNetSDKCard.NET_DVR_StopRemoteConfig(m_lCfgHandle);
                _logger.LogInformation($"停止长连接完成：result:{result} ");
            }
            m_lCfgHandle = -1;
        }
    }
}
