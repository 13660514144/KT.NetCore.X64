using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Hikvision
{
    public class HikvisionSdkExecuteDistributeTypeEnum : BaseEnum
    {
        public HikvisionSdkExecuteDistributeTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static HikvisionSdkExecuteDistributeTypeEnum GetCard => new HikvisionSdkExecuteDistributeTypeEnum(1, "GET_CARD", "获取卡");
        public static HikvisionSdkExecuteDistributeTypeEnum SetCard => new HikvisionSdkExecuteDistributeTypeEnum(2, "SET_CARD", "新增或修改卡");
        public static HikvisionSdkExecuteDistributeTypeEnum DeleteCard => new HikvisionSdkExecuteDistributeTypeEnum(3, "DELETE_CARD", "删除卡");
        public static HikvisionSdkExecuteDistributeTypeEnum GetFace => new HikvisionSdkExecuteDistributeTypeEnum(4, "GET_FACE", "获取卡");
        public static HikvisionSdkExecuteDistributeTypeEnum SetFace => new HikvisionSdkExecuteDistributeTypeEnum(5, "SET_FACE", "新增或修改卡");
        public static HikvisionSdkExecuteDistributeTypeEnum DeleteFace => new HikvisionSdkExecuteDistributeTypeEnum(6, "DELETE_FACE", "删除卡");
    }
}
