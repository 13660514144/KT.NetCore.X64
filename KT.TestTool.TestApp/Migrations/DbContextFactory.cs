using KT.Tools.TestApp.Datas.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KT.Tools.TestApp.Migrations
{
    public class DbContextFactory
    {
        public SqliteContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqliteContext>();
            string dbPath = Path.Combine(AppContext.BaseDirectory, "LocalData.db");
            optionsBuilder.UseSqlite("Data Source=" + dbPath);

            return new SqliteContext(optionsBuilder.Options);
        }
    }
}
