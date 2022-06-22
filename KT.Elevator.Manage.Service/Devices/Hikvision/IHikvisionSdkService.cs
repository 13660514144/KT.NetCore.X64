using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Devices.Hikvision.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Hikvision
{
    /// <summary>
    /// 海康设备接口定义
    /// 每个设备一对应一个实例
    /// </summary>
    public interface IHikvisionSdkService
    {
        /// <summary>
        /// 删除卡
        /// </summary>
        /// <param name="cardNo"></param>
        Task DeleteCardAsync(string cardNo);

        /// <summary>
        /// 删除人脸
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="cardReaderNo">读卡器</param>
        Task DeleteFaceAsync(string cardNo, int cardReaderNo = 1);

        /// <summary>
        /// 启用事件上传
        /// </summary>
        Task DeployEventAsync();

        /// <summary>
        /// 获取卡
        /// </summary>
        /// <param name="cardNo">卡号</param>
        Task GetCardAsync(string cardNo);

        /// <summary>
        /// 获取梯控输出
        /// </summary>
        /// <returns></returns>
        Task<int> GetCurACSDeviceDoorNumAsync();

        /// <summary>
        /// 获取人脸
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="cardReaderNo">读卡器</param>
        /// <returns>人脸本地文件路径</returns>
        Task<string> GetFaceAsync(string cardNo, int cardReaderNo = 0);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <param name="ip">ip地址</param>
        /// <param name="port">端口</param>
        Task<int> LoginAsync(IRemoteDevice remoteDevice, string account, string password);

        /// <summary>
        /// 新增卡
        /// </summary>
        /// <param name="personCard">卡信息</param>
        Task<bool> SetCardAsync(HikvisionPersonCard personCard);

        /// <summary>
        /// 新增人脸
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="facePath">人脸本地文件路径</param>
        /// <param name="cardReaderNo">读卡器</param>
        Task SetFaceAsync(string cardNo, string facePath, int cardReaderNo = 1);
    }
}
