
using ContralServer.CfgFileRead;
using HelperTools;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Common.Helper
{
    public class OnlyDataQueue
    {
        public List<QueuqModel> _QueCardData ;
        public QueuqModel RunModel;
        private int WaitSleep;
        public void Init()
        {
            RunModel=new QueuqModel();
            _QueCardData = new List<QueuqModel>();
            WaitSleep = Convert.ToInt32(AppConfigurtaionServices.Configuration["WaitSleep"].ToString());
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = WaitSleep;
            timer.Elapsed += RunQueue;
            timer.Enabled = true;
            timer.Start();
        }
        private void RunQueue(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                QueuqModel Req = new QueuqModel();
                if (_QueCardData.Count > 0)
                {
                    Req = _QueCardData[0];
                    _QueCardData.RemoveAt(0);
                    RestApi Re = new RestApi();
                    var Rp = Re.CreateRequest(Req.RunUrl, Method.POST, "JSON");
                    Rp.AddParameter("application/json", Req.RunBody, ParameterType.RequestBody);
                    var result = Re.SendAsync(Rp);
                }
            }
            catch (Exception ex)
            { 
            }
        }
        public class QueuqModel
        { 
            public string RunMode { get; set; }
            public string RunBody { get; set; }
            public string RunUrl { get; set; }
        }
        
    }
}
