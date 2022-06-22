using KT.Common.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Kone.ToolApp.Models
{
    public class DopMaskRecordViewModel : BindableBase
    {
        private string _id;
        private string _elevatorServer;
        private string _type;
        private string _operate;
        private bool _isSuccess;
        private int _status;
        private string _sendData;
        private string _receiveData;
        private string _message;
        private long _editedTime;

        /// <summary>
        /// Id主键
        /// </summary>
        public string Id
        {
            get => _id;
            set
            {
                SetProperty(ref _id, value);
            }
        }

        /// <summary>
        /// 电梯服务Address
        /// </summary>
        public string ElevatorServer
        {
            get => _elevatorServer;
            set
            {
                SetProperty(ref _elevatorServer, value);
            }
        }

        /// <summary>
        /// 类型
        /// <see cref="KoneEliSequenceTypeEnum"/>
        /// </summary>
        public string Type
        {
            get => _type;
            set
            {
                SetProperty(ref _type, value);
            }
        }

        /// <summary>
        /// 操作
        /// <see cref="KoneEliMaskRecoredOperateEnum"/>
        /// </summary>
        public string Operate
        {
            get => _operate;
            set
            {
                SetProperty(ref _operate, value);
            }
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess
        {
            get => _isSuccess;
            set
            {
                SetProperty(ref _isSuccess, value);
            }
        }

        /// <summary>
        /// 状态，返回结果状态
        /// </summary>
        public int Status
        {
            get => _status;
            set
            {
                SetProperty(ref _status, value);
            }
        }

        /// <summary>
        /// 发送数据，(如果是接收类型操作，发送数据可能会存在关联错误）
        /// </summary>
        public string SendData
        {
            get => _sendData;
            set
            {
                SetProperty(ref _sendData, value);
            }
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        public string ReceiveData
        {
            get => _receiveData;
            set
            {
                SetProperty(ref _receiveData, value);
            }
        }

        /// <summary>
        /// 消息，一般为错误消息
        /// </summary>
        public string Message
        {
            get => _message;
            set
            {
                SetProperty(ref _message, value);
            }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public long EditedTime
        {
            get => _editedTime;
            set
            {
                SetProperty(ref _editedTime, value);
            }
        }

    }
}
