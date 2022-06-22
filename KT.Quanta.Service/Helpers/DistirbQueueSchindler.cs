using ContralServer.CfgFileRead;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Hikvision;
using KT.Quanta.Service.Devices.Hikvision.Models;
using KT.Quanta.Service.Devices.Schindler.Clients;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Turnstile.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Helpers
{
    public class DistirbQueueSchindler
    {
        public SchindlerQueue _SchindlerQueue;
        public HiKiVison _HiKiVison;
        public List<HiKiVison> _ListHiKiVison;
        private int WaitSleep;
        public DistirbQueueSchindler()
        {

            WaitSleep = Convert.ToInt32(AppConfigurtaionServices.Configuration["WaitSleep"].ToString());
        }
        public void Init()
        {
            _SchindlerQueue = new SchindlerQueue();
            _HiKiVison = new HiKiVison();
            _ListHiKiVison = new List<HiKiVison>();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = WaitSleep;
            timer.Elapsed += RunQueue;
            timer.Enabled = true;
            timer.Start();
        }
        private async void RunQueue(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_SchindlerQueue.CommunicateDevices.Count > 0)
            {
                ICommunicateDevice item = _SchindlerQueue.CommunicateDevices[0];
                byte[] Data;
                Data = (byte[])_SchindlerQueue.Datas[0].Clone();
                _SchindlerQueue.CommunicateDevices.RemoveAt(0);
                _SchindlerQueue.Datas.RemoveAt(0);
                var client = item.GetLoginUserClient<ISchindlerDatabaseClientHost>();
                await client.SendAsync(Data);
            }
            /*else if (_ListHiKiVison.Count > 0)
            { 
                HiKiVison H = new HiKiVison();
                H = _ListHiKiVison[0];
                _ListHiKiVison.RemoveAt(0);
                var model = (HikvisionAddOrUpdatePassRightQuery<TurnstilePassRightModel>)H.queueModel.Data;
                var hikvisionSdkService = H.CommunicateDevices.GetLoginUserClient<IHikvisionSdkService>();
                await AddOrUpdatePassRightAsync(remoteDevice, hikvisionSdkService, model.Model, model.Face);
            }*/
        }
        public class SchindlerQueue
        {
            public List<ICommunicateDevice> CommunicateDevices { get; set; } = new List<ICommunicateDevice>();
            public List<byte[]> Datas { get; set; } = new List<byte[]>();
        }

        public class HiKiVison
        {
            public IRemoteDevice remoteDevice { get; set; }
            public HikvisionDeviceExecuteQueueModel queueModel { get; set; }
            public ICommunicateDevice CommunicateDevices { get; set; } 
            public string Mothed { get; set; }
        }
    }
}
