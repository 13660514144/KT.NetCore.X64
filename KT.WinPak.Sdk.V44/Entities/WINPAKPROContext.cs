using System;
using KT.WinPak.SDK.Entities.Part;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KT.WinPak.SDK.Entities
{
    public partial class WINPAKPROContext : DbContext
    {
        public WINPAKPROContext()
        {
        }

        public WINPAKPROContext(DbContextOptions<WINPAKPROContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessLevelEntrancePlus> AccessLevelEntrancePlus { get; set; }
        public virtual DbSet<AccessLevelEntrances> AccessLevelEntrances { get; set; }
        public virtual DbSet<AccessLevelEntrancesReport> AccessLevelEntrancesReport { get; set; }
        public virtual DbSet<AccessLevelPanel> AccessLevelPanel { get; set; }
        public virtual DbSet<AccessLevelPlus> AccessLevelPlus { get; set; }
        public virtual DbSet<AccessLevelReport> AccessLevelReport { get; set; }
        public virtual DbSet<AccessTreeEx> AccessTreeEx { get; set; }
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountMap> AccountMap { get; set; }
        public virtual DbSet<ActionGroup> ActionGroup { get; set; }
        public virtual DbSet<ActionMessage> ActionMessage { get; set; }
        public virtual DbSet<ActiveXdefaults> ActiveXdefaults { get; set; }
        public virtual DbSet<AttachedTimezones> AttachedTimezones { get; set; }
        public virtual DbSet<BadgeData> BadgeData { get; set; }
        public virtual DbSet<BadgeHeader> BadgeHeader { get; set; }
        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<CardAccessLevels> CardAccessLevels { get; set; }
        public virtual DbSet<CardEx> CardEx { get; set; }
        public virtual DbSet<CardHolder> CardHolder { get; set; }
        public virtual DbSet<CardHolderReport> CardHolderReport { get; set; }
        public virtual DbSet<CardHolderUserCodes> CardHolderUserCodes { get; set; }
        public virtual DbSet<CardReport> CardReport { get; set; }
        public virtual DbSet<CmdFile> CmdFile { get; set; }
        public virtual DbSet<CmdFileReport> CmdFileReport { get; set; }
        public virtual DbSet<Command> Command { get; set; }
        public virtual DbSet<ControlTreeEx> ControlTreeEx { get; set; }
        public virtual DbSet<CustomAlchangedEntrances> CustomAlchangedEntrances { get; set; }
        public virtual DbSet<CustomAlremovedEntrances> CustomAlremovedEntrances { get; set; }
        public virtual DbSet<DaylightSaving> DaylightSaving { get; set; }
        public virtual DbSet<DaylightSavingGroup> DaylightSavingGroup { get; set; }
        public virtual DbSet<Dbchanges> Dbchanges { get; set; }
        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<DeviceAdvs> DeviceAdvs { get; set; }
        public virtual DbSet<DeviceReport> DeviceReport { get; set; }
        public virtual DbSet<Finuser> Finuser { get; set; }
        public virtual DbSet<FloorPlan> FloorPlan { get; set; }
        public virtual DbSet<FloorPlanItem> FloorPlanItem { get; set; }
        public virtual DbSet<FloorPlanReport> FloorPlanReport { get; set; }
        public virtual DbSet<GroupAdvs> GroupAdvs { get; set; }
        public virtual DbSet<GuardTour> GuardTour { get; set; }
        public virtual DbSet<GuardTourCheckPoints> GuardTourCheckPoints { get; set; }
        public virtual DbSet<GuardTourReport> GuardTourReport { get; set; }
        public virtual DbSet<Header> Header { get; set; }
        public virtual DbSet<Hidquery> Hidquery { get; set; }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<HistoryReport> HistoryReport { get; set; }
        public virtual DbSet<Holiday> Holiday { get; set; }
        public virtual DbSet<HolidayGroup> HolidayGroup { get; set; }
        public virtual DbSet<HwindependentDevices> HwindependentDevices { get; set; }
        public virtual DbSet<NftabLayout> NftabLayout { get; set; }
        public virtual DbSet<NoteFieldTemplate> NoteFieldTemplate { get; set; }
        public virtual DbSet<Operator> Operator { get; set; }
        public virtual DbSet<OperatorActionValues> OperatorActionValues { get; set; }
        public virtual DbSet<OperatorLevel> OperatorLevel { get; set; }
        public virtual DbSet<OperatorLevelDb> OperatorLevelDb { get; set; }
        public virtual DbSet<OperatorLevelDeviceEx> OperatorLevelDeviceEx { get; set; }
        public virtual DbSet<OperatorLevelUi> OperatorLevelUi { get; set; }
        public virtual DbSet<OperatorReport> OperatorReport { get; set; }
        public virtual DbSet<PanelLog> PanelLog { get; set; }
        public virtual DbSet<PanelLogEx> PanelLogEx { get; set; }
        public virtual DbSet<PanelTimeZone> PanelTimeZone { get; set; }
        public virtual DbSet<ReportSeg> ReportSeg { get; set; }
        public virtual DbSet<ReportTemplate> ReportTemplate { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<ScheduleReport> ScheduleReport { get; set; }
        public virtual DbSet<SystemConfig> SystemConfig { get; set; }
        public virtual DbSet<SystemOperators> SystemOperators { get; set; }
        public virtual DbSet<TimeZone> TimeZone { get; set; }
        public virtual DbSet<TimeZoneRange> TimeZoneRange { get; set; }
        public virtual DbSet<TimezoneReport> TimezoneReport { get; set; }
        public virtual DbSet<TrackingTree> TrackingTree { get; set; }
        public virtual DbSet<Trailer> Trailer { get; set; }

        public static string ConnectString;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (ConnectString != SqlConnectHelper.ConnectString 
                && !string.IsNullOrEmpty(SqlConnectHelper.ConnectString))
            {
                optionsBuilder.UseSqlServer(SqlConnectHelper.ConnectString);
            }

            //            if (!optionsBuilder.IsConfigured)
            //            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            //                optionsBuilder.UseSqlServer("Data Source=192.168.0.211;Initial Catalog=WIN-PAK PRO;User ID=sa;Password=abc123!@#");
            //            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessLevelEntrancePlus>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK___2__10");

                entity.HasIndex(e => e.AccessLevelId)
                    .HasName("IX_AccessLevelEntrancePlus_1");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_AccessLevelEntrancePlus");

                entity.HasIndex(e => e.EntranceId)
                    .HasName("IX_AccessLevelEntrancePlus_2");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.EntranceId).HasColumnName("EntranceID");

                entity.Property(e => e.Flag).HasDefaultValueSql("((0))");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.TimeZoneId).HasColumnName("TimeZoneID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<AccessLevelEntrances>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AccessLevelEntrances");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccessTreeExName).HasMaxLength(30);

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Advname)
                    .HasColumnName("ADVName")
                    .HasMaxLength(40);

                entity.Property(e => e.GroupAdvname)
                    .HasColumnName("GroupADVName")
                    .HasMaxLength(40);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.TimeZoneName).HasMaxLength(50);

                entity.Property(e => e.TimezoneId).HasColumnName("TimezoneID");
            });

            modelBuilder.Entity<AccessLevelEntrancesReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AccessLevelEntrancesReport");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Advname)
                    .HasColumnName("ADVName")
                    .HasMaxLength(40);

                entity.Property(e => e.EntranceId).HasColumnName("EntranceID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.TimeZoneName).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<AccessLevelPanel>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_AccessLevelPanel_1__10");

                entity.HasIndex(e => e.AccessLevelId)
                    .HasName("IX_AccessLevelPanel_1");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_AccessLevelPanel");

                entity.HasIndex(e => e.HwdeviceId)
                    .HasName("IX_AccessLevelPanel_2");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.HwdeviceId).HasColumnName("HWDeviceID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<AccessLevelPlus>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK___1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_AccessLevelPlus");

                entity.HasIndex(e => e.Pw5000alid)
                    .HasName("IX_AccessLevelPlus_1");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ActivationDate).HasColumnType("datetime");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description).HasMaxLength(60);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Flag).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.Pw5000alid)
                    .HasColumnName("PW5000ALID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<AccessLevelReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AccessLevelReport");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Description).HasMaxLength(60);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.RecordId)
                    .HasColumnName("RecordID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<AccessTreeEx>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_AccessTreeEx_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_AccessTreeEx");

                entity.HasIndex(e => e.DeviceId)
                    .HasName("IX_AccessTreeEx_3");

                entity.HasIndex(e => e.NodeLevel)
                    .HasName("IX_AccessTreeEx_2");

                entity.HasIndex(e => e.ParentRecordId)
                    .HasName("IX_AccessTreeEx_1");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.ParentRecordId).HasColumnName("ParentRecordID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK___5__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_Account_1");

                entity.HasIndex(e => e.RecordId)
                    .HasName("IX_Account")
                    .IsUnique();

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AcctName).HasMaxLength(30);

                entity.Property(e => e.Blob).HasColumnType("image");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.MiscField1).HasMaxLength(30);

                entity.Property(e => e.MiscField10).HasMaxLength(30);

                entity.Property(e => e.MiscField2).HasMaxLength(30);

                entity.Property(e => e.MiscField3).HasMaxLength(30);

                entity.Property(e => e.MiscField4).HasMaxLength(30);

                entity.Property(e => e.MiscField5).HasMaxLength(30);

                entity.Property(e => e.MiscField6).HasMaxLength(30);

                entity.Property(e => e.MiscField7).HasMaxLength(30);

                entity.Property(e => e.MiscField8).HasMaxLength(30);

                entity.Property(e => e.MiscField9).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<AccountMap>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK__AccountM__FBDF78C959063A47");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AcctId).HasColumnName("AcctID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<ActionGroup>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_ActionGroup_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_ActionGroup");

                entity.HasIndex(e => e.ActionGroupId)
                    .HasName("IX_ActionGroup_1");

                entity.HasIndex(e => e.DeviceType)
                    .HasName("IX_ActionGroup_2");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ActionGroupId).HasColumnName("ActionGroupID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ActionMessage>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_ActionMessage_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_ActionMessage");

                entity.HasIndex(e => e.AckCmdfileId)
                    .HasName("IX_ActionMessage_4");

                entity.HasIndex(e => e.ActionGroupId)
                    .HasName("IX_ActionMessage_1");

                entity.HasIndex(e => e.CameraId)
                    .HasName("IX_ActionMessage_6");

                entity.HasIndex(e => e.ClearCmdfileId)
                    .HasName("IX_ActionMessage_5");

                entity.HasIndex(e => e.MonitorId)
                    .HasName("IX_ActionMessage_7");

                entity.HasIndex(e => e.RecvCmdfileId)
                    .HasName("IX_ActionMessage_3");

                entity.HasIndex(e => e.TimeZoneId)
                    .HasName("IX_ActionMessage_2");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AckCmdfileId).HasColumnName("AckCMDFileID");

                entity.Property(e => e.ActionGroupId).HasColumnName("ActionGroupID");

                entity.Property(e => e.AnimationFile).HasMaxLength(255);

                entity.Property(e => e.Blob).HasColumnType("image");

                entity.Property(e => e.CameraId).HasColumnName("CameraID");

                entity.Property(e => e.ClearCmdfileId).HasColumnName("ClearCMDFileID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Message).HasMaxLength(255);

                entity.Property(e => e.MonitorId).HasColumnName("MonitorID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.RecvCmdfileId).HasColumnName("RecvCMDFileID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.TimeZoneId).HasColumnName("TimeZoneID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ActiveXdefaults>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("ActiveXDefaults");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_ActiveXDefaults");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Defaults).HasColumnType("image");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<AttachedTimezones>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AttachedTimezones");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");
            });

            modelBuilder.Entity<BadgeData>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_BadgeData_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_BadgeData");

                entity.HasIndex(e => e.BadgeId)
                    .HasName("IX_BadgeData_1");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.BadgeId).HasColumnName("BadgeID");

                entity.Property(e => e.Blob).HasColumnType("image");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<BadgeHeader>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_BadgeHeader_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_BadgeHeader");

                entity.HasIndex(e => e.BadgeId)
                    .HasName("IX_BadgeHeader_1");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.BadgeId).HasColumnName("BadgeID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description).HasMaxLength(60);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_Card_1__10");

                entity.HasIndex(e => e.AccessLevelId)
                    .HasName("IX_Card_2");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_Card_3");

                entity.HasIndex(e => e.ActivationDate)
                    .HasName("IX_Card_5");

                entity.HasIndex(e => e.CardHolderId)
                    .HasName("IX_Card_1");

                entity.HasIndex(e => e.CardNumber)
                    .HasName("IX_Card_4");

                entity.HasIndex(e => e.CardStatus)
                    .HasName("IX_Card_7");

                entity.HasIndex(e => e.ExpirationDate)
                    .HasName("IX_Card_6");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ActionGroupId).HasColumnName("ActionGroupID");

                entity.Property(e => e.ActivationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BackDrop1Id).HasColumnName("BackDrop1ID");

                entity.Property(e => e.BackDrop2Id).HasColumnName("BackDrop2ID");

                entity.Property(e => e.CardHolderId).HasColumnName("CardHolderID");

                entity.Property(e => e.CardNumber).HasMaxLength(30);

                entity.Property(e => e.CmdfileId).HasColumnName("CMDFileID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LastReaderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LastReaderHid)
                    .HasColumnName("LastReaderHID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.Pin1)
                    .HasColumnName("PIN1")
                    .HasMaxLength(10);

                entity.Property(e => e.Pin2)
                    .HasColumnName("PIN2")
                    .HasMaxLength(5);

                entity.Property(e => e.PrintStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CardAccessLevels>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CardId).HasColumnName("CardID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CardEx>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CardEx");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ActionGroupId).HasColumnName("ActionGroupID");

                entity.Property(e => e.ActivationDate).HasColumnType("datetime");

                entity.Property(e => e.BackDrop1Id).HasColumnName("BackDrop1ID");

                entity.Property(e => e.BackDrop2Id).HasColumnName("BackDrop2ID");

                entity.Property(e => e.CardHolderId).HasColumnName("CardHolderID");

                entity.Property(e => e.CardNumber).HasMaxLength(30);

                entity.Property(e => e.CmdfileId).HasColumnName("CMDFileID");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(35);

                entity.Property(e => e.LastName).HasMaxLength(35);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.Pin1)
                    .HasColumnName("PIN1")
                    .HasMaxLength(10);

                entity.Property(e => e.Pin2)
                    .HasColumnName("PIN2")
                    .HasMaxLength(5);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CardHolder>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_CardHolder_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_CardHolder_3");

                entity.HasIndex(e => e.FirstName)
                    .HasName("IX_CardHolder");

                entity.HasIndex(e => e.LastName)
                    .HasName("IX_CardHolder_1");

                entity.HasIndex(e => new { e.LastName, e.FirstName })
                    .HasName("IX_CardHolder_2");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.FirstName).HasMaxLength(35);

                entity.Property(e => e.LastName).HasMaxLength(35);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.Note1).HasMaxLength(64);

                entity.Property(e => e.Note10).HasMaxLength(64);

                entity.Property(e => e.Note11).HasMaxLength(64);

                entity.Property(e => e.Note12).HasMaxLength(64);

                entity.Property(e => e.Note13).HasMaxLength(64);

                entity.Property(e => e.Note14).HasMaxLength(64);

                entity.Property(e => e.Note15).HasMaxLength(64);

                entity.Property(e => e.Note16).HasMaxLength(64);

                entity.Property(e => e.Note17).HasMaxLength(64);

                entity.Property(e => e.Note18).HasMaxLength(64);

                entity.Property(e => e.Note19).HasMaxLength(64);

                entity.Property(e => e.Note2).HasMaxLength(64);

                entity.Property(e => e.Note20).HasMaxLength(64);

                entity.Property(e => e.Note21).HasMaxLength(64);

                entity.Property(e => e.Note22).HasMaxLength(64);

                entity.Property(e => e.Note23).HasMaxLength(64);

                entity.Property(e => e.Note24).HasMaxLength(64);

                entity.Property(e => e.Note25).HasMaxLength(64);

                entity.Property(e => e.Note26).HasMaxLength(64);

                entity.Property(e => e.Note27).HasMaxLength(64);

                entity.Property(e => e.Note28).HasMaxLength(64);

                entity.Property(e => e.Note29).HasMaxLength(64);

                entity.Property(e => e.Note3).HasMaxLength(64);

                entity.Property(e => e.Note30).HasMaxLength(64);

                entity.Property(e => e.Note31).HasMaxLength(64);

                entity.Property(e => e.Note32).HasMaxLength(64);

                entity.Property(e => e.Note33).HasMaxLength(64);

                entity.Property(e => e.Note34).HasMaxLength(64);

                entity.Property(e => e.Note35).HasMaxLength(64);

                entity.Property(e => e.Note36).HasMaxLength(64);

                entity.Property(e => e.Note37).HasMaxLength(64);

                entity.Property(e => e.Note38).HasMaxLength(64);

                entity.Property(e => e.Note39).HasMaxLength(64);

                entity.Property(e => e.Note4).HasMaxLength(64);

                entity.Property(e => e.Note40).HasMaxLength(64);

                entity.Property(e => e.Note5).HasMaxLength(64);

                entity.Property(e => e.Note6).HasMaxLength(64);

                entity.Property(e => e.Note7).HasMaxLength(64);

                entity.Property(e => e.Note8).HasMaxLength(64);

                entity.Property(e => e.Note9).HasMaxLength(64);

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CardHolderReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CardHolderReport");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CardRecId).HasColumnName("CardRecID");

                entity.Property(e => e.CountOfRecordId).HasColumnName("CountOfRecordID");

                entity.Property(e => e.FirstName).HasMaxLength(35);

                entity.Property(e => e.LastName).HasMaxLength(35);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.Note1).HasMaxLength(64);

                entity.Property(e => e.Note10).HasMaxLength(64);

                entity.Property(e => e.Note11).HasMaxLength(64);

                entity.Property(e => e.Note12).HasMaxLength(64);

                entity.Property(e => e.Note13).HasMaxLength(64);

                entity.Property(e => e.Note14).HasMaxLength(64);

                entity.Property(e => e.Note15).HasMaxLength(64);

                entity.Property(e => e.Note16).HasMaxLength(64);

                entity.Property(e => e.Note17).HasMaxLength(64);

                entity.Property(e => e.Note18).HasMaxLength(64);

                entity.Property(e => e.Note19).HasMaxLength(64);

                entity.Property(e => e.Note2).HasMaxLength(64);

                entity.Property(e => e.Note20).HasMaxLength(64);

                entity.Property(e => e.Note21).HasMaxLength(64);

                entity.Property(e => e.Note22).HasMaxLength(64);

                entity.Property(e => e.Note23).HasMaxLength(64);

                entity.Property(e => e.Note24).HasMaxLength(64);

                entity.Property(e => e.Note25).HasMaxLength(64);

                entity.Property(e => e.Note26).HasMaxLength(64);

                entity.Property(e => e.Note27).HasMaxLength(64);

                entity.Property(e => e.Note28).HasMaxLength(64);

                entity.Property(e => e.Note29).HasMaxLength(64);

                entity.Property(e => e.Note3).HasMaxLength(64);

                entity.Property(e => e.Note30).HasMaxLength(64);

                entity.Property(e => e.Note31).HasMaxLength(64);

                entity.Property(e => e.Note32).HasMaxLength(64);

                entity.Property(e => e.Note33).HasMaxLength(64);

                entity.Property(e => e.Note34).HasMaxLength(64);

                entity.Property(e => e.Note35).HasMaxLength(64);

                entity.Property(e => e.Note36).HasMaxLength(64);

                entity.Property(e => e.Note37).HasMaxLength(64);

                entity.Property(e => e.Note38).HasMaxLength(64);

                entity.Property(e => e.Note39).HasMaxLength(64);

                entity.Property(e => e.Note4).HasMaxLength(64);

                entity.Property(e => e.Note40).HasMaxLength(64);

                entity.Property(e => e.Note5).HasMaxLength(64);

                entity.Property(e => e.Note6).HasMaxLength(64);

                entity.Property(e => e.Note7).HasMaxLength(64);

                entity.Property(e => e.Note8).HasMaxLength(64);

                entity.Property(e => e.Note9).HasMaxLength(64);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<CardHolderUserCodes>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_CardHolderUserCodes_1__10");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.HolderId).HasColumnName("HolderID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.PanelId).HasColumnName("PanelID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SpareStr2)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CardReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CardReport");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ActionGroup).HasMaxLength(50);

                entity.Property(e => e.AlplusActivationDate)
                    .HasColumnName("ALPlusActivationDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.AlplusExpirationDate)
                    .HasColumnName("ALPlusExpirationDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.AlplusName)
                    .HasColumnName("ALPlusName")
                    .HasMaxLength(30);

                entity.Property(e => e.AlplusRecId).HasColumnName("ALPlusRecID");

                entity.Property(e => e.BadgeHeader1Name)
                    .HasColumnName("BadgeHeader_1_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.BadgeHeaderName).HasMaxLength(50);

                entity.Property(e => e.CardActivationDate).HasColumnType("datetime");

                entity.Property(e => e.CardExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.CardHolderId).HasColumnName("CardHolderID");

                entity.Property(e => e.CardNumber).HasMaxLength(30);

                entity.Property(e => e.CardRecId).HasColumnName("CardRecID");

                entity.Property(e => e.FirstName).HasMaxLength(35);

                entity.Property(e => e.LastName).HasMaxLength(35);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.Pin1)
                    .HasColumnName("PIN1")
                    .HasMaxLength(10);

                entity.Property(e => e.Pin2)
                    .HasColumnName("PIN2")
                    .HasMaxLength(5);

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CmdFile>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_CmdFile_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_CmdFile");

                entity.HasIndex(e => e.CommandFileId)
                    .HasName("IX_CmdFile_1");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CommandFileId).HasColumnName("CommandFileID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description).HasMaxLength(60);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CmdFileReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CmdFileReport");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Blob).HasColumnType("image");

                entity.Property(e => e.CommandFileId).HasColumnName("CommandFileID");

                entity.Property(e => e.Hid).HasColumnName("HID");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Command>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_Command_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_Command");

                entity.HasIndex(e => e.CommandFileId)
                    .HasName("IX_Command_1");

                entity.HasIndex(e => e.Hid)
                    .HasName("IX_Command_2");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Blob).HasColumnType("image");

                entity.Property(e => e.Command1).HasColumnName("Command");

                entity.Property(e => e.CommandFileId).HasColumnName("CommandFileID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Hid).HasColumnName("HID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.Param1).HasDefaultValueSql("((0))");

                entity.Property(e => e.Param2).HasDefaultValueSql("((0))");

                entity.Property(e => e.Param3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ControlTreeEx>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_ControlTreeEx_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_ControlTreeEx");

                entity.HasIndex(e => e.DeviceId)
                    .HasName("IX_ControlTreeEx_3");

                entity.HasIndex(e => e.NodeLevel)
                    .HasName("IX_ControlTreeEx_2");

                entity.HasIndex(e => e.ParentRecordId)
                    .HasName("IX_ControlTreeEx_1");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.ParentRecordId).HasColumnName("ParentRecordID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CustomAlchangedEntrances>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CustomALChangedEntrances");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Advname)
                    .HasColumnName("ADVName")
                    .HasMaxLength(40);

                entity.Property(e => e.Advname1)
                    .HasColumnName("ADVName1")
                    .HasMaxLength(40);

                entity.Property(e => e.CardRecId).HasColumnName("CardRecID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<CustomAlremovedEntrances>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CustomALRemovedEntrances");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CardRecId).HasColumnName("CardRecID");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<DaylightSaving>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_DaylightSavings");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.DstgroupId).HasColumnName("DSTGroupID");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<DaylightSavingGroup>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description).HasMaxLength(60);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Dbchanges>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("DBChanges");

                entity.HasIndex(e => e.Dbid)
                    .HasName("IX_DBChanges");

                entity.HasIndex(e => e.Request)
                    .HasName("IX_DBChanges_1");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ChangeBlob).HasColumnType("image");

                entity.Property(e => e.Dbid).HasColumnName("DBID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.ServerId).HasColumnName("ServerID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SpareStr2)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_Device_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_Device");

                entity.HasIndex(e => e.DeviceNo)
                    .HasName("IX_Device_3");

                entity.HasIndex(e => e.DeviceType)
                    .HasName("IX_Device_1");

                entity.HasIndex(e => e.ParentId)
                    .HasName("IX_Device_2");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Blob).HasColumnType("image");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Machine).HasMaxLength(30);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<DeviceAdvs>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DeviceADVs");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ActionGroupId).HasColumnName("ActionGroupID");

                entity.Property(e => e.CommServerId).HasColumnName("CommServerID");

                entity.Property(e => e.CommandFileId).HasColumnName("CommandFileID");

                entity.Property(e => e.Desc2).HasMaxLength(60);

                entity.Property(e => e.Desc3).HasMaxLength(60);

                entity.Property(e => e.Desc4).HasMaxLength(60);

                entity.Property(e => e.Desc5).HasMaxLength(60);

                entity.Property(e => e.Description).HasMaxLength(60);

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.FloorPlanId).HasColumnName("FloorPlanID");

                entity.Property(e => e.HwdeviceId).HasColumnName("HWDeviceID");

                entity.Property(e => e.HwdeviceSubId1).HasColumnName("HWDeviceSubID1");

                entity.Property(e => e.HwdeviceSubId2).HasColumnName("HWDeviceSubID2");

                entity.Property(e => e.HwdeviceSubId3).HasColumnName("HWDeviceSubID3");

                entity.Property(e => e.HwdeviceSubId4).HasColumnName("HWDeviceSubID4");

                entity.Property(e => e.HwdeviceSubId5).HasColumnName("HWDeviceSubID5");

                entity.Property(e => e.LandisHwdeviceId).HasColumnName("LandisHWDeviceID");

                entity.Property(e => e.LandisOldPointId).HasColumnName("LandisOldPointID");

                entity.Property(e => e.LandisPointId).HasColumnName("LandisPointID");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.RecordId)
                    .HasColumnName("RecordID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<DeviceReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DeviceReport");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Blob).HasColumnType("image");

                entity.Property(e => e.DeviceAdvname)
                    .HasColumnName("DeviceADVName")
                    .HasMaxLength(40);

                entity.Property(e => e.DeviceName).HasMaxLength(30);

                entity.Property(e => e.Machine).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Finuser>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK__FINUser__FBDF78C948CFD27E");

                entity.ToTable("FINUser");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ActivationDate).HasColumnType("datetime");

                entity.Property(e => e.Blob).HasColumnType("image");

                entity.Property(e => e.CardHolderId).HasColumnName("CardHolderID");

                entity.Property(e => e.CardId)
                    .HasColumnName("CardID")
                    .HasMaxLength(30);

                entity.Property(e => e.CustomId)
                    .HasColumnName("CustomID")
                    .HasMaxLength(30);

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.LastReaderDate).HasColumnType("datetime");

                entity.Property(e => e.LastReaderHid).HasColumnName("LastReaderHID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.Password).HasMaxLength(10);

                entity.Property(e => e.Password1).HasMaxLength(10);

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SpareStr2)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<FloorPlan>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_FloorPlan_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_FloorPlan");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description).HasMaxLength(60);

                entity.Property(e => e.MetaFileName).HasMaxLength(30);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<FloorPlanItem>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_FloorPlanItem_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_FloorPlanItem");

                entity.HasIndex(e => e.FloorPlanId)
                    .HasName("IX_FloorPlanItem_1");

                entity.HasIndex(e => e.Hid)
                    .HasName("IX_FloorPlanItem_2");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.FloorPlanId).HasColumnName("FloorPlanID");

                entity.Property(e => e.Hid).HasColumnName("HID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.Olecontrol)
                    .HasColumnName("OLEControl")
                    .HasColumnType("image");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Xpoint1).HasColumnName("XPoint1");

                entity.Property(e => e.Xpoint2).HasColumnName("XPoint2");

                entity.Property(e => e.Xpoint3).HasColumnName("XPoint3");

                entity.Property(e => e.Xpoint4).HasColumnName("XPoint4");

                entity.Property(e => e.Ypoint1).HasColumnName("YPoint1");

                entity.Property(e => e.Ypoint2).HasColumnName("YPoint2");

                entity.Property(e => e.Ypoint3).HasColumnName("YPoint3");

                entity.Property(e => e.Ypoint4).HasColumnName("YPoint4");
            });

            modelBuilder.Entity<FloorPlanReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("FloorPlanReport");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Advname)
                    .HasColumnName("ADVName")
                    .HasMaxLength(40);

                entity.Property(e => e.FloorPlanId).HasColumnName("FloorPlanID");

                entity.Property(e => e.Hid).HasColumnName("HID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<GroupAdvs>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GroupADVs");

                entity.Property(e => e.HwdeviceId).HasColumnName("HWDeviceID");

                entity.Property(e => e.HwdeviceSubId2).HasColumnName("HWDeviceSubID2");

                entity.Property(e => e.Name).HasMaxLength(40);
            });

            modelBuilder.Entity<GuardTour>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_GuardTour_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_GuardTour");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<GuardTourCheckPoints>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_GuardTourCheckPoints_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_GuardTourCheckPoints");

                entity.HasIndex(e => e.GuardTourId)
                    .HasName("IX_GuardTourCheckPoints_1");

                entity.HasIndex(e => e.SequenceNum)
                    .HasName("IX_GuardTourCheckPoints_2");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ActionGroupId).HasColumnName("ActionGroupID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.GuardTourId).HasColumnName("GuardTourID");

                entity.Property(e => e.Hidid).HasColumnName("HIDID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<GuardTourReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GuardTourReport");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.GuardTourId).HasColumnName("GuardTourID");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Header>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Hidquery>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("HIDQuery");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ActionGroupId).HasColumnName("ActionGroupID");

                entity.Property(e => e.Advname)
                    .HasColumnName("ADVName")
                    .HasMaxLength(40);

                entity.Property(e => e.CommServerId).HasColumnName("CommServerID");

                entity.Property(e => e.CommandFileId).HasColumnName("CommandFileID");

                entity.Property(e => e.Desc2).HasMaxLength(60);

                entity.Property(e => e.Desc3).HasMaxLength(60);

                entity.Property(e => e.Desc4).HasMaxLength(60);

                entity.Property(e => e.Desc5).HasMaxLength(60);

                entity.Property(e => e.Description).HasMaxLength(60);

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.DeviceName).HasMaxLength(30);

                entity.Property(e => e.FloorPlanId).HasColumnName("FloorPlanID");

                entity.Property(e => e.HwdeviceId).HasColumnName("HWDeviceID");

                entity.Property(e => e.HwdeviceSubId1).HasColumnName("HWDeviceSubID1");

                entity.Property(e => e.HwdeviceSubId2).HasColumnName("HWDeviceSubID2");

                entity.Property(e => e.HwdeviceSubId3).HasColumnName("HWDeviceSubID3");

                entity.Property(e => e.HwdeviceSubId4).HasColumnName("HWDeviceSubID4");

                entity.Property(e => e.HwdeviceSubId5).HasColumnName("HWDeviceSubID5");

                entity.Property(e => e.LandisHwdeviceId).HasColumnName("LandisHWDeviceID");

                entity.Property(e => e.LandisOldPointId).HasColumnName("LandisOldPointID");

                entity.Property(e => e.LandisPointId).HasColumnName("LandisPointID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_History_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_History");

                entity.HasIndex(e => e.GenTime)
                    .HasName("IX_History_2");

                entity.HasIndex(e => e.RecvTime)
                    .HasName("IX_History_1");

                entity.HasIndex(e => e.SeqId)
                    .HasName("IX_History_3");

                entity.HasIndex(e => e.Type1)
                    .HasName("IX_History_4");

                entity.HasIndex(e => e.Type2)
                    .HasName("IX_History_5");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.GenTime).HasColumnType("datetime");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.Note).HasColumnType("image");

                entity.Property(e => e.Param3).HasMaxLength(1024);

                entity.Property(e => e.RecvTime).HasColumnType("datetime");

                entity.Property(e => e.SeqId).HasColumnName("SeqID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<HistoryReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("HistoryReport");

                entity.Property(e => e.CardHolderAccountId).HasColumnName("CardHolderAccountID");

                entity.Property(e => e.FirstName).HasMaxLength(35);

                entity.Property(e => e.GenTime).HasColumnType("datetime");

                entity.Property(e => e.HistoryAccountId).HasColumnName("HistoryAccountID");

                entity.Property(e => e.LastName).HasMaxLength(35);

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.Note).HasColumnType("image");

                entity.Property(e => e.OperatorName).HasMaxLength(30);

                entity.Property(e => e.Param3).HasMaxLength(1024);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.SeqId).HasColumnName("SeqID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Holiday>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_Holiday_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_Holiday");

                entity.HasIndex(e => e.HolidayGroupId)
                    .HasName("IX_Holiday_1");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Blob).HasColumnType("image");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.HolidayGroupId).HasColumnName("HolidayGroupID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<HolidayGroup>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_HolidayGroup_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_HolidayGroup");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<HwindependentDevices>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_HWIndependentDevices_1__10");

                entity.ToTable("HWIndependentDevices");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_HWIndependentDevices");

                entity.HasIndex(e => e.ActionGroupId)
                    .HasName("IX_HWIndependentDevices_7");

                entity.HasIndex(e => e.CommServerId)
                    .HasName("IX_HWIndependentDevices_2");

                entity.HasIndex(e => e.CommandFileId)
                    .HasName("IX_HWIndependentDevices_4");

                entity.HasIndex(e => e.DeviceId)
                    .HasName("IX_HWIndependentDevices_1");

                entity.HasIndex(e => e.FloorPlanId)
                    .HasName("IX_HWIndependentDevices_6");

                entity.HasIndex(e => e.LandisHwdeviceId)
                    .HasName("IX_HWIndependentDevices_8");

                entity.HasIndex(e => e.LandisOldPointId)
                    .HasName("IX_HWIndependentDevices_10");

                entity.HasIndex(e => e.LandisPointId)
                    .HasName("IX_HWIndependentDevices_9");

                entity.HasIndex(e => new { e.DeviceType, e.DeviceSubType1 })
                    .HasName("IX_HWIndependentDevices_3");

                entity.HasIndex(e => new { e.HwdeviceId, e.HwdeviceSubId1 })
                    .HasName("IX_HWIndependentDevices_5");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ActionGroupId).HasColumnName("ActionGroupID");

                entity.Property(e => e.CommServerId).HasColumnName("CommServerID");

                entity.Property(e => e.CommandFileId).HasColumnName("CommandFileID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Desc2).HasMaxLength(60);

                entity.Property(e => e.Desc3).HasMaxLength(60);

                entity.Property(e => e.Desc4).HasMaxLength(60);

                entity.Property(e => e.Desc5).HasMaxLength(60);

                entity.Property(e => e.Description).HasMaxLength(60);

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.FloorPlanId).HasColumnName("FloorPlanID");

                entity.Property(e => e.HwdeviceId).HasColumnName("HWDeviceID");

                entity.Property(e => e.HwdeviceSubId1).HasColumnName("HWDeviceSubID1");

                entity.Property(e => e.HwdeviceSubId2).HasColumnName("HWDeviceSubID2");

                entity.Property(e => e.HwdeviceSubId3).HasColumnName("HWDeviceSubID3");

                entity.Property(e => e.HwdeviceSubId4).HasColumnName("HWDeviceSubID4");

                entity.Property(e => e.HwdeviceSubId5).HasColumnName("HWDeviceSubID5");

                entity.Property(e => e.InControlTree).HasDefaultValueSql("((0))");

                entity.Property(e => e.LandisHwdeviceId).HasColumnName("LandisHWDeviceID");

                entity.Property(e => e.LandisOldPointId).HasColumnName("LandisOldPointID");

                entity.Property(e => e.LandisPointId).HasColumnName("LandisPointID");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<NftabLayout>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_NFTabLayout_1__10");

                entity.ToTable("NFTabLayout");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_NFTabLayout");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Nf1).HasColumnName("NF1");

                entity.Property(e => e.Nf10).HasColumnName("NF10");

                entity.Property(e => e.Nf11).HasColumnName("NF11");

                entity.Property(e => e.Nf12).HasColumnName("NF12");

                entity.Property(e => e.Nf13).HasColumnName("NF13");

                entity.Property(e => e.Nf14).HasColumnName("NF14");

                entity.Property(e => e.Nf15).HasColumnName("NF15");

                entity.Property(e => e.Nf16).HasColumnName("NF16");

                entity.Property(e => e.Nf17).HasColumnName("NF17");

                entity.Property(e => e.Nf18).HasColumnName("NF18");

                entity.Property(e => e.Nf19).HasColumnName("NF19");

                entity.Property(e => e.Nf2).HasColumnName("NF2");

                entity.Property(e => e.Nf20).HasColumnName("NF20");

                entity.Property(e => e.Nf21).HasColumnName("NF21");

                entity.Property(e => e.Nf22).HasColumnName("NF22");

                entity.Property(e => e.Nf23).HasColumnName("NF23");

                entity.Property(e => e.Nf24).HasColumnName("NF24");

                entity.Property(e => e.Nf25).HasColumnName("NF25");

                entity.Property(e => e.Nf26).HasColumnName("NF26");

                entity.Property(e => e.Nf27).HasColumnName("NF27");

                entity.Property(e => e.Nf28).HasColumnName("NF28");

                entity.Property(e => e.Nf29).HasColumnName("NF29");

                entity.Property(e => e.Nf3).HasColumnName("NF3");

                entity.Property(e => e.Nf30).HasColumnName("NF30");

                entity.Property(e => e.Nf31).HasColumnName("NF31");

                entity.Property(e => e.Nf32).HasColumnName("NF32");

                entity.Property(e => e.Nf33).HasColumnName("NF33");

                entity.Property(e => e.Nf34).HasColumnName("NF34");

                entity.Property(e => e.Nf35).HasColumnName("NF35");

                entity.Property(e => e.Nf36).HasColumnName("NF36");

                entity.Property(e => e.Nf37).HasColumnName("NF37");

                entity.Property(e => e.Nf38).HasColumnName("NF38");

                entity.Property(e => e.Nf39).HasColumnName("NF39");

                entity.Property(e => e.Nf4).HasColumnName("NF4");

                entity.Property(e => e.Nf40).HasColumnName("NF40");

                entity.Property(e => e.Nf5).HasColumnName("NF5");

                entity.Property(e => e.Nf6).HasColumnName("NF6");

                entity.Property(e => e.Nf7).HasColumnName("NF7");

                entity.Property(e => e.Nf8).HasColumnName("NF8");

                entity.Property(e => e.Nf9).HasColumnName("NF9");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TabName1).HasMaxLength(30);

                entity.Property(e => e.TabName2).HasMaxLength(30);

                entity.Property(e => e.TabName3).HasMaxLength(30);

                entity.Property(e => e.TabName4).HasMaxLength(30);

                entity.Property(e => e.TabName5).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<NoteFieldTemplate>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_NoteFieldTemplate_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_NoteFieldTemplate");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.FieldDefinition).HasColumnType("ntext");

                entity.Property(e => e.Nfname1)
                    .HasColumnName("NFName1")
                    .HasMaxLength(30);

                entity.Property(e => e.Nfname2)
                    .HasColumnName("NFName2")
                    .HasMaxLength(30);

                entity.Property(e => e.Nfname3)
                    .HasColumnName("NFName3")
                    .HasMaxLength(30);

                entity.Property(e => e.Nfname4)
                    .HasColumnName("NFName4")
                    .HasMaxLength(30);

                entity.Property(e => e.Nfname5)
                    .HasColumnName("NFName5")
                    .HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Operator>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_Operator_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_Operator");

                entity.HasIndex(e => e.AccountIdassigned)
                    .HasName("IX_Operator_4");

                entity.HasIndex(e => e.CardHolderId)
                    .HasName("IX_Operator_2");

                entity.HasIndex(e => e.OperatorName)
                    .HasName("IX_Operator_1");

                entity.HasIndex(e => e.TimeZoneId)
                    .HasName("IX_Operator_3");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AccountIdassigned).HasColumnName("AccountIDAssigned");

                entity.Property(e => e.CardHolderId).HasColumnName("CardHolderID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description).HasMaxLength(60);

                entity.Property(e => e.Language).HasMaxLength(60);

                entity.Property(e => e.LastLogOn).HasColumnType("datetime");

                entity.Property(e => e.LogOffCmd).HasColumnName("LogOffCMD");

                entity.Property(e => e.LogOnCmd).HasColumnName("LogOnCMD");

                entity.Property(e => e.LonOnattempts).HasColumnName("LonONAttempts");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.OperatorName).HasMaxLength(30);

                entity.Property(e => e.Password).HasColumnType("image");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.TimeZoneId).HasColumnName("TimeZoneID");

                entity.Property(e => e.UserDomain).HasMaxLength(255);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserSid)
                    .HasColumnName("UserSID")
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<OperatorActionValues>(entity =>
            {
                entity.HasKey(e => e.ActionId)
                    .HasName("PK__Operator__FFE3F4B92C3393D0");

                entity.Property(e => e.ActionId)
                    .HasColumnName("ActionID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ActionText).HasMaxLength(255);
            });

            modelBuilder.Entity<OperatorLevel>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_OperatorLevel_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_OperatorLevel");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description).HasMaxLength(60);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<OperatorLevelDb>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_OperatorLevelDB_1__10");

                entity.ToTable("OperatorLevelDB");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_OperatorLevelDB");

                entity.HasIndex(e => e.OperatorLevelId)
                    .HasName("IX_OperatorLevelDB_1");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Blob).HasColumnType("image");

                entity.Property(e => e.DatabaseId).HasColumnName("DatabaseID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.OperatorLevelId).HasColumnName("OperatorLevelID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<OperatorLevelDeviceEx>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_OperatorLevelDeviceEx_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_OperatorLevelDeviceEx");

                entity.HasIndex(e => e.OperatorLevelId)
                    .HasName("IX_OperatorLevelDeviceEx_1");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.OperatorLevelId).HasColumnName("OperatorLevelID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<OperatorLevelUi>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_OperatorLevelUI_1__10");

                entity.ToTable("OperatorLevelUI");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_OperatorLevelUI");

                entity.HasIndex(e => e.OperatorLevelId)
                    .HasName("IX_OperatorLevelUI_1");

                entity.HasIndex(e => e.UserInterfaceId)
                    .HasName("IX_OperatorLevelUI_2");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.OperatorLevelId).HasColumnName("OperatorLevelID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserInterfaceId).HasColumnName("UserInterfaceID");
            });

            modelBuilder.Entity<OperatorReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OperatorReport");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AccountName).HasMaxLength(30);

                entity.Property(e => e.CmdFile1Name)
                    .HasColumnName("CmdFile_1_Name")
                    .HasMaxLength(30);

                entity.Property(e => e.CmdFileName).HasMaxLength(30);

                entity.Property(e => e.Description).HasMaxLength(60);

                entity.Property(e => e.FirstName).HasMaxLength(35);

                entity.Property(e => e.LastLogOn).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(35);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.OperatorLevelName).HasMaxLength(30);

                entity.Property(e => e.OperatorName).HasMaxLength(30);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<PanelLog>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_PanelLog_1__10");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description).HasMaxLength(30);

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.Event).HasMaxLength(30);

                entity.Property(e => e.GenTime).HasColumnType("datetime");

                entity.Property(e => e.Group).HasMaxLength(40);

                entity.Property(e => e.ModuleName).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<PanelLogEx>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PanelLogEx");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Description).HasMaxLength(30);

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.Event).HasMaxLength(30);

                entity.Property(e => e.GenTime).HasColumnType("datetime");

                entity.Property(e => e.Group).HasMaxLength(40);

                entity.Property(e => e.ModuleName).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.Param3).HasMaxLength(1024);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RecvTime).HasColumnType("datetime");

                entity.Property(e => e.SeqId).HasColumnName("SeqID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<PanelTimeZone>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_PanelTimeZone_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_PanelTimeZone");

                entity.HasIndex(e => e.HwdeviceId)
                    .HasName("IX_PanelTimeZone_1");

                entity.HasIndex(e => e.TimeZoneId)
                    .HasName("IX_PanelTimeZone_2");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.HwdeviceId).HasColumnName("HWDeviceID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.TimeZoneId).HasColumnName("TimeZoneID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ReportSeg>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_ReportSeg_1__10");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ColName).HasMaxLength(30);

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.FaceName).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.ReportId)
                    .HasColumnName("ReportID")
                    .HasMaxLength(30);

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TableName).HasMaxLength(30);

                entity.Property(e => e.Text).HasMaxLength(255);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Xpos).HasColumnName("XPos");
            });

            modelBuilder.Entity<ReportTemplate>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_ReportTemplate_1__10");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Blob).HasColumnType("image");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Reports>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Description).HasMaxLength(60);

                entity.Property(e => e.LinkedCol).HasMaxLength(30);

                entity.Property(e => e.LinkedTable).HasMaxLength(30);

                entity.Property(e => e.MasterTable).HasMaxLength(30);

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.RecordId)
                    .HasColumnName("RecordID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_Schedule_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_Schedule");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Blob).HasColumnType("image");

                entity.Property(e => e.CommandFileId).HasColumnName("CommandFileID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.NextDateAndTime).HasColumnType("datetime");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ScheduleReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ScheduleReport");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Advname)
                    .HasColumnName("ADVName")
                    .HasMaxLength(40);

                entity.Property(e => e.CmdFileName).HasMaxLength(30);

                entity.Property(e => e.NextDateAndTime).HasColumnType("datetime");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.ScheduleName).HasMaxLength(30);

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<SystemConfig>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK___6__10");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ConfigBlob).HasColumnType("image");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<SystemOperators>(entity =>
            {
                entity.HasKey(e => e.OperatorId)
                    .HasName("PK__SystemOp__7BB12F8E300424B4");

                entity.Property(e => e.OperatorId)
                    .HasColumnName("OperatorID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OperatorName).HasMaxLength(255);
            });

            modelBuilder.Entity<TimeZone>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_TimeZone_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_TimeZone");

                entity.HasIndex(e => e.HolidayGroupId)
                    .HasName("IX_TimeZone_1");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description).HasMaxLength(60);

                entity.Property(e => e.HolidayGroupId).HasColumnName("HolidayGroupID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TimeZoneRange>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_TimeZoneRange_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_TimeZoneRange");

                entity.HasIndex(e => e.TimeZoneId)
                    .HasName("IX_TimeZoneRange_1");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.TimeZoneId).HasColumnName("TimeZoneID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TimezoneReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("TimezoneReport");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CountOfRecordId).HasColumnName("CountOfRecordID");

                entity.Property(e => e.Description).HasMaxLength(60);

                entity.Property(e => e.HolidayGroupId).HasColumnName("HolidayGroupID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<TrackingTree>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_TrackingTree_1__10");

                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_TrackingTree");

                entity.HasIndex(e => e.DeviceId)
                    .HasName("IX_TrackingTree_3");

                entity.HasIndex(e => e.NodeLevel)
                    .HasName("IX_TrackingTree_2");

                entity.HasIndex(e => e.ParentRecordId)
                    .HasName("IX_TrackingTree_1");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.ParentRecordId).HasColumnName("ParentRecordID");

                entity.Property(e => e.SpareDw1)
                    .HasColumnName("SpareDW1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw2)
                    .HasColumnName("SpareDW2")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw3)
                    .HasColumnName("SpareDW3")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareDw4)
                    .HasColumnName("SpareDW4")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Trailer>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
