﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace HelperTools
{
    public class MySqlDal : IDalMySql
    {

        public string CONN = "";



        #region 类型转换
        /// <summary>
        /// 类型转换
        /// </summary>
        public string ConvertDataType(Dictionary<string, string> column)
        {
            string data_type = "string";
            switch (column["data_type"])
            {
                case "int":
                    if (column["notnull"] == "1")
                    {
                        data_type = "int";
                    }
                    else
                    {
                        data_type = "int?";
                    }
                    break;
                case "bigint":
                    if (column["notnull"] == "1")
                    {
                        data_type = "long";
                    }
                    else
                    {
                        data_type = "long?";
                    }
                    break;
                case "decimal":
                    if (column["notnull"] == "1")
                    {
                        data_type = "decimal";
                    }
                    else
                    {
                        data_type = "decimal?";
                    }
                    break;
                case "nvarchar":
                    data_type = "string";
                    break;
                case "varchar":
                    data_type = "string";
                    break;
                case "text":
                    data_type = "string";
                    break;
                case "ntext":
                    data_type = "string";
                    break;
                case "datetime":
                    if (column["notnull"] == "1")
                    {
                        data_type = "DateTime";
                    }
                    else
                    {
                        data_type = "DateTime?";
                    }
                    break;
                default:
                    throw new Exception("Model生成器未实现数据库字段类型" + column["data_type"] + "的转换");
            }
            return data_type;
        }
        #endregion



        public DataTable MySqlListData(string SQLstring)
        {
            DataTable dt = null;
            MySQLHelper dbHelper = new MySQLHelper();
            dbHelper.connectionString = CONN;
            dt = dbHelper.ExecuteDataTable(SQLstring);
            return dt;
        }
    }
}
