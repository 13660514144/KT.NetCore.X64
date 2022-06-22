using KT.Turnstile.Unit.Entity.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace KT.Turnstile.Unit.ClientApp.Service.Helpers
{
    public class ElevatorDisplayDeviceDataAnalyze
    {
        private ILogger _logger;
        public ElevatorDisplayDeviceDataAnalyze(ILogger logger)
        {
            _logger = logger;
        }
        private List<byte> _bufferBytes = new List<byte>();

        /// <summary>
        /// 数据类型 字节数 描述
        /// 包头 1byte 固定为 0x41
        /// 包长度 1byte 整个数据包总长
        /// 命令 1byte 固定为 0xB1
        /// 数据 1（状态） 1byte 
        ///     0=无错误，
        ///     1=楼层不存在，
        ///     3=楼层已锁定，
        ///     4=楼层未指定，
        ///     6=通信失败，
        ///     7=请稍候，
        ///     8=现在无法服务，
        ///     50=终端未使用，
        ///     53=呼叫状态无效，
        ///     54=乘客过多，
        ///     55=无电梯可用，
        ///     56=拒绝访问
        /// 数据 2（梯号） 1byte 服务电梯的标识。
        ///     0=无服务电梯，
        ///     1=组内第一台电梯，
        ///     2=组内第二台电梯，
        ///     3=组内第三台电梯，
        /// ……
        /// 数据 3（楼层） 1byte 电梯到达的目的楼层
        /// 数据 4 1byte 保留，以作备用
        /// 数据 5 1byte 保留，以作备用
        /// 数据 …… 1byte 保留，以作备用
        /// 校验和 1byte 包长度+命令+数据，取低八位
        /// 包尾 1byte 固定为 0x4A
        /// 
        /// 1、群内 2 号电梯到 18 楼
        ///     41 08 B1 00 02 12 58 4A
        /// 2、当前无电梯可用
        ///     41 08 B1 37 00 05 80 4A 
        /// <param name="elevatorDisplay"></param>
        /// <returns></returns>
        public byte[] SendAnalyze(ElevatorDisplayModel elevatorDisplay)
        {
            var bytes = new List<byte>();

            //包头
            bytes.Add(0x41);
            //包长度
            bytes.Add(0x08);
            //命令
            bytes.Add(0xB1);
            //数据 1（状态）
            bytes.Add(0x00);
            //数据 2（梯号）
            bytes.Add((byte)elevatorDisplay.ElevatorNumber);
            //数据 3（楼层）
            bytes.Add((byte)elevatorDisplay.PhysicsFloor);

            //校验和
            var sum = 0x41 + 0x08 + 0xB1 + elevatorDisplay.ElevatorNumber + elevatorDisplay.PhysicsFloor + 0x4A;
            var hexSum = Convert.ToString(sum, 16);
            if (hexSum.Length > 2)
            {
                hexSum = hexSum.Substring(hexSum.Length - 2);
            }
            var byteSum = (byte)Convert.ToInt32(hexSum, 16);
            bytes.Add(byteSum);

            //包尾
            bytes.Add(0x4A);

            return bytes.ToArray();
        }
    }
}
