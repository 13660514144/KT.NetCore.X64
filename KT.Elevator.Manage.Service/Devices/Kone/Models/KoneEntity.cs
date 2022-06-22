using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Kone.Models
{
    public class KoneEntity
    {
        /// <summary>
        /// 心跳*格式
        /// </summary>
        /// <returns></returns>
        public byte[] HeartBeat()
        {
            var date = DateTime.Now;
            var realDate = date.GetHexBytes(8);
            var buffer2 = new byte[] { 0x81, 0x11, 0, 0, 0, 0, 0x80, 2, 0x80 };
            var buffer = new byte[17];

            buffer2.Copyto(buffer, 0);
            realDate.Copyto(buffer, 9);
            return buffer;
        }

        /// <summary>
        /// 派梯回传
        /// </summary>
        public byte[] PaddleLift(MsgPaddle model)
        {
            var times = 0x01;
            //buffer1
            var buffer1 = new byte[] { 0x80, 0, 0, 0, 0x40, 0x80, 0, 0x80, 0, 0, 2, 1, 0, 0, 0, 0, 0x01, 3, 0, 0, 0 };
            //80000000408000800000020100000000080000000001D4AE2ADCC11D900001000600000000000000000000000000000000000000020300050400000000000000

            //buffer2
            var date = DateTime.Now;
            var buffer2 = date.GetHexBytes(8);

            //buffer3
            var buffer3 = new byte[34];
            var terminal_Id = model.terminal_Id.toHexBtyes(2);
            var dest_call_Type = model.dest_call_Type.toHexBtyes(2);
            var responce_code = model.responce_code.toHexBtyes(1);
            buffer3[0] = terminal_Id[0];
            buffer3[1] = terminal_Id[1];
            buffer3[2] = dest_call_Type[0];
            buffer3[3] = dest_call_Type[1];
            buffer3[4] = responce_code[0];

            var passanger_Id_hex = model.passanger_Id_hex.toHexBtyes(16);
            var serving_elevator_id = model.serving_elevator_id.toHexBtyes(1);
            var serving_deck_id = model.serving_deck_id.toHexBtyes(1);

            passanger_Id_hex.Copyto(buffer3, 5);
            buffer3[21] = serving_elevator_id[0];
            buffer3[22] = serving_deck_id[0];

            var source_floor = model.source_floor.toHexBtyes(1);
            var source_side = model.source_side.toHexBtyes(1);
            var departure_lobby = model.departure_lobby.toHexBtyes(1);
            var destination_floor = model.destination_floor.toHexBtyes(1);
            var destination_side = model.destination_side.toHexBtyes(1);

            buffer3[23] = source_floor[0];
            buffer3[24] = source_side[0];
            buffer3[25] = departure_lobby[0];
            buffer3[26] = destination_floor[0];
            buffer3[27] = destination_side[0];

            var estimated_time_of_elevator_arriva = model.estimated_time_of_elevator_arriva.toHexBtyes(2);
            var transfer_floor = model.transfer_floor.toHexBtyes(1);
            var transfer_side = model.transfer_side.toHexBtyes(1);
            var transfer_lobby = model.transfer_lobby.toHexBtyes(1);
            var visual_guidance_id = model.visual_guidance_id.toHexBtyes(1);

            buffer3[28] = estimated_time_of_elevator_arriva[0];
            buffer3[29] = estimated_time_of_elevator_arriva[1];
            buffer3[30] = transfer_floor[0];
            buffer3[31] = transfer_side[0];
            buffer3[32] = transfer_lobby[0];
            buffer3[33] = visual_guidance_id[0];

            //buffer4
            var size = Convert.ToInt32(model.acu_data_size);
            var buffer4 = new byte[size + 1];

            var acu_data_size = model.acu_data_size.toHexBtyes(1);
            var acu_data = model.acu_data.toHexBtyes(size);

            buffer4[0] = acu_data_size[0];
            acu_data.Copyto(buffer4, 1);

            var len1 = buffer1.Length;
            var len2 = buffer2.Length + len1;
            var len3 = buffer3.Length + len2;
            var length = buffer4.Length + len3;

            var buffer = new byte[length];
            buffer1.Copyto(buffer, 0);
            buffer2.Copyto(buffer, len1);
            buffer3.Copyto(buffer, len2);
            buffer4.Copyto(buffer, len3);
            Console.WriteLine(buffer);
            return buffer;
        }

        /// <summary>
        /// 派梯请求
        /// </summary>
        //public byte[] calllift(int terminal_id, int dest_call_type, int call_state, int source_floor, int source_side, int destination_floor, int destination_side, int Lifts)
        public byte[] calllift(ResultPaddle model)
        {
            var date = DateTime.Now;
            var realDate = date.GetHexBytes(8);

            var buffer2 = new byte[] { 0x81, 0x38, 0, 0, 0, 0, 0x80, 0, 0x80 };
            var buffer3 = new byte[] { 1, 0, 1, 0, 0, 0, 0, 3, 0, 0, 0, 0 };

            var buffer4 = new byte[0x1b];

            var terminal_Id = model.terminal_Id.toHexBtyes(2);
            var dest_call_Type = model.dest_call_Type.toHexBtyes(2);
            var call_state = model.call_state.toHexBtyes(1);

            buffer4[0] = terminal_Id[1];
            buffer4[1] = terminal_Id[0];
            buffer4[2] = dest_call_Type[1];
            buffer4[3] = dest_call_Type[0];
            buffer4[4] = call_state[0];

            for (int i = 0; i < 0x10; i++)
            {
                buffer4[5 + i] = 0;
            }

            var source_floor = model.source_floor.toHexBtyes(1);
            var source_side = model.source_side.toHexBtyes(1);
            var destination_floor = model.destination_floor.toHexBtyes(1);
            var destination_side = model.destination_side.toHexBtyes(1);
            var Lifts = model.Lifts.toHexBtyes(1);

            buffer4[0x15] = source_side[0];
            buffer4[0x16] = source_floor[0];
            buffer4[0x17] = destination_floor[0];
            buffer4[0x18] = destination_side[0];
            buffer4[0x19] = 1;
            buffer4[0x1a] = Lifts[0];


            var buffer = new byte[0x38];

            buffer2.Copyto(buffer, 0);
            buffer3.Copyto(buffer, 9);
            realDate.Copyto(buffer, 21);
            buffer4.Copyto(buffer, 29);

            return buffer;
        }

        public byte[] disconnet(int reason)
        {
            var date = DateTime.Now;
            var realDate = date.GetHexBytes(8);

            var buffer = new byte[0x1f];
            buffer[0] = 0x81;
            buffer[1] = 0x1f;
            buffer[2] = 0;
            buffer[3] = 0;
            buffer[4] = 0;
            buffer[5] = 0;
            buffer[6] = 0x80;
            buffer[7] = 0;
            buffer[8] = 0x80;

            byte[] buffer2 = new byte[] { 0x66, 0, 1, 0, 0, 0, 0, 3, 0, 0, 0, 0 };
            buffer2.Copyto(buffer, 9);
            realDate.Copyto(buffer, 21);

            if (reason == 1)
            {
                buffer[0x1d] = 1;
                buffer[30] = 0;
                return buffer;
            }
            if (reason == 3)
            {
                buffer[0x1d] = 3;
                buffer[30] = 0;
            }
            return buffer;
        }
    }
}
