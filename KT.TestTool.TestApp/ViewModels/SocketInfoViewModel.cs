using Panuon.UI.Silver.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.TestTool.TestApp.ViewModels
{
    public class SocketInfoViewModel : PropertyChangedBase
    {
        private bool _isStarted;
        private string _localIp;
        private int _localPort;
        private string _sendIp;
        private int _sendPort;
        private string _sendText;

        public SocketInfoViewModel()
        {
            _isStarted = false;
            _localIp = "192.168.0.180";
            _localPort = 55221;
            _sendIp = "192.168.0.124";
            _sendPort = 12345;
            _sendText = "AT+STACH2=1,1\r\n";

        }
        /// <summary>
        /// 是否启动监听
        /// </summary>
        public bool IsStarted
        {
            get { return _isStarted; }
            set
            {
                _isStarted = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// ip地址
        /// </summary>
        public string LocalIp
        {

            get { return _localIp; }
            set
            {
                _localIp = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 端口
        /// </summary>
        public int LocalPort
        {

            get { return _localPort; }
            set
            {
                _localPort = value;
                NotifyPropertyChanged();
            }
        }

        public string SendIp
        {
            get
            {
                return _sendIp;
            }

            set
            {
                _sendIp = value;
                NotifyPropertyChanged();
            }
        }

        public int SendPort
        {
            get
            {
                return _sendPort;
            }

            set
            {
                _sendPort = value;
                NotifyPropertyChanged();
            }
        }

        public string SendText
        {
            get
            {
                return _sendText;
            }

            set
            {
                _sendText = value;
                NotifyPropertyChanged();
            }
        }
    }
}
