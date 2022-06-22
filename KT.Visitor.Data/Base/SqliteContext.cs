using KT.Visitor.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace KT.Visitor.Data.Base
{
    public class SqliteContext : DbContext
    {
        private ILoggerFactory _loggerFactory;

        //public SqliteContext(ILoggerFactory loggerFactory, DbContextOptions<SqliteContext> options) : base(options)
        //{
        //    _loggerFactory = loggerFactory;
        //}

        public SqliteContext() : base()
        {
        }

        /// <summary>
        /// 系统配置
        /// </summary>
        public DbSet<SystemConfigEntity> SystemConfigs { get; set; }

        /// <summary>
        /// 登录用户
        /// </summary>
        public DbSet<LoginUserEntity> LoginUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(AppContext.BaseDirectory, "LocalData.db");
            optionsBuilder.UseSqlite(@"Data Source=" + dbPath);

            base.OnConfiguring(optionsBuilder);

            //输出日记
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
