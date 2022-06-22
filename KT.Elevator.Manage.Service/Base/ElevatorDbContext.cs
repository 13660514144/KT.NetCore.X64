using KT.Elevator.Manage.Service;
using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KT.Elevator.Manage.Base
{
    public class ElevatorDbContext : DbContext
    {
        private ILoggerFactory _loggerFactory;

        public ElevatorDbContext(ILoggerFactory loggerFactory,
            DbContextOptions<ElevatorDbContext> options)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// 串口配置
        /// </summary>
        public DbSet<SerialConfigEntity> SerialConfigs { get; set; }

        /// <summary>
        /// 读卡器设备
        /// </summary>
        public DbSet<CardDeviceEntity> CardDevices { get; set; }

        /// <summary>
        /// 系统配置
        /// </summary>
        public DbSet<SystemConfigEntity> SystemConfigs { get; set; }

        /// <summary>
        /// 边缘处理器
        /// </summary>
        public DbSet<ProcessorEntity> Processors { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<LoginUserEntity> LoginUsers { get; set; }

        /// <summary>
        /// 大厦
        /// </summary>
        public DbSet<EdificeEntity> Edifices { get; set; }

        /// <summary>
        /// 电梯组
        /// </summary>
        public DbSet<ElevatorGroupEntity> ElevatorGroups { get; set; }

        /// <summary>
        /// 电梯组关联楼层
        /// </summary>
        public DbSet<ElevatorGroupRelationFloorEntity> ElevatorGroupRelationFloors { get; set; }

        /// <summary>
        /// 电梯服务
        /// </summary>
        public DbSet<ElevatorServerEntity> ElevatorServers { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public DbSet<FloorEntity> Floors { get; set; }

        /// <summary>
        /// 派梯设备
        /// </summary>
        public DbSet<HandleElevatorDeviceEntity> HandElevatorDevices { get; set; }

        /// <summary>
        /// 通行权限
        /// </summary>
        public DbSet<PassRightEntity> PassRights { get; set; }

        /// <summary>
        /// 通行权限关联楼层
        /// </summary>
        public DbSet<PassRightRelationFloorEntity> PassRightRelationFloors { get; set; }

        /// <summary>
        /// 分发数据错误
        /// </summary>
        public DbSet<DistributeErrorEntity> DistributeErrors { get; set; }

        /// <summary>
        /// 通行记录
        /// </summary>
        public DbSet<PassRecordEntity> PassRecords { get; set; }

        /// <summary>
        /// 派梯输入设备
        /// </summary>
        public DbSet<HandleElevatorInputDeviceEntity> HandleElevatorInputDevices { get; set; }

        /// <summary>
        /// 人脸信息
        /// </summary>
        public DbSet<FaceInfoEntity> FaceInfos { get; set; }

        /// <summary>
        /// 人员信息
        /// </summary>
        public DbSet<PersonEntity> Persons { get; set; }

        /// <summary>
        /// 人员信息
        /// </summary>
        public DbSet<ElevatorInfoEntity> ElevatorInfos { get; set; }

        /// <summary>
        /// 楼层映射
        /// </summary>
        public DbSet<ProcessorFloorEntity> ProcessorFloors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //输出日记
            optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //读卡器关联边缘处理器，关联边缘处理器不能为空
            modelBuilder.Entity<CardDeviceEntity>().HasOne(x => x.Processor).WithMany(x => x.CardDevices).OnDelete(DeleteBehavior.Cascade);
            //读卡器关联派梯设备，在派梯边缘处理器中，关联派梯设备不能为空
            modelBuilder.Entity<CardDeviceEntity>().HasOne(x => x.HandElevatorDevice).WithMany().OnDelete(DeleteBehavior.NoAction);
            //读卡器关联串口配置，关联串口配置不能为空
            modelBuilder.Entity<CardDeviceEntity>().HasOne(x => x.SerialConfig).WithMany().OnDelete(DeleteBehavior.NoAction);
            //电梯组关联所在大厦
            modelBuilder.Entity<ElevatorGroupEntity>().HasOne(x => x.Edifice).WithMany().OnDelete(DeleteBehavior.NoAction);//SetNull
            //电梯组关联可去楼层，多对多
            modelBuilder.Entity<ElevatorGroupRelationFloorEntity>()
                .HasOne(x => x.ElevatorGroup)
                .WithMany(x => x.RelationFloors)
                .HasForeignKey(x => x.ElevatorGroupId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ElevatorGroupRelationFloorEntity>()
                .HasOne(x => x.Floor)
                .WithMany()
                .HasForeignKey(x => x.FloorId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ElevatorGroupRelationFloorEntity>().HasIndex(x => new { x.ElevatorGroupId, x.FloorId }).IsUnique();
            //电梯服务关联电梯组，多对一
            modelBuilder.Entity<ElevatorServerEntity>().HasOne(x => x.ElevatorGroup).WithMany(c => c.ElevatorServers).OnDelete(DeleteBehavior.Cascade);
            //楼层关联大厦
            modelBuilder.Entity<FloorEntity>().HasOne(x => x.Edifice).WithMany(x => x.Floors).OnDelete(DeleteBehavior.Cascade);
            //派梯设备关联所在楼层，关联所在楼层不能为空
            modelBuilder.Entity<HandleElevatorDeviceEntity>().HasOne(x => x.Floor).WithMany().OnDelete(DeleteBehavior.NoAction);
            //派梯设备关联电梯组，多对一
            modelBuilder.Entity<HandleElevatorDeviceEntity>().HasOne(x => x.ElevatorGroup).WithMany().OnDelete(DeleteBehavior.NoAction);
            //派梯输入设备关联派梯设备，派梯设备不能为空，多对一
            modelBuilder.Entity<HandleElevatorInputDeviceEntity>().HasOne(x => x.HandElevatorDevice).WithMany().OnDelete(DeleteBehavior.Cascade);
            //通行权限关联所在楼层，多对一
            modelBuilder.Entity<PassRightEntity>().HasOne(x => x.Floor).WithMany().OnDelete(DeleteBehavior.NoAction);//SetNull
            //通行权限关联人员，多对一
            modelBuilder.Entity<PassRightEntity>().HasOne(x => x.Person).WithMany(x => x.PassRights).OnDelete(DeleteBehavior.Cascade);
            //通行权限关联可去楼层，多对多
            modelBuilder.Entity<PassRightRelationFloorEntity>().HasOne(x => x.PassRight).WithMany(x => x.RelationFloors).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PassRightRelationFloorEntity>().HasOne(x => x.Floor).WithMany().OnDelete(DeleteBehavior.Cascade);
            //边缘处理器关联所在楼层
            modelBuilder.Entity<ProcessorEntity>().HasOne(x => x.Floor).WithMany().OnDelete(DeleteBehavior.Cascade);
            //边缘处理器关联楼层
            modelBuilder.Entity<ProcessorFloorEntity>()
                .HasOne(x => x.Processor)
                .WithMany(x => x.ProcessorFloors)
                .HasForeignKey(x => x.ProcessorId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProcessorFloorEntity>()
                .HasOne(x => x.Floor)
                .WithMany()
                .HasForeignKey(x => x.FloorId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProcessorFloorEntity>().HasIndex(x => new { x.ProcessorId, x.FloorId }).IsUnique();
            //电梯映射 
            modelBuilder.Entity<ElevatorInfoEntity>().HasOne(x => x.ElevatorGroup).WithMany(x => x.ElevatorInfos).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ElevatorInfoEntity>().HasIndex(x => new { x.RealId, x.ElevatorGroupId }).IsUnique();
            //唯一与索引
            modelBuilder.Entity<PersonEntity>().HasIndex(x => x.Number).IsUnique();
            modelBuilder.Entity<PassRightEntity>().HasIndex(x => new { x.Sign, x.AccessType }).IsUnique();
            modelBuilder.Entity<ProcessorFloorEntity>().HasIndex(x => new { x.ProcessorId, x.SortId }).IsUnique();
            modelBuilder.Entity<ProcessorFloorEntity>().HasIndex(x => new { x.ProcessorId, x.FloorId }).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
