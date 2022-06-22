using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.Core.Exceptions
{
    /// <summary>
    /// 自定义异常类
    /// </summary>
    public class CustomException : Exception
    {
        public string Title { get; set; }

        private CustomException(string message) : base(message)
        {

        }

        private CustomException(string message, Exception ex) : base(message, ex)
        {

        }

        public static CustomException Run(string message, params object[] messages)
        {
            if (messages != null && messages.Length > 0)
            {
                message = string.Format(message, messages);
            }
            return new CustomException(message);
        }

        public static CustomException Run(string message, Exception ex)
        {
            return new CustomException(message, ex);
        }

        public static CustomException Run(string message)
        {
            var ex = new CustomException(message);

            return ex;
        }
        public static CustomException RunTitle(string title, string message)
        {
            var ex = new CustomException(message);
            ex.Title = title;

            return ex;
        }
    }
}
