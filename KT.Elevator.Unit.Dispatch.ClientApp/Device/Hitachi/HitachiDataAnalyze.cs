using KT.Common.Core.Utils;
using KT.Elevator.Unit.Dispatch.Entity.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Device.Hitachi
{
    public class HitachiDataAnalyze
    {
        private ILogger _logger;
        public HitachiDataAnalyze(ILogger logger)
        {
            _logger = logger;
        }
        private List<byte> _bufferBytes = new List<byte>();

        public HitachiReceiveModel Analyze(string portName, byte[] datas)
        {
            _logger.LogInformation($"Hitachi串口接收数据1：portName:{portName} datas:{datas.ToCommaPrintString()} ");
            //队列的形式读取
            if (datas != null && datas.Count() > 0)
            {
                datas.ToList().ForEach(x => _bufferBytes.Add(x));
            }

            _logger.LogInformation($"Hitachi串口接收总数据：portName:{portName} bytes:{_bufferBytes.ToCommaPrintString()} ");

            if (_bufferBytes[0] == 172)
            {
                //数据长度为6 + Data Length + 标识符2
                if (_bufferBytes.Count() < 3)
                {
                    _logger.LogInformation($"串口接收总数据：头长度不足1 length:{_bufferBytes.Count()} need:{8} ");
                    return null;
                }
                //5C 派梯回调
                else if (_bufferBytes[1] == 91)
                {
                    //数据长度
                    var totalDataLength = 14;
                    if (_bufferBytes.Count() < totalDataLength)
                    {
                        _logger.LogInformation($"串口接收总数据：总数据长度不足2 length:{_bufferBytes.Count()} need:{totalDataLength} ");
                        return null;
                    }
                    //获取
                    byte[] totalDataBytes = GetTotalDataBytes(totalDataLength);

                    var result = new HitachiReceiveModel();
                    result.PortName = portName;
                    result.Command = totalDataBytes[2];

                    //判断功能类型
                    if (_bufferBytes[2] == 1)
                    {
                        //派梯中
                        _logger.LogInformation($"Hitachi派梯中！");
                        return result;
                    }
                    else if (_bufferBytes[2] == 2)
                    {
                        result.DistinationFloorId = totalDataBytes[3];

                        //获取电梯名称
                        var elevatorNameBytes = new byte[4]
                        {
                            totalDataBytes[4],
                            totalDataBytes[5],
                            totalDataBytes[6],
                            totalDataBytes[7],
                        };
                        result.ElevatorName = Encoding.ASCII.GetString(elevatorNameBytes);

                        _logger.LogInformation($"Hitachi串口接收数据3：data:{JsonConvert.SerializeObject(result, JsonUtil.JsonPrintSettings)} ");

                        return result;
                    }
                    else if (_bufferBytes[2] == 3)
                    {
                        //派梯中
                        _logger.LogInformation($"Hitachi派梯无效！");
                        return result;
                    }
                    else
                    {
                        _logger.LogError($"找不到Hitachi回调功能类型！");
                        return null;
                    }
                }
                else
                {
                    _logger.LogError($"找不到Hitachi回调帧类型！");
                    return null;
                }
            }
            else
            {
                if (_bufferBytes.Count() > 1)
                {
                    _bufferBytes.RemoveAt(0);
                    return Analyze(portName, null);
                }
                return null;
            }
        }

        public byte[] ConfirmAnalyze(HitachiReceiveModel obj)
        {
            var bytes = new List<byte>();

            //帧头
            bytes.Add(0xAC);
            //帧类型
            bytes.Add(0x5B);

            //功能码
            bytes.Add(obj.Command);

            //xor
            bytes.Add(0x00);

            //帧尾
            bytes.Add(0xCA);

            return bytes.ToArray();
        }

        private byte[] GetTotalDataBytes(int totalDataLength)
        {
            var resultDataBytes = new byte[totalDataLength];
            for (int i = totalDataLength - 1; i >= 0; i--)
            {
                resultDataBytes[i] = _bufferBytes[i];
                _bufferBytes.RemoveAt(i);
            }
            _logger.LogInformation($"Hitachi串口接收解析前数据数据：data:{resultDataBytes.ToCommaPrintString()} ");

            return resultDataBytes;
        }

        public byte[] SendAnalyze(UnitDispatchSendHandleElevatorModel handleElevator)
        {
            var bytes = new List<byte>();

            //帧头
            bytes.Add(0xAC);
            //帧类型
            bytes.Add(0x5C);

            //系列卡号
            var cardBytes = BitConverter.GetBytes(handleElevator.CardNumber);
            bytes.AddRange(cardBytes.Reverse());

            //手动楼层
            if (handleElevator.ManualRealFloorIds?.FirstOrDefault() != null)
            {
                var byteFloors = handleElevator.ManualRealFloorIds.Select(x => Convert.ToByte(x.Floor.ToString())).ToList();
                var bitByteFloors = ByteBitUtil.ToBitValues(byteFloors, 63);
                bytes.AddRange(bitByteFloors);
            }
            else
            {
                bytes.AddRange(new byte[8]);
            }

            //保留
            //保留
            byte[] reserveBytes = new byte[27];
            for (int x = 0; x < 27; x++)
            {
                reserveBytes[x] = 0xFF;
            }
            bytes.AddRange(reserveBytes);

            //自动楼层
            bytes.Add(Convert.ToByte(handleElevator.AutoRealFloorId));

            //xor
            bytes.Add(0x00);

            //帧尾
            bytes.Add(0xCA);
            byte[] Abyte = bytes.ToArray();
            for (int i = 0; i < 42; i++)
            {
                Abyte[42] ^= Abyte[i];
            }
            return Abyte;
            //return bytes.ToArray();
        }
    }
}
