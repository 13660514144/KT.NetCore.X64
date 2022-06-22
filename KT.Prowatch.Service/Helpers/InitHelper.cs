using KT.Prowatch.Service.Models;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace KT.Prowatch.Service.Helpers
{
    /// <summary>
    /// 初始化数据库与服务器
    /// </summary>
    public class InitHelper
    {
        private ILogger<InitHelper> _logger;
        public InitHelper(ILogger<InitHelper> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 初始化数据库与服务器
        /// </summary>
        public bool Init(LoginUserModel connect)
        {
            _logger.LogInformation("开始初始化连接。");
            if (connect == null)
            {
                _logger.LogError("初始化连接失败：连接数据为空。");
                return false;
            }
            //Provider=SQLOLEDB.1; Server=192.168.0.251; Initial Catalog=PWNT; uid=sa; pwd=Admin123
            String strConnect = ConnectHelper.Instance.SetMsSqlConnectStr(connect.DBAddr, connect.DBName, connect.DBUser, connect.DBPassword);
            // 从文件中读取系统信息
            string sysKeyInfo = "";
            try
            {
                string filePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "bin\\syskey.dat";
                if (!File.Exists(filePath))
                {
                    filePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "syskey.dat";
                }
                FileStream fs = File.OpenRead(filePath);
                if (fs.Length > 0)
                {
                    byte[] data = new byte[fs.Length];
                    fs.Read(data, 0, data.Length);

                    sysKeyInfo = System.Text.Encoding.Default.GetString(data);
                }
                fs.Close();
            }
            catch (System.Exception ex)
            {
                _logger.LogError("获取加密文件【syskey.dat】失败：{0} ", ex);
            }
            //先反初始化，防止内存溢出
            ApiHelper.PWApi.Uninit();
            //初始化连接
            bool result = ApiHelper.PWApi.InitWithKeyEx(strConnect, connect.PCAddr, connect.PCUser, connect.PCPassword, sysKeyInfo);

            _logger.LogInformation($"初始化连接成功。strConnect:{strConnect},PCAddr:{ connect.PCAddr},PCUser:{ connect.PCUser},PCPassword:{ connect.PCPassword},sysKeyInfo:{ sysKeyInfo}");
            return result;
        }
    }
}