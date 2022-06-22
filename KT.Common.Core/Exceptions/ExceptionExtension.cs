using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Common.Core.Exceptions
{
    public static class ExceptionExtension
    {
        public static Exception GetBaseInner(this Exception ex)
        {
            ex = ex.GetBaseException();
            return ex.GetInner();
        }

        public static Exception GetInner(this Exception ex)
        {
            return ex.InnerException?.InnerException?.InnerException
               ?? ex.InnerException?.InnerException
               ?? ex.InnerException
               ?? ex;
        }

        public static string GetBaseInnerMessage(this Exception ex)
        {
            return ex.GetBaseInner().Message;
        }

        public static string GetInnerMessage(this Exception ex)
        {
            return ex.GetInner().Message;
        }

        public static Exception GetInner(this UnobservedTaskExceptionEventArgs e)
        {
            return e.Exception.GetInner();
        }

        public static Exception GetInner(this UnhandledExceptionEventArgs e)
        {
            return (e.ExceptionObject as Exception).GetInner();
        }
    }
}
