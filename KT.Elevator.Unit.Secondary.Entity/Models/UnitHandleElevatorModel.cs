﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Secodary.Entity.Models
{
    /// <summary>
    /// 用于记录最后一次派梯信息
    /// </summary>
    public class UnitHandleElevatorModel : UnitAutoHandleElevatorRequestModel
    {
        public UnitHandleElevatorModel()
        {
            HandleElevatorRights = new List<UnitHandleElevatorRightModel>();
        }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string DestinationFloorName { get; set; }

        /// <summary>
        /// 拥有的权限，未通行前必须使用这权限值
        /// </summary>
        public List<UnitHandleElevatorRightModel> HandleElevatorRights { get; set; }

        /// <summary>
        /// 通行的权限
        /// </summary>
        public UnitHandleElevatorRightModel HandleElevatorRight { get; set; }

        /// <summary>
        /// 卡类型，IC_CARD、QR_CODE...
        /// </summary>
        public string AccessType { get; set; }

        /// <summary>
        /// 通行设备Id 
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 设备类型,IC、QR...
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 通行时间
        /// </summary>
        public long PassTime { get; set; }

        /// <summary>
        /// 通行人脸图片
        /// </summary>
        [JsonIgnore]
        public byte[] FaceImage { get; set; }

        /// <summary>
        /// 人脸图片大小
        /// </summary>
        public long FaceImageSize { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        public string Extra { get; set; }

        /// <summary>
        /// 出入方向
        /// </summary>
        public string WayType { get; set; }

        /// <summary>
        /// 备注，用于填写第三方特别信息
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 权限
    /// </summary>
    public class UnitHandleElevatorRightModel
    {
        /// <summary>
        /// 通行权限
        /// </summary>
        public string PassRightId { get; set; }

        /// <summary>
        /// 通行标记，人脸Id、IC卡号...
        /// </summary>
        public string PassRightSign { get; set; }
    }
}