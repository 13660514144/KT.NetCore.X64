using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.IdReader.Common
{
    /// <summary>
    /// 证件读取人员信息类
    /// </summary>
    public class Person
    {
        private string name;
        private string gender;
        private string nation;
        private string birthday;
        private string address;
        private string idCode;
        private string agency;
        private string expireStart;
        private string expireEnd;
        private string samId;
        private Image portrait;
        private string cardType;

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value != null)
                {
                    name = value.Replace("\0", "").Trim();
                }
                else
                {
                    name = string.Empty;
                }
            }
        }

        /// <summary>
        /// 性别 
        /// </summary>
        public string Gender
        {
            get
            {
                return gender;
            }
            set
            {
                if (value != null)
                {
                    gender = value.Replace("\0", "").Trim();

                    if (gender == GenderEnum.MALE.Text)
                    {
                        gender = GenderEnum.MALE.Value;
                    }
                    else if (gender == GenderEnum.FEMALE.Text)
                    {
                        gender = GenderEnum.FEMALE.Value;
                    }
                }
                else
                {
                    gender = string.Empty;
                }
            }
        }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation
        {
            get
            {
                return nation;
            }
            set
            {
                if (value != null)
                {
                    nation = value.Replace("\0", "").Trim();
                }
                else
                {
                    nation = string.Empty;
                }
            }
        }

        /// <summary>
        /// 出生日期
        /// </summary>
        public string Birthday
        {
            get
            {
                return birthday;
            }
            set
            {
                if (value != null)
                {
                    birthday = value.Replace("\0", "").Trim();
                }
                else
                {
                    birthday = string.Empty;
                }
            }
        }

        /// <summary>
        /// 住址
        /// </summary>
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                if (value != null)
                {
                    address = value.Replace("\0", "").Trim();
                }
                else
                {
                    address = string.Empty;
                }
            }
        }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdCode
        {
            get
            {
                return idCode;
            }
            set
            {
                if (value != null)
                {
                    idCode = value.Replace("\0", "").Trim();
                }
                else
                {
                    idCode = string.Empty;
                }
            }
        }

        /// <summary>
        /// 归属地区
        /// </summary>
        public string Agency
        {
            get
            {
                return agency;
            }
            set
            {
                if (value != null)
                {
                    agency = value.Replace("\0", "").Trim();
                }
                else
                {
                    agency = string.Empty;
                }
            }
        }

        /// <summary>
        /// 有效期起始时间
        /// </summary>
        public string ExpireStart
        {
            get
            {
                return expireStart;
            }
            set
            {
                if (value != null)
                {
                    expireStart = value.Replace("\0", "").Trim();
                }
                else
                {
                    expireStart = string.Empty;
                }
            }
        }

        /// <summary>
        /// 有效期结束时间
        /// </summary>
        public string ExpireEnd
        {
            get
            {
                return expireEnd;
            }
            set
            {
                if (value != null)
                {
                    expireEnd = value.Replace("\0", "").Trim();
                }
                else
                {
                    expireEnd = string.Empty;
                }
            }
        }

        public string SamId
        {
            get
            {
                return samId;
            }
            set
            {
                if (value != null)
                {
                    samId = value.Replace("\0", "").Trim();
                }
                else
                {
                    samId = string.Empty;
                }
            }
        }

        /// <summary>
        /// 头像
        /// </summary>
        public Image Portrait
        {
            get
            {
                return portrait;
            }
            set
            {
                portrait = value;
            }
        }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string CardType
        {
            get
            {
                return cardType;
            }
            set
            {
                cardType = value;
            }
        }
    }
}
