using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KT.Proxy.BackendApi.Enums
{
    public class CertificateTypeEnum : BaseEnum
    {
        public static CertificateTypeEnum ID_CARD => new CertificateTypeEnum(2, "ID_CARD", "身份证");
        public static CertificateTypeEnum PASSPORT => new CertificateTypeEnum(13, "PASSPORT", "护照");
        public static CertificateTypeEnum HK_PASS => new CertificateTypeEnum(22, "HK_PASS", "港澳通行证");
        public static CertificateTypeEnum DRIVER_LICENSE => new CertificateTypeEnum(5, "DRIVER_LICENSE", "驾照");
        public static CertificateTypeEnum RESIDENCE_PERMIT => new CertificateTypeEnum(15, "RESIDENCE_PERMIT", "居住证");
        public static CertificateTypeEnum OTHER => new CertificateTypeEnum(9999, "OTHER", "其它");

        public CertificateTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        private static object _locker = new object();
        private static List<CertificateTypeEnum> _items;
        public static List<CertificateTypeEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<CertificateTypeEnum>() {
                                ID_CARD,
                                PASSPORT,
                                HK_PASS,
                                DRIVER_LICENSE,
                                OTHER
                            };
                        }
                    }
                }
                return _items;
            }
        }

        public static CertificateTypeEnum GetByValue(string value)
        {
            var item = Items.FirstOrDefault(x => x.Value == value);
            if (item == null)
            {
                return OTHER;
            }
            return item;
        }

        public static CertificateTypeEnum GetByText(string text)
        {
            var obj = Items.FirstOrDefault(x => x.Text == text);
            if (obj == null)
            {
                return OTHER;
            }
            return obj;
        }
        public static CertificateTypeEnum GetByValueOrText(string text)
        {
            var obj = Items.FirstOrDefault(x => x.Value == text || x.Text == text);
            if (obj == null)
            {
                return OTHER;
            }
            return obj;
        }

        public static string GetValueByCode(int code)
        {
            var obj = Items.FirstOrDefault(x => x.Code == code);
            if (obj == null)
            {
                return OTHER.Value;
            }
            return obj.Value;
        }

        public static string GetTextByValue(string value)
        {
            var obj = Items.FirstOrDefault(x => x.Value == value);
            return obj?.Text;
        }
    }
}
