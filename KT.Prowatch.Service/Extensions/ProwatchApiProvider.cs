using ProwatchAPICS;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Prowatch.Service.Extensions
{
    public class ProwatchApiProvider : ProwatchAPICS.ProwatchAPICS
    {
        public ProwatchApiProvider() : base()
        {

        }
        public new void RealEventCallback(IntPtr pInputParam, string pResultBuf, int nResultBufSize)
        {
            try
            {
                sPA_Event paEvent = JSON.parse<sPA_Event>(pResultBuf);
                if (realEventHandler != null)
                {
                    realEventHandler(paEvent);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
