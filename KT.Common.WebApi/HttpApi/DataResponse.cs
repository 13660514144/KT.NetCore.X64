using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.WebApi.HttpApi
{
    /// <summary>
    /// 返回数据结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataResponse<T> : IResponse
    {
        public T Data { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }

        public DataResponse(int code, string message, T data)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }

        /// <summary>
        /// 操作成功
        /// </summary>
        /// <param name="data">返回数据结果</param>
        /// <returns></returns>
        public static DataResponse<T> Ok(T data, string message = "")
        {
            return new DataResponse<T>(200, message, data);
        }

        /// <summary>
        /// 操作成功
        /// </summary>
        /// <param name="data">返回数据结果</param>
        /// <returns></returns>
        public static DataResponse<T> Result(int code, T data, string message = "")
        {
            return new DataResponse<T>(code, message, data);
        }
    }
}
