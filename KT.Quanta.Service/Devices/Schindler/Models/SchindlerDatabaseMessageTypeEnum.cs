using AutoMapper.Mappers;
using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler.Models
{
    public class SchindlerDatabaseMessageTypeEnum : BaseEnum
    {
        public SchindlerDatabaseMessageTypeEnum(int code, string value, string text) : base(code, value, text)
        {

        }

        public static SchindlerDatabaseMessageTypeEnum HEARTBEAT_REQUEST = new SchindlerDatabaseMessageTypeEnum(0, "HEARTBEAT_REQUEST", "心跳");
        public static SchindlerDatabaseMessageTypeEnum HEARTBEAT_RESPONSE = new SchindlerDatabaseMessageTypeEnum(1, "HEARTBEAT_RESPONSE", "心跳回调");

        public static SchindlerDatabaseMessageTypeEnum CHANGE_INSERT_PERSON_REQUEST = new SchindlerDatabaseMessageTypeEnum(2, "CHANGE_INSERT_PERSON_REQUEST", "新增或修改用户");
        public static SchindlerDatabaseMessageTypeEnum CHANGE_INSERT_PERSON_RESPONSE = new SchindlerDatabaseMessageTypeEnum(3, "CHANGE_INSERT_PERSON_RESPONSE", "新增或修改用户回调");

        public static SchindlerDatabaseMessageTypeEnum DELETE_PERSON_REQUEST = new SchindlerDatabaseMessageTypeEnum(4, "DELETE_PERSON_REQUEST", "删除用户");
        public static SchindlerDatabaseMessageTypeEnum DELETE_PERSON_RESPONSE = new SchindlerDatabaseMessageTypeEnum(5, "DELETE_PERSON_RESPONSE", "删除用户回调");

        public static SchindlerDatabaseMessageTypeEnum CHANGE_INSERT_PERSON_REQUEST_NACK = new SchindlerDatabaseMessageTypeEnum(6, "CHANGE_INSERT_PERSON_REQUEST_NACK", "新增或修改用户");
        public static SchindlerDatabaseMessageTypeEnum CHANGE_INSERT_PERSON_RESPONSE_NACK = new SchindlerDatabaseMessageTypeEnum(7, "CHANGE_INSERT_PERSON_RESPONSE_NACK", "新增或修改用户回调");

        public static SchindlerDatabaseMessageTypeEnum DELETE_PERSON_REQUEST_NACK = new SchindlerDatabaseMessageTypeEnum(8, "DELETE_PERSON_REQUEST_NACK", "删除用户");
        public static SchindlerDatabaseMessageTypeEnum DELETE_PERSON_RESPONSE_NACK = new SchindlerDatabaseMessageTypeEnum(9, "DELETE_PERSON_RESPONSE_NACK", "删除用户回调");

        public static SchindlerDatabaseMessageTypeEnum SET_ZONE_ACCESS_REQUEST = new SchindlerDatabaseMessageTypeEnum(10, "SET_ZONE_ACCESS_REQUEST", "设置权限");
        public static SchindlerDatabaseMessageTypeEnum SET_ZONE_ACCESS_RESPONSE = new SchindlerDatabaseMessageTypeEnum(11, "SET_ZONE_ACCESS_RESPONSE", "设置权限回调");
        public static SchindlerDatabaseMessageTypeEnum SET_ZONE_ACCESS_RESPONSE_NACK = new SchindlerDatabaseMessageTypeEnum(12, "SET_ZONE_ACCESS_RESPONSE_NACK", "设置权限回调");

    }
}
