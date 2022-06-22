using System.Collections.Generic;

namespace KT.Common.Tool.CleanFile.Models
{
    public class CleanFileSettings
    {
        public CleanFileSettings()
        {
            FileSettingses = new List<FileSettings>() {
                new FileSettings ()
                {
                    DirectoryUrl = "records/images",
                    DaysAgo = 30
                },
                new FileSettings ()
                {
                    DirectoryUrl = "Files/Images/Portraits",
                    DaysAgo = 30
                },
                new FileSettings ()
                {
                    DirectoryUrl = "Files/Images/Certificates",
                    DaysAgo = 30
                },
                new FileSettings ()
                {
                    DirectoryUrl = "logs",
                    DaysAgo = 30
                },
                new FileSettings ()
                {
                    DirectoryUrl = "../logs",
                    DaysAgo = 30
                },
                new FileSettings ()
                {
                    DirectoryUrl = "uploads/hikvision/faces",
                    DaysAgo = 30
                },
            };
        }

        /// <summary>
        /// 执行间隔时间（分钟）
        /// </summary>
        public decimal ExceuteIntervalMinuteTime { get; set; } = 10;

        /// <summary>
        /// 删除的文件
        /// </summary>
        public List<FileSettings> FileSettingses { get; set; }
    }

    public class FileSettings
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string DirectoryUrl { get; set; }

        /// <summary>
        /// ？天前的文件
        /// </summary>
        public double DaysAgo { get; set; }
    }
}
