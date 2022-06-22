using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.IdReader.Common
{
    public class IdReaderException : Exception
    {
        private IdReaderException(string message) : base(message)
        {

        }

        private IdReaderException(string message, Exception ex) : base(message, ex)
        {

        }

        public static IdReaderException Run(string message, params object[] messages)
        {
            if (messages != null && messages.Length > 0)
            {
                message = string.Format(message, messages);
            }
            return new IdReaderException(message);
        }

        public static IdReaderException Run(string message, Exception ex)
        {
            return new IdReaderException(message, ex);
        }

        public static IdReaderException Run(string message)
        {
            return new IdReaderException(message);
        }
    }
}
