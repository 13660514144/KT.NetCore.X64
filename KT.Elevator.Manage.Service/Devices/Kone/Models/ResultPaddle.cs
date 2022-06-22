using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Kone.Models
{
    public class ResultPaddle : MsgEnity
    {
        public override void Analysis(byte[] buffer)
        {
            base.Analysis(buffer);

            var size = buffer.Length;
            var buffer1 = new byte[2] { buffer[30], buffer[29] };
            var buffer2 = new byte[2] { buffer[32], buffer[31] };

            //int terminal_id, int dest_call_type, int call_state
            this.terminal_Id = buffer1.GetHexString().toHexInt();
            this.dest_call_Type = buffer2.GetHexString().toHexInt();

            this.call_state = buffer.GetHexString(33, 1).toHexInt();

            this.passanger_Id_hex = buffer.GetHexString(34, 16);

            //int source_floor, int source_side, int destination_floor, int destination_side,int Lifts
            this.source_floor = buffer.GetHexString(size - 6, 1).toHexInt();
            this.source_side = buffer.GetHexString(size - 5, 1).toHexInt();
            this.destination_floor = buffer.GetHexString(size - 4, 1).toHexInt();
            this.destination_side = buffer.GetHexString(size - 3, 1).toHexInt();
            this.Lifts = buffer.GetHexString(size - 1, 1).toHexInt();
        }

        public long Lifts { get; set; }
        public long call_state { get; set; }

        /// <summary>
        /// 终端Id
        /// </summary>
        public long terminal_Id { get; set; }

        /// <summary>
        /// 呼叫类型
        /// </summary>
        public long dest_call_Type { get; set; }

        /// <summary>
        /// 来源楼
        /// </summary>
        public long source_floor { get; set; }

        /// <summary>
        /// 来源方
        /// </summary>
        public long source_side { get; set; }

        /// <summary>
        /// 目的地楼层
        /// </summary>
        public long destination_floor { get; set; }

        /// <summary>
        /// 乘客CardId
        /// </summary>
        public string passanger_Id_hex { get; set; }

        /// <summary>
        /// 目的地方
        /// </summary>
        public long destination_side { get; set; }

        /// <summary>
        /// 服务电梯Id
        /// </summary>
        public long serving_elevator_id { get; set; }

    }
}
