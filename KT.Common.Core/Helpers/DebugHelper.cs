using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Common.Core.Helpers
{
    /// <summary>
    /// 调试帮助
    /// </summary>
    public class DebugHelper
    {
        /// <summary>
        /// Debug模式运行
        /// </summary>
        /// <param name="debugAction"></param>
        /// <param name="releaseAction"></param>
        public static async Task<T> DebugRunAsync<T>(Func<Task<T>> debugAction, Func<Task<T>> releaseAction)
        {
#if RELEASE
            return await releaseAction.Invoke();
#elif !DEBUG
           return await releaseAction.Invoke(); 
#else
            return await debugAction.Invoke();
#endif
        }

        /// <summary>
        /// Debug模式运行
        /// </summary>
        /// <param name="action"></param>
        public static async Task DebugRunAsync(Func<Task> debugAction)
        {
#if RELEASE

#elif !DEBUG
            
#else
            await debugAction.Invoke();
#endif
        }

        /// <summary>
        /// Debug模式运行
        /// </summary>
        /// <param name="debugAction"></param>
        /// <param name="releaseAction"></param>
        public static T DebugRun<T>(Func<T> debugAction, Func<T> releaseAction)
        {
#if RELEASE
            return releaseAction.Invoke();
#elif !DEBUG
           return releaseAction.Invoke(); 
#else
            return debugAction.Invoke();
#endif
        }

        /// <summary>
        /// Debug模式运行
        /// </summary>
        /// <param name="action"></param>
        public static void DebugRun(Action debugAction)
        {
#if RELEASE

#elif !DEBUG
            
#else
            debugAction.Invoke();
#endif
        }
    }
}
