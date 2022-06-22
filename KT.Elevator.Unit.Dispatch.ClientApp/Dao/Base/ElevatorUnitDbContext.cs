using KT.Elevator.Unit.Dispatch.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.IO;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Dao.Base
{
    public class ElevatorUnitDbContext : DbContext
    {
        //private ILoggerFactory _loggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });
        private ILoggerFactory _loggerFactory;

        public ElevatorUnitDbContext()
        {
            
        }

        public ElevatorUnitDbContext(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// 系统配置
        /// </summary>
        public DbSet<UnitDispatchSystemConfigEntity> SystemConfigs { get; set; }
         
        /// <summary>
        /// 通行记录
        /// </summary>
        public DbSet<UnitDispatchPassRecordEntity> PassRecords { get; set; }
         
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(AppContext.BaseDirectory, "LocalData.db");
            optionsBuilder.UseSqlite(@"Data Source=" + dbPath);

            base.OnConfiguring(optionsBuilder);

            //输出日记
            optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            //关系与索引 
            modelBuilder.Entity<UnitDispatchSystemConfigEntity>().HasIndex(x => x.Key).IsUnique();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
