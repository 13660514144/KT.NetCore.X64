using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.Core.Utils
{
    public class JsonUtil
    {
        private static object locker = new object();

        private static JsonSerializerSettings _jsonSettings;

        /// <summary>
        /// Json首字母小写配置
        /// </summary>
        public static JsonSerializerSettings JsonSettings
        {
            get
            {
                if (_jsonSettings == null)
                {
                    lock (locker)
                    {
                        if (_jsonSettings == null)
                        {
                            _jsonSettings = new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver(),

                                //忽略不是成员对象
                                MissingMemberHandling = MissingMemberHandling.Ignore,
                                //循环引用强制序列化
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                            };
                        }
                    }
                }

                return _jsonSettings;
            }
        }


        /// <summary>
        /// Json首字母小写配置
        /// </summary>
        public static JsonSerializerSettings JsonPrintSettings
        {
            get
            {
                if (_jsonSettings == null)
                {
                    lock (locker)
                    {
                        if (_jsonSettings == null)
                        {
                            _jsonSettings = new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                //NullValueHandling = NullValueHandling.Ignore

                                //忽略不是成员对象
                                MissingMemberHandling = MissingMemberHandling.Ignore,
                                //循环引用强制序列化
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                //自动缩进
                                Formatting = Formatting.Indented,
                            };
                        }
                    }
                }

                return _jsonSettings;
            }
        }
    }
}
