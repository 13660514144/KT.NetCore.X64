using AutoMapper.Mappers;
using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler.Models
{
    public class SchindlerDispatchMessageTypeEnum : BaseEnum
    {
        public SchindlerDispatchMessageTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        public static SchindlerDispatchMessageTypeEnum HEARTBEAT_REQUEST = new SchindlerDispatchMessageTypeEnum(0, "HEARTBEAT_REQUEST", "心跳");
        public static SchindlerDispatchMessageTypeEnum HEARTBEAT_RESPONSE = new SchindlerDispatchMessageTypeEnum(1, "HEARTBEAT_RESPONSE", "心跳回调");
         
        public static SchindlerDispatchMessageTypeEnum CALL_BY_PROFILE_REQUEST = new SchindlerDispatchMessageTypeEnum(12, "CALL_BY_PROFILE_REQUEST", "根据设备派梯");
        public static SchindlerDispatchMessageTypeEnum CALL_BY_PROFILE_RESPONSE = new SchindlerDispatchMessageTypeEnum(13, "CALL_BY_PROFILE_RESPONSE", "根据设备梯派梯回调");

        public static SchindlerDispatchMessageTypeEnum CALL_BY_ID_REQUEST = new SchindlerDispatchMessageTypeEnum(14, "CALL_BY_ID_REQUEST", "根据人员ID派梯");
        public static SchindlerDispatchMessageTypeEnum CALL_BY_ID_RESPONSE = new SchindlerDispatchMessageTypeEnum(15, "CALL_BY_ID_RESPONSE", "根据人员ID派梯派梯回调");

        public static SchindlerDispatchMessageTypeEnum DIRECT_CALL_REQUEST = new SchindlerDispatchMessageTypeEnum(16, "DIRECT_CALL_REQUEST", "直接派梯");
        public static SchindlerDispatchMessageTypeEnum DIRECT_CALL_RESPONSE = new SchindlerDispatchMessageTypeEnum(17, "DIRECT_CALL_RESPONSE", "直接派梯回调");
    }
}
