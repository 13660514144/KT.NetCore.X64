using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using ContralServer.CfgFileRead;
using FluentScheduler;
using KT.Common.WpfApp.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HelperTools
{
    public class JobChkVer
    {
        public static string dir = AppDomain.CurrentDomain.BaseDirectory;
        
        public JobChkVer()
        {
            JobManager.Initialize(new JobVer());            
        }
        public void Init()
        {
            ChkVerChange change = new ChkVerChange();
            change.ChkVer();
        }
        public class JobVer : Registry
        {
            public JobVer()
            {
                Schedule(() =>
                {
                    Random Rd = new Random();
                    int Sjs = Rd.Next(1, 59);
                    ChkVerChange change = new ChkVerChange();
                    change.ChkVer();
                }).ToRunEvery(1).Days().At(04, 30);
            }

        }
        public  class ChkVerChange
        {
            public void ChkVer()
            {
  
                try
                {

                    //读本地版本文件
                    StringBuilder Sb = new StringBuilder();
                    StreamReader sr = new StreamReader("Ver.json");
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Sb.Append(line);
                    }
                    sr.Close();
                    sr.Dispose();
                    JObject O = JObject.Parse(Sb.ToString());
                    Dictionary<string, string> SendData = new Dictionary<string, string>
                {
                    {"Ver", O["Ver"].ToString().Trim() },
                    {"DeviceType", O["DeviceType"].ToString().Trim()}
                };
                    string OwnerIp = "None"; 
                    string OwnerPort = "None";
                    string Uri = AppConfigurtaionServices.Configuration["AppSettings:ServerIp"].ToString();
                    string Port = AppConfigurtaionServices.Configuration["AppSettings:ServerPort"].ToString();
                    int _Port = Convert.ToInt32(Port) - 100;
                    string Controller = "Api/PassRecoadGuest/ChkVer";
                    Dispatch _Dispatch = new Dispatch();                    
                    string Result = _Dispatch.ApiGetData(Controller, SendData, Uri, _Port.ToString());
                    JObject Data = JObject.Parse(Result);
                    if ((int)Data["Flg"] == 1 && O["DeviceType"].ToString().Trim() == Data["DeviceType"].ToString().Trim())
                    {
                        File.Copy("Ver.json", "Ver-bak.json",true);
                        //本类型版本发设变动 先写版本文件
                        JObject obj = new JObject(
                            new JProperty("DeviceType", O["DeviceType"].ToString().Trim()),
                            new JProperty("Ver", Data["Ver"].ToString().Trim())
                        );

                        string filePath = $@"{dir}\Ver.json";
                        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(fs))
                            {
                                sw.Write(JsonConvert.SerializeObject(obj));
                                sw.Flush();
                                sw.Dispose();
                            }
                        }
                        string ChangeExe = "KT.Visitor.SelfApp.exe";
                        //转自动下载
                        long ISize = (long)Data["FileSize"];                        
                        
                        string Url = $"http://{Uri}:{_Port}/dw/";
                        Process process = new Process();
                        process.StartInfo.FileName = "AutoVerDown.exe";
                        //process.StartInfo.Arguments = $"{Url}{Data["PackageFile"]} {ChangeExe}";
                        process.StartInfo.Arguments = $"{Url}{Data["PackageFile"]} {ChangeExe} {O["Ver"]} {Data["Ver"]} {O["DeviceType"]} {OwnerIp} {OwnerPort} {Uri} {_Port} {ISize}";
                        process.StartInfo.UseShellExecute = true;
                        process.Start();
                        System.Environment.Exit(0);
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
