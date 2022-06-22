using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Prowatch.Service.Models;
using KT.TestTool.TestApp.Settings;

namespace KT.TestTool.TestApp.ViewModels
{
    public class ConnectViewModel : BindableBase
    {
        /// <summary>
        /// 数据连接地址
        /// </summary>
        private string dBAddr;

        /// <summary>
        /// 数据库名称
        /// </summary>
        private string dBName;

        /// <summary>
        /// 数据库用户
        /// </summary>
        private string dBUser;

        /// <summary>
        /// 数据库密码
        /// </summary>
        private string dBPassword;

        /// <summary>
        /// 服务器地址
        /// </summary>
        private string pCAddr;

        /// <summary>
        /// 服务器用户名
        /// </summary>
        private string pCUser;

        /// <summary>
        /// 服务器密码
        /// </summary>
        private string pCPassword;

        /// <summary>
        /// 后台服务器地址
        /// </summary>
        private string serverAddress;

        public ConnectViewModel(LoginUserModel model)
        {
            DBAddr = model.DBAddr;
            DBName = model.DBName;
            DBUser = model.DBUser;
            DBPassword = model.DBPassword;
            PCAddr = model.PCAddr;
            PCUser = model.PCUser;
            PCPassword = model.PCPassword;
            ServerAddress = model.ServerAddress;
        }

        public LoginUserModel ToModel()
        {
            LoginUserModel model = new LoginUserModel();

            model.DBAddr = DBAddr;
            model.DBName = DBName;
            model.DBUser = DBUser;
            model.DBPassword = DBPassword;
            model.PCAddr = PCAddr;
            model.PCUser = PCUser;
            model.PCPassword = PCPassword;
            model.ServerAddress = ServerAddress;

            return model;
        }


        public string DBAddr
        {
            get
            {
                return dBAddr;
            }

            set
            {
                SetProperty(ref dBAddr, value);
            }
        }

        public string DBName
        {
            get
            {
                return dBName;
            }

            set
            {
                SetProperty(ref dBName, value);
            }
        }

        public string DBUser
        {
            get
            {
                return dBUser;
            }

            set
            {
                SetProperty(ref dBUser, value);
            }
        }

        public string DBPassword
        {
            get
            {
                return dBPassword;
            }

            set
            {
                SetProperty(ref dBPassword, value);
            }
        }

        public string PCAddr
        {
            get
            {
                return pCAddr;
            }

            set
            {
                SetProperty(ref pCAddr, value);
            }
        }

        public string PCUser
        {
            get
            {
                return pCUser;
            }

            set
            {
                SetProperty(ref pCUser, value);
            }
        }

        public string PCPassword
        {
            get
            {
                return pCPassword;
            }

            set
            {
                SetProperty(ref pCPassword, value);
            }
        }

        public string ServerAddress
        {
            get
            {
                return serverAddress;
            }

            set
            {
                SetProperty(ref serverAddress, value);
            }
        }
    }
}
