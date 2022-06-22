using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace HelperTools
{
    public class HttpClientFactory
    {
        public string UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36 MicroMessenger/7.0.9.501 NetType/WIFI MiniProgramEnv/Windows WindowsWechat";
        public string Refer = "";
        public string Accept = "application/json, text/javascript, */*; q=0.01";
        public string PostData = "";
        public string Url = "";
        public string RequestMothed;
        
        private string BuildResult(string Scuess, bool Flg, string Result)
        {
            string ReturnStr = "";
            string FlgValue = (Flg == true) ? "0" : "1";
            string Msg = (Flg == true) ? "提交完成" : "请求返错误";
            Dictionary<string, string> Dict = new Dictionary<string, string>()
            {
                {"scuess", Scuess},
                {"Msg",Msg},
                {"Result",Result}
            };
            ReturnStr = JsonConvert.SerializeObject(Dict);
            return ReturnStr;
        }


        public string SendHttpRequest()
        {
            string ReqResult = string.Empty;
            System.Net.ServicePointManager.DefaultConnectionLimit = 50;
            //json格式请求数据
            string requestData = PostData;
            //拼接URL
            string serviceUrl = Url;
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            //post请求
            myRequest.Method = RequestMothed;
            //utf-8编码
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(requestData);

            myRequest.ContentLength = buf.Length;
            myRequest.Timeout = 1000*60;
            //指定为json否则会出错
            myRequest.ContentType = "application/json";
            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;
            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();

            //获得接口返回值
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            ReqResult = reader.ReadToEnd();
            reader.Close();
            myResponse.Close();

            return ReqResult;
        }
        public async Task<string> SendAsyncHttpRequest()
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 50;
            //json格式请求数据
            string requestData = PostData;
            //拼接URL
            string serviceUrl = Url;
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            //post请求
            myRequest.Method = RequestMothed;
            //utf-8编码
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(requestData);

            myRequest.ContentLength = buf.Length;
            myRequest.Timeout = 1000*60;
            //指定为json否则会出错
            myRequest.ContentType = "application/json";
            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;
            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();

            //获得接口返回值
            HttpWebResponse myResponse = await myRequest.GetResponseAsync() as HttpWebResponse;
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string ReqResult = reader.ReadToEnd();
            reader.Close();
            myResponse.Close();
            return ReqResult;
        }


        #region HttpGet
        public string HttpGet(WebRequestOption options)
        {
            string formData = GetFormData(options.Paras);
            if (options.SecurityProtocol != SecurityProtocolType.SystemDefault)
            {
                ServicePointManager.SecurityProtocol = options.SecurityProtocol;
            }
            string url = string.IsNullOrEmpty(formData) ? options.Url : options.Url + "/" + formData;
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            if (options.Headers.Count > 0)
            {
                foreach (var item in options.Headers)
                {
                    myRequest.Headers.Add(item.Key, item.Value);
                }
            }
            if (options.CC != null)
            {
                myRequest.CookieContainer = options.CC;
            }
            myRequest.UserAgent = UserAgent;
            if (url.StartsWith("https://"))
            {
                //myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentType = "application/json";
            }
            myRequest.Accept = "*/*";
            myRequest.KeepAlive = true;
            if (options.MyProxy != null)
            {
                myRequest.Proxy = options.MyProxy;
            }
            try
            {
                using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
                {
                    using (Stream sResponse = myResponse.GetResponseStream())
                    {
                        using (StreamReader myReader = new StreamReader(sResponse, options.MyEncoding))
                        {
                            options.Status = myResponse.StatusCode;
                            string responseText = myReader.ReadToEnd();
                            return responseText;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                options.MyException = ex;
                return null;
            }
        }

        public async Task<string> HttpGetAsync(WebRequestOption options)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

            string formData = GetFormData(options.Paras);
            if (options.SecurityProtocol != SecurityProtocolType.SystemDefault)
            {
                ServicePointManager.SecurityProtocol = options.SecurityProtocol;
            }
            //string url = string.IsNullOrEmpty(formData) ? options.Url : options.Url + "?" + formData;
            string url = string.IsNullOrEmpty(formData) ? options.Url : options.Url + "/" + formData;
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            if (options.Headers.Count > 0)
            {
                foreach (var item in options.Headers)
                {
                    myRequest.Headers.Add(item.Key, item.Value);
                }
            }
            if (options.CC != null)
            {
                myRequest.CookieContainer = options.CC;
            }
            myRequest.UserAgent = UserAgent;
            if (url.StartsWith("https://"))
            {
                //myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentType = "application/json";
            }
            myRequest.Accept = "*/*";
            myRequest.KeepAlive = true;
            if (options.MyProxy != null)
            {
                myRequest.Proxy = options.MyProxy;
            }
            try
            {
                using (HttpWebResponse myResponse = (HttpWebResponse)await myRequest.GetResponseAsync())
                {
                    using (Stream sResponse = myResponse.GetResponseStream())
                    {
                        using (StreamReader myReader = new StreamReader(sResponse, options.MyEncoding))
                        {
                            options.Status = myResponse.StatusCode;
                            string responseText = myReader.ReadToEnd();
                            return responseText;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                options.MyException = ex;
                return null;
            }
        }
        #endregion

        #region HttpPost
        public string HttpPost(WebRequestOption options)
        {
            if (options.SecurityProtocol != SecurityProtocolType.SystemDefault)
            {
                ServicePointManager.SecurityProtocol = options.SecurityProtocol;
            }
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(options.Url);
            myRequest.Method = "POST";
            if (options.CC != null)
            {
                myRequest.CookieContainer = options.CC;
            }
            if (options.Headers.Count > 0)
            {
                foreach (var item in options.Headers)
                {
                    myRequest.Headers.Add(item.Key, item.Value);
                }
            }
            if (options.ContentType != null)
            {
                myRequest.ContentType = options.ContentType;
            }
            else
            {
                //myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentType = "application/json";
            }
            myRequest.UserAgent = UserAgent;

            byte[] payload = null;
            string formData = GetFormData(options.Paras);
            if (!string.IsNullOrEmpty(formData))
            {
                payload = Encoding.UTF8.GetBytes(formData);
                myRequest.ContentLength = payload.Length;
            }
            if (options.MyProxy != null)
            {
                myRequest.Proxy = options.MyProxy;
            }
            using (Stream mySream = myRequest.GetRequestStream())
            {
                if (payload != null && payload.Length > 0)
                {
                    mySream.Write(payload, 0, payload.Length);
                }
            }

            try
            {
                using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
                {
                    using (StreamReader myReader = new StreamReader(myResponse.GetResponseStream(), options.MyEncoding))
                    {
                        string responseText = myReader.ReadToEnd();
                        return responseText;
                    }
                }
            }
            catch (Exception ex)
            {
                options.MyException = ex;
                return null;
            }
        }

        public async Task<string> HttpPostAsync(WebRequestOption options)
        {
            if (options.SecurityProtocol != SecurityProtocolType.SystemDefault)
            {
                ServicePointManager.SecurityProtocol = options.SecurityProtocol;
            }
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(options.Url);
            myRequest.Method = "POST";
            if (options.CC != null)
            {
                myRequest.CookieContainer = options.CC;
            }
            if (options.Headers.Count > 0)
            {
                foreach (var item in options.Headers)
                {
                    myRequest.Headers.Add(item.Key, item.Value);
                }
            }
            if (options.ContentType != null)
            {
                myRequest.ContentType = options.ContentType;
            }
            else
            {
                myRequest.ContentType = "application/x-www-form-urlencoded";
                //myRequest.ContentType = "application/json";
            }
            myRequest.UserAgent = UserAgent;

            byte[] payload = null;
            string formData = GetFormData(options.Paras);
            if (!string.IsNullOrEmpty(formData))
            {
                payload = Encoding.UTF8.GetBytes(formData);
                myRequest.ContentLength = payload.Length;
            }
            if (options.MyProxy != null)
            {
                myRequest.Proxy = options.MyProxy;
            }
            using (Stream mySream = myRequest.GetRequestStream())
            {
                if (payload != null && payload.Length > 0)
                {
                    mySream.Write(payload, 0, payload.Length);
                }
            }

            try
            {
                using (HttpWebResponse myResponse = (HttpWebResponse)await myRequest.GetResponseAsync())
                {
                    using (StreamReader myReader = new StreamReader(myResponse.GetResponseStream(), options.MyEncoding))
                    {
                        string responseText = myReader.ReadToEnd();
                        return responseText;
                    }
                }
            }
            catch (Exception ex)
            {
                options.MyException = ex;
                return null;
            }
        }

        public async Task<string> HttpPostJsonAsync(WebRequestOption options)
        {
            if (options.SecurityProtocol != SecurityProtocolType.SystemDefault)
            {
                ServicePointManager.SecurityProtocol = options.SecurityProtocol;
            }
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(options.Url);
            myRequest.Method = "POST";
            if (options.CC != null)
            {
                myRequest.CookieContainer = options.CC;
            }
            if (options.Headers.Count > 0)
            {
                foreach (var item in options.Headers)
                {
                    myRequest.Headers.Add(item.Key, item.Value);
                }
            }
            myRequest.ContentType = "application/json";
            myRequest.UserAgent = UserAgent;

            byte[] payload = null;
            string jsonData = options.Paras.ToString();
            if (!string.IsNullOrEmpty(jsonData))
            {
                payload = Encoding.UTF8.GetBytes(jsonData);
                myRequest.ContentLength = payload.Length;
            }
            if (options.MyProxy != null)
            {
                myRequest.Proxy = options.MyProxy;
            }
            using (Stream mySream = myRequest.GetRequestStream())
            {
                if (payload != null && payload.Length > 0)
                {
                    mySream.Write(payload, 0, payload.Length);
                }
            }

            try
            {
                using (HttpWebResponse myResponse = (HttpWebResponse)await myRequest.GetResponseAsync())
                {
                    using (StreamReader myReader = new StreamReader(myResponse.GetResponseStream(), options.MyEncoding))
                    {
                        string responseText = myReader.ReadToEnd();
                        return responseText;
                    }
                }
            }
            catch (WebException ex)
            {
                var res = (HttpWebResponse)ex.Response;
                using (StreamReader myReader = new StreamReader(res.GetResponseStream(), options.MyEncoding))
                {
                    string responseText = myReader.ReadToEnd();
                    return responseText;
                }
            }
            catch (Exception ex)
            {
                options.MyException = ex;
                return null;
            }
        }
        #endregion

        public static async Task<string> ExecQueryAsync(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string resultStr = await response.Content.ReadAsStringAsync();
                return resultStr;
            }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        #region 帮助方法
        public static string GetFormData(object paras)
        {
            StringBuilder formData = new StringBuilder();
            if (paras != null)
            {
                Type t = paras.GetType();
                if (t.Name.Contains("Dictionary"))
                {
                    foreach (KeyValuePair<String, String> kvp in paras as Dictionary<string, string>)
                    {
                        if (formData.ToString() == "")
                        {
                            formData.Append(kvp.Key + "=" + kvp.Value);
                        }
                        else
                        {
                            formData.Append("&" + kvp.Key + "=" + kvp.Value);
                        }
                    }
                }
                else if (t.Name == "String")
                {
                    formData.Append(paras.ToString());
                }
                else
                {
                    foreach (PropertyInfo pi in t.GetProperties())
                    {
                        var jsonProperty = pi.CustomAttributes.SingleOrDefault(p => p.AttributeType.FullName == "Newtonsoft.Json.JsonPropertyAttribute");
                        string name = jsonProperty == null ? pi.Name : jsonProperty.ConstructorArguments[0].Value.ToString();
                        object val = pi.GetValue(paras, null);

                        if (formData.ToString() == "")
                        {
                            formData.Append(name + "=" + val);
                        }
                        else
                        {
                            formData.Append("&" + name + "=" + val);
                        }
                    }
                }
            }
            return formData.ToString();
        }
        #endregion
    }
    public class WebRequestOption
    {
        public string Url { get; set; }
        /// <summary>
        /// 可以是匿名对象， 可以是字典
        /// </summary>
        public object Paras { get; set; }
        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public CookieContainer CC { get; set; }
        public Encoding MyEncoding { get; set; }
        public WebProxy MyProxy { get; set; }
        public HttpStatusCode Status { get; set; }
        public Exception MyException { get; set; }
        public SecurityProtocolType SecurityProtocol { get; set; }
        public string ContentType { get; set; }

        /// <summary>
        /// 没有任何关于SecurityProtocold的默认设置
        /// </summary>
        public WebRequestOption()
        {
            if (this.MyEncoding == null)
            {
                this.MyEncoding = Encoding.UTF8;
            }
        }
        /// <summary>
        /// htts协议将默认设置为Tls1.0
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paras"></param>
        public WebRequestOption(string url, object paras = null)
        {
            this.Url = url;
            if (paras != null)
            {
                this.Paras = paras;
            }
            if (this.MyEncoding == null)
            {
                this.MyEncoding = Encoding.UTF8;
            }
            if (this.Url.StartsWith("https") && this.SecurityProtocol == SecurityProtocolType.SystemDefault)
            {
                this.SecurityProtocol = SecurityProtocolType.Tls12;
            }
        }

    }
}
