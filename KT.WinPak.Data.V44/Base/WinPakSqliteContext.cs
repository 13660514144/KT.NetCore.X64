using KT.WinPak.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KT.WinPak.Data.Base
{
    /// <summary>
    /// WinPakLocalData
    /// </summary>
    public class WinPakSqliteContext : DbContext
    {
        private ILoggerFactory _loggerFactory;

        public WinPakSqliteContext() : base()
        {
        }

        public WinPakSqliteContext(DbContextOptions<WinPakSqliteContext> options)
            : base(options)
        {
        }

        public WinPakSqliteContext(ILoggerFactory loggerFactory) : base()
        {
            _loggerFactory = loggerFactory;
        }

        public WinPakSqliteContext(ILoggerFactory loggerFactory, DbContextOptions<WinPakSqliteContext> options)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        public DbSet<LoginUserEntity> LoginUsers { get; set; }

        public DbSet<UserTokenEntity> UserTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //输出日记
            optionsBuilder.UseLoggerFactory(_loggerFactory);

            if (!optionsBuilder.IsConfigured)
            {
                string dbPath = Path.Combine(AppContext.BaseDirectory, "WinPakLocalData.db");
                optionsBuilder.UseSqlite(@"Data Source=" + dbPath);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
