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
            //_logger = ContainerHelper.Resolve<ILogger>();
            //_logger = logger;
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
                    //_logger.LogError($"\r\n==Error:>{ex}");
                    //throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return ds.Tables[0];

            }
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public long ExecuteMySql(string SQLString)
        {
            long rows = 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        rows = cmd.ExecuteNonQuery();                        
                    }
                    catch (MySql.Data.MySqlClient.MySqlException e)
                    {                        
                        //throw e;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return rows;
        }
    }
}
