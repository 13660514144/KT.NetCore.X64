using KT.TestTool.TestApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KT.TestTool.TestApp.Datas.Base
{
    public class TestAppContext : DbContext
    {
        private ILoggerFactory _loggerFactory;

        public TestAppContext() : base()
        {
        }

        public TestAppContext(DbContextOptions<TestAppContext> options)
            : base(options)
        {
        }

        public TestAppContext(ILoggerFactory loggerFactory) : base()
        {
            _loggerFactory = loggerFactory;
        }

        public TestAppContext(ILoggerFactory loggerFactory, DbContextOptions<TestAppContext> options)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }


        public DbSet<SystemConfigEntity> SystemConfigs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //输出日记
            optionsBuilder.UseLoggerFactory(_loggerFactory);

            if (!optionsBuilder.IsConfigured)
            {
                string dbPath = Path.Combine(AppContext.BaseDirectory, "TestToolLocalData.db");
                optionsBuilder.UseSqlite(@"Data Source=" + dbPath);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}