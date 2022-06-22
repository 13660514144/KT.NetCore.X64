using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Visitor.Interface.Helpers
{
    /// <summary>
    /// 时间事件，使用静态，防止GC回收
    /// </summary>
    public class StatisticTimerHelper
    {
        private Func<Task> _funcTask;
        private Timer _timer;

        public StatisticTimerHelper()
        {

        }

        public void Start(Func<Task> funcTask)
        {
            _funcTask = funcTask;
            _timer = new Timer(StaticCallback, null, 5000, 60000);
        }

        private void StaticCallback(object state)
        {
            _funcTask?.Invoke();
        }
    }
}
