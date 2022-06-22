using KT.Proxy.WebApi.Backend.Models;
using KT.Visitor.Interface.Helpers;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Auth.Controls
{
    /// <summary>
    /// AppointAuthDetailControl.xaml 的交互逻辑
    /// </summary>
    public partial class AppointAuthDetailControl : UserControl
    {
        public AppointAuthDetailControl(VisitorInfoModel record)
        {
            InitializeComponent();

            if (record != null)
            {
                var newRecord = new VisitorInfoModel();

                newRecord.BeVisitStaffName = "被访问人:" + record.BeVisitStaffName;
                newRecord.BeVisitCompanyName = "公司:" + record.BeVisitCompanyName;
                newRecord.BeVisitCompanyLocation = "来访地点:" + record.BeVisitCompanyLocation;
                newRecord.Name = "访客姓名:" + record.Name;
                newRecord.Phone = "手机号:" + record.Phone;
                newRecord.IcCard = "IC卡:" + record.IcCard;
                newRecord.VisitDate = "来访时间:" + record.VisitDate;
                newRecord.FaceImg = StaticProperty.ServerAddress + record.FaceImg;

                if (string.IsNullOrEmpty(record.FaceImg))
                {
                    newRecord.FaceImg = "/KT.Visitor.Interface;component/Resources/grils.png";
                }
                this.DataContext = newRecord;
            }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; tb_title.Text = value; }
        }




    }
}
