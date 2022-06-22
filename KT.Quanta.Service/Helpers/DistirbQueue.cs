using ContralServer.CfgFileRead;
using KT.Quanta.Service.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Helpers
{
    public class DistirbQueue
    {
   
        public DisbSend _DisbSend;
        public List<DisbSend> ListDisbSend;
        private IHubContext<QuantaDistributeHub> _distributeHub;
        private int WaitSleep;
        public DistirbQueue(IHubContext<QuantaDistributeHub> distributeHub)
        {
            _distributeHub = distributeHub;
            WaitSleep= Convert.ToInt32(AppConfigurtaionServices.Configuration["WaitSleep"].ToString());
        }
        public void Init()
        {    
            _DisbSend = new DisbSend();
            ListDisbSend = new List<DisbSend>();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = WaitSleep;
            timer.Elapsed += RunQueue;
            timer.Enabled = true;
            timer.Start();
        }
        private async void RunQueue(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                DisbSend Req = new DisbSend();
                if (ListDisbSend.Count > 0)
                {
                    Req = ListDisbSend[0];
                    ListDisbSend.RemoveAt(0);
                    switch (Req.sourceReq)
                    {
                        case "Elevator_CardDeviceController":
                            if (Req.Mothed == "AddOrEditCardDevice")
                            {
                                await _distributeHub.Clients.Client(Req.ConnectionId).SendAsync("AddOrEditCardDevice", Req.data);
                            }
                            else if (Req.Mothed == "DeleteCardDevice")
                            {
                                await _distributeHub.Clients.Client(Req.ConnectionId).SendAsync("DeleteCardDevice", Req.id, Req.time);
                            }
                            break;
                        case "Elevator_PassRightController":
                            if (Req.Mothed == "AddOrEditPassRight")
                            {
                                await _distributeHub.Clients.Client(Req.ConnectionId).SendAsync("AddOrEditPassRight", Req.data);
                            }
                            else if (Req.Mothed == "DeletePassRight")
                            {
                                await _distributeHub.Clients.Client(Req.ConnectionId).SendAsync("DeletePassRight", Req.id, Req.time);
                            }
                            break;
                        case "Elevator_PassRightController_Qutan":
                            if (Req.Mothed == "AddOrEditPassRight")
                            {
                                await _distributeHub.Clients.Client(Req.ConnectionId).SendAsync("AddOrEditPassRight", Req.data);
                            }
                            else if (Req.Mothed == "DeletePassRight")
                            {
                                await _distributeHub.Clients.Client(Req.ConnectionId).SendAsync("DeletePassRight", Req.id, Req.time);
                            }
                            break;
                        case "TurnstileCardDeviceController":
                            if (Req.Mothed == "AddOrEditCardDevice")
                            {
                                await _distributeHub.Clients.Client(Req.ConnectionId).SendAsync("AddOrEditCardDevice", Req.data);
                            }
                            else if (Req.Mothed == "DeleteCardDevice")
                            {
                                await _distributeHub.Clients.Client(Req.ConnectionId).SendAsync("DeleteCardDevice", Req.id, Req.time);
                            }
                            break;
                        case "TurnstileCardDeviceRightGroupService":
                            if (Req.Mothed == "AddOrEditRightGroup")
                            {
                                await _distributeHub.Clients.Client(Req.ConnectionId).SendAsync("AddOrEditRightGroup", Req.data);
                            }
                            else if (Req.Mothed == "DeleteRightGroup")
                            {
                                await _distributeHub.Clients.Client(Req.ConnectionId).SendAsync("DeleteRightGroup", Req.id, Req.time);
                            }
                            break;
                    }
                    
                }
            }
            catch (Exception ex)
            {
            }
        }

        public class DisbSend
        {
            public string ConnectionId { get; set; }
            public string Mothed { get; set; }
            public object data { get; set; } = new object();
            public string id { get; set; }
            public long time { get; set; }
            public string sourceReq { get; set; }
        }
    }
}
