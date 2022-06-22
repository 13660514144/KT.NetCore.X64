using KT.Prowatch.Service.DllModels;
using Microsoft.Extensions.Logging;
using ProwatchAPICS;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace KT.Prowatch.Service.Helpers
{
    /// <summary>
    /// 读卡器操作
    /// 单例
    /// </summary>
    public class ReaderHelper
    { 
        private ILogger<ReaderHelper> _logger;

        public ReaderHelper(ILogger<ReaderHelper> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 读卡器列表
        /// </summary>
        private ConcurrentBag<ReaderData> readers;

        /// <summary>
        /// 刷新读卡器
        /// </summary>
        private void RefreshReaders()
        {
            var spReaders = new List<sPA_Reader>();
            bool result = ApiHelper.PWApi.GetAllReaders(string.Empty, ref spReaders);
            if (!result || spReaders == null || spReaders.Count <= 0)
            {
                this.readers = new ConcurrentBag<ReaderData>();
            }
            else
            {
                List<ReaderData> data = spReaders.ToModels();
                this.readers = new ConcurrentBag<ReaderData>(data);
            }
        }

        /// <summary>
        /// 获取读卡器Id
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="location"></param>
        /// <param name="isRefresh">不存在是否刷新列表再继续查询</param>
        /// <returns></returns>
        public string GetReaderIdByLocation(string location, bool isRefresh = true)
        {
            if (this.readers == null)
            {
                //读卡器不存在刷新
                RefreshReaders();
                //刷新过后不再刷新
                isRefresh = false;
            }
            var reader = this.readers.FirstOrDefault(x => x.Location == location);
            if (reader != null)
            {
                return reader.Id;
            }
            else if (isRefresh)
            {
                //不存在读卡器刷新列表
                RefreshReaders();
                return GetReaderIdByLocation(location, false);
            }
            _logger.LogError("获取读卡器Id错误：不存在的读卡器 Location:{1} ", location);
            throw new Exception("获取读卡器Id错误！");
        }
    }
}