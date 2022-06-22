using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Kone.Network
{
    public class SocketClient
    {
        /// <summary>
        /// 关闭时
        /// </summary>
        public Action ClosedHandler { get; set; }

        /// <summary>
        /// 接收信息
        /// </summary>
        public Func<object, byte[], Task> ReceivedHandler { get; set; }

        /// <summary>
        /// 输出信息
        /// </summary>
        public Action<Exception> ErrorHandler { get; set; }

        private Socket _socket = null;
        /// <summary>
        /// 客户端IP地址和端口信息
        /// </summary>
        private IPEndPoint _localEP = null;
        private byte[] _buffer = new byte[1024];
        private IPEndPoint _remoteEP = null;

        public SocketClient()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _localEP = (IPEndPoint)_socket.LocalEndPoint;
        }

        public SocketClient(IPAddress address, int port)
        {
            _localEP = new IPEndPoint(address, port);
            _socket.Bind(_localEP);
        }

        public void Connect(string ip, int port)
        {
            var address = IPAddress.Parse(ip);
            _remoteEP = new IPEndPoint(address, port);
            ConnectSocket();
        }

        /// <summary>
        /// 开始异步接收
        /// </summary>
        public void BeginReceive()
        {
            this.BeginReceive(_socket);
        }

        /// <summary>
        /// 开始异步接收
        /// </summary>
        /// <param name="socket"></param>
        private void BeginReceive(Socket socket)
        {
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
        }

        /// <summary>
        /// 异步接收
        /// </summary>
        private void ReceiveCallback(IAsyncResult ar)
        {
            var socket = ar.AsyncState as Socket;
            try
            {
                var len = socket.EndReceive(ar);

                //读取出来消息内容
                var bytes = new byte[len];
                Array.Copy(_buffer, 0, bytes, 0, len);

                //异步接收下一个消息
                this.BeginReceive(socket);

                if (len > 0)
                {
                    ReceivedHandler?.Invoke(socket, bytes);
                }
            }
            catch (Exception ex)
            {
                //断开连接
                socket.Close();
                ErrorHandler?.Invoke(ex);
            }
        }

        /// <summary>
        /// 获取*是否远程主机
        /// </summary>
        public bool Connected
        {
            get
            {
                return _socket.Connected;
            }
        }

        /// <summary>
        /// 发送
        /// </summary>
        public void Send(byte[] value)
        {
            if (!_socket.Connected)
            {
                ConnectSocket();
            }
            _socket.Send(value);
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Close()
        {
            _socket.Close();
            _socket.Dispose();
        }

        internal void ConnectSocket()
        {
            try
            {
                _socket.Connect(_remoteEP);
                BeginReceive();
            }
            catch (Exception ex)
            {
                ErrorHandler?.Invoke(ex);
            }
        }
    }
}
