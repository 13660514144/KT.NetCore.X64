using KT.Proxy.BackendApi.Models;
using KT.Front.WriteCard.SDK;
using KT.Front.WriteCard.Util;
using System;
using System.Collections.Generic;
using System.Text;
using static KT.Front.WriteCard.Entity.CommonClass;
using System.Threading.Tasks;

namespace KT.Front.WriteCard.Entity
{
    public class HitachiRule : WriteRule
    {
        public HitachiRule()
        {
            RuleType = WriteRuleEnum.HITACHI;
            Name = "HITACHI-DFRS目的选层系统";
        }

        public override async Task<bool> WriteCardAsync(List<int> elevatorFloorIds, string deviceCode)
        {
            await D300M.ConnectAsync();

            bool success = await InitCardAsync();
            if (!success)
            {
                return success;
            }
            int count = elevatorFloorIds.Count;
            if (count < 5)
            {
                return WriteCardByBlockOne(elevatorFloorIds, deviceCode);
            }
            else if (count >= 5 && count < 9)
            {
                success = WriteCardByBlockOne(elevatorFloorIds, deviceCode);
                if (success)
                {
                    success = WriteCardByBlockTwo(elevatorFloorIds);
                }
            }
            else if (count >= 9 && count < 14)
            {
                success = WriteCardByBlockOne(elevatorFloorIds, deviceCode);
                if (success)
                {
                    success = WriteCardByBlockTwo(elevatorFloorIds);
                    if (success)
                    {
                        success = WriteCardByBlockThree(elevatorFloorIds);
                    }
                }
            }
            else
            {
                success = WriteCardByBlockOne(elevatorFloorIds, deviceCode);
                if (success)
                {
                    success = WriteCardByBlockTwo(elevatorFloorIds);
                    if (success)
                    {
                        success = WriteCardByBlockThree(elevatorFloorIds);
                        if (success)
                        {
                            success = WriteCardByBlockOther(elevatorFloorIds);
                        }
                    }
                }
            }
            return success;
        }

        public static bool WriteCardByBlockOne(List<int> elevatorFloorIds, string deviceCode)
        {
            byte[] data = new byte[16] { 0x78, 0xB1, 0xA2, 0x00, 0, 0, 0x63, 0x9c, 0xff, 0, 0, 0, 0, 0, 0, 0 };
            data[4] = Convert.ToByte(ConvertHelper.GetHighWord(Convert.ToInt32(deviceCode)));
            data[5] = Convert.ToByte(ConvertHelper.GetLowWord(Convert.ToInt32(deviceCode)));
            int count = elevatorFloorIds.Count > 4 ? 4 : elevatorFloorIds.Count;
            byte startBlock = 12;
            IntPtr cardSetialNum = IntPtr.Zero;
            for (int i = 0; i < count; i++)
            {
                int j = 2 * i;
                data[j + 8] = 0xff;
                data[j + 9] = (byte)elevatorFloorIds[i];
            }
            int c = D300MSdk.MFHLWrite(D300MSdk.Address, D300MSdk.RequestMode, D300MSdk.AuthMode, D300MSdk.BlockAmount, startBlock, D300MSdk.Key, data, D300MSdk.DataLength, ref cardSetialNum);
            if (c != 0)
            {
                return false;
            }
            return true;
        }

        public static bool WriteCardByBlockTwo(List<int> elevatorFloorIds)
        {
            int count = elevatorFloorIds.Count > 8 ? 8 : elevatorFloorIds.Count;
            byte startBlock = 13;
            IntPtr cardSetialNum = IntPtr.Zero;
            byte[] data = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0x01, 0x00, 0x17, 0x3b, 0xff, 0xff, 0, 0, };
            for (int i = 0; i < count - 4; i++)
            {
                int j = 2 * i;
                data[j + 0] = 0xff;
                data[j + 1] = (byte)elevatorFloorIds[4 + i];
            }
            int c = D300MSdk.MFHLWrite(D300MSdk.Address, D300MSdk.RequestMode, D300MSdk.AuthMode, D300MSdk.BlockAmount, startBlock, D300MSdk.Key, data, D300MSdk.DataLength, ref cardSetialNum);
            if (c != 0)
            {
                return false;
            }
            return true;
        }

