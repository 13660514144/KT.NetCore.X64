using System;
using System.Diagnostics;
using System.Globalization;

namespace KT.Common.Interop
{
    internal static class LibraryLoaderTrace
    {
        private static bool printToConsole = false;

        private static void Print(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void TraceInformation(string format, params object[] args)
        {
            if (printToConsole)
                Print(string.Format(CultureInfo.CurrentCulture, format, args));
            else
                Trace.TraceInformation(string.Format(CultureInfo.CurrentCulture, format, args));
        }

        public static void TraceError(string format, params object[] args)
        {
            if (printToConsole)
                Print(string.Format(CultureInfo.CurrentCulture, format, args));
            else
                Trace.TraceError(string.Format(CultureInfo.CurrentCulture, format, args));
        }

        public static void TraceWarning(string format, params object[] args)
        {
            if (printToConsole)
                Print(string.Format(CultureInfo.CurrentCulture, format, args));
            else
                Trace.TraceWarning(string.Format(CultureInfo.CurrentCulture, format, args));
        }
    }
}