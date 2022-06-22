using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KT.WinPak.WebApi.V48.Common.Queues
{
    /// <summary>
    /// 单例执行队列，用于API调用时存入队列中单条逐步执行，防止COM组件出错
    /// </summary>
    public class SingletonExecuteQueue
    {
        private ILogger _logger;
        private ConcurrentQueue<Action> _actions;

        public SingletonExecuteQueue(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("SingletonExecuteQueue");
            _actions = new ConcurrentQueue<Action>();
        }

        public void Add(Action action)
        {
            _actions.Append(action);
        }

        public void Exce()
        {
            while (true)
            {
                _actions.TryDequeue(out Action action);
                if (action == null)
                {
                    Thread.Sleep(3000);
                    continue;
                }
                action.Invoke();
            }
        }
    }
}
