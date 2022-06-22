using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Elevator.Manage.Service.Migrations
{
    public partial class V154 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DISTRIBUTE_ERROR",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    DeviceKey = table.Column<string>(nullable: true),
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
                name: "ELEVATOR_INFO",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    RealId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ELEVATOR_INFO", x => x.Id);
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
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASS_RECORD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
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
                    ProductType = table.Column<string>(nullable: true),
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
                name: "HANDLE_ELEVATOR_DEVICE",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    DeviceKey = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DeviceId = table.Column<string>(nullable: true),
                    ProductType = table.Column<string>(nullable: true),
                    DeviceType = table.Column<string>(nullable: true),
                    CommunicateType = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    FaceAppId = table.Column<string>(nullable: true),
                    FaceSdkKey = table.Column<string>(nullable: true),
                    FaceActivateCode = table.Column<string>(nullable: true),
                    FloorId = table.Column<string>(nullable: true),
                    ElevatorGroupId = table.Column<string>(nullable: true)
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
                    TimeNow = table.Column<long>(nullable: false),
                    TimeOut = table.Column<long>(nullable: false),
                    FloorId = table.Column<string>(nullable: true),
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
                        name: "FK_PASS_RIGHT_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    ProcessorKey = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    FloorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROCESSOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PROCESSOR_FLOOR_FloorId",
                        column: x => x.FloorId,
                        principalTable: "FLOOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "PASS_RIGHT_RELATION_FLOOR",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    PassRightId = table.Column<string>(nullable: true),
                    FloorId = table.Column<string>(nullable: true)
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
                name: "CARD_DEVICE",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProductType = table.Column<string>(nullable: true),
                    DeviceType = table.Column<string>(nullable: true),
                    PortName = table.Column<string>(nullable: true),
                    ProcessorId = table.Column<string>(nullable: true),
                    HandElevatorDeviceId = table.Column<string>(nullable: true),
                    SerialConfigId = table.Column<string>(nullable: true)
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CARD_DEVICE_SERIAL_CONFIG_SerialConfigId",
                        column: x => x.SerialConfigId,
                        principalTable: "SERIAL_CONFIG",
                        principalColumn: "Id");
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

            migrationBuilder.CreateIndex(
                name: "IX_CARD_DEVICE_HandElevatorDeviceId",
                table: "CARD_DEVICE",
                column: "HandElevatorDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_CARD_DEVICE_ProcessorId",
                table: "CARD_DEVICE",
                column: "ProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_CARD_DEVICE_SerialConfigId",
                table: "CARD_DEVICE",
                column: "SerialConfigId");

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
                name: "IX_PASS_RIGHT_Sign_AccessType",
                table: "PASS_RIGHT",
                columns: new[] { "Sign", "AccessType" },
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
                name: "IX_Persons_Number",
                table: "Persons",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PROCESSOR_FloorId",
                table: "PROCESSOR",
                column: "FloorId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CARD_DEVICE");

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
                name: "PASS_RIGHT_RELATION_FLOOR");

            migrationBuilder.DropTable(
                name: "PROCESSOR_FLOOR");

            migrationBuilder.DropTable(
                name: "SYSTEM_CONFIG");

            migrationBuilder.DropTable(
                name: "SERIAL_CONFIG");

            migrationBuilder.DropTable(
                name: "HANDLE_ELEVATOR_DEVICE");

            migrationBuilder.DropTable(
                name: "PASS_RIGHT");

            migrationBuilder.DropTable(
                name: "PROCESSOR");

            migrationBuilder.DropTable(
                name: "ELEVATOR_GROUP");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "FLOOR");

            migrationBuilder.DropTable(
                name: "EDIFICE");
        }
    }
}
