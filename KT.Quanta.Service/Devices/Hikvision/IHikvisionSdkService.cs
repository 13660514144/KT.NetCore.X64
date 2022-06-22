using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Hikvision.Models;
using KT.Quanta.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Hikvision
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
        Task DeleteCardAsync(string personId, string cardNo);

        /// <summary>
        /// 删除人脸
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="cardReaderNo">读卡器</param>
        Task DeleteFaceAsync(string personId, HikvisionDeleteFaceQuery query);

        /// <summary>
        /// 启用事件上传
        /// </summary>
        Task<bool> DeployEventAsync();

        /// <summary>
        /// 获取卡
        /// </summary>
        /// <param name="cardNo">卡号</param>
        Task<HikvisionPersonCardQuery> GetCardAsync(string personId, string cardNo);

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
        Task<string> GetFaceAsync(string personId, string cardNo, int cardReaderNo = 1);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <param name="ip">ip地址</param>
        /// <param name="port">端口</param>
        Task<int> LoginAsync(string account, string password);

        /// <summary>
        /// 新增卡
        /// </summary>
        /// <param name="personCard">卡信息</param>
        Task SetCardAsync(string personId, HikvisionPersonCardQuery personCard);

        /// <summary>
        /// 新增人脸
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="facePath">人脸本地文件路径</param>
        /// <param name="cardReaderNo">读卡器</param>
        Task SetFaceAsync(string personId, HikvisionSetFaceQuery model);

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        Task LogoutAsync();

        /// <summary>
        /// 初始化
        /// </summary> 
        Task InitAsync(CommunicateDeviceInfoModel communicateDevice, HikvisionTypeParameterModel typeParameter);

        /// <summary>
        /// 设备编码方式
        /// <param name="type">
        /// 0- 无字符编码信息(老设备)，
        /// 1- GB2312(简体中文)，
        /// 2- GBK，
        /// 3- BIG5(繁体中文)，
        /// 4- Shift_JIS(日文)，
        /// 5- EUC-KR(韩文)，
        /// 6- UTF-8，
        /// 7- ISO8859-1，
        /// 8- ISO8859-2，
        /// 9- ISO8859-3，
        /// …，
        /// 依次类推，
        /// 21- ISO8859-15(西欧) 
        /// </param>
        /// </summary>
        /// <returns></returns>
        Encoding GetEncoding();
    }
}
