using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Common.Core.Helpers
{
    public class RetryHelper
    {
        /// <summary>
        /// 重试操作，包含原方法执行，最终异常抛出
        /// </summary>
        /// <typeparam name="T">操作结果类型</typeparam>
        /// <param name="title">标题</param>
        /// <param name="retimes">重试次数</param>
        /// <param name="attachTimes">附加方法执行次数</param>
        /// <param name="operateAction">操作方法</param>
        /// <param name="attachAction">附加操作方法，用于失败后执行附加方法，再重试操作</param> 
        /// <param name="retryWhereAction">重试条件</param> 
        /// <param name="currentTimes">累计操作次数</param>
        /// <returns>操作返回结果</returns>
        public static async Task<T> StartAttachAsync<T>(ILogger logger, int retimes, int attachTimes,
            Func<T> operateAction, Action attachAction, Func<Exception, bool> retryWhereAction)
        {
            var operateTask = TaskRun(operateAction);
            return await StartAttachAsync<T>(logger, retimes, attachTimes, operateTask, attachAction, retryWhereAction);
        }

        private static Func<Task<T>> TaskRun<T>(Func<T> operateAction)
        {
            return new Func<Task<T>>(async () =>
            {
                return await Task.Run(() =>
                {
                    return operateAction.Invoke();
                });
            });
        }

        /// <summary>
        /// 重试操作，包含原方法执行，最终异常抛出
        /// </summary>
        /// <typeparam name="T">操作结果类型</typeparam>
        /// <param name="title">标题</param>
        /// <param name="retimes">重试次数</param>
        /// <param name="attachTimes">附加方法执行次数</param>
        /// <param name="operateAction">操作方法</param>
        /// <param name="attachAction">附加操作方法，用于失败后执行附加方法，再重试操作</param> 
        /// <param name="retryWhereAction">重试条件</param> 
        /// <param name="currentTimes">累计操作次数</param>
        /// <returns>操作返回结果</returns>
        public static async Task<T> StartAttachAsync<T>(ILogger logger, int retimes, int attachTimes, Func<Task<T>> operateAction, Action attachAction, Func<Exception, bool> retryWhereAction)
        {
            try
            {
                return await operateAction?.Invoke();
            }
            catch (Exception ex)
            {
                logger.LogInformation("执行操作方法失败：ex:{0} ", ex);
                var isRetry = retryWhereAction?.Invoke(ex);
                if (isRetry.HasValue && isRetry.Value)
                {
                    return await StartAttachChildAsync(logger, ex, retimes, attachTimes, operateAction, attachAction);
                }
                else
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 重试操作，包含原方法执行，最终异常抛出
        /// </summary>
        /// <typeparam name="T">操作结果类型</typeparam>
        /// <param name="times">重试次数</param>
        /// <param name="operateAction">操作方法</param>
        /// <param name="attachAction">重试操作方法</param> 
        /// <param name="currentTimes">累计操作次数</param>
        /// <returns>操作返回结果</returns>
        private static async Task<T> StartAttachChildAsync<T>(ILogger logger, Exception oldEx, int times, int attachTimes, Func<Task<T>> operateAction, Action attachAction, int currentTimes = 0, int currentAttachTimes = 0)
        {
            //执行附加方法
            try
            {
                if (currentAttachTimes < attachTimes)
                {
                    attachAction?.Invoke();
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation("执行附加方法失败：ex:{0} ", ex);
                currentAttachTimes++;
                if (currentAttachTimes < attachTimes)
                {
                    Thread.Sleep(currentAttachTimes * 1000);
                    await StartAttachChildAsync(logger, oldEx, times, attachTimes, operateAction, attachAction, currentTimes, currentAttachTimes);
                }
            }

            //附加方法执行成功或执行次数结束执行操作方法
            try
            {
                if (currentTimes < attachTimes)
                {
                    return await operateAction?.Invoke();
                }
                else
                {
                    throw oldEx;
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation("执行重试操作失败：ex:{0} ", ex);
                currentTimes++;
                if (currentTimes < times)
                {
                    Thread.Sleep(currentTimes * 1000);
                    return await StartAttachChildAsync(logger, oldEx, times, attachTimes, operateAction, attachAction, currentTimes, currentAttachTimes);
                }
                else
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 重试操作，不包含原方法执行
        /// </summary>
        /// <param name="retimes">重试次数</param>
        /// <param name="alwayTryAction">总是执行重试前操作，如退出登录再重试、清除缓存再重试等</param>
        /// <param name="retryAction">操作方法</param>
        /// <param name="errorAction">重试完成还是错误执行</param>
        /// <param name="currentTimes">累计操作次数</param>
        public static async Task StartRetryAsync(ILogger logger, int retimes, Action alwayTryAction, Action retryAction, Action<Exception> errorAction, int currentTimes = 0)
        {
            try
            {
                //执行重试前操作方法，不重试不执行
                if (currentTimes < retimes)
                {
                    alwayTryAction?.Invoke();
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation("执行重试前操作失败：ex:{0} ", ex);
            }
            finally
            {
                try
                {
                    //执行重试方法
                    if (currentTimes < retimes)
                    {
                        retryAction?.Invoke();
                    }
                }
                catch (Exception ex)
                {
                    //上面已经重试一次了，重试次数先加1再比较
                    currentTimes++;
                    if (currentTimes < retimes)
                    {
                        Thread.Sleep(currentTimes * 1000);
                        await StartRetryAsync(logger, retimes, alwayTryAction, retryAction, errorAction, currentTimes);
                    }
                    else
                    {
                        errorAction?.Invoke(ex);
                    }
                }
            }
        }


        /// <summary>
        /// 重试操作，不包含原方法执行
        /// </summary>
        /// <param name="retimes">重试次数</param>
        /// <param name="retryAction">操作方法</param>
        /// <param name="errorAction">重试完成还是错误执行</param>
        /// <param name="currentTimes">累计操作次数</param>
        public static async Task StartRetryAsync(ILogger logger, int retimes, Action retryAction, Action<Exception> errorAction, int currentTimes = 0)
        {
            try
            {
                //执行重试方法
                if (currentTimes < retimes)
                {
                    retryAction?.Invoke();
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation("执行重试操作失败：ex:{0} ", ex);
                //上面已经重试一次了，重试次数先加1再比较
                currentTimes++;
                if (currentTimes < retimes)
                {
                    Thread.Sleep(currentTimes * 1000);
                    await StartRetryAsync(logger, retimes, retryAction, errorAction, currentTimes);
                }
                else
                {
                    errorAction?.Invoke(ex);
                }
            }
        }

        ///// <summary>
        ///// 操作重试，包含原方法执行
        ///// </summary>
        ///// <param name="retimes">重试次数</param>
        ///// <param name="operateAction">操作方法</param>
        ///// <param name="retryAction">重试方法</param>
        ///// <param name="errorAction">重试完成还是错误执行</param> 
        //public static async Task StartOperateAsync(ILogger logger, int retimes, Action operateAction, Action retryAction, Action<Exception> errorAction)
        //{
        //    try
        //    {
        //        //执行操作方法
        //        operateAction?.Invoke();
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogInformation("执行操作失败：ex:{0} ", ex);
        //        await StartOperateChildAsync(logger, retimes, operateAction, retryAction, errorAction);
        //    }
        //}

        ///// <summary>
        ///// 操作重试，包含原方法执行
        ///// </summary>
        ///// <param name="retimes">重试次数</param>
        ///// <param name="operateAction">操作方法</param>
        ///// <param name="retryAction">重试方法</param>
        ///// <param name="errorAction">重试完成还是错误执行</param>
        ///// <param name="currentTimes">累计操作次数</param>
        //private static async Task StartOperateChildAsync(ILogger logger, int retimes, Action retryAction, Action<Exception> errorAction, int currentTimes = 0)
        //{
        //    try
        //    {
        //        //执行操作方法
        //        if (currentTimes < retimes)
        //        {
        //            retryAction?.Invoke();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogInformation("执行重试操作失败：ex:{0} ", ex);
        //        //上面已经重试一次了，重试次数先加1再比较
        //        currentTimes++;
        //        if (currentTimes < retimes)
        //        {
        //            Thread.Sleep(currentTimes * 1000);
        //            await StartRetryAsync(logger, retimes, retryAction, errorAction, currentTimes);
        //        }
        //        else
        //        {
        //            errorAction?.Invoke(ex);
        //        }
        //    }
        //}


        /// <summary>
        /// 操作重试，包含原方法执行
        /// </summary>
        /// <param name="retimes">重试次数</param>
        /// <param name="operateAction">操作方法</param>
        /// <param name="retryAction">重试方法</param>
        /// <param name="errorAction">重试完成还是错误执行</param>
        /// <param name="currentTimes">累计操作次数</param>
        public static async Task StartOperateAsync(ILogger logger, int retimes, Action operateAction, Action<Exception> errorAction = null, int currentTimes = 0)
        {
            try
            {
                //执行操作方法
                operateAction?.Invoke();
            }
            catch (Exception ex)
            {
                logger.LogInformation("重试操作失败：ex:{0} ", ex);

                if (currentTimes < retimes)
                {
                    currentTimes++;
                    Thread.Sleep(currentTimes * 1000);
                    await StartRetryAsync(logger, retimes, operateAction, errorAction, currentTimes);
                }
                else
                {
                    errorAction?.Invoke(ex);
                }
            }
        }


        /// <summary>
        /// 操作重试，包含原方法执行
        /// </summary>
        /// <param name="retimes">重试次数</param>
        /// <param name="operateAction">操作方法</param>
        /// <param name="retryAction">重试方法</param>
        /// <param name="errorAction">重试完成还是错误执行</param>
        /// <param name="currentTimes">累计操作次数</param>
        public static async Task StartOperateAsync(ILogger logger, int retimes, Func<Task> operateAction, Action<Exception> errorAction = null, int currentTimes = 0)
        {
            try
            {
                //执行操作方法
                await operateAction?.Invoke();
            }
            catch (Exception ex)
            {
                logger.LogInformation("重试操作失败：ex:{0} ", ex);

                if (currentTimes < retimes)
                {
                    currentTimes++;
                    Thread.Sleep(currentTimes * 1000);
                    await StartRetryAsync(logger, retimes, operateAction, errorAction, currentTimes);
                }
                else
                {
                    errorAction?.Invoke(ex);
                }
            }
        }


        /// <summary>
        /// 重试操作，不包含原方法执行
        /// </summary>
        /// <param name="retimes">重试次数</param>
        /// <param name="retryAction">操作方法</param>
        /// <param name="errorAction">重试完成还是错误执行</param>
        /// <param name="currentTimes">累计操作次数</param>
        public static async Task StartRetryAsync(ILogger logger, int retimes, Func<Task> retryAction, Action<Exception> errorAction, int currentTimes = 0)
        {
            try
            {
                //执行重试方法
                if (currentTimes < retimes)
                {
                    await retryAction?.Invoke();
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation("执行重试操作失败：ex:{0} ", ex);
                //上面已经重试一次了，重试次数先加1再比较
                currentTimes++;
                if (currentTimes < retimes)
                {
                    Thread.Sleep(currentTimes * 1000);
                    await StartRetryAsync(logger, retimes, retryAction, errorAction, currentTimes);
                }
                else
                {
                    errorAction?.Invoke(ex);
                }
            }
        }

    }
}