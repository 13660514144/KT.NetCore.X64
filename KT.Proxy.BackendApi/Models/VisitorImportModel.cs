namespace KT.Proxy.BackendApi.Models
{
    /// <summary>
    /// 访客导入
    /// </summary>
    public class VisitorImportModel
    {
        private string _beVisitStaffName;
        private string _beVisitCompanyName;
        private string _beVisitFullName;

        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 事由
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        public string BeVisitCompanyName
        {
            get
            {
                return _beVisitCompanyName;
            }
            set
            {
                _beVisitCompanyName = value;
                _beVisitFullName = $"{value}-{_beVisitStaffName}".Trim('-');
            }
        }

        /// <summary>
        /// 被访人
        /// </summary>
        public string BeVisitStaffName
        {
            get
            {
                return _beVisitStaffName;
            }
            set
            {
                _beVisitStaffName = value;
                _beVisitFullName = $"{_beVisitCompanyName}-{value}".Trim('-');
            }
        }

        /// <summary>
        /// 公司
        /// </summary>
        public string BeVisitFullName
        {
            get
            {
                return _beVisitFullName;
            }
            set
            {
                _beVisitFullName = value;
            }
        }

        /// <summary>
        /// xls
        /// </summary>
        public string Xls { get; set; }

        /// <summary>
        /// zip头像
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        ///  结束时间
        /// </summary>
        public string EndVisitDate { get; set; }

        /// <summary>
        /// 开始访问时间
        /// </summary>
        public string StartVisitDate { get; set; }

        /// <summary>
        /// 状态，DOING：处理中、SUCCESS：导入成功、FAIL：导入失败
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string Results { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string UpdateTime { get; set; }

        /// <summary>
        /// 访问楼层的Id
        /// </summary>
        public long BeVisitFloorId { get; set; }

        /// <summary>
        /// 访问员工的Id
        /// </summary>
        public string BeVisitStaffId { get; set; }

        /// <summary>
        /// 是否只允许进出一次，true：一进一出、false：不限
        /// </summary>
        public bool Once { get; set; }

        /// <summary>
        /// 访问公司的Id
        /// </summary>
        public long? BeVisitCompanyId { get; set; }

    }
}
