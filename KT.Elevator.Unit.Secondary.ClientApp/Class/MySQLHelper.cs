using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using MySql.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Logging;
using KT.Common.WpfApp.Helpers;

namespace HelperTools
{
    public class MySQLHelper
    {
        private ILogger _logger;
        public MySQLHelper()
        {
            _logger = ContainerHelper.Resolve<ILogger>();
        }
        int TimeOut = 300;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string connectionString = "";
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>

        /// <summary>
        /// 执行查询语句，返回DataTable
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteDataTable(string SQLString)
        {

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection);
                    command.Fill(ds);
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    _logger.LogError($"\r\n==Error:>{ex.Message.ToString()}");
                    //throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    connection.Close();
                }
                return ds.Tables[0];
            }
        }


       
    }
}
