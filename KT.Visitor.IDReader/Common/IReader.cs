using IDevices;
using KT.Visitor.IdReader.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.IdReader
{
    public interface IReader
    {
        void Close();

        /// <summary>
        /// 获取证件阅读器名称
        /// </summary>
        /// <returns></returns>
        string GetReaderType();


        /// <summary>
        /// 初始化阅读器
        /// </summary>
        /// <param name="device"></param>
        void Init(IDevice device, IDevice scanDevice = null, bool isReadAndScan = false, bool isEveryConnect = true,string ScanType="");

        /// <summary>
        /// 读取身份证件数据
        /// </summary>
        /// <returns>从身份证件获取的人员信息</returns>
        Task<Person> ReadAsync();

        /// <summary>
        /// 扫描身份证件
        /// </summary>
        /// <returns>从身份证件获取的人员信息</returns>
        Task<Person> ScanAsync(string operateIdType);
    }
}
