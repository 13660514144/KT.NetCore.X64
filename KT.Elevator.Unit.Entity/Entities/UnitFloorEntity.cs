﻿using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Unit.Entity.Entities
{
    /// <summary>
    /// 通行权限楼层，
    /// </summary>
    [Table("UNIT_FLOOR")]
    public class UnitFloorEntity : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 梯控楼层Id
        /// </summary>
        public string RealFloorId { get; set; }

        /// <summary>
        /// 是否为公共楼层
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// 大厦Id
        /// </summary>
        public string EdificeId { get; set; }

        /// <summary>
        /// 大厦名称
        /// </summary>
        public string EdificeName { get; set; }

        /// <summary>
        /// 关联电梯组
        /// </summary>
        public string ElevatorGroupId { get; set; }

        /// <summary>
        /// 关联派梯设备
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }


        public string CommBox { get; set; } = string.Empty;
        public string DestinationFloorId { get; set; } = string.Empty;
        public string Sourceid { get; set; } = string.Empty;
        public string HandDeviceid { get; set; } = string.Empty;
        public string AccessType { get; set; } = string.Empty;
        public string DeviceType { get; set; } = string.Empty;
        public string Sign { get; set; } = string.Empty;
       
        public UnitFloorEntity()
        {

        }
    }
}