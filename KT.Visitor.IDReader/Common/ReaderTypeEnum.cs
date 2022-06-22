using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.IdReader.Common
{
    /// <summary>
    /// 阅读器类型
    /// </summary>
    public class ReaderTypeEnum : BaseEnum
    {
        public static ReaderTypeEnum CVR100U = new ReaderTypeEnum(1, "CVR100U", "CVR100U");
        public static ReaderTypeEnum CVR100XG = new ReaderTypeEnum(2, "CVR100XG", "CVR100XG");
        public static ReaderTypeEnum HD900 = new ReaderTypeEnum(3, "HD900", "HD900");
        public static ReaderTypeEnum IDR210 = new ReaderTypeEnum(4, "IDR210", "IDR210");
        public static ReaderTypeEnum THPR210 = new ReaderTypeEnum(5, "THPR210", "THPR210");
        public static ReaderTypeEnum FS531 = new ReaderTypeEnum(6, "FS531", "FS531");
        public static ReaderTypeEnum IDR210_FS531 = new ReaderTypeEnum(7, "IDR210_FS531", "IDR210_FS531");
        public static ReaderTypeEnum DSK5022 = new ReaderTypeEnum(8, "DSK5022", "海康DSK5022");
        public static ReaderTypeEnum THPR210_CVR100U = new ReaderTypeEnum(10, "THPR210_CVR100U", "THPR210_CVR100U");
        public static ReaderTypeEnum DEVELOPMENT = new ReaderTypeEnum(9, "DEVELOPMENT", "开发测试");
        public static ReaderTypeEnum IDR210_FS531X = new ReaderTypeEnum(7, "IDR210_FS531X", "IDR210_FS531X");//2022-02-16
        public ReaderTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        //没有添加sdk不要显示

        private static object _locker = new object();
        private static List<ReaderTypeEnum> _items;
        public static List<ReaderTypeEnum> Items
        {
            get
            {
                if (_items == null)
                {
                    lock (_locker)
                    {
                        if (_items == null)
                        {
                            _items = new List<ReaderTypeEnum>() {
                                CVR100U,
                                CVR100XG ,
                                HD900 ,
                                IDR210 ,
                                THPR210 ,
                                FS531,
                                IDR210_FS531,
                                IDR210_FS531X,//2022-02-16
                                DSK5022,
                                THPR210_CVR100U,
                                DEVELOPMENT
                            };
                        }
                    }
                }
                return _items;
            }
        }

        public static ReaderTypeEnum GetByText(string name)
        {
            return Items.FirstOrDefault(x => x.Text == name);
        }
    }
}