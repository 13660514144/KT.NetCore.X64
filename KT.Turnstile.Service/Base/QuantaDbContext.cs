using KT.Turnstile.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KT.Turnstile.Manage.Service.Base
{
    public class QuantaDbContext : DbContext
    {
        private ILoggerFactory _loggerFactory;

        public QuantaDbContext(ILoggerFactory loggerFactory,
            DbContextOptions<QuantaDbContext> options)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// 通行记录
        /// </summary>
        public DbSet<PassRecordEntity> PassRecords { get; set; }

        /// <summary>
        /// 通行权限
        /// </summary>
        public DbSet<PassRightEntity> PassRights { get; set; }
 
        /// <summary>
        /// 设备权限组关联读卡器设备
        /// </summary>
        public DbSet<CardDeviceRightGroupEntity> CardDeviceRightGroups { get; set; }
 
        /// <summary>
        /// 串口配置
        /// </summary>
        public DbSet<SerialConfigEntity> SerialConfigs { get; set; }

        /// <summary>
        /// 读卡器设备
        /// </summary>
        public DbSet<CardDeviceEntity> CardDevices { get; set; }

        /// <summary>
        /// 继电器设备
        /// </summary>
        public DbSet<RelayDeviceEntity> RelayDevices { get; set; }

        /// <summary>
        /// 系统配置
        /// </summary>
        public DbSet<SystemConfigEntity> SystemConfigs { get; set; }

        /// <summary>
        /// 边缘处理器
        /// </summary>
        public DbSet<ProcessorEntity> Processors { get; set; }

        /// <summary>
        /// 分发数据错误记录
        /// </summary>
        public DbSet<DistributeErrorEntity> DistributeErrors { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<LoginUserEntity> LoginUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string dbPath = Path.Combine(AppContext.BaseDirectory, "LocalData.db");
            //optionsBuilder.UseSqlite(@"Data Source=" + dbPath);

            base.OnConfiguring(optionsBuilder);

            //输出日记
            optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //读卡器关系
            modelBuilder.Entity<CardDeviceEntity>().HasOne(x => x.Processor).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CardDeviceEntity>().HasOne(x => x.RelayDevice).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CardDeviceEntity>().HasOne(x => x.SerialConfig).WithMany().OnDelete(DeleteBehavior.NoAction);

            //读卡器权限组关联读卡器，多对多
            modelBuilder.Entity<CardDeviceRightGroupRelationCardDeviceEntity>()
                .HasOne(x => x.CardDeviceRightGroup)
                .WithMany(x => x.RelationCardDevices)
                .HasForeignKey(x => x.CardDeviceRightGroupId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CardDeviceRightGroupRelationCardDeviceEntity>()
                .HasOne(x => x.CardDevice)
                .WithMany()
                .HasForeignKey(x => x.CardDeviceId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CardDeviceRightGroupRelationCardDeviceEntity>().HasIndex(x => new { x.CardDeviceId, x.CardDeviceRightGroupId }).IsUnique();

            //分发错误关联边缘处理器，后期要去除关系，可能有其它设备接入
            modelBuilder.Entity<DistributeErrorEntity>().HasOne(x => x.Processor).WithMany().OnDelete(DeleteBehavior.NoAction);

            //权限关联权限组，多对多
            modelBuilder.Entity<PassRightRelationCardDeviceRightGroupEntity>()
                .HasOne(x => x.PassRight)
                .WithMany(x => x.RelationCardDeviceRightGroups)
                .HasForeignKey(x => x.PassRightId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PassRightRelationCardDeviceRightGroupEntity>()
                .HasOne(x => x.CardDeviceRightGroup)
                .WithMany()
                .HasForeignKey(x => x.CardDeviceRightGroupId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PassRightRelationCardDeviceRightGroupEntity>().HasIndex(x => new { x.PassRightId, x.CardDeviceRightGroupId }).IsUnique();

            //索引与约束  
            modelBuilder.Entity<PassRightEntity>().HasIndex(x => x.CardNumber).IsUnique();
            modelBuilder.Entity<RelayDeviceEntity>().HasIndex(x => new { x.IpAddress, x.Port }).IsUnique();
            modelBuilder.Entity<SystemConfigEntity>().HasIndex(x => x.Key).IsUnique();
            modelBuilder.Entity<ProcessorEntity>().HasIndex(x => new { x.IpAddress, x.Port }).IsUnique();
            modelBuilder.Entity<ProcessorEntity>().HasIndex(x => x.ProcessorKey).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
