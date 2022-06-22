using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HelperTools
{
    /// <summary>
    /// 数据库操作工厂类
    /// </summary>
    public class DalFactory
    {
        /// <summary>
        /// 创建Dal
        /// </summary>
        /// <param name="databaseType">数据库类型，如SQLite、MySql</param>
       
        public static MySqlDal CreateMysqlDal(string databaseType, string CONN)
        {
            MySqlDal MysqlDal = new MySqlDal();
            MysqlDal.CONN = CONN;
            return MysqlDal;
        }
    }
}

