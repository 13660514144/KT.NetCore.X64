
using KT.Quanta.SerialDevice.Common.Models;
using Microsoft.Extensions.Logging;

namespace KT.Quanta.SerialDevice.Common.IDevices
{
    /// <summary>
    /// 串口数据解析
    /// </summary>
    public interface ISerialDataAnalyze
    {
        /// <summary>
        /// 解析数据返回结果
        /// </summary>
        /// <typeparam name="T">返回的数据结果类型</typeparam>
        /// <param name="data">原始数据</param>
        /// <returns>解析后的结果</returns>
        T AnalyzeAsync<T>(SerialMessageModel data);
    }
}
