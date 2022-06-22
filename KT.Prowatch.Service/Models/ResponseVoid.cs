using Newtonsoft.Json;

namespace KT.Prowatch.Service.Models
{
    /// <summary>
    /// 返回结果封装
    /// </summary> 
    public class ResponseVoid
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
        public object Data { get; set; }

        private ResponseVoid(int code, string msg)
        {
            this.Code = code;
            this.Msg = msg;
            this.Message = msg;
        }

        /// <summary>
        /// 返回自定义结果
        /// </summary>
        /// <param name="code">状态态码</param>
        /// <param name="msg">状态信息</param>
        /// <param name="data">结果数据</param>
        /// <returns></returns>
        public static ResponseVoid Result(int code, string msg)
        {
            return new ResponseVoid(code, msg);
        }

        /// <summary>
        /// 操作成功
        /// </summary>
        /// <returns></returns>
        public static ResponseVoid Ok()
        {
            return Result(200, string.Empty );
        }
 
        /// <summary>
        /// 操作失败
        /// </summary>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public static ResponseVoid Error(string msg)
        {
            return Result(500, msg );
        }

        /// <summary>
        /// 返回结果(无返回结果的时候根据result判断结果)
        /// </summary>
        /// <param name="result">操作是否成功</param>
        /// <param name="errorMsg">如果错误（result=false），则赋值到Msg信息，如果（result=true），则Msg无信息</param>
        /// <returns></returns>
        public static ResponseVoid Result(bool result, string errorMsg)
        {
            if (result)
            {
                return Result(200, null );
            }
            else
            {
                //返回错误时不带数据结果
                return Result(500, errorMsg );
            }
        }

    }
}