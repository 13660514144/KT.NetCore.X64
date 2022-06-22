using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Models
{
    /// <summary>
    /// 远程连接设备
    /// </summary>
    public class RemoteDeviceModel
    {
        #region 本地数据
        /// <summary>
        /// 设备类型
        /// <see cref="KT.Elevator.Common.Enums.RemoteDeviceTypeEnum"/>
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 品牌型号
        /// </summary>
        public string BrandModel { get; set; }

        /// <summary>
        /// 设备Key,区分设备的唯一Id
        /// </summary>
        public string DeviceKey { get; set; }

        /// <summary>
        /// 设备Id,用于后台或第三方id
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 真实设备Id,用于记录第三方设备Id,只用于操作功能，不作为一标识
        /// </summary>
        public string RealId { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        #endregion


        #region 非本地数据
        /// <summary>
        /// 派梯设备派梯标识,所有设备唯一
        /// </summary>
        public ushort TerminalId { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline { get; set; }

        /// <summary>
        /// 最后同步数据时间
        /// </summary>
        public long SyncDataTime { get; set; }

        /// <summary>
        /// 是否正在同步数据，同步数据不能同时进行，不持久化
        /// </summary>
        public bool IsSyncingData { get; set; }

        ///// <summary>
        ///// 是否有未完成同步的数据完成，不持久化
        ///// 设备新接入时，会直接推送数据库中的所有数据，当前标识记录同步状态
        ///// 有新数据传入时，会自动推送当条数据，如果已经同步完成，会时时更新最后同步时间，否则不更新最后同步时间
        ///// </summary>
        /// <summary>
        /// 是否有推送错误数据
        /// </summary>
        public bool HasDistributeError { get; set; }

        /// <summary>
        /// Hub连接Id,每次连接都不一样，所以不用持久化
        /// </summary>
        public string ConnectionId { get; set; }
        #endregion
    }
}
