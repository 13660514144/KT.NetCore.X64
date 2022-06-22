using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.IdReader.SDK
{
    public class IDR210SDKX
    {
        /*  IDR210
        [DllImport("ReferenceFiles/IdReaderSdks/IDR210X/Sdtapi.dll")]
        internal static extern int InitComm(int iPort);

        [DllImport("ReferenceFiles/IdReaderSdks/IDR210X/Sdtapi.dll")]
        internal static extern int Authenticate();

        [DllImport("ReferenceFiles/IdReaderSdks/IDR210X/Sdtapi.dll")]
        internal static extern int ReadBaseInfos(StringBuilder Name, StringBuilder Gender, StringBuilder Folk,
                                                    StringBuilder BirthDay, StringBuilder Code, StringBuilder Address,
                                                        StringBuilder Agency, StringBuilder ExpireStart, StringBuilder ExpireEnd);

        [DllImport("ReferenceFiles/IdReaderSdks/IDR210X/Sdtapi.dll")]
        internal static extern int CloseComm();

        [DllImport("ReferenceFiles/IdReaderSdks/IDR210X/Sdtapi.dll")]
        internal static extern int ReadBaseMsg(byte[] pMsg, ref int len);

        [DllImport("ReferenceFiles/IdReaderSdks/IDR210X/Sdtapi.dll")]
        internal static extern int ReadBaseMsgW(byte[] pMsg, ref int len);
        */
        #region IDR210X
        /// <summary>
        /// 功        能：加载人证识别合并SDK所需的动态库
        ///参 数：无
        ///返  回 值：	11111   所需动态库全部加载成功
        ///其 他：   缺少部分动态库可参考日志文件
        /// </summary>
        /// <returns></returns>
        [DllImport("ReferenceFiles/IdReaderSdks/IDR210X/Sdtapi1.dll")]
        internal static extern int faceRecogLoad();

        /// <summary>
        /// 功        能：身份证读卡器初始化 
        /// 返  回 值：1      精伦读卡器初始化成功
        ///2      德生读卡器初始化成功
        ///3      金诚信读卡器初始化成功
        ///-1      无读卡器初始化成功
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        [DllImport("ReferenceFiles/IdReaderSdks/IDR210X/Sdtapi1.dll")]
        internal static extern int IDCardLoading(int port);

        /// <summary>
        /// 功        能：身份证读卡器认证
        /// 返  回 值：1      精伦读卡器认证成功
        ///2      德生读卡器认证成功
        ///3      金诚信读卡器认证成功
        ///-1      精伦读卡器初始化成功，但认证失败
        ///-2      德生读卡器初始化成功，但认证失败
        ///-10     未初始化成功
        /// </summary>
        /// <returns></returns>
        [DllImport("ReferenceFiles/IdReaderSdks/IDR210X/Sdtapi1.dll")]
        internal static extern int auth();

        /// <summary>
        /// 功        能：读取身份证相关信息
        /// 返 	   回	   值：1    精伦读卡器读卡成功
        ///2    德生读卡器读卡成功
        ///3    金诚信读卡器读卡成功
        ///-1    精伦读卡器认证成功，但读卡失败
        ///-2    德生读卡器认证成功，但读卡失败
        ///-3    金诚信读卡器读卡失败
        /// </summary>
        /// <param name="pPersonInfo"></param>
        /// <returns></returns>
        [DllImport("ReferenceFiles/IdReaderSdks/IDR210X/Sdtapi1.dll")]
        internal static extern int readCardInfo(StringBuilder pPersonInfo);
        [DllImport("ReferenceFiles/IdReaderSdks/IDR210X/Sdtapi1.dll")]
        internal static extern int ReadBaseInfos(StringBuilder Name, StringBuilder Gender, StringBuilder Folk,
                                                    StringBuilder BirthDay, StringBuilder Code, StringBuilder Address,
                                                        StringBuilder Agency, StringBuilder ExpireStart, StringBuilder ExpireEnd);

        [DllImport("ReferenceFiles/IdReaderSdks/IDR210X/Sdtapi1.dll")]
        internal static extern int close();

        
        #endregion
    }
}
