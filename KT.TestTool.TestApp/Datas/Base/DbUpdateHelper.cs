using KT.Prowatch.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.TestTool.TestApp.Datas.Base
{
    public class DbUpdateHelper
    {
        public static void Init()
        {
            using (var context = new TestAppContext())
            {
                //如果数据库不存在时则创建
                context.Database.EnsureCreated();

                //var isExists = context.Database.ExecuteSqlRaw("select name from sqlite_master where type='SystemConfig' order by name;"); 
                //var isCreated = context.Database.ExecuteSqlRaw("select name from sqlite_master where type='SystemConfig' order by name;"); 
            }
            using (var context = new ProwatchSqliteContext())
            {
                //如果数据库不存在时则创建
                context.Database.EnsureCreated();
            }
        }
    }
}
