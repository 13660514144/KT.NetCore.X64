using KT.Turnstile.Unit.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.IO;

namespace KT.Turnstile.Unit.ClientApp.Dao.Base
{
    public class TurnstileUnitContext : DbContext
    {
        private ILoggerFactory _loggerFactory;

        public TurnstileUnitContext()
        {

        }

        public TurnstileUnitContext(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// 系统配置
        /// </summary>
        public DbSet<TurnstileUnitSystemConfigEntity> SystemConfigs { get; set; }

        /// <summary>
        /// 读卡器
        /// </summary>
        public DbSet<TurnstileUnitCardDeviceEntity> CardDevices { get; set; }

        /// <summary>
        /// 通行记录
        /// </summary>
        public DbSet<TurnstileUnitPassRecordEntity> PassRecords { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public DbSet<TurnstileUnitPassRightEntity> PassRights { get; set; }

        /// <summary>
        /// 权限组
        /// </summary>
        public DbSet<TurnstileUnitRightGroupEntity> RightGroups { get; set; }

        /// <summary>
        /// 权限组明细
        /// </summary>
        public DbSet<TurnstileUnitRightGroupDetailEntity> RightGroupDetails { get; set; }

        /// <summary>
        /// 权限关联组明细
        /// </summary>
        public DbSet<TurnstileUnitPassRightDetailEntity> PassRightDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(AppContext.BaseDirectory, "LocalData.db");
            optionsBuilder.UseSqlite(@"Data Source=" + dbPath);

            //输出日记
            optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //数据关系
            modelBuilder.Entity<TurnstileUnitPassRightDetailEntity>()
                .HasOne(x => x.PassRight)
                .WithMany(x => x.Details)
                .HasForeignKey(x => x.PassRightId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TurnstileUnitPassRightDetailEntity>()
                .HasOne(x => x.RightGroup)
                .WithMany()
                .HasForeignKey(x => x.RightGroupId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TurnstileUnitPassRightDetailEntity>().HasIndex(x => new { x.PassRightId, x.RightGroupId }).IsUnique();

            //权限组
            modelBuilder.Entity<TurnstileUnitRightGroupDetailEntity>()
                .HasOne(x => x.RightGroup)
                .WithMany(x => x.Details)
                .HasForeignKey(x => x.RightGroupId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TurnstileUnitRightGroupDetailEntity>()
                .HasOne(x => x.CardDevice)
                .WithMany()
                .HasForeignKey(x => x.CardDeviceId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TurnstileUnitRightGroupDetailEntity>().HasIndex(x => new { x.RightGroupId, x.CardDeviceId }).IsUnique();

            //关系索引
            modelBuilder.Entity<TurnstileUnitSystemConfigEntity>().HasIndex(x => x.Key).IsUnique();
            modelBuilder.Entity<TurnstileUnitCardDeviceEntity>().HasIndex(x => x.PortName).IsUnique(); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