        public static bool WriteCardByBlockThree(List<int> elevatorFloorIds)
        {
            int count = elevatorFloorIds.Count > 13 ? 13 : elevatorFloorIds.Count;
            byte startBlock = 14;
            IntPtr cardSetialNum = IntPtr.Zero;
            byte[] data = new byte[16] { 0xff, (byte)elevatorFloorIds[8], 0, 0, 0, 0, 0x3f, 0x01, 0, 0, 0, 0, 0, 0, 0, 0, };
            if (elevatorFloorIds.Count > 13 && elevatorFloorIds.Count < 37)
            {
                data[2] = 0x06;
                data[3] = 0;
                data[4] = 0;
                data[5] = 0;
            }
            else if (elevatorFloorIds.Count >= 37 && elevatorFloorIds.Count < 61)
            {
                data[2] = 0x06;
                data[3] = 0x07;
                data[4] = 0;
                data[5] = 0;
            }
            else if (elevatorFloorIds.Count >= 61 && elevatorFloorIds.Count < 85)
            {
                data[2] = 0x06;
                data[3] = 0x07;
                data[4] = 0x08;
                data[5] = 0;
            }
            else if (elevatorFloorIds.Count >= 85)
            {
                data[2] = 0x06;
                data[3] = 0x07;
                data[4] = 0x08;
                data[5] = 0x09;
            }
            else
            {
                data[2] = 0;
                data[3] = 0;
                data[4] = 0;
                data[5] = 0;
            }
            for (int i = 0; i < count - 9; i++)
            {
                int j = 2 * i;
                data[j + 8] = 0xff;
                data[j + 9] = (byte)elevatorFloorIds[i + 9];
            }
            int c = D300MSdk.MFHLWrite(D300MSdk.Address, D300MSdk.RequestMode, D300MSdk.AuthMode, D300MSdk.BlockAmount, startBlock, D300MSdk.Key, data, D300MSdk.DataLength, ref cardSetialNum);
            if (c != 0)
            {
                return false;
            }
            return true;
        }

        public static bool WriteCardByBlockOther(List<int> elevatorFloorIds)
        {
            bool success = true;
            int start = 0;
            byte startBlock = 15;

            while (true)
            {
                byte[] data = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                if ((startBlock + 2) % 4 == 0)
                {
                    startBlock += 2;
                }
                else
                {
                    startBlock += 1;
                }
                for (int i = 0; i < elevatorFloorIds.Count - (13 + start * 8); i++)
                {
                    int j = 2 * i;
                    data[j + 0] = 0xff;
                    data[j + 1] = (byte)elevatorFloorIds[i + (13 + start * 8)];
                }

                IntPtr cardSetialNum = IntPtr.Zero;
                //logger?.LogInformation($"WriteCard Info:{elevatorAuthInfoModel.Name} 正在写{elevatorAuthInfoModel.AuthInfos[i].ElevatorFloorId} ");
                int c = D300MSdk.MFHLWrite(D300MSdk.Address, D300MSdk.RequestMode, D300MSdk.AuthMode, D300MSdk.BlockAmount, startBlock, D300MSdk.Key, data, D300MSdk.DataLength, ref cardSetialNum);
                if (c != 0)
                {
                    success = false;
                }
                start++;
                if ((13 + start * 8) > elevatorFloorIds.Count)
                {
                    break;
                }
            }
            return success;
        }

        public override Task<bool> InitCardAsync()
        {
            bool suceess = true;
            byte startBlock = 7;
            byte[] data = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            for (int i = 0; i < 18; i++)
            {
                if ((startBlock + 2) % 4 == 0)
                {
                    startBlock += 2;
                }
                else
                {
                    startBlock += 1;
                }
                IntPtr cardSetialNum = IntPtr.Zero;
                int c = D300MSdk.MFHLWrite(D300MSdk.Address, D300MSdk.RequestMode, D300MSdk.AuthMode, D300MSdk.BlockAmount, startBlock, D300MSdk.Key, data, D300MSdk.DataLength, ref cardSetialNum);
                if (c != 0)
                {
                    suceess = false;
                }
            }
            return Task.FromResult(suceess);
        }
    }
}
