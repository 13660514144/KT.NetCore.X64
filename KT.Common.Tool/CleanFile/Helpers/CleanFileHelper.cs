using KT.Common.Core.Utils;
using KT.Common.Tool.CleanFile.Models;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Common.Tool.CleanFile.Helpers
{
    /// <summary>
    /// 文件清理类，单例
    /// </summary>
    public class CleanFileHelper
    {
        private Timer _timer;
        private CleanFileSettings _cleanFileSettings;

        private readonly ILogger<CleanFileHelper> _logger;
        public CleanFileHelper(ILogger<CleanFileHelper> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CleanFileSettings cleanFileSettings)
        {
            _cleanFileSettings = cleanFileSettings;
            _timer = new Timer(CleanFileTimerCallback, null, 10 * 1000, (int)(cleanFileSettings.ExceuteIntervalMinuteTime * 60 * 1000));

            return Task.CompletedTask;
        }

        private void CleanFileTimerCallback(object state)
        {
            try
            {

                if (_cleanFileSettings.FileSettingses?.FirstOrDefault() == null)
                {
                    return;
                }

                foreach (var item in _cleanFileSettings.FileSettingses)
                {
                    var directoryUrl = item.DirectoryUrl;
                    if (directoryUrl.Substring(1, 1) != ":" && !directoryUrl.StartsWith("\\\\"))
                    {
                        directoryUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directoryUrl);
                    }
                    var utcMills = DateTimeUtil.ToMillis(DateTime.UtcNow.AddDays(-item.DaysAgo));

                    DeleteFiles(directoryUrl, utcMills);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除旧文件出错！");
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="directoryUrl">文件夹路径</param>
        /// <param name="utcMillis">文件最后时间</param>
        public void DeleteFiles(string directoryUrl, long utcMillis)
        {
            // 删除文件夹下过期的文件
            if (!Directory.Exists(directoryUrl))
            {
                return;
            }
            var files = Directory.GetFiles(directoryUrl);
            DeleteFiles(files, utcMillis);

            var directories = Directory.GetDirectories(directoryUrl);
            if (directories == null || directories.Count() == 0)
            {
                return;
            }

            //删除所有子文件夹一下的文件
            foreach (var item in directories)
            {
                DeleteFiles(item, utcMillis);
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="files">要删除的文件</param>
        /// <param name="utcMillis">文件最后时间</param>
        private void DeleteFiles(string[] files, long utcMillis)
        {
            if (files == null || files.Count() == 0)
            {
                return;
            }

            foreach (var item in files)
            {
                try
                {
                    var fileInfo = new FileInfo(item);
                    var lasUtcMillis = DateTimeUtil.ToMillis(fileInfo.LastWriteTimeUtc);
                    if (lasUtcMillis < utcMillis)
                    {
                        fileInfo.Delete();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"删除文件错误：file:{item} ");
                }
            }
        }
    }
}
