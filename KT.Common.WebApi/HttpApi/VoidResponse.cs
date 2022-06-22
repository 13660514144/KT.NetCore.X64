using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.WebApi.HttpApi
{
    public class VoidResponse : IResponse
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public VoidResponse(int code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        /// <summary>
        /// 操作成功
        /// </summary>
        /// <returns></returns>
        public static VoidResponse Ok(string message = "")
        {
            return new VoidResponse(200, message);
        }

        /// <summary>
        /// 操作失败
        /// </summary>
        /// <param name="message">返回错误信息</param>
        /// <returns></returns>
        public static VoidResponse Error(string message)
        {
            return new VoidResponse(400, message);
        }
    }
}
