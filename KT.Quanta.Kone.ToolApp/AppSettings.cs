using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Kone.ToolApp
{
    public class AppSettings
    {
        public string ServerAddress { get; set; }

        public int DisonnectMaskState { get; set; } = 1;

        public int ConnectMaskState { get; set; } = 2;

        public int DisconnectedAndConnectedMaskState { get; set; } = 3;
    }
}
