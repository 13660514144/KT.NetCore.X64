using KT.WinPak.SDK.V48.Entities.Part;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
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

        public virtual DbSet<AccessLevelEntrance> AccessLevelEntrances { get; set; }
        public virtual DbSet<AccessLevelEntrancePlu> AccessLevelEntrancePlus { get; set; }
        public virtual DbSet<AccessLevelEntrancesReport> AccessLevelEntrancesReports { get; set; }
        public virtual DbSet<AccessLevelPanel> AccessLevelPanels { get; set; }
        public virtual DbSet<AccessLevelPlu> AccessLevelPlus { get; set; }
        public virtual DbSet<AccessLevelReport> AccessLevelReports { get; set; }
        public virtual DbSet<AccessTreeEx> AccessTreeices { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountMap> AccountMaps { get; set; }
        public virtual DbSet<ActionGroup> ActionGroups { get; set; }
        public virtual DbSet<ActionMessage> ActionMessages { get; set; }
        public virtual DbSet<ActiveXdefault> ActiveXdefaults { get; set; }
        public virtual DbSet<AttachedTimezone> AttachedTimezones { get; set; }
        public virtual DbSet<BadgeDatum> BadgeData { get; set; }
        public virtual DbSet<BadgeHeader> BadgeHeaders { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<CardAccessLevel> CardAccessLevels { get; set; }
        public virtual DbSet<CardAuditReport> CardAuditReports { get; set; }
        public virtual DbSet<CardEx> Cardices { get; set; }
        public virtual DbSet<CardHolder> CardHolders { get; set; }
        public virtual DbSet<CardHolderReport> CardHolderReports { get; set; }
        public virtual DbSet<CardHolderUserCode> CardHolderUserCodes { get; set; }
        public virtual DbSet<CardReport> CardReports { get; set; }
        public virtual DbSet<CmdFile> CmdFiles { get; set; }
        public virtual DbSet<CmdFileReport> CmdFileReports { get; set; }
        public virtual DbSet<Command> Commands { get; set; }
        public virtual DbSet<ControlTreeEx> ControlTreeices { get; set; }
        public virtual DbSet<CsBadgeDetail> CsBadgeDetails { get; set; }
        public virtual DbSet<CsRptAccessLevelReader> CsRptAccessLevelReaders { get; set; }
        public virtual DbSet<CsRptAccessTree> CsRptAccessTrees { get; set; }
        public virtual DbSet<CsRptCard> CsRptCards { get; set; }
        public virtual DbSet<CsRptCardNumaric> CsRptCardNumarics { get; set; }
        public virtual DbSet<CsRptCardhistory> CsRptCardhistories { get; set; }
        public virtual DbSet<CsRptCardholder> CsRptCardholders { get; set; }
        public virtual DbSet<CsRptMcard> CsRptMcards { get; set; }
        public virtual DbSet<CsRptMcardNumeric> CsRptMcardNumerics { get; set; }
        public virtual DbSet<CsRptMcardholder> CsRptMcardholders { get; set; }
        public virtual DbSet<CustomAlchangedEntrance> CustomAlchangedEntrances { get; set; }
        public virtual DbSet<CustomAlremovedEntrance> CustomAlremovedEntrances { get; set; }
        public virtual DbSet<DaylightSaving> DaylightSavings { get; set; }
        public virtual DbSet<DaylightSavingGroup> DaylightSavingGroups { get; set; }
        public virtual DbSet<Dbchange> Dbchanges { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<DeviceAdv> DeviceAdvs { get; set; }
        public virtual DbSet<DeviceReport> DeviceReports { get; set; }
        public virtual DbSet<Finuser> Finusers { get; set; }
        public virtual DbSet<FloorPlan> FloorPlans { get; set; }
        public virtual DbSet<FloorPlanItem> FloorPlanItems { get; set; }
        public virtual DbSet<FloorPlanReport> FloorPlanReports { get; set; }
        public virtual DbSet<GroupAdv> GroupAdvs { get; set; }
        public virtual DbSet<GuardTour> GuardTours { get; set; }
        public virtual DbSet<GuardTourCheckPoint> GuardTourCheckPoints { get; set; }
        public virtual DbSet<GuardTourReport> GuardTourReports { get; set; }
        public virtual DbSet<Header> Headers { get; set; }
        public virtual DbSet<Hidquery> Hidqueries { get; set; }
        public virtual DbSet<History> Histories { get; set; }
        public virtual DbSet<HistoryReport> HistoryReports { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<HolidayGroup> HolidayGroups { get; set; }
        public virtual DbSet<HolidayMaster> HolidayMasters { get; set; }
        public virtual DbSet<HwindependentDevice> HwindependentDevices { get; set; }
        public virtual DbSet<NftabLayout> NftabLayouts { get; set; }
        public virtual DbSet<NoteFieldTemplate> NoteFieldTemplates { get; set; }
        public virtual DbSet<Operator> Operators { get; set; }
        public virtual DbSet<OperatorActionValue> OperatorActionValues { get; set; }
        public virtual DbSet<OperatorLevel> OperatorLevels { get; set; }
        public virtual DbSet<OperatorLevelDb> OperatorLevelDbs { get; set; }
        public virtual DbSet<OperatorLevelDeviceEx> OperatorLevelDeviceices { get; set; }
        public virtual DbSet<OperatorLevelUi> OperatorLevelUis { get; set; }
        public virtual DbSet<OperatorReport> OperatorReports { get; set; }
        public virtual DbSet<PanelLog> PanelLogs { get; set; }
        public virtual DbSet<PanelTimeZone> PanelTimeZones { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<ReportSeg> ReportSegs { get; set; }
        public virtual DbSet<ReportTemplate> ReportTemplates { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<ScheduleReport> ScheduleReports { get; set; }
        public virtual DbSet<SubAccount> SubAccounts { get; set; }
        public virtual DbSet<SystemConfig> SystemConfigs { get; set; }
        public virtual DbSet<SystemOperator> SystemOperators { get; set; }
        public virtual DbSet<TaAdmin> TaAdmins { get; set; }
        public virtual DbSet<TaAttnRegularizeMaster> TaAttnRegularizeMasters { get; set; }
        public virtual DbSet<TaBackendServiceSetting> TaBackendServiceSettings { get; set; }
        public virtual DbSet<TaDepartmentMaster> TaDepartmentMasters { get; set; }
        public virtual DbSet<TaDesignationMaster> TaDesignationMasters { get; set; }
        public virtual DbSet<TaEmailTemplate> TaEmailTemplates { get; set; }
        public virtual DbSet<TaEmpLeaveAvailability> TaEmpLeaveAvailabilities { get; set; }
        public virtual DbSet<TaEmpReportSchedule> TaEmpReportSchedules { get; set; }
        public virtual DbSet<TaEmpReportScheduleDetail> TaEmpReportScheduleDetails { get; set; }
        public virtual DbSet<TaEmpTraverseToTum> TaEmpTraverseToTa { get; set; }
        public virtual DbSet<TaEmployee> TaEmployees { get; set; }
        public virtual DbSet<TaEmployeeAttnRegularize> TaEmployeeAttnRegularizes { get; set; }
        public virtual DbSet<TaEmployeeAudit> TaEmployeeAudits { get; set; }
        public virtual DbSet<TaEmployeeLeave> TaEmployeeLeaves { get; set; }
        public virtual DbSet<TaEmployeeLeaveDetail> TaEmployeeLeaveDetails { get; set; }
        public virtual DbSet<TaEmployeeOutDuty> TaEmployeeOutDuties { get; set; }
        public virtual DbSet<TaEmployeeOutDutyDetail> TaEmployeeOutDutyDetails { get; set; }
        public virtual DbSet<TaEmployeeShift> TaEmployeeShifts { get; set; }
        public virtual DbSet<TaEmployeeTypeMaster> TaEmployeeTypeMasters { get; set; }
        public virtual DbSet<TaGender> TaGenders { get; set; }
        public virtual DbSet<TaHolidayMaster> TaHolidayMasters { get; set; }
        public virtual DbSet<TaLanguageSupport> TaLanguageSupports { get; set; }
        public virtual DbSet<TaLeaveMaster> TaLeaveMasters { get; set; }
        public virtual DbSet<TaLeaveTypeMaster> TaLeaveTypeMasters { get; set; }
        public virtual DbSet<TaLocationMaster> TaLocationMasters { get; set; }
        public virtual DbSet<TaLoginConfiguration> TaLoginConfigurations { get; set; }
        public virtual DbSet<TaOutDutyTypeMaster> TaOutDutyTypeMasters { get; set; }
        public virtual DbSet<TaReportFormat> TaReportFormats { get; set; }
        public virtual DbSet<TaReportQueue> TaReportQueues { get; set; }
        public virtual DbSet<TaResource> TaResources { get; set; }
        public virtual DbSet<TaRole> TaRoles { get; set; }
        public virtual DbSet<TaScheduledReport> TaScheduledReports { get; set; }
        public virtual DbSet<TaScheduledReportsFrequency> TaScheduledReportsFrequencies { get; set; }
        public virtual DbSet<TaShiftDetail> TaShiftDetails { get; set; }
        public virtual DbSet<TaShiftMaster> TaShiftMasters { get; set; }
        public virtual DbSet<TaShiftRotation> TaShiftRotations { get; set; }
        public virtual DbSet<TaSmtpSetting> TaSmtpSettings { get; set; }
        public virtual DbSet<TaStatusMaster> TaStatusMasters { get; set; }
        public virtual DbSet<TaUpdateLeaveDetail> TaUpdateLeaveDetails { get; set; }
        public virtual DbSet<TaUserReportSchedule> TaUserReportSchedules { get; set; }
        public virtual DbSet<TaUserReportScheduleDetail> TaUserReportScheduleDetails { get; set; }
        public virtual DbSet<TimeZone> TimeZones { get; set; }
        public virtual DbSet<TimeZoneRange> TimeZoneRanges { get; set; }
        public virtual DbSet<TimezoneReport> TimezoneReports { get; set; }
        public virtual DbSet<TrackingTree> TrackingTrees { get; set; }
        public virtual DbSet<Trailer> Trailers { get; set; }

        public static string ConnectString;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (ConnectString != SqlConnectHelper.ConnectString
                && !string.IsNullOrEmpty(SqlConnectHelper.ConnectString))
            {
                optionsBuilder.UseSqlServer(SqlConnectHelper.ConnectString);
                ConnectString = SqlConnectHelper.ConnectString;
            }

            //            if (!optionsBuilder.IsConfigured)
            //            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //                optionsBuilder.UseSqlServer("Data Source=192.168.0.240;Initial Catalog=WIN-PAK PRO;User ID=sa;Password=1qaz!QAZ");
            //            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessLevelEntrance>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AccessLevelEntrances");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccessTreeExName).HasMaxLength(30);

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Advname)
                    .HasMaxLength(40)
                    .HasColumnName("ADVName");

                entity.Property(e => e.EntranceId).HasColumnName("EntranceID");

                entity.Property(e => e.GroupAdvname)
                    .HasMaxLength(40)
                    .HasColumnName("GroupADVName");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.TimeZoneId).HasColumnName("TimeZoneID");

                entity.Property(e => e.TimeZoneName).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<AccessLevelEntrancePlu>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK___2__10");

                //entity.HasIndex(e => e.AccountId, "IX_AccessLevelEntrancePlus");

                //entity.HasIndex(e => e.AccessLevelId, "IX_AccessLevelEntrancePlus_1");

                //entity.HasIndex(e => e.EntranceId, "IX_AccessLevelEntrancePlus_2");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.TimeZoneId).HasColumnName("TimeZoneID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<AccessLevelEntrancesReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AccessLevelEntrancesReport");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Advname)
                    .HasMaxLength(40)
                    .HasColumnName("ADVName");

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

                entity.Property(e => e.TimeZoneId).HasColumnName("TimeZoneID");

                entity.Property(e => e.TimeZoneName).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<AccessLevelPanel>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_AccessLevelPanel_1__10");

                entity.ToTable("AccessLevelPanel");

                //entity.HasIndex(e => e.AccountId, "IX_AccessLevelPanel");

                //entity.HasIndex(e => e.AccessLevelId, "IX_AccessLevelPanel_1");

                //entity.HasIndex(e => e.HwdeviceId, "IX_AccessLevelPanel_2");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<AccessLevelPlu>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK___1__10");

                //entity.HasIndex(e => e.AccountId, "IX_AccessLevelPlus");

                //entity.HasIndex(e => e.Pw5000alid, "IX_AccessLevelPlus_1");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

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
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RecordID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<AccessTreeEx>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_AccessTreeEx_1__10");

                entity.ToTable("AccessTreeEx");

                //entity.HasIndex(e => e.AccountId, "IX_AccessTreeEx");

                //entity.HasIndex(e => e.ParentRecordId, "IX_AccessTreeEx_1");

                //entity.HasIndex(e => e.NodeLevel, "IX_AccessTreeEx_2");

                //entity.HasIndex(e => e.DeviceId, "IX_AccessTreeEx_3");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK___5__10");

                entity.ToTable("Account");

                //entity.HasIndex(e => e.RecordId, "IX_Account").IsUnique();

                //entity.HasIndex(e => e.AccountId, "IX_Account_1");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AcctName).HasMaxLength(30);

                entity.Property(e => e.Blob).HasColumnType("image");

                entity.Property(e => e.ContactAddress).HasMaxLength(255);

                entity.Property(e => e.ContactName).HasMaxLength(30);

                entity.Property(e => e.ContactPhone).HasMaxLength(30);

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Fax).HasMaxLength(30);

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<AccountMap>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK__AccountM__FBDF78C9CC8BC3A9");

                entity.ToTable("AccountMap");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<ActionGroup>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_ActionGroup_1__10");

                entity.ToTable("ActionGroup");

                //entity.HasIndex(e => e.AccountId, "IX_ActionGroup");

                //entity.HasIndex(e => e.ActionGroupId, "IX_ActionGroup_1");

                //entity.HasIndex(e => e.DeviceType, "IX_ActionGroup_2");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ActionMessage>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_ActionMessage_1__10");

                entity.ToTable("ActionMessage");

                //entity.HasIndex(e => e.AccountId, "IX_ActionMessage");

                //entity.HasIndex(e => e.ActionGroupId, "IX_ActionMessage_1");

                //entity.HasIndex(e => e.TimeZoneId, "IX_ActionMessage_2");

                //entity.HasIndex(e => e.RecvCmdfileId, "IX_ActionMessage_3");

                //entity.HasIndex(e => e.AckCmdfileId, "IX_ActionMessage_4");

                //entity.HasIndex(e => e.ClearCmdfileId, "IX_ActionMessage_5");

                //entity.HasIndex(e => e.CameraId, "IX_ActionMessage_6");

                //entity.HasIndex(e => e.MonitorId, "IX_ActionMessage_7");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AckCmdfileId).HasColumnName("AckCMDFileID");

                entity.Property(e => e.ActionGroupId).HasColumnName("ActionGroupID");

                entity.Property(e => e.AnimationFile).HasMaxLength(255);

                entity.Property(e => e.Blob).HasColumnType("image");

                entity.Property(e => e.CameraId).HasColumnName("CameraID");

                entity.Property(e => e.ClearCmdfileId).HasColumnName("ClearCMDFileID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.MailFlag).HasDefaultValueSql("((0))");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.TimeZoneId).HasColumnName("TimeZoneID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ActiveXdefault>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("ActiveXDefaults");

                //entity.HasIndex(e => e.AccountId, "IX_ActiveXDefaults");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<AttachedTimezone>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AttachedTimezones");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");
            });

            modelBuilder.Entity<BadgeDatum>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_BadgeData_1__10");

                //entity.HasIndex(e => e.AccountId, "IX_BadgeData");

                //entity.HasIndex(e => e.BadgeId, "IX_BadgeData_1");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<BadgeHeader>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_BadgeHeader_1__10");

                entity.ToTable("BadgeHeader");

                //entity.HasIndex(e => e.AccountId, "IX_BadgeHeader");

                //entity.HasIndex(e => e.BadgeId, "IX_BadgeHeader_1");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_Card_1__10");

                entity.ToTable("Card");

                //entity.HasIndex(e => e.CardHolderId, "IX_Card_1");

                //entity.HasIndex(e => e.AccessLevelId, "IX_Card_2");

                //entity.HasIndex(e => e.AccountId, "IX_Card_3");

                //entity.HasIndex(e => e.CardNumber, "IX_Card_4");

                //entity.HasIndex(e => e.ActivationDate, "IX_Card_5");

                //entity.HasIndex(e => e.ExpirationDate, "IX_Card_6");

                //entity.HasIndex(e => e.CardStatus, "IX_Card_7");

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
                    .HasMaxLength(10)
                    .HasColumnName("PIN1");

                entity.Property(e => e.Pin2)
                    .HasMaxLength(5)
                    .HasColumnName("PIN2");

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

                entity.Property(e => e.SpareStr2).HasMaxLength(120);

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CardAccessLevel>(entity =>
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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CardAuditReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CardAuditReport");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AcctName).HasMaxLength(30);

                entity.Property(e => e.GenTime).HasColumnType("datetime");

                entity.Property(e => e.OpName).HasMaxLength(30);

                entity.Property(e => e.Param3).HasMaxLength(1024);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.SubAcctName).HasMaxLength(30);
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

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.Pin1)
                    .HasMaxLength(10)
                    .HasColumnName("PIN1");

                entity.Property(e => e.Pin2)
                    .HasMaxLength(5)
                    .HasColumnName("PIN2");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(60);

                entity.Property(e => e.SpareStr2).HasMaxLength(120);

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CardHolder>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_CardHolder_1__10");

                entity.ToTable("CardHolder");

                //entity.HasIndex(e => e.AccountId, "IX_CardHolder_3").HasFillFactor((byte)100);

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

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

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

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<CardHolderUserCode>(entity =>
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
                    .IsUnicode(false)
                    .HasComment("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.SpareStr2)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasComment("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

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
                    .HasColumnType("datetime")
                    .HasColumnName("ALPlusActivationDate");

                entity.Property(e => e.AlplusExpirationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ALPlusExpirationDate");

                entity.Property(e => e.AlplusName)
                    .HasMaxLength(30)
                    .HasColumnName("ALPlusName");

                entity.Property(e => e.AlplusRecId).HasColumnName("ALPlusRecID");

                entity.Property(e => e.BadgeHeader1Name)
                    .HasMaxLength(50)
                    .HasColumnName("BadgeHeader_1_Name");

                entity.Property(e => e.BadgeHeaderName).HasMaxLength(50);

                entity.Property(e => e.CardActivationDate).HasColumnType("datetime");

                entity.Property(e => e.CardExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.CardHolderId).HasColumnName("CardHolderID");

                entity.Property(e => e.CardNumber).HasMaxLength(30);

                entity.Property(e => e.CardRecId).HasColumnName("CardRecID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.Pin1)
                    .HasMaxLength(10)
                    .HasColumnName("PIN1");

                entity.Property(e => e.Pin2)
                    .HasMaxLength(5)
                    .HasColumnName("PIN2");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(60);

                entity.Property(e => e.SpareStr2).HasMaxLength(120);

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.SubAccountName).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CmdFile>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_CmdFile_1__10");

                entity.ToTable("CmdFile");

                //entity.HasIndex(e => e.AccountId, "IX_CmdFile");

                //entity.HasIndex(e => e.CommandFileId, "IX_CmdFile_1");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

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

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Command>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_Command_1__10");

                entity.ToTable("Command");

                //entity.HasIndex(e => e.AccountId, "IX_Command");

                //entity.HasIndex(e => e.CommandFileId, "IX_Command_1");

                //entity.HasIndex(e => e.Hid, "IX_Command_2");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ControlTreeEx>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_ControlTreeEx_1__10");

                entity.ToTable("ControlTreeEx");

                //entity.HasIndex(e => e.AccountId, "IX_ControlTreeEx");

                //entity.HasIndex(e => e.ParentRecordId, "IX_ControlTreeEx_1");

                //entity.HasIndex(e => e.NodeLevel, "IX_ControlTreeEx_2");

                //entity.HasIndex(e => e.DeviceId, "IX_ControlTreeEx_3");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CsBadgeDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("cs_BadgeDetails");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ActivationDate).HasColumnType("datetime");

                entity.Property(e => e.CardHolderId).HasColumnName("CardHolderID");

                entity.Property(e => e.CardNumber).HasMaxLength(30);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");
            });

            modelBuilder.Entity<CsRptAccessLevelReader>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("cs_RptAccessLevelReaders");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccessLevelName).HasMaxLength(30);

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ReaderId).HasColumnName("ReaderID");

                entity.Property(e => e.ReaderName).HasMaxLength(40);

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.SubAccountName).HasMaxLength(30);

                entity.Property(e => e.TimezoneId).HasColumnName("TimezoneID");

                entity.Property(e => e.TimezoneName).HasMaxLength(50);
            });

            modelBuilder.Entity<CsRptAccessTree>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("cs_RptAccessTree");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.BranchName).HasMaxLength(30);

                entity.Property(e => e.EntranceId).HasColumnName("EntranceID");

                entity.Property(e => e.ParentRecordId).HasColumnName("ParentRecordID");

                entity.Property(e => e.ReaderName).HasMaxLength(40);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");
            });

            modelBuilder.Entity<CsRptCard>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("cs_RptCard");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccessLevelName).HasMaxLength(30);

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CardActivationDate)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CardExpirationDate)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CardNumber).HasMaxLength(30);

                entity.Property(e => e.CardStatus)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Sparedw3)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Sparedw4).HasColumnName("sparedw4");

                entity.Property(e => e.Sparew4).HasColumnName("sparew4");

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.SubAccountName).HasMaxLength(30);
            });

            modelBuilder.Entity<CsRptCardNumaric>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("cs_RptCardNumaric");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccessLevelName).HasMaxLength(30);

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CardActivationDate)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CardExpirationDate)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CardNumber).HasColumnType("numeric(20, 0)");

                entity.Property(e => e.CardStatus)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.Sparedw3)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.SubAccountName).HasMaxLength(30);
            });

            modelBuilder.Entity<CsRptCardhistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("cs_RptCardhistory");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Cardnumber).HasMaxLength(1024);

                entity.Property(e => e.Gentime).HasColumnType("datetime");

                entity.Property(e => e.ReaderId).HasColumnName("ReaderID");

                entity.Property(e => e.ReaderName).HasMaxLength(40);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(33)
                    .IsUnicode(false);

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CsRptCardholder>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("cs_RptCardholder");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Cardnumber).HasMaxLength(30);

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<CsRptMcard>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("cs_RptMCard");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccesslevelName).HasMaxLength(30);

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CardActivationDate)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CardExpirationDate)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CardNumber).HasMaxLength(30);

                entity.Property(e => e.CardStatus)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Sparedw3)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Sparedw4).HasColumnName("sparedw4");

                entity.Property(e => e.Sparew4).HasColumnName("sparew4");

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.SubAccountName).HasMaxLength(30);
            });

            modelBuilder.Entity<CsRptMcardNumeric>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("cs_RptMCardNumeric");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccesslevelName).HasMaxLength(30);

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CardActivationDate)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CardExpirationDate)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CardNumber).HasColumnType("numeric(20, 0)");

                entity.Property(e => e.CardStatus)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Sparedw3)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Sparedw4).HasColumnName("sparedw4");

                entity.Property(e => e.Sparew4).HasColumnName("sparew4");

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.SubAccountName).HasMaxLength(30);
            });

            modelBuilder.Entity<CsRptMcardholder>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("cs_RptMCardholder");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Cardnumber).HasMaxLength(30);

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<CustomAlchangedEntrance>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CustomALChangedEntrances");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Advname)
                    .HasMaxLength(40)
                    .HasColumnName("ADVName");

                entity.Property(e => e.Advname1)
                    .HasMaxLength(40)
                    .HasColumnName("ADVName1");

                entity.Property(e => e.CardRecId).HasColumnName("CardRecID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(60);

                entity.Property(e => e.SpareStr2).HasMaxLength(120);

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<CustomAlremovedEntrance>(entity =>
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

                entity.Property(e => e.SpareStr1).HasMaxLength(60);

                entity.Property(e => e.SpareStr2).HasMaxLength(120);

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<DaylightSaving>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_DaylightSavings");

                entity.ToTable("DaylightSaving");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<DaylightSavingGroup>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("DaylightSavingGroup");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Dbchange>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("DBChanges");

                //entity.HasIndex(e => e.Dbid, "IX_DBChanges");

                //entity.HasIndex(e => e.Request, "IX_DBChanges_1");

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
                    .IsFixedLength(true);

                entity.Property(e => e.SpareStr2)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_Device_1__10");

                entity.ToTable("Device");

                //entity.HasIndex(e => e.AccountId, "IX_Device");

                //entity.HasIndex(e => e.DeviceType, "IX_Device_1");

                //entity.HasIndex(e => e.ParentId, "IX_Device_2");

                //entity.HasIndex(e => e.DeviceNo, "IX_Device_3");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<DeviceAdv>(entity =>
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
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RecordID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<DeviceReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DeviceReport");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Blob).HasColumnType("image");

                entity.Property(e => e.DeviceAdvname)
                    .HasMaxLength(40)
                    .HasColumnName("DeviceADVName");

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

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Finuser>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK__FINUser__FBDF78C9D4C06C41");

                entity.ToTable("FINUser");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccessLevelId).HasColumnName("AccessLevelID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ActivationDate).HasColumnType("datetime");

                entity.Property(e => e.Blob).HasColumnType("image");

                entity.Property(e => e.CardHolderId).HasColumnName("CardHolderID");

                entity.Property(e => e.CardId)
                    .HasMaxLength(30)
                    .HasColumnName("CardID");

                entity.Property(e => e.CustomId)
                    .HasMaxLength(30)
                    .HasColumnName("CustomID");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<FloorPlan>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_FloorPlan_1__10");

                entity.ToTable("FloorPlan");

                //entity.HasIndex(e => e.AccountId, "IX_FloorPlan");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<FloorPlanItem>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_FloorPlanItem_1__10");

                entity.ToTable("FloorPlanItem");

                //entity.HasIndex(e => e.AccountId, "IX_FloorPlanItem");

                //entity.HasIndex(e => e.FloorPlanId, "IX_FloorPlanItem_1");

                //entity.HasIndex(e => e.Hid, "IX_FloorPlanItem_2");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.FloorPlanId).HasColumnName("FloorPlanID");

                entity.Property(e => e.Hid).HasColumnName("HID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.Olecontrol)
                    .HasColumnType("image")
                    .HasColumnName("OLEControl");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

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
                    .HasMaxLength(40)
                    .HasColumnName("ADVName");

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

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<GroupAdv>(entity =>
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

                entity.ToTable("GuardTour");

                //entity.HasIndex(e => e.AccountId, "IX_GuardTour");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<GuardTourCheckPoint>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_GuardTourCheckPoints_1__10");

                //entity.HasIndex(e => e.AccountId, "IX_GuardTourCheckPoints");

                //entity.HasIndex(e => e.GuardTourId, "IX_GuardTourCheckPoints_1");

                //entity.HasIndex(e => e.SequenceNum, "IX_GuardTourCheckPoints_2");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

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

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Header>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.ToTable("Header");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

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
                    .HasMaxLength(40)
                    .HasColumnName("ADVName");

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

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_History_1__10");

                entity.ToTable("History");

                //entity.HasIndex(e => e.AccountId, "IX_History");

                //entity.HasIndex(e => e.RecvTime, "IX_History_1");

                //entity.HasIndex(e => e.GenTime, "IX_History_2");

                //entity.HasIndex(e => e.SeqId, "IX_History_3");

                //entity.HasIndex(e => e.Type1, "IX_History_4");

                //entity.HasIndex(e => e.Type2, "IX_History_5");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<HistoryReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("HistoryReport");

                entity.Property(e => e.CardHolderAccountId).HasColumnName("CardHolderAccountID");

                entity.Property(e => e.GenTime).HasColumnType("datetime");

                entity.Property(e => e.HistoryAccountId).HasColumnName("HistoryAccountID");

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

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Holiday>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_Holiday_1__10");

                entity.ToTable("Holiday");

                //entity.HasIndex(e => e.AccountId, "IX_Holiday");

                //entity.HasIndex(e => e.HolidayGroupId, "IX_Holiday_1");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<HolidayGroup>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_HolidayGroup_1__10");

                entity.ToTable("HolidayGroup");

                //entity.HasIndex(e => e.AccountId, "IX_HolidayGroup");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<HolidayMaster>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_HolidayMaster_1__10");

                entity.ToTable("HolidayMaster");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Holiday).HasColumnType("datetime");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<HwindependentDevice>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_HWIndependentDevices_1__10");

                entity.ToTable("HWIndependentDevices");

                //entity.HasIndex(e => e.AccountId, "IX_HWIndependentDevices");

                //entity.HasIndex(e => e.DeviceId, "IX_HWIndependentDevices_1");

                //entity.HasIndex(e => e.LandisOldPointId, "IX_HWIndependentDevices_10");

                //entity.HasIndex(e => e.CommServerId, "IX_HWIndependentDevices_2");

                //entity.HasIndex(e => new { e.DeviceType, e.DeviceSubType1 }, "IX_HWIndependentDevices_3");

                //entity.HasIndex(e => e.CommandFileId, "IX_HWIndependentDevices_4");

                //entity.HasIndex(e => new { e.HwdeviceId, e.HwdeviceSubId1 }, "IX_HWIndependentDevices_5");

                //entity.HasIndex(e => e.FloorPlanId, "IX_HWIndependentDevices_6");

                //entity.HasIndex(e => e.ActionGroupId, "IX_HWIndependentDevices_7");

                //entity.HasIndex(e => e.LandisHwdeviceId, "IX_HWIndependentDevices_8");

                //entity.HasIndex(e => e.LandisPointId, "IX_HWIndependentDevices_9");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<NftabLayout>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_NFTabLayout_1__10");

                entity.ToTable("NFTabLayout");

                //entity.HasIndex(e => e.AccountId, "IX_NFTabLayout");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

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

                entity.ToTable("NoteFieldTemplate");

                //entity.HasIndex(e => e.AccountId, "IX_NoteFieldTemplate");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.FieldDefinition).HasColumnType("ntext");

                entity.Property(e => e.Nfname1)
                    .HasMaxLength(30)
                    .HasColumnName("NFName1");

                entity.Property(e => e.Nfname2)
                    .HasMaxLength(30)
                    .HasColumnName("NFName2");

                entity.Property(e => e.Nfname3)
                    .HasMaxLength(30)
                    .HasColumnName("NFName3");

                entity.Property(e => e.Nfname4)
                    .HasMaxLength(30)
                    .HasColumnName("NFName4");

                entity.Property(e => e.Nfname5)
                    .HasMaxLength(30)
                    .HasColumnName("NFName5");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Operator>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_Operator_1__10");

                entity.ToTable("Operator");

                //entity.HasIndex(e => e.AccountId, "IX_Operator");

                //entity.HasIndex(e => e.OperatorName, "IX_Operator_1");

                //entity.HasIndex(e => e.CardHolderId, "IX_Operator_2");

                //entity.HasIndex(e => e.TimeZoneId, "IX_Operator_3");

                //entity.HasIndex(e => e.AccountIdassigned, "IX_Operator_4");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.TimeZoneId).HasColumnName("TimeZoneID");

                entity.Property(e => e.UserDomain).HasMaxLength(255);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserSid)
                    .HasMaxLength(60)
                    .HasColumnName("UserSID");
            });

            modelBuilder.Entity<OperatorActionValue>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ActionId).HasColumnName("ActionID");

                entity.Property(e => e.ActionText).HasMaxLength(255);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.RecordId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RecordID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<OperatorLevel>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_OperatorLevel_1__10");

                entity.ToTable("OperatorLevel");

                //entity.HasIndex(e => e.AccountId, "IX_OperatorLevel");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<OperatorLevelDb>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_OperatorLevelDB_1__10");

                entity.ToTable("OperatorLevelDB");

                //entity.HasIndex(e => e.AccountId, "IX_OperatorLevelDB");

                //entity.HasIndex(e => e.OperatorLevelId, "IX_OperatorLevelDB_1");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<OperatorLevelDeviceEx>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_OperatorLevelDeviceEx_1__10");

                entity.ToTable("OperatorLevelDeviceEx");

                //entity.HasIndex(e => e.AccountId, "IX_OperatorLevelDeviceEx");

                //entity.HasIndex(e => e.OperatorLevelId, "IX_OperatorLevelDeviceEx_1");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<OperatorLevelUi>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_OperatorLevelUI_1__10");

                entity.ToTable("OperatorLevelUI");

                //entity.HasIndex(e => e.AccountId, "IX_OperatorLevelUI");

                //entity.HasIndex(e => e.OperatorLevelId, "IX_OperatorLevelUI_1");

                //entity.HasIndex(e => e.UserInterfaceId, "IX_OperatorLevelUI_2");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

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
                    .HasMaxLength(30)
                    .HasColumnName("CmdFile_1_Name");

                entity.Property(e => e.CmdFileName).HasMaxLength(30);

                entity.Property(e => e.Description).HasMaxLength(60);

                entity.Property(e => e.LastLogOn).HasColumnType("datetime");

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

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<PanelLog>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_PanelLog_1__10");

                entity.ToTable("PanelLog");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description)
                    .HasMaxLength(30)
                    .HasComment("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.Event)
                    .HasMaxLength(30)
                    .HasComment("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.GenTime).HasColumnType("datetime");

                entity.Property(e => e.Group)
                    .HasMaxLength(40)
                    .HasComment("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ModuleName)
                    .HasMaxLength(30)
                    .HasComment("SQL_Latin1_General_CP1_CI_AS");

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

                entity.Property(e => e.SpareStr1)
                    .HasMaxLength(30)
                    .HasComment("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.SpareStr2)
                    .HasMaxLength(30)
                    .HasComment("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.SpareW1).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW2).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW3).HasDefaultValueSql("((0))");

                entity.Property(e => e.SpareW4).HasDefaultValueSql("((0))");

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<PanelTimeZone>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_PanelTimeZone_1__10");

                entity.ToTable("PanelTimeZone");

                //entity.HasIndex(e => e.AccountId, "IX_PanelTimeZone");

                //entity.HasIndex(e => e.HwdeviceId, "IX_PanelTimeZone_1");

                //entity.HasIndex(e => e.TimeZoneId, "IX_PanelTimeZone_2");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.TimeZoneId).HasColumnName("TimeZoneID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Report>(entity =>
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
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RecordID");

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<ReportSeg>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_ReportSeg_1__10");

                entity.ToTable("ReportSeg");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ColName).HasMaxLength(30);

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.FaceName).HasMaxLength(30);

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.ReportId)
                    .HasMaxLength(30)
                    .HasColumnName("ReportID");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

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

                entity.ToTable("ReportTemplate");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_Schedule_1__10");

                entity.ToTable("Schedule");

                //entity.HasIndex(e => e.AccountId, "IX_Schedule");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Blob).HasColumnType("image");

                entity.Property(e => e.CommandFileId).HasColumnName("CommandFileID");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(255);

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<ScheduleReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ScheduleReport");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Advname)
                    .HasMaxLength(40)
                    .HasColumnName("ADVName");

                entity.Property(e => e.CmdFileName).HasMaxLength(30);

                entity.Property(e => e.NextDateAndTime).HasColumnType("datetime");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.ScheduleName).HasMaxLength(255);

                entity.Property(e => e.SpareDw1).HasColumnName("SpareDW1");

                entity.Property(e => e.SpareDw2).HasColumnName("SpareDW2");

                entity.Property(e => e.SpareDw3).HasColumnName("SpareDW3");

                entity.Property(e => e.SpareDw4).HasColumnName("SpareDW4");

                entity.Property(e => e.SpareStr1).HasMaxLength(30);

                entity.Property(e => e.SpareStr2).HasMaxLength(30);

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<SubAccount>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_SubAccount___5__10");

                entity.ToTable("SubAccount");

                //entity.HasIndex(e => e.RecordId, "IX_SubAccount").IsUnique();

                //entity.HasIndex(e => e.SubAccountId, "IX_SubAccount_1");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Blob).HasColumnType("image");

                entity.Property(e => e.ContactAddress).HasMaxLength(255);

                entity.Property(e => e.ContactName).HasMaxLength(30);

                entity.Property(e => e.ContactPhone).HasMaxLength(30);

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Fax).HasMaxLength(30);

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SubAcctName).HasMaxLength(30);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<SystemConfig>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK___6__10");

                entity.ToTable("SystemConfig");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<SystemOperator>(entity =>
            {
                entity.HasKey(e => e.OperatorId)
                    .HasName("PK__SystemOp__7BB12F8E4156E14F");

                entity.Property(e => e.OperatorId)
                    .ValueGeneratedNever()
                    .HasColumnName("OperatorID");

                entity.Property(e => e.OperatorName).HasMaxLength(255);
            });

            modelBuilder.Entity<TaAdmin>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__TA_Admin__7AD04F11C841674E");

                entity.ToTable("TA_Admin");

                entity.Property(e => e.AdminMailId).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastLogOn).HasColumnType("datetime");

                entity.Property(e => e.LoginPassword).HasMaxLength(100);

                entity.Property(e => e.Role).HasMaxLength(20);

                entity.Property(e => e.UserName).HasMaxLength(100);
            });

            modelBuilder.Entity<TaAttnRegularizeMaster>(entity =>
            {
                entity.HasKey(e => e.AttnRegularizeTypeId)
                    .HasName("PK__TA_AttnR__7CFAEE4717B235F7");

                entity.ToTable("TA_AttnRegularizeMaster");

                entity.Property(e => e.AttnRegularizeCode).HasMaxLength(50);

                entity.Property(e => e.AttnRegularizeDesc).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TaBackendServiceSetting>(entity =>
            {
                entity.HasKey(e => e.SettingId)
                    .HasName("PK__TA_Backe__54372B1DABF591A5");

                entity.ToTable("TA_BackendServiceSettings");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TaDepartmentMaster>(entity =>
            {
                entity.HasKey(e => e.DeptId)
                    .HasName("PK__TA_Depar__014881AE98CC0B57");

                entity.ToTable("TA_DepartmentMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeptCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DeptName).HasMaxLength(100);
            });

            modelBuilder.Entity<TaDesignationMaster>(entity =>
            {
                entity.HasKey(e => e.DesignationId)
                    .HasName("PK__TA_Desig__BABD60DE9F0423C3");

                entity.ToTable("TA_DesignationMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DesignationCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DesignationName).HasMaxLength(100);
            });

            modelBuilder.Entity<TaEmailTemplate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TA_EmailTemplate");

                entity.Property(e => e.Subject).IsRequired();

                entity.Property(e => e.TemplateDescription)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Language)
                    .WithMany()
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_EmailTemplate_TA_LanguageSupport");
            });

            modelBuilder.Entity<TaEmpLeaveAvailability>(entity =>
            {
                entity.HasKey(e => e.LeaveAvailableId)
                    .HasName("PK__TA_EmpLe__DD43043148CC77E3");

                entity.ToTable("TA_EmpLeaveAvailability");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TaEmpLeaveAvailabilities)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_EmpLeaveAvailability_Employee");

                entity.HasOne(d => d.LeaveType)
                    .WithMany(p => p.TaEmpLeaveAvailabilities)
                    .HasForeignKey(d => d.LeaveTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_EmpLeaveAvailability_LeaveTypeMaster");
            });

            modelBuilder.Entity<TaEmpReportSchedule>(entity =>
            {
                entity.HasKey(e => e.ScheduleId)
                    .HasName("PK__TA_EmpRe__9C8A5B49F9714C9D");

                entity.ToTable("TA_EmpReportSchedule");

                entity.Property(e => e.ScheduleName).HasMaxLength(50);

                entity.Property(e => e.ScheduleTime).HasColumnType("datetime");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.TaEmpReportSchedules)
                    .HasForeignKey(d => d.ReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_EmpReportSchedule_ScheduledReports");
            });

            modelBuilder.Entity<TaEmpReportScheduleDetail>(entity =>
            {
                entity.HasKey(e => e.ScheduleDetailsId)
                    .HasName("PK__TA_EmpRe__D034FDFF15FD2C4A");

                entity.ToTable("TA_EmpReportScheduleDetails");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TaEmpReportScheduleDetails)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_EmpReportScheduleDetails_Employee");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.TaEmpReportScheduleDetails)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_EmpReportScheduleDetails_EmpReportSchedule");
            });

            modelBuilder.Entity<TaEmpTraverseToTum>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TA_EmpTraverseToTA");
            });

            modelBuilder.Entity<TaEmployee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__TA_Emplo__7AD04F1153C0B95D");

                entity.ToTable("TA_Employee");

                entity.Property(e => e.AccessNo).HasMaxLength(20);

                entity.Property(e => e.BloodGroup).HasMaxLength(20);

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.Country).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Dob)
                    .HasMaxLength(50)
                    .HasColumnName("DOB");

                entity.Property(e => e.EmpAddress).HasMaxLength(200);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.JoinDate).HasColumnType("date");

                entity.Property(e => e.LastLogOn).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.LoginPassword).HasMaxLength(100);

                entity.Property(e => e.MailId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PhoneNo)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ResignationDate).HasMaxLength(50);

                entity.Property(e => e.Role).HasMaxLength(20);

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.Property(e => e.ZipCode).HasMaxLength(20);

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.TaEmployees)
                    .HasForeignKey(d => d.DeptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_Employee_DepartmentMaster");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.TaEmployees)
                    .HasForeignKey(d => d.DesignationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_Employee_DesignationMaster");

                entity.HasOne(d => d.EmpType)
                    .WithMany(p => p.TaEmployees)
                    .HasForeignKey(d => d.EmpTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_Employee_EmployeeTypeMaster");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.TaEmployees)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_Employee_LocationMaster");
            });

            modelBuilder.Entity<TaEmployeeAttnRegularize>(entity =>
            {
                entity.HasKey(e => e.EmpAttnRegularizeId)
                    .HasName("PK__TA_Emplo__109E9355EAB2B40C");

                entity.ToTable("TA_EmployeeAttnRegularize");

                entity.Property(e => e.AttnRegularizeDate).HasColumnType("date");

                entity.Property(e => e.AuthorizedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Comments).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.InTime)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.OutTime)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.HasOne(d => d.AttnRegularizeType)
                    .WithMany(p => p.TaEmployeeAttnRegularizes)
                    .HasForeignKey(d => d.AttnRegularizeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_EmployeeAttnRegularize_AttnRegularizeMaster");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TaEmployeeAttnRegularizes)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_EmployeeAttnRegularize_Employee");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TaEmployeeAttnRegularizes)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_EmployeeAttnRegularize_StatusMaster");
            });

            modelBuilder.Entity<TaEmployeeAudit>(entity =>
            {
                entity.HasKey(e => e.EmployeeAuditId)
                    .HasName("PK__TA_Emplo__611F099A86F2BCA1");

                entity.ToTable("TA_EmployeeAudit");

                entity.Property(e => e.AccessNo)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BloodGroup)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Country)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(((1)/(1))/(1990))");

                entity.Property(e => e.DeptId).HasDefaultValueSql("((0))");

                entity.Property(e => e.DesignationId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB")
                    .HasDefaultValueSql("(((1)/(1))/(1990))");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EmpAddress)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EmpTypeId).HasDefaultValueSql("((0))");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("('')");

                entity.Property(e => e.IsShiftRotate).HasDefaultValueSql("('')");

                entity.Property(e => e.JoinDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(((1)/(1))/(1990))");

                entity.Property(e => e.LastLogOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(((1)/(1))/(1990))");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LocationId).HasDefaultValueSql("((0))");

                entity.Property(e => e.LogInAttempt).HasDefaultValueSql("((0))");

                entity.Property(e => e.LoginPassword)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(((1)/(1))/(1990))");

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ReporteeId).HasDefaultValueSql("((0))");

                entity.Property(e => e.ResignationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(((1)/(1))/(1990))");

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<TaEmployeeLeave>(entity =>
            {
                entity.HasKey(e => e.EmpLeaveId)
                    .HasName("PK__TA_Emplo__ED09215FFC0A8B6F");

                entity.ToTable("TA_EmployeeLeave");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.AuthorizedBy).HasMaxLength(100);

                entity.Property(e => e.Comments).HasMaxLength(200);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.Property(e => e.ToDate).HasColumnType("date");
            });

            modelBuilder.Entity<TaEmployeeLeaveDetail>(entity =>
            {
                entity.HasKey(e => e.LeaveDetailsId)
                    .HasName("PK__TA_Emplo__451C8BD0606C0BEB");

                entity.ToTable("TA_EmployeeLeaveDetails");

                entity.Property(e => e.Comments).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.IsHalfDay).HasComment("0 - Full Day, 1- First Half, 2- Second Half");

                entity.Property(e => e.LeaveDate).HasColumnType("date");

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.HasOne(d => d.EmpLeave)
                    .WithMany(p => p.TaEmployeeLeaveDetails)
                    .HasForeignKey(d => d.EmpLeaveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_EmployeeLeaveDetails_TA_EmployeeLeave");
            });

            modelBuilder.Entity<TaEmployeeOutDuty>(entity =>
            {
                entity.HasKey(e => e.EmpOutDutyId)
                    .HasName("PK__TA_Emplo__FC4C4B2325800E5A");

                entity.ToTable("TA_EmployeeOutDuty");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasComment("''");

                entity.Property(e => e.AuthorizedBy).HasComment("0");

                entity.Property(e => e.Comments).HasMaxLength(200);

                entity.Property(e => e.CreatedBy).HasComment("0");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasComment("01/01/1900");

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.ModifiedBy).HasComment("0");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("date")
                    .HasComment("01/01/1900");

                entity.Property(e => e.Reason)
                    .HasMaxLength(100)
                    .HasComment("''");

                entity.Property(e => e.ToDate).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TaEmployeeOutDuties)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_EmployeeOutDuty_Employee");

                entity.HasOne(d => d.OutDutyType)
                    .WithMany(p => p.TaEmployeeOutDuties)
                    .HasForeignKey(d => d.OutDutyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_EmployeeOutDuty_OutDutyTypeMaster");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TaEmployeeOutDuties)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_EmployeeOutDuty_StatusMaster");
            });

            modelBuilder.Entity<TaEmployeeOutDutyDetail>(entity =>
            {
                entity.HasKey(e => e.EmpOutDutyDetailsId)
                    .HasName("PK__TA_Emplo__F2F139098456CE8C");

                entity.ToTable("TA_EmployeeOutDutyDetails");

                entity.Property(e => e.Comments).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.IsHalfDay).HasComment("0- FullDay, 1- First Half, 2- Second Half");

                entity.Property(e => e.ModifiedDate).HasColumnType("date");

                entity.Property(e => e.OutDutyDate).HasColumnType("date");

                entity.HasOne(d => d.EmpOutDuty)
                    .WithMany(p => p.TaEmployeeOutDutyDetails)
                    .HasForeignKey(d => d.EmpOutDutyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_EmployeeOutDutyDetails_EmployeeOutDuty");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TaEmployeeOutDutyDetails)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_EmployeeOutDutyDetails_StatusMaster");
            });

            modelBuilder.Entity<TaEmployeeShift>(entity =>
            {
                entity.HasKey(e => e.EmployeeShiftId)
                    .HasName("PK__TA_Emplo__2FBBBA338DED6407");

                entity.ToTable("TA_EmployeeShift");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ToDate).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TaEmployeeShifts)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_EmployeeShift_Employee");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.TaEmployeeShifts)
                    .HasForeignKey(d => d.ShiftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_EmployeeShift_ShiftMaster");
            });

            modelBuilder.Entity<TaEmployeeTypeMaster>(entity =>
            {
                entity.HasKey(e => e.EmpTypeId)
                    .HasName("PK__TA_Emplo__B3AF1B2F650A6D7B");

                entity.ToTable("TA_EmployeeTypeMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmpTypeCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmpTypeDesc).HasMaxLength(100);
            });

            modelBuilder.Entity<TaGender>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TA_Gender");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GenderId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TaHolidayMaster>(entity =>
            {
                entity.HasKey(e => e.HolidayId)
                    .HasName("PK__TA_Holid__2D35D57A3A0BE776");

                entity.ToTable("TA_HolidayMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.HolidayDate).HasColumnType("date");

                entity.Property(e => e.HolidayDesc).HasMaxLength(50);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.TaHolidayMasters)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_TA_LocationMasterr_LocationMaster");
            });

            modelBuilder.Entity<TaLanguageSupport>(entity =>
            {
                entity.HasKey(e => e.LanguageId)
                    .HasName("PK__TA_Langu__B93855AB86980E15");

                entity.ToTable("TA_LanguageSupport");

                entity.Property(e => e.LanguageDescription)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Locale)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<TaLeaveMaster>(entity =>
            {
                entity.HasKey(e => e.LeaveId)
                    .HasName("PK__TA_Leave__796DB9594A8EF6EF");

                entity.ToTable("TA_LeaveMaster");

                entity.Property(e => e.LeaveCode).HasMaxLength(20);

                entity.Property(e => e.LeaveName).HasMaxLength(50);
            });

            modelBuilder.Entity<TaLeaveTypeMaster>(entity =>
            {
                entity.HasKey(e => e.LeaveTypeId)
                    .HasName("PK__TA_Leave__43BE8F1421182FE1");

                entity.ToTable("TA_LeaveTypeMaster");

                entity.Property(e => e.CreatedBy).HasComment("0");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("(((1)/(1))/(1900))");

                entity.Property(e => e.LeaveApplicable)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Leave)
                    .WithMany(p => p.TaLeaveTypeMasters)
                    .HasForeignKey(d => d.LeaveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_LeaveTypeMaster_LeaveMaster");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.TaLeaveTypeMasters)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_LeaveMaster_LocationMaster");
            });

            modelBuilder.Entity<TaLocationMaster>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__TA_Locat__E7FEA49784F85F85");

                entity.ToTable("TA_LocationMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LocationCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LocationName).HasMaxLength(100);
            });

            modelBuilder.Entity<TaLoginConfiguration>(entity =>
            {
                entity.HasKey(e => e.LoginConfigId)
                    .HasName("PK__TA_Login__5A8557058C68C53A");

                entity.ToTable("TA_LoginConfiguration");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DblockTime).HasColumnName("DBLockTime");
            });

            modelBuilder.Entity<TaOutDutyTypeMaster>(entity =>
            {
                entity.HasKey(e => e.OutDutyTypeId)
                    .HasName("PK__TA_OutDu__F7E46FA8870C5266");

                entity.ToTable("TA_OutDutyTypeMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.OutDutyCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OutDutyDesc).HasMaxLength(100);
            });

            modelBuilder.Entity<TaReportFormat>(entity =>
            {
                entity.HasKey(e => e.FormatId)
                    .HasName("PK__TA_Repor__5D3DCB59ADF95797");

                entity.ToTable("TA_ReportFormats");

                entity.Property(e => e.FormatName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TaReportQueue>(entity =>
            {
                entity.ToTable("TA_ReportQueue");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmailLastAttemptOn).HasColumnType("datetime");

                entity.Property(e => e.ReportLastAttemptOn).HasColumnType("datetime");

                entity.Property(e => e.ReportName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ReportPath).HasMaxLength(500);

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.ScheduleName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ScheduleTime).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TaReportQueues)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_TA_ReportQueue_TA_ReportQueue");
            });

            modelBuilder.Entity<TaResource>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TA_Resources");

                entity.Property(e => e.Culture)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(4000);
            });

            modelBuilder.Entity<TaRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TA_Role");

                entity.Property(e => e.Role).HasMaxLength(50);

                entity.Property(e => e.RoleId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TaScheduledReport>(entity =>
            {
                entity.HasKey(e => e.ReportId)
                    .HasName("PK__TA_Sched__D5BD4805199BB8E4");

                entity.ToTable("TA_ScheduledReports");

                //entity.HasIndex(e => e.ReportName, "UQ__TA_Sched__930D5CE713F34B70").IsUnique();

                entity.Property(e => e.ReportName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TaScheduledReportsFrequency>(entity =>
            {
                entity.HasKey(e => e.FrequencyId)
                    .HasName("PK__TA_Sched__59247498ADDB43DA");

                entity.ToTable("TA_ScheduledReportsFrequency");

                entity.Property(e => e.FrequencyName).HasMaxLength(20);
            });

            modelBuilder.Entity<TaShiftDetail>(entity =>
            {
                entity.HasKey(e => e.ShiftDetailsId)
                    .HasName("PK__TA_Shift__0D6A1573D96F1153");

                entity.ToTable("TA_ShiftDetails");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ShiftDay)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.TaShiftDetails)
                    .HasForeignKey(d => d.ShiftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_ShiftDetails_ShiftMaster");
            });

            modelBuilder.Entity<TaShiftMaster>(entity =>
            {
                entity.HasKey(e => e.ShiftId)
                    .HasName("PK__TA_Shift__C0A83881BF699F1C");

                entity.ToTable("TA_ShiftMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EarlyGo).HasMaxLength(50);

                entity.Property(e => e.Half1End).HasMaxLength(50);

                entity.Property(e => e.Half2Start).HasMaxLength(50);

                entity.Property(e => e.LateUpto).HasMaxLength(50);

                entity.Property(e => e.MinHrsFullDay).HasMaxLength(50);

                entity.Property(e => e.MinHrsHalfDay).HasMaxLength(50);

                entity.Property(e => e.ShiftEarly).HasMaxLength(50);

                entity.Property(e => e.ShiftEnd).HasMaxLength(50);

                entity.Property(e => e.ShiftInGrace).HasMaxLength(50);

                entity.Property(e => e.ShiftName).HasMaxLength(50);

                entity.Property(e => e.ShiftStart).HasMaxLength(50);

                entity.Property(e => e.WorkingHrs).HasMaxLength(50);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.TaShiftMasters)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_ShiftMaster_LocationMaster");
            });

            modelBuilder.Entity<TaShiftRotation>(entity =>
            {
                entity.HasKey(e => e.ShiftRotationId)
                    .HasName("PK__TA_Shift__98DF745F1F16135C");

                entity.ToTable("TA_ShiftRotation");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(((1)/(1))/(1900))");

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(((1)/(1))/(1900))");

                entity.Property(e => e.RotationName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.FromShiftNavigation)
                    .WithMany(p => p.TaShiftRotationFromShiftNavigations)
                    .HasForeignKey(d => d.FromShift)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_ShiftRotation_ShiftMaster");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.TaShiftRotations)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_ShiftRotation_LocationMaster1");

                entity.HasOne(d => d.ToShiftNavigation)
                    .WithMany(p => p.TaShiftRotationToShiftNavigations)
                    .HasForeignKey(d => d.ToShift)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_ShiftRotation_ShiftMaster1");
            });

            modelBuilder.Entity<TaSmtpSetting>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TA_SmtpSettings");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EnableSsl).HasColumnName("EnableSSL");

                entity.Property(e => e.FromAddress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HostName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<TaStatusMaster>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__TA_Statu__C8EE20632D6BD043");

                entity.ToTable("TA_StatusMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TaUpdateLeaveDetail>(entity =>
            {
                entity.HasKey(e => e.UpdateLeaveId)
                    .HasName("PK__TA_Updat__68217679ECCB65E6");

                entity.ToTable("TA_UpdateLeaveDetails");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TaUpdateLeaveDetails)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_UpdateLeaveDetails_TA_Employee");
            });

            modelBuilder.Entity<TaUserReportSchedule>(entity =>
            {
                entity.HasKey(e => e.ScheduleId)
                    .HasName("PK__TA_UserR__9C8A5B49F9750D6D");

                entity.ToTable("TA_UserReportSchedule");

                entity.Property(e => e.ScheduleName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ScheduleTime).HasColumnType("datetime");

                entity.HasOne(d => d.Format)
                    .WithMany(p => p.TaUserReportSchedules)
                    .HasForeignKey(d => d.FormatId)
                    .HasConstraintName("FK_TA_UserReportSchedule_ReportFormats");
            });

            modelBuilder.Entity<TaUserReportScheduleDetail>(entity =>
            {
                entity.HasKey(e => e.ScheduleDetailsId)
                    .HasName("PK__TA_UserR__D034FDFF56F20BB1");

                entity.ToTable("TA_UserReportScheduleDetails");

                entity.Property(e => e.NextScheduleDate).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TaUserReportScheduleDetails)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_UserReportScheduleDetails_Employee");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.TaUserReportScheduleDetails)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TA_UserReportScheduleDetails_UserReportSchedule");
            });

            modelBuilder.Entity<TimeZone>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_TimeZone_1__10");

                entity.ToTable("TimeZone");

                //entity.HasIndex(e => e.AccountId, "IX_TimeZone");

                //entity.HasIndex(e => e.HolidayGroupId, "IX_TimeZone_1");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<TimeZoneRange>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_TimeZoneRange_1__10");

                entity.ToTable("TimeZoneRange");

                //entity.HasIndex(e => e.AccountId, "IX_TimeZoneRange");

                //entity.HasIndex(e => e.TimeZoneId, "IX_TimeZoneRange_1");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

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

                entity.Property(e => e.SubAccountId).HasColumnName("SubAccountID");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<TrackingTree>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_TrackingTree_1__10");

                entity.ToTable("TrackingTree");

                //entity.HasIndex(e => e.AccountId, "IX_TrackingTree");

                //entity.HasIndex(e => e.ParentRecordId, "IX_TrackingTree_1");

                //entity.HasIndex(e => e.NodeLevel, "IX_TrackingTree_2");

                //entity.HasIndex(e => e.DeviceId, "IX_TrackingTree_3");

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

                entity.Property(e => e.SubAccountId)
                    .HasColumnName("SubAccountID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Trailer>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Trailer");

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
