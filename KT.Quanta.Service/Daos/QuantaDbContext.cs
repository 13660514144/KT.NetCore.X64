using KT.Quanta.Entity.Entities;
using KT.Quanta.Entity.Kone;
using KT.Quanta.Service.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KT.Quanta.Service.Daos
{
    public class QuantaDbContext : DbContext
    {
        private ILoggerFactory _loggerFactory;

        public QuantaDbContext(DbContextOptions<QuantaDbContext> options)
            : base(options)
        {
            
        }

        public QuantaDbContext(ILoggerFactory loggerFactory,
            DbContextOptions<QuantaDbContext> options)
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
        public DbSet<ElevatorGroupFloorEntity> ElevatorGroupFloors { get; set; }

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

        /// <summary>
        /// 设备权限组关联读卡器设备
        /// </summary>
        public DbSet<CardDeviceRightGroupEntity> CardDeviceRightGroups { get; set; }

        /// <summary>
        /// 继电器设备
        /// </summary>
        public DbSet<RelayDeviceEntity> RelayDevices { get; set; }

        /// <summary>
        /// 通信设备
        /// </summary>
        public DbSet<CommunicateInfoEntity> CommunicateInfos { get; set; }

        /// <summary>
        /// 权限楼层方向
        /// </summary>
        public DbSet<PassRightAccessibleFloorDetailEntity> PassRightAccessibleFloorDetails { get; set; }

        /// <summary>
        /// 权限楼层方向
        /// </summary>
        public DbSet<PassRightAccessibleFloorEntity> PassRightAccessibleFloors { get; set; }

        /// <summary>
        /// 权限楼层方向
        /// </summary>
        public DbSet<PassRightDestinationFloorEntity> PassRightDestinationFloors { get; set; }

        /// <summary>
        /// 权限楼层方向
        /// </summary>
        public DbSet<HandleElevatorDeviceAuxiliaryEntity> HandleElevatorDeviceAuxiliaries { get; set; }

        /// <summary>
        /// 通力
        /// </summary>
        public DbSet<DopGlobalDefaultAccessFloorMaskEntity> DopGlobalDefaultAccessFloorMasks { get; set; }

        /// <summary>
        /// 通力
        /// </summary>
        public DbSet<DopGlobalDefaultAccessMaskEntity> DopGlobalDefaultAccessMasks { get; set; }

        /// <summary>
        /// 通力
        /// </summary>
        public DbSet<DopSpecificDefaultAccessFloorMaskEntity> DopSpecificDefaultAccessFloorMasks { get; set; }

        /// <summary>
        /// 通力
        /// </summary>
        public DbSet<DopSpecificDefaultAccessMaskEntity> DopSpecificDefaultAccessMasks { get; set; }

        /// <summary>
        /// 通力
        /// </summary>
        public DbSet<DopMaskRecordEntity> DopMaskRecords { get; set; }
        /// <summary>
        /// 通力
        /// </summary>
        public DbSet<EliOpenAccessForDopMessageTypeEntity> EliOpenAccessForDopMessageTypes { get; set; }
        /// <summary>
        /// 通力
        /// </summary>
        public DbSet<EliPassRightHandleElevatorDeviceCallTypeEntity> EliPassRightHandleElevatorDeviceCallTypes { get; set; }
        /// <summary>
        /// 通力
        /// </summary>
        public DbSet<RcgifPassRightHandleElevatorDeviceCallTypeEntity> RcgifPassRightHandleElevatorDeviceCallTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //输出日记
            optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //读卡器关系
            modelBuilder.Entity<CardDeviceEntity>()
                .HasOne(x => x.Processor)
                .WithMany(x => x.CardDevices)
                .HasForeignKey(x => x.ProcessorId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CardDeviceEntity>()
                .HasOne(x => x.HandleElevatorDevice)
                .WithMany()
                .HasForeignKey(x => x.HandleElevatorDeviceId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CardDeviceEntity>()
                .HasOne(x => x.SerialConfig)
                .WithMany()
                .HasForeignKey(x => x.SerialConfigId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CardDeviceEntity>()
                .HasOne(x => x.RelayDevice)
                .WithMany()
                .HasForeignKey(x => x.RelayDeviceId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CardDeviceEntity>().HasIndex(x => new { x.ProcessorId, x.PortName }).IsUnique();

            //电梯组关联所在大厦
            modelBuilder.Entity<ElevatorGroupEntity>()
                .HasOne(x => x.Edifice)
                .WithMany()
                .HasForeignKey(x => x.EdificeId)
                .OnDelete(DeleteBehavior.NoAction);
            //电梯组关联可去楼层，多对多
            modelBuilder.Entity<ElevatorGroupFloorEntity>()
                .HasOne(x => x.ElevatorGroup)
                .WithMany(x => x.ElevatorGroupFloors)
                .HasForeignKey(x => x.ElevatorGroupId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ElevatorGroupFloorEntity>()
                .HasOne(x => x.Floor)
                .WithMany()
                .HasForeignKey(x => x.FloorId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ElevatorGroupFloorEntity>().HasIndex(x => new { x.ElevatorGroupId, x.FloorId }).IsUnique();

            //电梯服务关联电梯组，多对一
            modelBuilder.Entity<ElevatorServerEntity>()
                .HasOne(x => x.ElevatorGroup)
                .WithMany(c => c.ElevatorServers)
                .HasForeignKey(x => x.ElevatorGroupId)
                .OnDelete(DeleteBehavior.Cascade);
            //楼层关联大厦
            modelBuilder.Entity<FloorEntity>()
                .HasOne(x => x.Edifice)
                .WithMany(x => x.Floors)
                .HasForeignKey(x => x.EdificeId)
                .OnDelete(DeleteBehavior.Cascade);
            //派梯设备关联所在楼层，关联所在楼层不能为空
            modelBuilder.Entity<HandleElevatorDeviceEntity>()
                .HasOne(x => x.Floor)
                .WithMany()
                .HasForeignKey(x => x.FloorId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<HandleElevatorDeviceEntity>()
                .HasOne(x => x.ElevatorGroup)
                .WithMany()
                .HasForeignKey(x => x.ElevatorGroupId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<HandleElevatorDeviceEntity>()
                .HasOne(x => x.Processor)
                .WithMany()
                .HasForeignKey(x => x.ProcessorId)
                .OnDelete(DeleteBehavior.SetNull);

            //派梯输入设备关联派梯设备，派梯设备不能为空，多对一
            modelBuilder.Entity<HandleElevatorInputDeviceEntity>()
                .HasOne(x => x.HandElevatorDevice)
                .WithMany()
                .HasForeignKey(x => x.HandElevatorDeviceId)
                .OnDelete(DeleteBehavior.Cascade);

            //通行权限关联所在楼层，多对一
            modelBuilder.Entity<PassRightEntity>()
                .HasOne(x => x.Floor)
                .WithMany()
                .HasForeignKey(x => x.FloorId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PassRightEntity>()
                .HasOne(x => x.Person)
                .WithMany(x => x.PassRights)
                .HasForeignKey(x => x.PersonId)
                .OnDelete(DeleteBehavior.NoAction);

            //通行权限关联可去楼层，多对多
            modelBuilder.Entity<PassRightRelationFloorEntity>()
                .HasOne(x => x.PassRight)
                .WithMany(x => x.RelationFloors)
                .HasForeignKey(x => x.PassRightId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PassRightRelationFloorEntity>()
                .HasOne(x => x.Floor)
                .WithMany()
                .HasForeignKey(x => x.FloorId)
                .OnDelete(DeleteBehavior.Cascade);
            //边缘处理器关联所在楼层
            modelBuilder.Entity<ProcessorEntity>()
                .HasOne(x => x.Floor)
                .WithMany()
                .HasForeignKey(x => x.FloorId)
                .OnDelete(DeleteBehavior.NoAction);
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
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProcessorFloorEntity>().HasIndex(x => new { x.ProcessorId, x.FloorId }).IsUnique();
            //电梯映射 
            modelBuilder.Entity<ElevatorInfoEntity>()
                .HasOne(x => x.ElevatorGroup)
                .WithMany(x => x.ElevatorInfos)
                .HasForeignKey(x => x.ElevatorGroupId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ElevatorInfoEntity>().HasIndex(x => new { x.RealId, x.ElevatorGroupId }).IsUnique();
            
            //电梯映射 1.5.4
            /*modelBuilder.Entity<ElevatorInfoEntity>()
                .HasOne(x => x.ElevatorGroup).
                WithMany(x => x.ElevatorInfos).
                OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ElevatorInfoEntity>().HasIndex(x => new { x.RealId, x.ElevatorGroupId }).IsUnique();
            */

            //唯一与索引
            modelBuilder.Entity<PassRightEntity>().HasIndex(x => new { x.Sign, x.AccessType, x.RightType }).IsUnique();
            modelBuilder.Entity<ProcessorFloorEntity>().HasIndex(x => new { x.ProcessorId, x.SortId }).IsUnique();
            modelBuilder.Entity<ProcessorFloorEntity>().HasIndex(x => new { x.ProcessorId, x.FloorId }).IsUnique();
            modelBuilder.Entity<SystemConfigEntity>().HasIndex(x => x.Key).IsUnique();
            modelBuilder.Entity<ProcessorEntity>().HasIndex(x => new { x.IpAddress, x.Port, x.DeviceType }).IsUnique();

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
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CardDeviceRightGroupRelationCardDeviceEntity>().HasIndex(x => new { x.CardDeviceId, x.CardDeviceRightGroupId }).IsUnique();

            //权限关联权限组，多对多
            modelBuilder.Entity<PassRightRelationCardDeviceRightGroupEntity>()
                .HasOne(x => x.PassRight)
                .WithMany(x => x.RelationCardDeviceRightGroups)
                .HasForeignKey(x => x.PassRightId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PassRightRelationCardDeviceRightGroupEntity>()
                .HasOne(x => x.CardDeviceRightGroup)
                .WithMany(x => x.RelationPassRights)
                .HasForeignKey(x => x.CardDeviceRightGroupId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PassRightRelationCardDeviceRightGroupEntity>().HasIndex(x => new { x.PassRightId, x.CardDeviceRightGroupId }).IsUnique();
            modelBuilder.Entity<PassRightRelationCardDeviceRightGroupEntity>().Property(x => x.PassRightId).IsRequired();
            modelBuilder.Entity<PassRightRelationCardDeviceRightGroupEntity>().Property(x => x.CardDeviceRightGroupId).IsRequired();

            //索引与约束   
            modelBuilder.Entity<RelayDeviceEntity>().HasIndex(x => new { x.IpAddress, x.Port }).IsUnique();

            modelBuilder.Entity<CommunicateInfoEntity>();

            modelBuilder.Entity<DopGlobalDefaultAccessMaskEntity>()
                .HasOne(x => x.ElevatorGroup)
                .WithMany()
                .HasForeignKey(x => x.ElevatorGroupId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<DopGlobalDefaultAccessMaskEntity>()
                .HasIndex(x => new { x.ConnectedState, x.ElevatorGroupId })
                .IsUnique();
            modelBuilder.Entity<DopGlobalDefaultAccessFloorMaskEntity>()
                .HasOne(x => x.AccessMask)
                .WithMany(x => x.MaskFloors)
                .HasForeignKey(x => x.AccessMaskId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<DopSpecificDefaultAccessFloorMaskEntity>()
                .HasOne(x => x.Floor)
                .WithMany()
                .HasForeignKey(x => x.FloorId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<DopSpecificDefaultAccessFloorMaskEntity>()
                .HasIndex(x => new { x.AccessMaskId, x.FloorId })
                .IsUnique();

            modelBuilder.Entity<DopSpecificDefaultAccessMaskEntity>()
                .HasOne(x => x.ElevatorGroup)
                .WithMany()
                .HasForeignKey(x => x.ElevatorGroupId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<DopSpecificDefaultAccessMaskEntity>()
                .HasOne(x => x.HandleElevatorDevice)
                .WithMany()
                .HasForeignKey(x => x.HandleElevatorDeviceId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<DopSpecificDefaultAccessMaskEntity>()
                .HasIndex(x => new { x.ConnectedState, x.ElevatorGroupId, x.HandleElevatorDeviceId })
                .IsUnique();
            modelBuilder.Entity<DopSpecificDefaultAccessFloorMaskEntity>()
                .HasOne(x => x.AccessMask)
                .WithMany(x => x.MaskFloors)
                .HasForeignKey(x => x.AccessMaskId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<DopSpecificDefaultAccessFloorMaskEntity>()
                .HasOne(x => x.Floor)
                .WithMany()
                .HasForeignKey(x => x.FloorId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<DopSpecificDefaultAccessFloorMaskEntity>()
                .HasIndex(x => new { x.AccessMaskId, x.FloorId })
                .IsUnique();

            modelBuilder.Entity<PassRightAccessibleFloorDetailEntity>()
                .HasOne(x => x.PassRightAccessibleFloor)
                .WithMany(x => x.PassRightAccessibleFloorDetails)
                .HasForeignKey(x => x.PassRightAccessibleFloorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PassRightAccessibleFloorDetailEntity>()
                .HasOne(x => x.Floor)
                .WithMany()
                .HasForeignKey(x => x.FloorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PassRightDestinationFloorEntity>()
                .HasOne(x => x.Floor)
                .WithMany()
                .HasForeignKey(x => x.FloorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HandleElevatorDeviceAuxiliaryEntity>()
                .HasIndex(x => x.HandleElevatorDeviceId)
                .IsUnique();

            modelBuilder.Entity<DopMaskRecordEntity>();
            modelBuilder.Entity<EliOpenAccessForDopMessageTypeEntity>();
            modelBuilder.Entity<EliPassRightHandleElevatorDeviceCallTypeEntity>();
            modelBuilder.Entity<RcgifPassRightHandleElevatorDeviceCallTypeEntity>();
        }
    }
}
