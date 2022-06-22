﻿using KT.Prowatch.Service.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;


namespace KT.Prowatch.Service.Base
{
    public class ProwatchSqliteContext : DbContext
    {
        private ILoggerFactory _loggerFactory;

        public ProwatchSqliteContext() : base()
        {
        }

        public ProwatchSqliteContext(DbContextOptions<ProwatchSqliteContext> options)
            : base(options)
        {
        }

        public ProwatchSqliteContext(ILoggerFactory loggerFactory) : base()
        {
            _loggerFactory = loggerFactory;
        }

        public ProwatchSqliteContext(ILoggerFactory loggerFactory, DbContextOptions<ProwatchSqliteContext> options)
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
                string dbPath = Path.Combine(AppContext.BaseDirectory, "ProwatchLocalData.db");
                optionsBuilder.UseSqlite(@"Data Source=" + dbPath);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}