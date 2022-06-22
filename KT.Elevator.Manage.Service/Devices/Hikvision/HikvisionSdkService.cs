using KT.Common.Core.Enums;
using KT.Common.Core.Utils;
using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Devices.Hikvision.Models;
using KT.Elevator.Manage.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace KT.Elevator.Manage.Service.Devices.Hikvision
{
    /// <summary>
    /// 海康SDK
    /// </summary>
    public class HikvisionSdkService : IHikvisionSdkService
    {
        //是否初始化，初始化只操作一次，所以设置成静态
        private bool _isInit = false;

        private IRemoteDevice _remoteDevice;

        private int m_UserID = -1;

        private int m_lGetCardCfgHandle = -1;
        private int m_lSetCardCfgHandle = -1;
        private int m_lDelCardCfgHandle = -1;

        private int m_lGetFaceCfgHandle = -1;
        private int m_lSetFaceCfgHandle = -1;

        private ILogger<HikvisionSdkService> _logger;

        private const uint XML_ABILITY_OUT_LEN = 3 * 1024 * 1024;

        private IServiceScopeFactory _serviceScopeFactory;
        public HikvisionSdkService(ILogger<HikvisionSdkService> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<int> LoginAsync(IRemoteDevice remoteDevice, string account, string password)
        {
            _remoteDevice = remoteDevice;
            return await Task.Run(() =>
            {
                if (!_isInit)
                {
                    //初始化Sdk
                    if (CHCNetSDK.NET_DVR_Init() == false)
                    {
                        _logger.LogError("Hikvision SDK NET_DVR_Init error!");
                        return -1;
                    }
                    var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                    CHCNetSDK.NET_DVR_SetLogToFile(3, logPath, false);
                }

                //已登录，退出登录
                if (m_UserID >= 0)
                {
                    CHCNetSDK.NET_DVR_Logout_V30(m_UserID);
                    m_UserID = -1;
                }

                CHCNetSDK.NET_DVR_USER_LOGIN_INFO struLoginInfo = new CHCNetSDK.NET_DVR_USER_LOGIN_INFO();
                CHCNetSDK.NET_DVR_DEVICEINFO_V40 struDeviceInfoV40 = new CHCNetSDK.NET_DVR_DEVICEINFO_V40();
                struDeviceInfoV40.struDeviceV30.sSerialNumber = new byte[CHCNetSDK.SERIALNO_LEN];

                struLoginInfo.sDeviceAddress = remoteDevice.RemoteDeviceInfo.IpAddress;
                struLoginInfo.sUserName = account;
                struLoginInfo.sPassword = password;
                struLoginInfo.wPort = (ushort)remoteDevice.RemoteDeviceInfo.Port;

                m_UserID = CHCNetSDK.NET_DVR_Login_V40(ref struLoginInfo, ref struDeviceInfoV40);
                if (m_UserID >= 0)
                {
                    //登录成功
                    _logger.LogInformation("Login Successful");

                    return m_UserID;
                }

                uint nErr = CHCNetSDK.NET_DVR_GetLastError();
                if (nErr == CHCNetSDK.NET_DVR_PASSWORD_ERROR)
                {
                    _logger.LogError("user name or password error!");
                    if (1 == struDeviceInfoV40.bySupportLock)
                    {
                        string strTemp1 = string.Format("Left {0} try opportunity", struDeviceInfoV40.byRetryLoginTime);
                        _logger.LogError(strTemp1);
                    }
                }
                else if (nErr == CHCNetSDK.NET_DVR_USER_LOCKED)
                {
                    if (1 == struDeviceInfoV40.bySupportLock)
                    {
                        string strTemp1 = string.Format("user is locked, the remaining lock time is {0}", struDeviceInfoV40.dwSurplusLockTime);
                        _logger.LogError(strTemp1);
                    }
                }
                else
                {
                    _logger.LogError($"Hikvision login erro:{nErr} !");
                }

                return m_UserID;
            });
        }

        public async Task<bool> SetCardAsync(HikvisionPersonCard personCard)
        {
            var result = false;
            if (m_lSetCardCfgHandle != -1)
            {
                if (CHCNetSDK.NET_DVR_StopRemoteConfig(m_lSetCardCfgHandle))
                {
                    m_lSetCardCfgHandle = -1;
                }
            }

            CHCNetSDK.NET_DVR_CARD_COND struCond = new CHCNetSDK.NET_DVR_CARD_COND();
            struCond.Init();
            struCond.dwSize = (uint)Marshal.SizeOf(struCond);
            struCond.dwCardNum = 1;
            IntPtr ptrStruCond = Marshal.AllocHGlobal((int)struCond.dwSize);
            Marshal.StructureToPtr(struCond, ptrStruCond, false);

            m_lSetCardCfgHandle = CHCNetSDK.NET_DVR_StartRemoteConfig(m_UserID, CHCNetSDK.NET_DVR_SET_CARD, ptrStruCond, (int)struCond.dwSize, null, IntPtr.Zero);
            if (m_lSetCardCfgHandle < 0)
            {
                _logger.LogError("NET_DVR_SET_CARD error:" + CHCNetSDK.NET_DVR_GetLastError());
                Marshal.FreeHGlobal(ptrStruCond);
                return result;
            }
            else
            {
                result = await SendCardDataAsync(personCard);
                Marshal.FreeHGlobal(ptrStruCond);
            }
            return result;
        }

        private async Task<bool> SendCardDataAsync(HikvisionPersonCard personCard)
        {
            return await Task.Run(() =>
            {
                var result = false;
                CHCNetSDK.NET_DVR_CARD_RECORD struData = new CHCNetSDK.NET_DVR_CARD_RECORD();
                struData.Init();
                struData.dwSize = (uint)Marshal.SizeOf(struData);
                struData.byCardType = 1;
                byte[] byTempCardNo = new byte[CHCNetSDK.ACS_CARD_NO_LEN];
                byTempCardNo = System.Text.Encoding.UTF8.GetBytes(personCard.CardNo);
                for (int i = 0; i < byTempCardNo.Length; i++)
                {
                    struData.byCardNo[i] = byTempCardNo[i];
                }
                struData.wCardRightPlan[0] = personCard.CardRightPlan;
                struData.dwEmployeeNo = personCard.EmployeeNo;

                byte[] byTempName = new byte[CHCNetSDK.NAME_LEN];
                byTempName = System.Text.Encoding.Default.GetBytes(personCard.EmployeeName);
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
                struData.byDoorRight[0] = 1;
                struData.wCardRightPlan[0] = 1;
                IntPtr ptrStruData = Marshal.AllocHGlobal((int)struData.dwSize);
                Marshal.StructureToPtr(struData, ptrStruData, false);

                CHCNetSDK.NET_DVR_CARD_STATUS struStatus = new CHCNetSDK.NET_DVR_CARD_STATUS();
                struStatus.Init();
                struStatus.dwSize = (uint)Marshal.SizeOf(struStatus);
                IntPtr ptrdwState = Marshal.AllocHGlobal((int)struStatus.dwSize);
                Marshal.StructureToPtr(struStatus, ptrdwState, false);

                int dwState = (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS;
                uint dwReturned = 0;
                while (true)
                {
                    dwState = CHCNetSDK.NET_DVR_SendWithRecvRemoteConfig(m_lSetCardCfgHandle, ptrStruData, struData.dwSize, ptrdwState, struStatus.dwSize, ref dwReturned);
                    struStatus = (CHCNetSDK.NET_DVR_CARD_STATUS)Marshal.PtrToStructure(ptrdwState, typeof(CHCNetSDK.NET_DVR_CARD_STATUS));
                    if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_NEEDWAIT)
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                    else if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FAILED)
                    {
                        _logger.LogError("NET_DVR_SET_CARD fail error: " + CHCNetSDK.NET_DVR_GetLastError());
                    }
                    else if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS)
                    {
                        if (struStatus.dwErrorCode != 0)
                        {
                            _logger.LogError("NET_DVR_SET_CARD success but errorCode:" + struStatus.dwErrorCode);
                        }
                        else
                        {
                            _logger.LogInformation("NET_DVR_SET_CARD success");
                            result = true;
                        }
                    }
                    else if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FINISH)
                    {
                        _logger.LogInformation("NET_DVR_SET_CARD finish");
                        break;
                    }
                    else if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_EXCEPTION)
                    {
                        var errorCode = CHCNetSDK.NET_DVR_GetLastError();
                        _logger.LogError($"Hivision NET_DVR_SET_CARD exception error:{errorCode} ");
                        break;
                    }
                    else
                    {
                        var errorCode = CHCNetSDK.NET_DVR_GetLastError();
                        _logger.LogError($"Hivision unknown status error:{errorCode} ");
                        break;
                    }
                }
                CHCNetSDK.NET_DVR_StopRemoteConfig(m_lSetCardCfgHandle);
                m_lSetCardCfgHandle = -1;
                Marshal.FreeHGlobal(ptrStruData);
                Marshal.FreeHGlobal(ptrdwState);
                return result;
            });
        }

        public async Task GetCardAsync(string cardNo)
        {
            await Task.Run(() =>
            {
                HikvisionPersonCard personCard = null;
                if (m_lGetCardCfgHandle != -1)
                {
                    if (CHCNetSDK.NET_DVR_StopRemoteConfig(m_lGetCardCfgHandle))
                    {
                        m_lGetCardCfgHandle = -1;
                    }
                }
                CHCNetSDK.NET_DVR_CARD_COND struCond = new CHCNetSDK.NET_DVR_CARD_COND();
                struCond.Init();
                struCond.dwSize = (uint)Marshal.SizeOf(struCond);
                struCond.dwCardNum = 1;
                IntPtr ptrStruCond = Marshal.AllocHGlobal((int)struCond.dwSize);
                Marshal.StructureToPtr(struCond, ptrStruCond, false);

                CHCNetSDK.NET_DVR_CARD_RECORD struData = new CHCNetSDK.NET_DVR_CARD_RECORD();
                struData.Init();
                struData.dwSize = (uint)Marshal.SizeOf(struData);
                byte[] byTempCardNo = new byte[CHCNetSDK.ACS_CARD_NO_LEN];
                byTempCardNo = System.Text.Encoding.UTF8.GetBytes(cardNo);
                for (int i = 0; i < byTempCardNo.Length; i++)
                {
                    struData.byCardNo[i] = byTempCardNo[i];
                }
                IntPtr ptrStruData = Marshal.AllocHGlobal((int)struData.dwSize);
                Marshal.StructureToPtr(struData, ptrStruData, false);

                CHCNetSDK.NET_DVR_CARD_SEND_DATA struSendData = new CHCNetSDK.NET_DVR_CARD_SEND_DATA();
                struSendData.Init();
                struSendData.dwSize = (uint)Marshal.SizeOf(struSendData);
                for (int i = 0; i < byTempCardNo.Length; i++)
                {
                    struSendData.byCardNo[i] = byTempCardNo[i];
                }
                IntPtr ptrStruSendData = Marshal.AllocHGlobal((int)struSendData.dwSize);
                Marshal.StructureToPtr(struSendData, ptrStruSendData, false);
                m_lGetCardCfgHandle = CHCNetSDK.NET_DVR_StartRemoteConfig(m_UserID, CHCNetSDK.NET_DVR_GET_CARD, ptrStruCond, (int)struCond.dwSize, null, IntPtr.Zero);
                if (m_lGetCardCfgHandle < 0)
                {
                    _logger.LogError("NET_DVR_GET_CARD error: " + CHCNetSDK.NET_DVR_GetLastError());
                    Marshal.FreeHGlobal(ptrStruCond);
                    return;
                }
                else
                {
                    int dwState = (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS;
                    uint dwReturned = 0;
                    while (true)
                    {
                        dwState = CHCNetSDK.NET_DVR_SendWithRecvRemoteConfig(m_lGetCardCfgHandle, ptrStruSendData, struSendData.dwSize, ptrStruData, struData.dwSize, ref dwReturned);
                        struData = (CHCNetSDK.NET_DVR_CARD_RECORD)Marshal.PtrToStructure(ptrStruData, typeof(CHCNetSDK.NET_DVR_CARD_RECORD));
                        if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_NEEDWAIT)
                        {
                            Thread.Sleep(10);
                            continue;
                        }
                        else if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FAILED)
                        {
                            _logger.LogError("NET_DVR_GET_CARD fail error: " + CHCNetSDK.NET_DVR_GetLastError());
                        }
                        else if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS)
                        {
                            personCard = new HikvisionPersonCard();
                            personCard.CardNo = System.Text.Encoding.Default.GetString(struData.byCardNo);
                            personCard.CardRightPlan = struData.wCardRightPlan[0];
                            personCard.EmployeeNo = struData.dwEmployeeNo;
                            personCard.EmployeeName = System.Text.Encoding.Default.GetString(struData.byName);
                            _logger.LogError("NET_DVR_GET_CARD success");
                        }
                        else if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FINISH)
                        {
                            _logger.LogError("NET_DVR_GET_CARD finish");
                            break;
                        }
                        else if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_EXCEPTION)
                        {
                            _logger.LogError("NET_DVR_GET_CARD exception error: " + CHCNetSDK.NET_DVR_GetLastError());
                            break;
                        }
                        else
                        {
                            _logger.LogError("unknown status error: " + CHCNetSDK.NET_DVR_GetLastError());
                            break;
                        }
                    }
                }
                CHCNetSDK.NET_DVR_StopRemoteConfig(m_lGetCardCfgHandle);
                m_lGetCardCfgHandle = -1;
                Marshal.FreeHGlobal(ptrStruSendData);
                Marshal.FreeHGlobal(ptrStruData);
                return;
            });
        }

        public async Task DeleteCardAsync(string cardNo)
        {
            await Task.Run(() =>
            {
                if (m_lDelCardCfgHandle != -1)
                {
                    if (CHCNetSDK.NET_DVR_StopRemoteConfig(m_lDelCardCfgHandle))
                    {
                        m_lDelCardCfgHandle = -1;
                    }
                }
                CHCNetSDK.NET_DVR_CARD_COND struCond = new CHCNetSDK.NET_DVR_CARD_COND();
                struCond.Init();
                struCond.dwSize = (uint)Marshal.SizeOf(struCond);
                struCond.dwCardNum = 1;
                IntPtr ptrStruCond = Marshal.AllocHGlobal((int)struCond.dwSize);
                Marshal.StructureToPtr(struCond, ptrStruCond, false);

                CHCNetSDK.NET_DVR_CARD_SEND_DATA struSendData = new CHCNetSDK.NET_DVR_CARD_SEND_DATA();
                struSendData.Init();
                struSendData.dwSize = (uint)Marshal.SizeOf(struSendData);
                byte[] byTempCardNo = new byte[CHCNetSDK.ACS_CARD_NO_LEN];
                byTempCardNo = System.Text.Encoding.UTF8.GetBytes(cardNo);
                for (int i = 0; i < byTempCardNo.Length; i++)
                {
                    struSendData.byCardNo[i] = byTempCardNo[i];
                }
                IntPtr ptrStruSendData = Marshal.AllocHGlobal((int)struSendData.dwSize);
                Marshal.StructureToPtr(struSendData, ptrStruSendData, false);

                CHCNetSDK.NET_DVR_CARD_STATUS struStatus = new CHCNetSDK.NET_DVR_CARD_STATUS();
                struStatus.Init();
                struStatus.dwSize = (uint)Marshal.SizeOf(struStatus);
                IntPtr ptrdwState = Marshal.AllocHGlobal((int)struStatus.dwSize);
                Marshal.StructureToPtr(struStatus, ptrdwState, false);

                m_lGetCardCfgHandle = CHCNetSDK.NET_DVR_StartRemoteConfig(m_UserID, CHCNetSDK.NET_DVR_DEL_CARD, ptrStruCond, (int)struCond.dwSize, null, IntPtr.Zero);
                if (m_lGetCardCfgHandle < 0)
                {
                    _logger.LogError("NET_DVR_DEL_CARD error:" + CHCNetSDK.NET_DVR_GetLastError());
                    Marshal.FreeHGlobal(ptrStruCond);
                    return;
                }
                else
                {
                    int dwState = (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS;
                    uint dwReturned = 0;
                    while (true)
                    {
                        dwState = CHCNetSDK.NET_DVR_SendWithRecvRemoteConfig(m_lGetCardCfgHandle, ptrStruSendData, struSendData.dwSize, ptrdwState, struStatus.dwSize, ref dwReturned);
                        if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_NEEDWAIT)
                        {
                            Thread.Sleep(10);
                            continue;
                        }
                        else if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FAILED)
                        {
                            _logger.LogError("NET_DVR_DEL_CARD fail error: " + CHCNetSDK.NET_DVR_GetLastError());
                        }
                        else if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_SUCCESS)
                        {
                            _logger.LogInformation("NET_DVR_DEL_CARD success");
                        }
                        else if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_FINISH)
                        {
                            _logger.LogInformation("NET_DVR_DEL_CARD finish");
                            break;
                        }
                        else if (dwState == (int)CHCNetSDK.NET_SDK_SENDWITHRECV_STATUS.NET_SDK_CONFIG_STATUS_EXCEPTION)
                        {
                            _logger.LogError("NET_DVR_DEL_CARD exception error: " + CHCNetSDK.NET_DVR_GetLastError());
                            break;
                        }
                        else
                        {
                            _logger.LogError("unknown status error: " + CHCNetSDK.NET_DVR_GetLastError());
                            break;
                        }
                    }
                }
                CHCNetSDK.NET_DVR_StopRemoteConfig(m_lDelCardCfgHandle);
                m_lDelCardCfgHandle = -1;
                Marshal.FreeHGlobal(ptrStruSendData);
                Marshal.FreeHGlobal(ptrdwState);
            });
        }

        public async Task SetFaceAsync(string cardNo, string facePath, int cardReaderNo = 1)
        {
            await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(facePath))
                {
                    _logger.LogError("Please choose human Face path");
                    return;
                }

                CHCNetSDK.NET_DVR_FACE_COND struCond = new CHCNetSDK.NET_DVR_FACE_COND();
                struCond.init();
                struCond.dwSize = Marshal.SizeOf(struCond);
                struCond.dwFaceNum = 1;
                struCond.dwEnableReaderNo = cardReaderNo;
                byte[] byTemp = System.Text.Encoding.UTF8.GetBytes(cardNo);
                for (int i = 0; i < byTemp.Length; i++)
                {
                    struCond.byCardNo[i] = byTemp[i];
                }

                int dwInBufferSize = struCond.dwSize;
                IntPtr ptrstruCond = Marshal.AllocHGlobal(dwInBufferSize);
                Marshal.StructureToPtr(struCond, ptrstruCond, false);
                m_lSetFaceCfgHandle = CHCNetSDK.NET_DVR_StartRemoteConfig(m_UserID, CHCNetSDK.NET_DVR_SET_FACE, ptrstruCond, dwInBufferSize, null, IntPtr.Zero);
                if (-1 == m_lSetFaceCfgHandle)
                {
                    Marshal.FreeHGlobal(ptrstruCond);
                    _logger.LogError("NET_DVR_SET_FACE_FAIL, ERROR CODE" + CHCNetSDK.NET_DVR_GetLastError().ToString());
                    return;
                }

                CHCNetSDK.NET_DVR_FACE_RECORD struRecord = new CHCNetSDK.NET_DVR_FACE_RECORD();
                struRecord.init();
                struRecord.dwSize = Marshal.SizeOf(struRecord);

                byte[] byRecordNo = System.Text.Encoding.UTF8.GetBytes(cardNo);
                for (int i = 0; i < byRecordNo.Length; i++)
                {
                    struRecord.byCardNo[i] = byRecordNo[i];
                }

                ReadFaceData(facePath, ref struRecord);
                int dwInBuffSize = Marshal.SizeOf(struRecord);
                int dwStatus = 0;

                CHCNetSDK.NET_DVR_FACE_STATUS struStatus = new CHCNetSDK.NET_DVR_FACE_STATUS();
                struStatus.init();
                struStatus.dwSize = Marshal.SizeOf(struStatus);
                int dwOutBuffSize = struStatus.dwSize;
                IntPtr ptrOutDataLen = Marshal.AllocHGlobal(sizeof(int));
                bool Flag = true;
                while (Flag)
                {
                    dwStatus = CHCNetSDK.NET_DVR_SendWithRecvRemoteConfig(m_lSetFaceCfgHandle, ref struRecord, dwInBuffSize, ref struStatus, dwOutBuffSize, ptrOutDataLen);
                    switch (dwStatus)
                    {
                        //成功读取到数据，处理完本次数据后需调用next
                        case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_SUCCESS:
                            ProcessSetFaceData(ref struStatus, ref Flag);
                            break;
                        case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_NEED_WAIT:
                            break;
                        case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_FAILED:
                            CHCNetSDK.NET_DVR_StopRemoteConfig(m_lSetFaceCfgHandle);
                            _logger.LogError("NET_SDK_GET_NEXT_STATUS_FAILED" + CHCNetSDK.NET_DVR_GetLastError().ToString());
                            Flag = false;
                            break;
                        case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_FINISH:
                            CHCNetSDK.NET_DVR_StopRemoteConfig(m_lSetFaceCfgHandle);
                            Flag = false;
                            break;
                        default:
                            _logger.LogError("NET_SDK_GET_NEXT_STATUS_UNKOWN" + CHCNetSDK.NET_DVR_GetLastError().ToString());
                            Flag = false;
                            CHCNetSDK.NET_DVR_StopRemoteConfig(m_lSetFaceCfgHandle);
                            break;
                    }
                }

                Marshal.FreeHGlobal(ptrstruCond);
                Marshal.FreeHGlobal(ptrOutDataLen);
            });
        }

        public async Task<string> GetFaceAsync(string cardNo, int cardReaderNo = 0)
        {
            return await Task.Run(() =>
            {
                var facePath = string.Empty;

                if (m_lGetFaceCfgHandle != -1)
                {
                    CHCNetSDK.NET_DVR_StopRemoteConfig(m_lGetFaceCfgHandle);
                    m_lGetFaceCfgHandle = -1;
                }

                //if (pictureBoxFace.Image != null)
                //{
                //    pictureBoxFace.Image.Dispose();
                //    pictureBoxFace.Image = null;
                //}

                CHCNetSDK.NET_DVR_FACE_COND struCond = new CHCNetSDK.NET_DVR_FACE_COND();
                struCond.init();
                struCond.dwSize = Marshal.SizeOf(struCond);
                int dwSize = struCond.dwSize;
                struCond.dwEnableReaderNo = cardReaderNo;
                //人脸数量是1
                struCond.dwFaceNum = 1;
                byte[] byTemp = System.Text.Encoding.UTF8.GetBytes(cardNo);
                for (int i = 0; i < byTemp.Length; i++)
                {
                    struCond.byCardNo[i] = byTemp[i];
                }

                IntPtr ptrStruCond = Marshal.AllocHGlobal(dwSize);
                Marshal.StructureToPtr(struCond, ptrStruCond, false);

                m_lGetFaceCfgHandle = CHCNetSDK.NET_DVR_StartRemoteConfig(m_UserID, CHCNetSDK.NET_DVR_GET_FACE, ptrStruCond, dwSize, null, IntPtr.Zero);
                if (m_lGetFaceCfgHandle == -1)
                {
                    Marshal.FreeHGlobal(ptrStruCond);
                    _logger.LogError("NET_DVR_GET_FACE_FAIL, ERROR CODE" + CHCNetSDK.NET_DVR_GetLastError().ToString());
                    return facePath;
                }

                bool Flag = true;
                int dwStatus = 0;

                CHCNetSDK.NET_DVR_FACE_RECORD struRecord = new CHCNetSDK.NET_DVR_FACE_RECORD();
                struRecord.init();
                struRecord.dwSize = Marshal.SizeOf(struRecord);
                int dwOutBuffSize = struRecord.dwSize;
                while (Flag)
                {
                    dwStatus = CHCNetSDK.NET_DVR_GetNextRemoteConfig(m_lGetFaceCfgHandle, ref struRecord, dwOutBuffSize);
                    switch (dwStatus)
                    {
                        //成功读取到数据，处理完本次数据后需调用next
                        case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_SUCCESS:
                            facePath = ProcessFaceData(cardNo, ref struRecord, ref Flag);
                            break;
                        case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_NEED_WAIT:
                            break;
                        case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_FAILED:
                            CHCNetSDK.NET_DVR_StopRemoteConfig(m_lGetFaceCfgHandle);
                            _logger.LogError("NET_SDK_GET_NEXT_STATUS_FAILED" + CHCNetSDK.NET_DVR_GetLastError().ToString());
                            Flag = false;
                            break;
                        case CHCNetSDK.NET_SDK_GET_NEXT_STATUS_FINISH:
                            _logger.LogInformation("NET_SDK_GET_NEXT_STATUS_FINISH");
                            CHCNetSDK.NET_DVR_StopRemoteConfig(m_lGetFaceCfgHandle);
                            Flag = false;
                            break;
                        default:
                            _logger.LogError("NET_SDK_GET_STATUS_UNKOWN" + CHCNetSDK.NET_DVR_GetLastError().ToString());
                            Flag = false;
                            CHCNetSDK.NET_DVR_StopRemoteConfig(m_lGetFaceCfgHandle);
                            break;
                    }
                }

                Marshal.FreeHGlobal(ptrStruCond);

                return facePath;
            });
        }

        public async Task DeleteFaceAsync(string cardNo, int cardReaderNo = 1)
        {
            await Task.Run(() =>
            {
                CHCNetSDK.NET_DVR_FACE_PARAM_CTRL_CARDNO struCardNo = new CHCNetSDK.NET_DVR_FACE_PARAM_CTRL_CARDNO();
                struCardNo.init();
                struCardNo.dwSize = Marshal.SizeOf(struCardNo);
                struCardNo.byMode = 0;
                int dwSize = struCardNo.dwSize;
                byte[] byCardNo = System.Text.Encoding.UTF8.GetBytes(cardNo);
                for (int i = 0; i < byCardNo.Length; i++)
                {
                    struCardNo.struByCard.byCardNo[i] = byCardNo[i];
                }

                struCardNo.struByCard.byEnableCardReader[cardReaderNo - 1] = 1;

                for (int i = 0; i < CHCNetSDK.MAX_FACE_NUM; ++i)
                {
                    //全部写1删除人脸
                    struCardNo.struByCard.byFaceID[i] = 1;
                }

                if (false == CHCNetSDK.NET_DVR_RemoteControl(m_UserID, CHCNetSDK.NET_DVR_DEL_FACE_PARAM_CFG, ref struCardNo, dwSize))
                {
                    _logger.LogError("NET_SDK_DEL_FACE_FAILED" + CHCNetSDK.NET_DVR_GetLastError().ToString());
                }
                else
                {
                    _logger.LogInformation("NET_SDK_DEL_FACE_SUCCEED");
                }
            });
        }

        public async Task<int> GetCurACSDeviceDoorNumAsync()
        {
            return await Task.Run(() =>
            {
                //get device ability for door num ,read carder num is door num*2
                string exceptionInfo = null;
                int doorNum = 0;

                string pOutXMLBuf = GetACSDeviceAbility(m_UserID, CHCNetSDK.ACS_ABILITY, "<AcsAbility version=\"2.0\">\r\n</AcsAbility>");
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
                    catch (System.Xml.XmlException ex)
                    {
                        exceptionInfo = ex.Message;
                    }
                    finally
                    {
                        if (exceptionInfo != null)
                        {
                            _logger.LogError("operation exception!" + exceptionInfo, "Prompt");
                        }
                    }
                }
                return doorNum;
            });
        }

        private void ReadFaceData(string facePath, ref CHCNetSDK.NET_DVR_FACE_RECORD struRecord)
        {
            if (!File.Exists(facePath))
            {
                _logger.LogError("The face picture does not exist!");
                return;
            }
            FileStream fs = new FileStream(facePath, FileMode.OpenOrCreate);
            if (0 == fs.Length)
            {
                _logger.LogError("The face picture is 0k,please input another picture!");
                return;
            }
            if (200 * 1024 < fs.Length)
            {
                _logger.LogError("The face picture is larger than 200k,please input another picture!");
                return;
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
                _logger.LogError($"Read Face Data failed:ex:{ex}");
                fs.Close();
                return;
            }
        }

        private void ProcessSetFaceData(ref CHCNetSDK.NET_DVR_FACE_STATUS struStatus, ref bool flag)
        {
            switch (struStatus.byRecvStatus)
            {
                case 1:
                    _logger.LogInformation("SetFaceDataSuccessful");
                    break;
                default:
                    flag = false;
                    _logger.LogError("NET_SDK_SET_Face_DATA_FAILED" + struStatus.byRecvStatus.ToString());
                    break;
            }
        }

        private string ProcessFaceData(string cardNo, ref CHCNetSDK.NET_DVR_FACE_RECORD struRecord, ref bool flag)
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
                CHCNetSDK.NET_DVR_StopRemoteConfig(m_lGetFaceCfgHandle);
                _logger.LogError($"ProcessFingerData failed:ex:{ex}");
            }
            return string.Empty;
        }

        private string GetACSDeviceAbility(int lUserID, int iAbilityType, string inParam)
        {
            String exceptionInfo = null;
            String errorinfo = null;
            int dwLastError = 0;

            IntPtr ptrCfgVer;
            IntPtr m_pOutBuf;
            bool dwReturnValue = false;
            uint dwInSize = 0;

            string pOutXMLBuf = null;
            string strInputParam = inParam;

            m_pOutBuf = Marshal.AllocHGlobal((Int32)XML_ABILITY_OUT_LEN);
            try
            {


                dwInSize = (uint)strInputParam.Length;
                ptrCfgVer = Marshal.StringToCoTaskMemAnsi(strInputParam);
                dwReturnValue = CHCNetSDK.NET_DVR_GetDeviceAbility(lUserID, (uint)iAbilityType, ptrCfgVer, dwInSize, m_pOutBuf, XML_ABILITY_OUT_LEN);
                Marshal.FreeHGlobal(ptrCfgVer);

                if (dwReturnValue)
                {
                    pOutXMLBuf = Marshal.PtrToStringAnsi(m_pOutBuf);
                }

                dwLastError = (int)CHCNetSDK.NET_DVR_GetLastError();
                errorinfo = Marshal.PtrToStringAnsi(CHCNetSDK.NET_DVR_GetErrorMsg(ref dwLastError));

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
                    _logger.LogError("operation exception, please check the exception information" + exceptionInfo, "Prompt");
                }
                else
                {
                    if (dwReturnValue)
                    {
                        _logger.LogInformation($"ACS_ABILITY:Get ability success!OPERATION_SUCC_T:{AcsDemoPublic.OPERATION_SUCC_T}");
                        //MessageBox.Show("Get ability success!", "Prompt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _logger.LogError("Get ability failed! error code：" + dwLastError + ",error message:" + errorinfo, "Prompt");
                    }
                }
            }
            return pOutXMLBuf;
        }

        public async Task DeployEventAsync()
        {
            await Task.Run(() =>
            {
                CHCNetSDK.NET_DVR_SETUPALARM_PARAM struSetupAlarmParam = new CHCNetSDK.NET_DVR_SETUPALARM_PARAM();
                struSetupAlarmParam.dwSize = (uint)Marshal.SizeOf(struSetupAlarmParam);
                struSetupAlarmParam.byLevel = 1;
                struSetupAlarmParam.byAlarmInfoType = 1;
                struSetupAlarmParam.byDeployType = 0;

                if (CHCNetSDK.NET_DVR_SetupAlarmChan_V41(m_UserID, ref struSetupAlarmParam) < 0)
                {
                    _logger.LogError("NET_DVR_SetupAlarmChan_V41 fail error: " + CHCNetSDK.NET_DVR_GetLastError(), "Setup alarm channel failed");

                    return;
                }
                else
                {
                    _logger.LogInformation("Setup alarm channel succeed");
                }

                var m_falarmData = new CHCNetSDK.MSGCallBack(MsgCallback);
                if (CHCNetSDK.NET_DVR_SetDVRMessageCallBack_V50(0, m_falarmData, IntPtr.Zero))
                {
                    _logger.LogInformation("NET_DVR_SetDVRMessageCallBack_V50 success!");
                }
                else
                {
                    _logger.LogError("NET_DVR_SetDVRMessageCallBack_V50 fail!");
                }
            });
        }


        private void MsgCallback(int lCommand, ref CHCNetSDK.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        {
            switch (lCommand)
            {
                case CHCNetSDK.COMM_ALARM_ACS:
                    ProcessCommAlarmACS(ref pAlarmer, pAlarmInfo, dwBufLen, pUser);
                    break;
                default:
                    break;
            }
        }

        private void ProcessCommAlarmACS(ref CHCNetSDK.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        {
            var struAcsAlarmInfo = (CHCNetSDK.NET_DVR_ACS_ALARM_INFO)Marshal.PtrToStructure(pAlarmInfo, typeof(CHCNetSDK.NET_DVR_ACS_ALARM_INFO));
            var struFileInfo = new CHCNetSDK.NET_DVR_LOG_V30();
            struFileInfo.dwMajorType = struAcsAlarmInfo.dwMajor;
            struFileInfo.dwMinorType = struAcsAlarmInfo.dwMinor;
            char[] csTmp = new char[256];

            if (CHCNetSDK.MAJOR_ALARM == struFileInfo.dwMajorType)
            {
                HikvisionTypeMap.AlarmMinorTypeMap(struFileInfo, csTmp);
            }
            else if (CHCNetSDK.MAJOR_OPERATION == struFileInfo.dwMajorType)
            {
                HikvisionTypeMap.OperationMinorTypeMap(struFileInfo, csTmp);
            }
            else if (CHCNetSDK.MAJOR_EXCEPTION == struFileInfo.dwMajorType)
            {
                HikvisionTypeMap.ExceptionMinorTypeMap(struFileInfo, csTmp);
            }
            else if (CHCNetSDK.MAJOR_EVENT == struFileInfo.dwMajorType)
            {
                HikvisionTypeMap.EventMinorTypeMap(struFileInfo, csTmp);
            }

            /**************************************************/
            _logger.LogInformation($"Hikvision 接收数据1：{JsonConvert.SerializeObject(struAcsAlarmInfo, JsonUtil.JsonSettings)} ");

            var passRecord = new PassRecordModel();
            passRecord.DeviceId = _remoteDevice.RemoteDeviceInfo.DeviceId;
            passRecord.DeviceType = _remoteDevice.RemoteDeviceInfo.DeviceType;

            string szInfoBuf = string.Empty;

            passRecord.PassLocalTime = string.Format("{0}-{1:D2}-{2} {3:D2}:{4:D2}:{5:D2}", struAcsAlarmInfo.struTime.dwYear, struAcsAlarmInfo.struTime.dwMonth,
                struAcsAlarmInfo.struTime.dwDay, struAcsAlarmInfo.struTime.dwHour, struAcsAlarmInfo.struTime.dwMinute, struAcsAlarmInfo.struTime.dwSecond);

            if (struAcsAlarmInfo.struAcsEventInfo.byCardNo[0] != 0)
            {
                passRecord.PassRightSign = System.Text.Encoding.UTF8.GetString(struAcsAlarmInfo.struAcsEventInfo.byCardNo).TrimEnd('\0');
            }
            else
            {
                _logger.LogInformation($"状态不符合的上传数据：卡号为空！ ");
                return;
            }

            //string[] szCardType = { "normal card", "disabled card", "blacklist card", "night watch card", "stress card", "super card", "guest card" };
            byte byCardType = struAcsAlarmInfo.struAcsEventInfo.byCardType;
            if (byCardType == 2 || byCardType == 3)
            {
                _logger.LogInformation($"状态不符合的上传数据：hivisoion card number:{passRecord.PassRightSign} card type:{byCardType} ");
                return;
            }

            if (struAcsAlarmInfo.struAcsEventInfo.dwCardReaderNo != 0)
            {
                passRecord.DeviceId = struAcsAlarmInfo.struAcsEventInfo.dwCardReaderNo.ToString();
                szInfoBuf = szInfoBuf + "+Card Reader Number:" + passRecord.DeviceId;
            }
            if (struAcsAlarmInfo.struAcsEventInfo.dwDoorNo != 0)
            {
                passRecord.Remark = struAcsAlarmInfo.struAcsEventInfo.dwDoorNo.ToString();
            }
            if (struAcsAlarmInfo.struAcsEventInfo.dwEmployeeNo != 0)
            {
                szInfoBuf = szInfoBuf + "+EmployeeNo:" + struAcsAlarmInfo.struAcsEventInfo.dwEmployeeNo;
            }
            if (struAcsAlarmInfo.struAcsEventInfo.byDeviceNo != 0)
            {
                szInfoBuf = szInfoBuf + "+byDeviceNo:" + struAcsAlarmInfo.struAcsEventInfo.byDeviceNo.ToString();
            }
            //其它消息先不罗列了......
            _logger.LogInformation($"Hikvision 接收数据2：{szInfoBuf} ");

            if (struAcsAlarmInfo.dwPicDataLen > 0)
            {
                var fileName = $"{IdUtil.NewId()}.bmp";
                var fileFullName = PathUtil.GetFileRandomFullName(PathEnum.RECORD_IMAGE_PATH, fileName);
                using (FileStream fs = new FileStream(fileFullName, FileMode.Create))
                {
                    int iLen = (int)struAcsAlarmInfo.dwPicDataLen;
                    byte[] by = new byte[iLen];
                    Marshal.Copy(struAcsAlarmInfo.pPicData, by, 0, iLen);

                    passRecord.FaceImage = by;
                    passRecord.FaceImageSize = iLen;

                    fs.Write(by, 0, iLen);
                    fs.Close(); 
                }

                passRecord.AccessType = CardTypeEnum.FACE.Value;
            }
            else
            {
                passRecord.AccessType = CardTypeEnum.IC_CARD.Value;
            }

            //异步 上传事件
            UploadPassRecordAsync(passRecord); 
        }

        private async Task UploadPassRecordAsync(PassRecordModel passRecord)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var serivce = scope.ServiceProvider.GetRequiredService<IHikvisionService>();
                await serivce.UploadPassRecordAsync(passRecord);
            };
        }
    }
}
