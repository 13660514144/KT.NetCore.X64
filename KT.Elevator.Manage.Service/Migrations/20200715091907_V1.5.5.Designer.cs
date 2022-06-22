﻿// <auto-generated />
using System;
using KT.Elevator.Manage.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KT.Elevator.Manage.Service.Migrations
{
    [DbContext(typeof(ElevatorDbContext))]
    [Migration("20200715091907_V1.5.5")]
    partial class V155
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.CardDeviceEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DeviceKey")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DeviceType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("HandElevatorDeviceId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PortName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ProcessorId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProductType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("SerialConfigId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("HandElevatorDeviceId");

                    b.HasIndex("ProcessorId");

                    b.HasIndex("SerialConfigId");

                    b.ToTable("CARD_DEVICE");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.DistributeErrorEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DataContent")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DataId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DataModelName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DeviceKey")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("ErrorTimes")
                        .HasColumnType("int");

                    b.Property<string>("PartUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("DISTRIBUTE_ERROR");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.EdificeEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("EDIFICE");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.ElevatorGroupEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("EdificeId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ProductType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Version")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("EdificeId");

                    b.ToTable("ELEVATOR_GROUP");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.ElevatorGroupRelationFloorEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ElevatorGroupId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("FloorId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("FloorId");

                    b.HasIndex("ElevatorGroupId", "FloorId")
                        .IsUnique();

                    b.ToTable("ELEVATOR_GROUP_RELATION_FLOOR");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.ElevatorInfoEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RealId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("ELEVATOR_INFO");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.ElevatorServerEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DBAccount")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DBPassword")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ElevatorGroupId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("IpAddress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsMain")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PCAccount")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PCPassword")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ElevatorGroupId");

                    b.ToTable("ELEVATOR_SERVER");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.FaceInfoEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Extension")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FaceUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<byte[]>("Feature")
                        .HasColumnType("longblob");

                    b.Property<int>("FeatureSize")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("SourceUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("FACE_INFO");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.FloorEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("EdificeId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RealFloorId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("EdificeId");

                    b.ToTable("FLOOR");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.HandleElevatorDeviceEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("CommunicateType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DeviceId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DeviceKey")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DeviceType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ElevatorGroupId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("FaceActivateCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FaceAppId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FaceSdkKey")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FloorId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("IpAddress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<string>("ProductType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ElevatorGroupId");

                    b.HasIndex("FloorId");

                    b.ToTable("HANDLE_ELEVATOR_DEVICE");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.HandleElevatorInputDeviceEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("AccessType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DeviceType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("HandElevatorDeviceId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PortName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("HandElevatorDeviceId");

                    b.ToTable("HANDLE_ELEVATOR_INPUT_DEVICE");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.LoginUserEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DBAddr")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DBName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DBPassword")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DBUser")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PCAddr")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PCPassword")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PCUser")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ServerAddress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("LOGIN_USER");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.PassRecordEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("AccessType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DeviceId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DeviceType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Extra")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<byte[]>("FaceImage")
                        .HasColumnType("longblob");

                    b.Property<long>("FaceImageSize")
                        .HasColumnType("bigint");

                    b.Property<string>("PassLocalTime")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PassRightId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PassRightSign")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("PassTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Remark")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("WayType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("PASS_RECORD");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.PassRightEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("AccessType")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FloorId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("PersonId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Sign")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("TimeNow")
                        .HasColumnType("bigint");

                    b.Property<long>("TimeOut")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FloorId");

                    b.HasIndex("PersonId");

                    b.HasIndex("Sign", "AccessType")
                        .IsUnique();

                    b.ToTable("PASS_RIGHT");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.PassRightRelationFloorEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FloorId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("PassRightId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("FloorId");

                    b.HasIndex("PassRightId");

                    b.ToTable("PASS_RIGHT_RELATION_FLOOR");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.PersonEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Number")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("Number")
                        .IsUnique();

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.ProcessorEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FloorId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("IpAddress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<string>("ProcessorKey")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("FloorId");

                    b.ToTable("PROCESSOR");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.ProcessorFloorEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FloorId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ProcessorId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("SortId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FloorId");

                    b.HasIndex("ProcessorId", "FloorId")
                        .IsUnique();

                    b.HasIndex("ProcessorId", "SortId")
                        .IsUnique();

                    b.ToTable("PROCESSOR_FLOOR");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.SerialConfigEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("Baudrate")
                        .HasColumnType("int");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Databits")
                        .HasColumnType("int");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Encoding")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Parity")
                        .HasColumnType("int");

                    b.Property<int>("ReadTimeout")
                        .HasColumnType("int");

                    b.Property<int>("Stopbits")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SERIAL_CONFIG");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.SystemConfigEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("CreatedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Creator")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("EditedTime")
                        .HasColumnType("bigint");

                    b.Property<string>("Editor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Key")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("SYSTEM_CONFIG");
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.CardDeviceEntity", b =>
                {
                    b.HasOne("KT.Elevator.Manage.Service.Entities.HandleElevatorDeviceEntity", "HandElevatorDevice")
                        .WithMany()
                        .HasForeignKey("HandElevatorDeviceId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("KT.Elevator.Manage.Service.Entities.ProcessorEntity", "Processor")
                        .WithMany("CardDevices")
                        .HasForeignKey("ProcessorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KT.Elevator.Manage.Service.Entities.SerialConfigEntity", "SerialConfig")
                        .WithMany()
                        .HasForeignKey("SerialConfigId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.ElevatorGroupEntity", b =>
                {
                    b.HasOne("KT.Elevator.Manage.Service.Entities.EdificeEntity", "Edifice")
                        .WithMany()
                        .HasForeignKey("EdificeId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.ElevatorGroupRelationFloorEntity", b =>
                {
                    b.HasOne("KT.Elevator.Manage.Service.Entities.ElevatorGroupEntity", "ElevatorGroup")
                        .WithMany("RelationFloors")
                        .HasForeignKey("ElevatorGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KT.Elevator.Manage.Service.Entities.FloorEntity", "Floor")
                        .WithMany()
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.ElevatorServerEntity", b =>
                {
                    b.HasOne("KT.Elevator.Manage.Service.Entities.ElevatorGroupEntity", "ElevatorGroup")
                        .WithMany("ElevatorServers")
                        .HasForeignKey("ElevatorGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.FloorEntity", b =>
                {
                    b.HasOne("KT.Elevator.Manage.Service.Entities.EdificeEntity", "Edifice")
                        .WithMany("Floors")
                        .HasForeignKey("EdificeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.HandleElevatorDeviceEntity", b =>
                {
                    b.HasOne("KT.Elevator.Manage.Service.Entities.ElevatorGroupEntity", "ElevatorGroup")
                        .WithMany()
                        .HasForeignKey("ElevatorGroupId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("KT.Elevator.Manage.Service.Entities.FloorEntity", "Floor")
                        .WithMany()
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.HandleElevatorInputDeviceEntity", b =>
                {
                    b.HasOne("KT.Elevator.Manage.Service.Entities.HandleElevatorDeviceEntity", "HandElevatorDevice")
                        .WithMany()
                        .HasForeignKey("HandElevatorDeviceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.PassRightEntity", b =>
                {
                    b.HasOne("KT.Elevator.Manage.Service.Entities.FloorEntity", "Floor")
                        .WithMany()
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("KT.Elevator.Manage.Service.Entities.PersonEntity", "Person")
                        .WithMany("PassRights")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.PassRightRelationFloorEntity", b =>
                {
                    b.HasOne("KT.Elevator.Manage.Service.Entities.FloorEntity", "Floor")
                        .WithMany()
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KT.Elevator.Manage.Service.Entities.PassRightEntity", "PassRight")
                        .WithMany("RelationFloors")
                        .HasForeignKey("PassRightId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.ProcessorEntity", b =>
                {
                    b.HasOne("KT.Elevator.Manage.Service.Entities.FloorEntity", "Floor")
                        .WithMany()
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KT.Elevator.Manage.Service.Entities.ProcessorFloorEntity", b =>
                {
                    b.HasOne("KT.Elevator.Manage.Service.Entities.FloorEntity", "Floor")
                        .WithMany()
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("KT.Elevator.Manage.Service.Entities.ProcessorEntity", "Processor")
                        .WithMany("ProcessorFloors")
                        .HasForeignKey("ProcessorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
