using System;
using System.Net;
using System.Net.Sockets;

namespace KT.Elevator.Manage.Service.Devices.Kone.Network
{
    public class SocketInfo
    {
        private Action<string> _errorAction;

        public Socket Socket { get; set; }
        public byte[] Buffer { get; set; }

        //计数
        private int _times;

        public SocketInfo()
        {
        }

        public void Init(Action<string> errorAction)
        {
            _errorAction = errorAction;
        }

        public IPEndPoint RemoteEndPoint
        {
            get
            {
                return (IPEndPoint)Socket.RemoteEndPoint;
            }
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="buffer"></param>
        public void Send(byte[] buffer)
        {
            try
            {
                if (Socket != null && Socket.Connected)
                {
                    _times += 1;
                    Socket.Send(buffer);
                }
            }
            catch (Exception ex)
            {
                Socket.Close();
                _errorAction?.Invoke(string.Format("Error:{0}", ex.Message));
            }
        }

    }
}
