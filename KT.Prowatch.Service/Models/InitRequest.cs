using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KT.Prowatch.Service.Models
{
    public class InitRequest
    {
        /// Prowatch 服务器数据库地址   
        [JsonProperty("dbAddr")]
        public string DBAddr { get; set; }
        /// Prowatch 服务器数据库名称   
        [JsonProperty("dbName")]
        public string DBName { get; set; }
        /// Prowatch 服务器数据库用户名, 为空时使用 Windows 身份认证登录 
        [JsonProperty("dbUser")]
        public string DBUser { get; set; }
        /// Prowatch 服务器数据库密码    
        [JsonProperty("dbPassword")]
        public string DBPassword { get; set; }
        /// Prowatch 服务器地址 
        [JsonProperty("pcAddr")]
        public String PCAddr { get; set; }
        /// Prowatch 服务器系统用户名    
        [JsonProperty("pcUser")]
        public String PCUser { get; set; }
        /// Prowatch 服务器系统密码    
        [JsonProperty("pcPassword")]
        public String PCPassword { get; set; }
        /// 后台服务器地址    
        [JsonProperty("serverAddress")]
        public String ServerAddress { get; set; }
    }
}