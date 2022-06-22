using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Kone.Models
{
    public class MsgPaddle : MsgEnity
    {
        public string Version { get; set; }
        public long MsgID { get; set; }
        public long RespID { get; set; }
        public long AppMsgType { get; set; }
        public string Timestamp { get; set; }

        public override void Analysis(byte[] buffer)
        {
            base.Analysis(buffer);

            this.AppMsgType = buffer.toHexInt(9, 2);
            this.Version = buffer.GetString(11, 1);
            this.MsgID = buffer.toHexInt(16, 1);
            this.RespID = buffer.toHexInt(17, 1);

            this.Timestamp = buffer.GetHexString(21, 8);

            this.terminal_Id = buffer.toHexInt(29, 2);
            this.dest_call_Type = buffer.toHexInt(31, 2);
            this.responce_code = buffer.toHexInt(33, 1);

            this.passanger_Id_hex = buffer.GetHexString(34, 16);
            this.serving_elevator_id = buffer.toHexInt(50, 1);
            this.serving_deck_id = buffer.toHexInt(51, 1);

            this.source_floor = buffer.toHexInt(52, 1);
            this.source_side = buffer.toHexInt(53, 1);
            this.departure_lobby = buffer.toHexInt(54, 1);
            this.destination_floor = buffer.toHexInt(55, 1);
            this.destination_side = buffer.toHexInt(56, 1);

            this.estimated_time_of_elevator_arriva = buffer.toHexInt(57, 2);
            this.transfer_floor = buffer.toHexInt(59, 1);
            this.transfer_side = buffer.toHexInt(60, 1);
            this.transfer_lobby = buffer.toHexInt(61, 1);

            this.visual_guidance_id = buffer.toHexInt(62, 1);
            this.acu_data_size = buffer.toHexInt(63, 1);

            var datasize = Convert.ToInt32(acu_data_size);
            this.acu_data = buffer.GetHexString(64, datasize);
        }


        public long visual_guidance_id { get; set; }
        public long acu_data_size { get; set; }

        private string _acu_data = null;
        public string acu_data
        {
            set { _acu_data = value; }
            get
            {
                if (String.IsNullOrEmpty(_acu_data))
                    return "";
                return _acu_data;
            }
        }


        /// <summary>
        /// 终端Id
        /// </summary>
        public long terminal_Id { get; set; }

        /// <summary>
        /// 呼叫类型
        /// </summary>
        public long dest_call_Type { get; set; }

        /// <summary>
        /// 响应代码
        /// </summary>
        public long responce_code { get; set; }

        /// <summary>
        /// 乘客CardId
        /// </summary>
        public string passanger_Id_hex
        {
            set { _passanger_Id = value; }
            get
            {
                if (String.IsNullOrEmpty(_passanger_Id))
                {
                    return "";
                }
                return _passanger_Id;
            }
        }
        private string _passanger_Id = null;

        /// <summary>
        /// 服务电梯Id
        /// </summary>
        public long serving_elevator_id { get; set; }

        /// <summary>
        /// 服务甲板身份
        /// </summary>
        public long serving_deck_id { get; set; }

        /// <summary>
        /// 来源楼
        /// </summary>
        public long source_floor { get; set; }

        /// <summary>
        /// 来源方
        /// </summary>
        public long source_side { get; set; }

        /// <summary>
        /// 出发大厅
        /// </summary>
        public long departure_lobby { get; set; }

        /// <summary>
        /// 目的地楼层
        /// </summary>
        public long destination_floor { get; set; }

        /// <summary>
        /// 目的地方
        /// </summary>
        public long destination_side { get; set; }

        /// <summary>
        /// 预计电梯到达时间
        /// </summary>
        public long estimated_time_of_elevator_arriva { get; set; }

        /// <summary>
        /// 转移楼层
        /// </summary>
        public long transfer_floor { get; set; }

        /// <summary>
        /// 转移方
        /// </summary>
        public long transfer_side { get; set; }

        /// <summary>
        /// 转移大厅
        /// </summary>
        public long transfer_lobby { get; set; }
    }

}
