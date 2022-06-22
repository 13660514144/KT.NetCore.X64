using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using log4net.Core;

namespace KT.Elevator.Manage.Service.Devices.Kone.Network
{
    public class SocketServer : IDisposable
    {
        private int _port = 2004;
        private Socket _server = null;
        // 接收信息 
        public Action<object, byte[]> _receiveHandler { get; set; }
        // 输出信息 
        public Action<string> _errorHandler { get; set; }

        private ILogger _logger;
        public SocketServer(ILogger logger)
        {
            _logger = logger;
        }

        public void Init(int port, Action<object, byte[]> receiveMessage, Action<string> errorAction)
        {
            _port = port;
            _receiveHandler = receiveMessage;
            _errorHandler = errorAction;

            Start();
        }

        public void Start()
        {
            _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _server.Bind(new IPEndPoint(IPAddress.Any, _port));
            _server.Listen(20);

            _server.BeginAccept(new AsyncCallback(ClientAccepted), _server);
        }

        /// <summary>
        /// 接收客户
        /// </summary>
        private void ClientAccepted(IAsyncResult ar)
        {
            try
            {
                var socket = ar.AsyncState as Socket;
                var client = socket.EndAccept(ar);

                //客户端IP地址和端口信息
                var clientipe = (IPEndPoint)client.RemoteEndPoint;

                var model = new SocketInfo();
                model.Init(_errorHandler);
                model.Buffer = new byte[1024];
                model.Socket = client;

                //异步接收客户端的消息
                client.BeginReceive(model.Buffer, 0, model.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), model);

                //准备接受下一个客户端请求(异步)
                socket.BeginAccept(new AsyncCallback(ClientAccepted), socket);

            }
            catch (Exception ex)
            {
                _server = null;
                _errorHandler?.Invoke(String.Format("Error:{0}", ex.Message));
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            var model = ar.AsyncState as SocketInfo;
            try
            {
                var socket = model.Socket;
                //客户端IP地址和端口信息
                IPEndPoint clientipe = (IPEndPoint)model.RemoteEndPoint;
                var len = socket.EndReceive(ar);

                //读取出来消息内容
                var bytes = new byte[len];
                Array.Copy(model.Buffer, 0, bytes, 0, len);

                //异步接收下一个消息
                socket.BeginReceive(model.Buffer, 0, model.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), model);

                if (len > 0)
                {
                    _receiveHandler?.Invoke(model, bytes);
                }

            }
            catch (Exception ex)
            {
                //断开连接
                model.Socket.Close();
                _errorHandler?.Invoke(String.Format("Error:{0}", ex.Message));
            }
        }


        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            if (_server != null)
            {
                _server.Close();
                _server.Dispose();
                _server = null;
            }
        }
    }
}
