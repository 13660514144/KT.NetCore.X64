using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Data.Enums
{
    /// <summary>
    /// IC,QR，FACE
    /// </summary>
    public class AuthModelEnum : BaseEnum
    {
        public AuthModelEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        private static object _locker = new object();
        private static List<AuthModelEnum> _items;
        public static List<AuthModelEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<AuthModelEnum>() {
                                IC,
                                QR,
                                FACE
                            };
                        }
                    }
                }
                return _items;
            }
        }

        public static AuthModelEnum IC { get; } = new AuthModelEnum(1, "IC", "IC卡");
        public static AuthModelEnum QR { get; } = new AuthModelEnum(2, "QR", "二维码");
        public static AuthModelEnum FACE { get; } = new AuthModelEnum(3, "FACE", "人脸");

    }
}
