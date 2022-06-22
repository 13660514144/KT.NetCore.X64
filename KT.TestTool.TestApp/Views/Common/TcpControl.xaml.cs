using KT.Common.WpfApp.Attributes;
using KT.TestTool.TestApp.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KT.TestTool.TestApp.Views
{
    /// <summary>
    /// TcpControl.xaml 的交互逻辑
    /// </summary>
    [NavigationItem]
    public partial class TcpControl : UserControl
    {
        private TcpControlViewModel _viewModel;
        private SocketSettings _socketSettings;
        private ILogger<TcpControl> _logger;
        /// <summary>
        /// 用于Tcp接收的网络服务类
        /// </summary>
        private TcpClient _tcpClient;
        private IPEndPoint _localPoint;
        private IPEndPoint _sendPoint;
        /// <summary>
        /// 线程：不断监听Tcp报文
        /// </summary>
        private Task _udpTask;

        public TcpControl(ILogger<TcpControl> logger, TcpControlViewModel viewModel, IOptions<SocketSettings> socketSettings)
        {
            InitializeComponent();

            _viewModel = viewModel;
            _socketSettings = socketSettings.Value;
            _logger = logger;

            this.DataContext = _viewModel;
        }

        private void Btn_StartReceiveTcpData_Click(object sender, RoutedEventArgs e)
        {
            // 未监听的情况，开始监听
            _viewModel.SocketInfo.IsStarted = true;

            IPAddress sendIp = IPAddress.Parse(_viewModel.SocketInfo.SendIp);
            _sendPoint = new IPEndPoint(sendIp, _viewModel.SocketInfo.SendPort);

            IPAddress ip = IPAddress.Parse(_viewModel.SocketInfo.LocalIp);
            // 本机IP和监听端口号
            _localPoint = new IPEndPoint(ip, _viewModel.SocketInfo.LocalPort);
            _tcpClient = new TcpClient(_localPoint);
            _tcpClient.Client.Connect(_sendPoint);

            _udpTask = ReceiveMessage();
            _viewModel.ScrollMessage.InsertTop("Tcp监听器已成功启动");
        }


        /// <summary>
        /// 发送数据
        /// </summary> 
        public void SendMessage(string message)
        {
            Task.Run(() =>
            {
                _viewModel.ScrollMessage.InsertTop("Tcp发送器发送数据：ip:{0} port:{0} message:{1} ", _sendPoint, message);
                try
                {
                    var bytes = Encoding.ASCII.GetBytes(message);
                    if (!_tcpClient.Connected)
                    {
                        _tcpClient.Connect(_sendPoint);
                    }
                    _tcpClient.Client.Send(bytes);
                }
                catch (Exception ex)
                {
                    _viewModel.ScrollMessage.InsertTop("Tcp发送器发送数据失败：ip:{0} port:{1} ex:{2} ", _sendPoint.Address, _sendPoint.Port, ex);
                }
            });
        }

        /// <summary>
        /// 接收数据
        /// </summary> 
        private Task ReceiveMessage()
        {
            return Task.Run(() =>
            {
                try
                {
                    byte[] byteReceive = new byte[1024];
                    var stream = _tcpClient.GetStream();
                    while (_viewModel.SocketInfo.IsStarted)
                    {
                        //无数据读取不操作，否则中断时会报错
                        if (_tcpClient.Available <= 0)
                        {
                            Thread.Sleep(100);
                            continue;
                        }
                        stream.Read(byteReceive, 0, byteReceive.Length);
                        string message = Encoding.ASCII.GetString(byteReceive, 0, byteReceive.Length);
                        //处理数据
                        _viewModel.ScrollMessage.InsertTop("接收数据：message:{0} ", message);
                    }
                    stream.Close();
                    _logger.LogInformation("TCP设备已关闭！");
                }
                catch (Exception ex)
                {
                    _logger.LogError("TCP设备接收数据出错：{0} ", ex);
                }
            });
        }

        private void Btn_SendTcpData_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(_viewModel.SocketInfo.SendText);
        }

        private void Btn_CloseReceiveTcpData_Click(object sender, RoutedEventArgs e)
        {

            // 正在监听的情况，终止监听
            _viewModel.SocketInfo.IsStarted = false;
            _udpTask.Wait();
            _tcpClient.Close();
            _viewModel.ScrollMessage.InsertTop("Tcp监听器已成功关闭");
        }
    }
}
