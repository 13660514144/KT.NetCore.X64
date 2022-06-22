using Newtonsoft.Json;

namespace KT.Prowatch.WebApi.Common
{
    /// <summary>
    /// 返回结果封装
    /// </summary> 
    public class ResponseData<T>
    {
        /// <summary>
        /// 状态态码
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// 状态信息
        /// </summary>
        [JsonProperty("msg")]
        public string Msg { get; set; }

        /// <summary>
        /// 状态信息
        /// </summary> 
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// 结果数据
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }

        private ResponseData(int code, string msg, T data)
        {
            this.Code = code;
            this.Msg = msg;
            this.Message = msg;
            this.Data = data;
        }

        /// <summary>
        /// 返回自定义结果
        /// </summary>
        /// <param name="code">状态态码</param>
        /// <param name="msg">状态信息</param>
        /// <param name="data">结果数据</param>
        /// <returns></returns>
        public static ResponseData<T> Result(int code, string msg, T data)
        {
            return new ResponseData<T>(code, msg, data);
        }

        /// <summary>
        /// 操作成功
        /// </summary>
        /// <returns></returns>
        public static ResponseData<T> Ok()
        {
            return Result(200, string.Empty, default(T));
        }

        /// <summary>
        /// 操作成功并返回结果数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ResponseData<T> Ok(T data)
        {
            return Result(200, null, data);
        }

        /// <summary>
        /// 操作失败
        /// </summary>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public static ResponseData<T> Error(string msg)
        {
            return Result(500, msg, default(T));
        }

        /// <summary>
        /// 返回结果(无返回结果的时候根据result判断结果)
        /// </summary>
        /// <param name="result">操作是否成功</param>
        /// <param name="errorMsg">如果错误（result=false），则赋值到Msg信息，如果（result=true），则Msg无信息</param>
        /// <returns></returns>
        public static ResponseData<T> Result(bool result, string errorMsg)
        {
            if (result)
            {
                return Result(200, null, default(T));
            }
            else
            {
                //返回错误时不带数据结果
                return Result(500, errorMsg, default(T));
            }
        }

        /// <summary>
        /// 返回结果(有返回结果的时候不能根据result判断)
        /// </summary>
        /// <param name="result">操作是否成功</param>
        /// <param name="errorMsg">如果错误（result=false），则赋值到Msg信息，如果（result=true），则Msg无信息</param>
        /// <param name="data">返回的数据</param>
        /// <returns></returns>
        public static ResponseData<T> Result(bool result, string errorMsg, T data)
        {
            return Result(200, null, data);
        }

    }
}