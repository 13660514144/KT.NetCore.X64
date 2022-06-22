using ContralServer.CfgFileRead;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Services;
using KT.Quanta.Service.Turnstile.Dtos;
using KT.Quanta.Service.Turnstile.Services;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Common.Helper
{
    public class SendClient
    {
     
        private ITurnstilePersonService _personService;
        private ITurnstilePassRightService _service;
        private IPersonService _person;
        private IPassRightService _passRightService;
        private IServiceProvider _serviceProvider;
        //private NetFlowTimer _NetFlowTimer;
        private int WaitSleep;
        //private ILogger _log;
        public SendClient(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var _scope = _serviceProvider.CreateScope();
            _personService = _scope.ServiceProvider.GetRequiredService<ITurnstilePersonService>(); 
            _service = _scope.ServiceProvider.GetRequiredService<ITurnstilePassRightService>(); 
            _person = _scope.ServiceProvider.GetRequiredService<IPersonService>(); 
            _passRightService = _scope.ServiceProvider.GetRequiredService<IPassRightService>();
            //_NetFlowTimer = _scope.ServiceProvider.GetRequiredService<NetFlowTimer>();
            //_log= _scope.ServiceProvider.GetRequiredService<ILogger>();
            WaitSleep = Convert.ToInt32(AppConfigurtaionServices.Configuration["WaitSleep"].ToString());
        }
        public void Init()
        {
            _PrightModel = new Pright();
            _PerModel = new Per();
            _TpassModel = new Tpass();
            _TperModel = new Tper();

            _ListPright = new List<Pright>();
            _ListPer = new List<Per>();
            _ListTpass = new List<Tpass>();
            _ListTper = new List<Tper>();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = WaitSleep;
            timer.Elapsed += SendToClient;
            timer.Enabled = true;
            timer.Start();
        }
        public void SendToClient(object sender, System.Timers.ElapsedEventArgs e)
        {
            /*流量超限不执行
            if (_NetFlowTimer._SleepInOut.OutOctetsCurrent > _NetFlowTimer._SleepInOut.FlowValueLimits)
            {
                return;
            }
            */
            //_log.LogInformation($"start");
            Task.Run(()=> {
                if (_ListPright.Count > 0)
                {
                    Pright O = new Pright();
                    O.WorkModel = _ListPright[0].WorkModel;
                    O.Ele = _ListPright[0].Ele;
                    _ListPright.RemoveAt(0);
                    try
                    {
                        if (O.WorkModel == "DEL")
                        {
                            _passRightService.DeleteAsync(O.Ele.Id);
                        }
                        else
                        {
                            var result = _passRightService.AddOrEditAsync(O.Ele);
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    Console.WriteLine($"发送队列数据包OVER 等待下一次");
                }
                else if (_ListPer.Count > 0)
                {
                    Per pE = new Per();
                    pE.WorkModel = _ListPer[0].WorkModel;
                    pE.Pmodel = _ListPer[0].Pmodel;
                    _ListPer.RemoveAt(0);
                    try
                    {
                        if (pE.WorkModel == "DEL")
                        {
                            _person.DeleteAsync(pE.Pmodel.Id);
                        }
                        else
                        {
                            var result = _person.AddOrEditAsync(pE.Pmodel);
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    Console.WriteLine($"发送队列数据包OVER 等待下一次");
                }
                else if (_ListTpass.Count > 0)
                {
                    Tpass Tp = new Tpass();
                    Tp.WorkModel = _ListTpass[0].WorkModel;
                    Tp.Tpright = _ListTpass[0].Tpright;
                    _ListTpass.RemoveAt(0);
                    try
                    {
                        if (Tp.WorkModel == "DEL")
                        {
                            _service.DeleteAsync(Tp.Tpright);
                        }
                        else
                        {
                            var result = _service.AddOrEditAsync(Tp.Tpright);
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    Console.WriteLine($"发送队列数据包OVER 等待下一次");
                }
                else if (_ListTper.Count > 0)
                {
                    Tper tper = new Tper();
                    tper.WorkModel = _ListTper[0].WorkModel;
                    tper.Tperson = _ListTper[0].Tperson;
                    _ListTper.RemoveAt(0);
                    try
                    {
                        if (tper.WorkModel == "ADDOREDIT")
                        {
                            var result = _personService.AddOrEditAsync(tper.Tperson);
                        }
                        else
                        {
                            _personService.DeleteAsync(tper.Tperson.Id);
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    Console.WriteLine($"发送队列数据包 _ListTper OVER 等待下一次");
                }
            });
            
        }
        
        public class Pright
        {
            public string WorkModel { get; set; }
            public PassRightModel Ele { get; set; } = new PassRightModel();
        }
        public List<Pright> _ListPright;
        public Pright _PrightModel;

        public class Per
        { 
            public string WorkModel { get; set; }
            public PersonModel Pmodel { get; set; } = new PersonModel();
        }
        public List<Per> _ListPer;
        public Per _PerModel;

        public class Tpass
        {
            public string WorkModel { get; set; }
            public TurnstilePassRightModel Tpright { get; set; } = new TurnstilePassRightModel();
        }
        public List<Tpass> _ListTpass;
        public Tpass _TpassModel;
       
        public class Tper
        {
            public string WorkModel { get; set; }
            public TurnstilePersonModel Tperson { get; set; } = new TurnstilePersonModel();
        }
        public List<Tper> _ListTper;
        public Tper _TperModel;
    }
}
