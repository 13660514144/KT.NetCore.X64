using KT.Quanta.Service.Elevator.Dtos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Helpers
{
    /// <summary>
    /// 派梯等待返回结果
    /// 单例
    /// </summary>
    public class FloorHandleElevatorResponseList
    {
        private ConcurrentDictionary<string, object> _responseActions;

        public FloorHandleElevatorResponseList()
        {
            _responseActions = new ConcurrentDictionary<string, object>();
        }

        public void Add(string key, object action)
        {
            _responseActions.AddOrUpdate(key, action, UpdateValueFactory(action));
        }

        private Func<string, object, object> UpdateValueFactory(object arg2)
        {
            return (key, oldData) => arg2;
        }

        public void EndHandle(string key, FloorHandleElevatorSuccessModel handleResult)
        {
            var isGet = _responseActions.TryGetValue(key, out object action);
            if (isGet)
            {
                ((Action<FloorHandleElevatorSuccessModel>)action)?.Invoke(handleResult);
                _responseActions.TryRemove(key, out action);
            }
        }
    }
}
