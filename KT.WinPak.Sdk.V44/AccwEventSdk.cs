using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using Accw;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.WinPak.SDK.Helpers;
using KT.WinPak.SDK.Models;
using KT.WinPak.SDK.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KT.WinPak.SDK
{
    public class AccwEventSdk
    {
        private WPCallbackClient client;
        private MTSCBServerClass server;

        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        private delegate void InsertMessageCallback(string text);

        private Thread _messageProcessor;

        private readonly ILogger<AccwEventSdk> _logger;
        private OpenApi _openApi;
        private AppSettings _appSettings;

        public AccwEventSdk(ILogger<AccwEventSdk> logger,
            OpenApi openApi,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _openApi = openApi;
            _appSettings = appSettings.Value;

            try
            {
                client = new WPCallbackClient();
                server = new MTSCBServerClass();
            }
            catch (Exception ex)
            {
                _logger.LogError("启动事件对象失败：ex:{0} ", ex);
            }
        }

        private bool _isLogin = false;
        private string _account;
        private string _password;
        public void Login(string account, string password)
        {
            _account = account;
            _password = password;

            if (_isLogin)
            {
                try
                {
                    Logout();
                }
                catch (Exception ex)
                {
                    _logger?.LogError("推出事件失败：{0} ", ex);
                }
                finally
                {
                    try
                    {
                        client = new WPCallbackClient();
                        server = new MTSCBServerClass();
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogError("推出事件失败1：{0} ", ex);
                    }
                }
            }

            int iUserID = 1;
            bool bConnected = false;

            try
            {
                bConnected = server.InitServer(client, 3, account, password, iUserID);
                _isLogin = true;
                //bConnected = server.InitServer2(client, 3, account, password, "", iUserID);
            }
            catch (Exception ex)
            {
                _logger?.LogError("开启事件失败：{0} ", ex);
                throw;
            }

            if (bConnected)
            {
                _messageProcessor = new Thread(ProcessMessages);
                _messageProcessor.IsBackground = true;
                _messageProcessor.Start();
            }
        }

        public void Logout()
        {
            server?.DoneServer(client);
        }

        private void ProcessMessages()
        {
            try
            {
                while (true)
                {
                    lock (client.messageQueue)
                    {
                        if (client.messageQueue.Count > 0)
                        {
                            string sMessage = client.messageQueue.Dequeue();
                            InsertMessage(sMessage);
                        }
                        else
                        {
                            Thread.Sleep(100);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("事件服务错误：{0} ", ex);
                Thread.Sleep(1000);
            }
        }


        private void InsertMessage(string message)
        {
            _logger?.LogInformation("事件上传接收数据：{0}", message);

            var eventModel = Deserialize<EventModel>(message);

            //未验证通过的数据
            var isNotDataVal = eventModel == null || string.IsNullOrEmpty(eventModel.DeviceID) || string.IsNullOrEmpty(eventModel.CardNumber);
            if (isNotDataVal)
            {
                _logger.LogInformation("未验证通过的数据：DeviceID:{0} CardNumber:{1} ", eventModel.DeviceID, eventModel.CardNumber);
                return;
            }

            //是否是有效卡
            if (!_appSettings.PushStatus.Contains(eventModel.Status))
            {
                _logger.LogInformation("不是有效的上传数据：status:{0} ", eventModel.Status);
                return;
            }

            PushPassRecordModel passRecord = new PushPassRecordModel();
            /// 人脸通行时的抓拍图片
            passRecord.File = null;
            /// 设备ID，除了人脸摄像头、门禁读卡器是第三方系统的设备ID，其余都系本系统的设备ID
            passRecord.EquipmentId = eventModel.HID;
            /// 扩展字段，在人脸摄像头时，该字段是人脸服务器的token
            passRecord.Extra = string.Empty;
            /// 设备类型
            passRecord.EquipmentType = "ACCESS_READER";
            /// 通行类型
            passRecord.AccessType = "IC_CARD";
            /// 通行码，IC卡、二维码、人脸ID
            passRecord.AccessToken = eventModel.CardNumber;
            /// 通行时间，2019-11-06 15:20:45
            passRecord.AccessDate = string.Format("{0} {1}", eventModel.Date, eventModel.Time);

            passRecord.AccessDate = DateTime.Parse(passRecord.AccessDate).ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                _openApi.PushRecord(SdkStaticInfo.PushUrl, passRecord).Wait();
            }
            catch (Exception ex)
            {
                _logger.LogError("上传数据错误：{0} ", ex);
            }

            //if (this.InvokeRequired)
            //{
            //    this.Invoke(new InsertMessageCallback(InsertMessage), new object[] { message });
            //    return;
            //}

            //listMessages.Items.Add(message);DESerializer
        }
        public T Deserialize<T>(string xml) where T : class
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    var deserializer = serializer.Deserialize(sr);
                    return deserializer as T;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError("事件转实体错误：{0} ", ex);
                return null;
            }
        }

    }
}
