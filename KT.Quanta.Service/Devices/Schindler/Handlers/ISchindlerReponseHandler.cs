using KT.Quanta.Service.Devices.Schindler.Models;
using KT.Quanta.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Schindler
{
    /// <summary>
    /// 迅达派梯返回结果操作
    /// 当前类对象对应每个连接只初始化一次
    /// </summary>
    public interface ISchindlerReponseHandler
    {
        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        Task HeartbeatAsync(List<string> buffer);

        /// <summary>
        /// 派梯
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns> 
        Task PaddleAsync(SchindlerTelegramHeaderResponse headResponse);

        /// <summary>
        /// 新增或修改用户
        /// </summary>
        /// <param name="headResponse"></param>
        /// <returns></returns>
        Task ChangeInsertPersonAsync(SchindlerTelegramHeaderResponse headResponse);

        /// <summary>
        /// 新增或修改用户
        /// </summary>
        /// <param name="headResponse"></param>
        /// <returns></returns>
        Task ChangeInsertPersonNackAsync(SchindlerTelegramHeaderResponse headResponse);

        /// <summary>
        /// 设置用户Zone
        /// </summary>
        /// <param name="headResponse"></param>
        /// <returns></returns>
        Task SetZoneAccessResponseAsync(SchindlerTelegramHeaderResponse headResponse);

        /// <summary>
        /// 上传数据
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        Task UploadPassAsync(SchindlerReportAllocationResponse result, CommunicateDeviceInfoModel communicateInfo);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="headResponse"></param>
        /// <returns></returns>
        Task DeletePersonAsync(SchindlerTelegramHeaderResponse headResponse);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="headResponse"></param>
        /// <returns></returns>
        Task DeletePersonNackAsync(SchindlerTelegramHeaderResponse headResponse);
    }
}
