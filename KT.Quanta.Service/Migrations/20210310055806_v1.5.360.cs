using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Quanta.Service.Migrations
{
    public partial class v15360 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CARD_DEVICE_RIGHT_GROUP",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARD_DEVICE_RIGHT_GROUP", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommunicateInfos",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    IpAddress = table.Column<string>(nullable: false),
                    Port = table.Column<int>(nullable: false),
                    Account = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicateInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DISTRIBUTE_ERROR",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    DeviceId = table.Column<string>(nullable: true),
                    PartUrl = table.Column<string>(nullable: true),
                    DataModelName = table.Column<string>(nullable: true),
                    DataId = table.Column<string>(nullable: true),
                    ErrorMessage = table.Column<string>(nullable: true),
                    ErrorTimes = table.Column<int>(nullable: false),
                    DataContent = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DISTRIBUTE_ERROR", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EDIFICE",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EDIFICE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FACE_INFO",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    SourceUrl = table.Column<string>(nullable: true),
                    FaceUrl = table.Column<string>(nullable: true),
                    Feature = table.Column<byte[]>(nullable: true),
                    FeatureSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FACE_INFO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LOGIN_USER",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    DBAddr = table.Column<string>(nullable: true),
                    DBName = table.Column<string>(nullable: true),
                    DBUser = table.Column<string>(nullable: true),
                    DBPassword = table.Column<string>(nullable: true),
                    PCAddr = table.Column<string>(nullable: true),
                    PCUser = table.Column<string>(nullable: true),
                    PCPassword = table.Column<string>(nullable: true),
                    ServerAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOGIN_USER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PASS_RECORD",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    DeviceId = table.Column<string>(nullable: true),
                    DeviceType = table.Column<string>(nullable: true),
                    AccessType = table.Column<string>(nullable: true),
                    PassRightSign = table.Column<string>(nullable: true),
                    PassLocalTime = table.Column<string>(nullable: true),
                    PassTime = table.Column<long>(nullable: false),
                    PassRightId = table.Column<string>(nullable: true),
                    FaceImage = table.Column<byte[]>(nullable: true),
                    FaceImageSize = table.Column<long>(nullable: false),
                    Extra = table.Column<string>(nullable: true),
                    WayType = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Temperature = table.Column<decimal>(nullable: true),
                    IsMask = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASS_RECORD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PASS_RIGHT_FLOOR_DIRECTION",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Sign = table.Column<string>(nullable: true),
                    FloorId = table.Column<string>(nullable: true),
                    Direction = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASS_RIGHT_FLOOR_DIRECTION", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PERSON",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSON", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RELAY_DEVICE",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    CommunicateType = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RELAY_DEVICE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SERIAL_CONFIG",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Baudrate = table.Column<int>(nullable: false),
                    Databits = table.Column<int>(nullable: false),
                    Stopbits = table.Column<int>(nullable: false),
                    Parity = table.Column<int>(nullable: false),
                    ReadTimeout = table.Column<int>(nullable: false),
                    Encoding = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SERIAL_CONFIG", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SYSTEM_CONFIG",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSTEM_CONFIG", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ELEVATOR_GROUP",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    BrandModel = table.Column<string>(nullable: true),
                    Version = table.Column<string>(nullable: true),
                    EdificeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ELEVATOR_GROUP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ELEVATOR_GROUP_EDIFICE_EdificeId",
                        column: x => x.EdificeId,
                        principalTable: "EDIFICE",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FLOOR",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Direction = table.Column<string>(nullable: true),
                    RealFloorId = table.Column<string>(nullable: true),
                    IsPublic = table.Column<bool>(nullable: false),
                    EdificeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FLOOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FLOOR_EDIFICE_EdificeId",
                        column: x => x.EdificeId,
                        principalTable: "EDIFICE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ELEVATOR_INFO",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    RealId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ElevatorGroupId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ELEVATOR_INFO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ELEVATOR_INFO_ELEVATOR_GROUP_ElevatorGroupId",
                        column: x => x.ElevatorGroupId,
                        principalTable: "ELEVATOR_GROUP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ELEVATOR_SERVER",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    IpAddress = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    PCAccount = table.Column<string>(nullable: true),
                    PCPassword = table.Column<string>(nullable: true),
                    DBAccount = table.Column<string>(nullable: true),
                    DBPassword = table.Column<string>(nullable: true),
                    IsMain = table.Column<bool>(nullable: false),
                    ElevatorGroupId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ELEVATOR_SERVER", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ELEVATOR_SERVER_ELEVATOR_GROUP_ElevatorGroupId",
                        column: x => x.ElevatorGroupId,
                        principalTable: "ELEVATOR_GROUP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ELEVATOR_GROUP_RELATION_FLOOR",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    ElevatorGroupId = table.Column<string>(nullable: true),
                    FloorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ELEVATOR_GROUP_RELATION_FLOOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ELEVATOR_GROUP_RELATION_FLOOR_ELEVATOR_GROUP_ElevatorGroupId",
                        column: x => x.ElevatorGroupId,
                        principalTable: "ELEVATOR_GROUP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ELEVATOR_GROUP_RELATION_FLOOR_FLOOR_FloorId",
                        column: x => x.FloorId,
                        principalTable: "FLOOR",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PASS_RIGHT",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Sign = table.Column<string>(nullable: true),
                    AccessType = table.Column<string>(nullable: true),
                    RightType = table.Column<string>(nullable: true),
                    TimeNow = table.Column<long>(nullable: false),
                    TimeOut = table.Column<long>(nullable: false),
                    FloorId = table.Column<string>(nullable: true),
                    Direction = table.Column<string>(nullable: true),
                    PersonId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASS_RIGHT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PASS_RIGHT_FLOOR_FloorId",
                        column: x => x.FloorId,
                        principalTable: "FLOOR",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PASS_RIGHT_PERSON_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PERSON",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PROCESSOR",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    DeviceType = table.Column<string>(nullable: true),
                    BrandModel = table.Column<string>(nullable: true),
                    FloorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROCESSOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PROCESSOR_FLOOR_FloorId",
                        column: x => x.FloorId,
                        principalTable: "FLOOR",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    PassRightId = table.Column<string>(nullable: false),
                    CardDeviceRightGroupId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP_CARD_DEVICE_RIGH~",
                        column: x => x.CardDeviceRightGroupId,
                        principalTable: "CARD_DEVICE_RIGHT_GROUP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP_PASS_RIGHT_PassR~",
                        column: x => x.PassRightId,
                        principalTable: "PASS_RIGHT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PASS_RIGHT_RELATION_FLOOR",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    PassRightId = table.Column<string>(nullable: true),
                    FloorId = table.Column<string>(nullable: true),
                    Direction = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASS_RIGHT_RELATION_FLOOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PASS_RIGHT_RELATION_FLOOR_FLOOR_FloorId",
                        column: x => x.FloorId,
                        principalTable: "FLOOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PASS_RIGHT_RELATION_FLOOR_PASS_RIGHT_PassRightId",
                        column: x => x.PassRightId,
                        principalTable: "PASS_RIGHT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HANDLE_ELEVATOR_DEVICE",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DeviceId = table.Column<string>(nullable: true),
                    DeviceType = table.Column<string>(nullable: true),
                    BrandModel = table.Column<string>(nullable: true),
                    CommunicateType = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    FaceAppId = table.Column<string>(nullable: true),
                    FaceSdkKey = table.Column<string>(nullable: true),
                    FaceActivateCode = table.Column<string>(nullable: true),
                    FloorId = table.Column<string>(nullable: true),
                    ElevatorGroupId = table.Column<string>(nullable: true),
                    ProcessorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HANDLE_ELEVATOR_DEVICE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HANDLE_ELEVATOR_DEVICE_ELEVATOR_GROUP_ElevatorGroupId",
                        column: x => x.ElevatorGroupId,
                        principalTable: "ELEVATOR_GROUP",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HANDLE_ELEVATOR_DEVICE_FLOOR_FloorId",
                        column: x => x.FloorId,
                        principalTable: "FLOOR",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HANDLE_ELEVATOR_DEVICE_PROCESSOR_ProcessorId",
                        column: x => x.ProcessorId,
                        principalTable: "PROCESSOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PROCESSOR_FLOOR",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    SortId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProcessorId = table.Column<string>(nullable: true),
                    FloorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROCESSOR_FLOOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PROCESSOR_FLOOR_FLOOR_FloorId",
                        column: x => x.FloorId,
                        principalTable: "FLOOR",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PROCESSOR_FLOOR_PROCESSOR_ProcessorId",
                        column: x => x.ProcessorId,
                        principalTable: "PROCESSOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CARD_DEVICE",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    BrandModel = table.Column<string>(nullable: true),
                    CardDeviceType = table.Column<string>(nullable: true),
                    DeviceType = table.Column<string>(nullable: true),
                    PortName = table.Column<string>(nullable: true),
                    ProcessorId = table.Column<string>(nullable: true),
                    HandElevatorDeviceId = table.Column<string>(nullable: true),
                    SerialConfigId = table.Column<string>(nullable: true),
                    RelayDeviceId = table.Column<string>(nullable: true),
                    RelayDeviceOut = table.Column<int>(nullable: false),
                    HandleElevatorDeviceId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARD_DEVICE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CARD_DEVICE_HANDLE_ELEVATOR_DEVICE_HandElevatorDeviceId",
                        column: x => x.HandElevatorDeviceId,
                        principalTable: "HANDLE_ELEVATOR_DEVICE",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CARD_DEVICE_PROCESSOR_ProcessorId",
                        column: x => x.ProcessorId,
                        principalTable: "PROCESSOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CARD_DEVICE_RELAY_DEVICE_RelayDeviceId",
                        column: x => x.RelayDeviceId,
                        principalTable: "RELAY_DEVICE",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CARD_DEVICE_SERIAL_CONFIG_SerialConfigId",
                        column: x => x.SerialConfigId,
                        principalTable: "SERIAL_CONFIG",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HANDLE_ELEVATOR_INPUT_DEVICE",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AccessType = table.Column<string>(nullable: true),
                    DeviceType = table.Column<string>(nullable: true),
                    PortName = table.Column<string>(nullable: true),
                    HandElevatorDeviceId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HANDLE_ELEVATOR_INPUT_DEVICE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HANDLE_ELEVATOR_INPUT_DEVICE_HANDLE_ELEVATOR_DEVICE_HandElev~",
                        column: x => x.HandElevatorDeviceId,
                        principalTable: "HANDLE_ELEVATOR_DEVICE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    CardDeviceRightGroupId = table.Column<string>(nullable: true),
                    CardDeviceId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE_CARD_DEVICE_Car~",
                        column: x => x.CardDeviceId,
                        principalTable: "CARD_DEVICE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE_CARD_DEVICE_RIG~",
                        column: x => x.CardDeviceRightGroupId,
                        principalTable: "CARD_DEVICE_RIGHT_GROUP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CARD_DEVICE_HandElevatorDeviceId",
                table: "CARD_DEVICE",
                column: "HandElevatorDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_CARD_DEVICE_RelayDeviceId",
                table: "CARD_DEVICE",
                column: "RelayDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_CARD_DEVICE_SerialConfigId",
                table: "CARD_DEVICE",
                column: "SerialConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_CARD_DEVICE_ProcessorId_PortName",
                table: "CARD_DEVICE",
                columns: new[] { "ProcessorId", "PortName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE_CardDeviceRight~",
                table: "CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE",
                column: "CardDeviceRightGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE_CardDeviceId_Ca~",
                table: "CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE",
                columns: new[] { "CardDeviceId", "CardDeviceRightGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ELEVATOR_GROUP_EdificeId",
                table: "ELEVATOR_GROUP",
                column: "EdificeId");

            migrationBuilder.CreateIndex(
                name: "IX_ELEVATOR_GROUP_RELATION_FLOOR_FloorId",
                table: "ELEVATOR_GROUP_RELATION_FLOOR",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_ELEVATOR_GROUP_RELATION_FLOOR_ElevatorGroupId_FloorId",
                table: "ELEVATOR_GROUP_RELATION_FLOOR",
                columns: new[] { "ElevatorGroupId", "FloorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ELEVATOR_INFO_ElevatorGroupId",
                table: "ELEVATOR_INFO",
                column: "ElevatorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ELEVATOR_INFO_RealId_ElevatorGroupId",
                table: "ELEVATOR_INFO",
                columns: new[] { "RealId", "ElevatorGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ELEVATOR_SERVER_ElevatorGroupId",
                table: "ELEVATOR_SERVER",
                column: "ElevatorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FLOOR_EdificeId",
                table: "FLOOR",
                column: "EdificeId");

            migrationBuilder.CreateIndex(
                name: "IX_HANDLE_ELEVATOR_DEVICE_ElevatorGroupId",
                table: "HANDLE_ELEVATOR_DEVICE",
                column: "ElevatorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_HANDLE_ELEVATOR_DEVICE_FloorId",
                table: "HANDLE_ELEVATOR_DEVICE",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_HANDLE_ELEVATOR_DEVICE_ProcessorId",
                table: "HANDLE_ELEVATOR_DEVICE",
                column: "ProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_HANDLE_ELEVATOR_INPUT_DEVICE_HandElevatorDeviceId",
                table: "HANDLE_ELEVATOR_INPUT_DEVICE",
                column: "HandElevatorDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_PASS_RIGHT_FloorId",
                table: "PASS_RIGHT",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_PASS_RIGHT_PersonId",
                table: "PASS_RIGHT",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PASS_RIGHT_Sign_AccessType_RightType",
                table: "PASS_RIGHT",
                columns: new[] { "Sign", "AccessType", "RightType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP_CardDeviceRightG~",
                table: "PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP",
                column: "CardDeviceRightGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP_PassRightId_Card~",
                table: "PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP",
                columns: new[] { "PassRightId", "CardDeviceRightGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PASS_RIGHT_RELATION_FLOOR_FloorId",
                table: "PASS_RIGHT_RELATION_FLOOR",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_PASS_RIGHT_RELATION_FLOOR_PassRightId",
                table: "PASS_RIGHT_RELATION_FLOOR",
                column: "PassRightId");

            migrationBuilder.CreateIndex(
                name: "IX_PROCESSOR_FloorId",
                table: "PROCESSOR",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_PROCESSOR_IpAddress_Port_DeviceType",
                table: "PROCESSOR",
                columns: new[] { "IpAddress", "Port", "DeviceType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PROCESSOR_FLOOR_FloorId",
                table: "PROCESSOR_FLOOR",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_PROCESSOR_FLOOR_ProcessorId_FloorId",
                table: "PROCESSOR_FLOOR",
                columns: new[] { "ProcessorId", "FloorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PROCESSOR_FLOOR_ProcessorId_SortId",
                table: "PROCESSOR_FLOOR",
                columns: new[] { "ProcessorId", "SortId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RELAY_DEVICE_IpAddress_Port",
                table: "RELAY_DEVICE",
                columns: new[] { "IpAddress", "Port" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SYSTEM_CONFIG_Key",
                table: "SYSTEM_CONFIG",
                column: "Key",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE");

            migrationBuilder.DropTable(
                name: "CommunicateInfos");

            migrationBuilder.DropTable(
                name: "DISTRIBUTE_ERROR");

            migrationBuilder.DropTable(
                name: "ELEVATOR_GROUP_RELATION_FLOOR");

            migrationBuilder.DropTable(
                name: "ELEVATOR_INFO");

            migrationBuilder.DropTable(
                name: "ELEVATOR_SERVER");

            migrationBuilder.DropTable(
                name: "FACE_INFO");

            migrationBuilder.DropTable(
                name: "HANDLE_ELEVATOR_INPUT_DEVICE");

            migrationBuilder.DropTable(
                name: "LOGIN_USER");

            migrationBuilder.DropTable(
                name: "PASS_RECORD");

            migrationBuilder.DropTable(
                name: "PASS_RIGHT_FLOOR_DIRECTION");

            migrationBuilder.DropTable(
                name: "PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP");

            migrationBuilder.DropTable(
                name: "PASS_RIGHT_RELATION_FLOOR");

            migrationBuilder.DropTable(
                name: "PROCESSOR_FLOOR");

            migrationBuilder.DropTable(
                name: "SYSTEM_CONFIG");

            migrationBuilder.DropTable(
                name: "CARD_DEVICE");

            migrationBuilder.DropTable(
                name: "CARD_DEVICE_RIGHT_GROUP");

            migrationBuilder.DropTable(
                name: "PASS_RIGHT");

            migrationBuilder.DropTable(
                name: "HANDLE_ELEVATOR_DEVICE");

            migrationBuilder.DropTable(
                name: "RELAY_DEVICE");

            migrationBuilder.DropTable(
                name: "SERIAL_CONFIG");

            migrationBuilder.DropTable(
                name: "PERSON");

            migrationBuilder.DropTable(
                name: "ELEVATOR_GROUP");

            migrationBuilder.DropTable(
                name: "PROCESSOR");

            migrationBuilder.DropTable(
                name: "FLOOR");

            migrationBuilder.DropTable(
                name: "EDIFICE");
        }
    }
}
