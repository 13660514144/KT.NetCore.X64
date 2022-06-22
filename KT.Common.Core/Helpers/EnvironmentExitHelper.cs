using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Common.Core.Helpers
{
    public class EnvironmentExitHelper
    {
        private Timer _timer;
        public EnvironmentExitHelper(int secondTime = 10)
        {
            _timer = new Timer(EnvironmentExitTimerCallback, null, secondTime * 1000, 1 * 1000);
        }

        private void EnvironmentExitTimerCallback(object state)
        {
            Environment.Exit(-1);
        }

        private static EnvironmentExitHelper _environmentExitHelper;
        public static void Start()
        {
            Task.Run(() =>
            {
                _environmentExitHelper = new EnvironmentExitHelper();
            });
        }
    }
}
