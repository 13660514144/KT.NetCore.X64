using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Common
{
    /// <summary>
    /// 派梯messageKey与messageId关系
    /// 单例
    /// </summary>
    public class HandleElevatorSequenceList<T1, T2> : IHandleElevatorSequenceList<T1, T2>
    {
        private ConcurrentDictionary<T1, T2> _responseActions;

        public HandleElevatorSequenceList()
        {
            _responseActions = new ConcurrentDictionary<T1, T2>();
        }

        public void Add(T1 key, T2 data)
        {
            _responseActions.AddOrUpdate(key, data, UpdateValueFactory(data));
        }

        private Func<T1, T2, T2> UpdateValueFactory(T2 data)
        {
            return (key, oldData) => data;
        }

        public T2 Get(T1 key)
        {
            bool isGet = _responseActions.TryGetValue(key, out T2 @value);
            if (isGet)
            {
                return @value;
            }
            return default;
        }
    }
}
