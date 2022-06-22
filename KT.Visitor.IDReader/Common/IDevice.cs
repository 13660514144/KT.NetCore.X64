using KT.Visitor.IdReader;
using KT.Visitor.IdReader.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDevices
{
    public interface IDevice
    {
        /// <summary>
        /// 阅读器名称
        /// 名称用于动态dll库，dll库存放于**文件夹下，并保持文件夹名称与阅读器名称相同
        /// </summary>
        ReaderTypeEnum ReaderType { get; }

        //以下为主要API函数
        /// <summary>
        /// 初始化连接
        /// </summary>
        /// <param name="Port"></param>
        /// <returns></returns>
        bool InitComm();

        /// <summary>
        /// 卡认证
        /// </summary>
        /// <returns></returns>
        bool Authenticate();

        /// <summary>
        /// 读卡操作
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        Person ReadContent();

        /// <summary>
        /// 读卡操作
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        Person ScanContent(string operateIdType);

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <returns></returns>
        bool CloseComm();
        void StartSignalLamp();

        /// <summary>
        /// 结果回调
        /// </summary>
        Action<object> ResultCallBack { get; set; }
    }
}
