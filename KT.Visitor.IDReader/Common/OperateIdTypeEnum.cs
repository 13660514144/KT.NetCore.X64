using KT.Proxy.BackendApi.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.IdReader.Common
{
    public class OperateIdTypeEnum : CertificateTypeEnum
    {
        public OperateIdTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }
        public static CertificateTypeEnum ALL => new CertificateTypeEnum(1000, "ALL", "全部");
    }
}
