using KT.Prowatch.Service.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Prowatch.Service.Base
{
    public class ProwatchContext : DbContext
    {
        private ILoggerFactory _loggerFactory;

        public ProwatchContext(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public ProwatchContext(ILoggerFactory loggerFactory, DbContextOptions<ProwatchContext> options) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        public static string ConnectString;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (ConnectString != SqlConnectHelper.ConnectString
                && !string.IsNullOrEmpty(SqlConnectHelper.ConnectString))
            {
                optionsBuilder.UseSqlServer(SqlConnectHelper.ConnectString);
            }
            //输出日记
            optionsBuilder.UseLoggerFactory(_loggerFactory);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}