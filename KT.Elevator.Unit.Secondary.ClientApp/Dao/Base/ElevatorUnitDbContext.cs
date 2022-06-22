using KT.Elevator.Unit.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.IO;

namespace KT.Elevator.Unit.Secondary.ClientApp.Dao.Base
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
        public DbSet<UnitSystemConfigEntity> SystemConfigs { get; set; }

        /// <summary>
        /// 读卡器
        /// </summary>
        public DbSet<UnitCardDeviceEntity> CardDevices { get; set; }

        /// <summary>
        /// 通行记录
        /// </summary>
        public DbSet<UnitPassRecordEntity> PassRecords { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public DbSet<UnitPassRightEntity> PassRights { get; set; }

        /// <summary>
        /// 权限关联组明细
        /// </summary>
        public DbSet<UnitPassRightDetailEntity> PassRightDetails { get; set; }

        /// <summary>
        /// 楼层映射
        /// </summary>
        public DbSet<UnitFloorEntity> Floors { get; set; }

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
            //数据结构关系
            modelBuilder.Entity<UnitPassRightDetailEntity>()
                .HasOne(x => x.PassRight)
                .WithMany(x => x.PassRightDetails)
                .HasForeignKey(x => x.PassRightId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UnitPassRightDetailEntity>().HasIndex(x => new { x.PassRightId, x.FloorId }).IsUnique();

            //关系与索引 
            modelBuilder.Entity<UnitSystemConfigEntity>().HasIndex(x => x.Key).IsUnique();
            modelBuilder.Entity<UnitCardDeviceEntity>().HasIndex(x => x.PortName).IsUnique();
            modelBuilder.Entity<UnitPassRightEntity>().HasIndex(x => new { x.Sign, x.AccessType }).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
