﻿using KT.Visitor.Data.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Data.Updates
{
    /// <summary>
    /// 数据库初始化与更新
    /// </summary>
    public class DbUpdateHelper
    {
        public static async Task InitDbAsync()
        {
            using (var context = new SqliteContext())
            {
                //如果数据库不存在时则创建
                context.Database.EnsureCreated();

                //版本更新
                await EntitySeed.ForLoginUserAsync(context);
                //var isExists = context.Database.ExecuteSqlRaw("select name from sqlite_master where type='SystemConfig' order by name;"); 
                //var isCreated = context.Database.ExecuteSqlRaw("select name from sqlite_master where type='SystemConfig' order by name;"); 

                //初始化数据

            }
        }
    }
}
