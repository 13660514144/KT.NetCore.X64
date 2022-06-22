using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.IdReader.SDK
{
    public class IDR210SDK
    {
        [DllImport("ReferenceFiles/IdReaderSdks/IDR210/Sdtapi.dll")]
        internal static extern int InitComm(int iPort);

        [DllImport("ReferenceFiles/IdReaderSdks/IDR210/Sdtapi.dll")]
        internal static extern int Authenticate();

        [DllImport("ReferenceFiles/IdReaderSdks/IDR210/Sdtapi.dll")]
        internal static extern int ReadBaseInfos(StringBuilder Name, StringBuilder Gender, StringBuilder Folk,
                                                    StringBuilder BirthDay, StringBuilder Code, StringBuilder Address,
                                                        StringBuilder Agency, StringBuilder ExpireStart, StringBuilder ExpireEnd);

        [DllImport("ReferenceFiles/IdReaderSdks/IDR210/Sdtapi.dll")]
        internal static extern int CloseComm();

        [DllImport("ReferenceFiles/IdReaderSdks/IDR210/Sdtapi.dll")]
        internal static extern int ReadBaseMsg(byte[] pMsg, ref int len);

        [DllImport("ReferenceFiles/IdReaderSdks/IDR210/Sdtapi.dll")]
        internal static extern int ReadBaseMsgW(byte[] pMsg, ref int len);

    }
}
