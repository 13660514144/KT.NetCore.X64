using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Model.Kone
{
    public class DopMaskRecordModel
    {
        /// <summary>
        /// Id主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 电梯服务Address
        /// </summary>
        public string ElevatorServer { get; set; }

        /// <summary>
        /// 类型
        /// <see cref="KoneEliSequenceTypeEnum"/>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 操作
        /// <see cref="KoneEliMaskRecoredOperateEnum"/>
        /// </summary>
        public string Operate { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 状态，返回结果状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 发送数据，(如果是接收类型操作，发送数据可能会存在关联错误）
        /// </summary>
        public string SendData { get; set; }

        /// <summary>
        /// 接收数据
        /// </summary>
        public string ReceiveData { get; set; }

        /// <summary>
        /// 消息，一般为错误消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public long EditedTime { get; set; }
    }
}
