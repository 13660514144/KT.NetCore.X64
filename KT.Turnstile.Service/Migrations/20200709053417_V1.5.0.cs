using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Turnstile.Manage.Service.Migrations
{
    public partial class V150 : Migration
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
                name: "LOGIN_USER",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
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
                    CardType = table.Column<string>(nullable: true),
                    CardNumber = table.Column<string>(nullable: true),
                    PassLocalTime = table.Column<string>(nullable: true),
                    PassTime = table.Column<long>(nullable: false),
                    PassRightId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASS_RECORD", x => x.Id);
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
                    CardNumber = table.Column<string>(nullable: true),
                    TimeNow = table.Column<long>(nullable: false),
                    TimeOut = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASS_RIGHT", x => x.Id);
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
                    Remark = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    SyncDataTime = table.Column<long>(nullable: false),
                    HasDistributeError = table.Column<bool>(nullable: false),
                    ProcessorKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROCESSOR", x => x.Id);
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
                    Remark = table.Column<string>(nullable: true),
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
                name: "THIRD_SERVER",
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
                    PCPort = table.Column<int>(nullable: false),
                    ServerType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_THIRD_SERVER", x => x.Id);
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
                    PassRightId = table.Column<string>(nullable: true),
                    CardDeviceRightGroupId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP_CARD_DEVICE_RIGH~",
                        column: x => x.CardDeviceRightGroupId,
                        principalTable: "CARD_DEVICE_RIGHT_GROUP",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP_PASS_RIGHT_PassR~",
                        column: x => x.PassRightId,
                        principalTable: "PASS_RIGHT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    ProcessorId = table.Column<string>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_DISTRIBUTE_ERROR_PROCESSOR_ProcessorId",
                        column: x => x.ProcessorId,
                        principalTable: "PROCESSOR",
                        principalColumn: "Id");
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
                    Type = table.Column<string>(nullable: true),
                    CardType = table.Column<string>(nullable: true),
                    PortName = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    RelayDeviceOut = table.Column<int>(nullable: false),
                    ProcessorId = table.Column<string>(nullable: true),
                    RelayDeviceId = table.Column<string>(nullable: true),
                    SerialConfigId = table.Column<string>(nullable: true),
                    HandleElevatorDeviceId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARD_DEVICE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CARD_DEVICE_PROCESSOR_ProcessorId",
                        column: x => x.ProcessorId,
                        principalTable: "PROCESSOR",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE_CARD_DEVICE_RIG~",
                        column: x => x.CardDeviceRightGroupId,
                        principalTable: "CARD_DEVICE_RIGHT_GROUP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CARD_DEVICE_ProcessorId",
                table: "CARD_DEVICE",
                column: "ProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_CARD_DEVICE_RelayDeviceId",
                table: "CARD_DEVICE",
                column: "RelayDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_CARD_DEVICE_SerialConfigId",
                table: "CARD_DEVICE",
                column: "SerialConfigId");

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
                name: "IX_DISTRIBUTE_ERROR_ProcessorId",
                table: "DISTRIBUTE_ERROR",
                column: "ProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_PASS_RIGHT_CardNumber",
                table: "PASS_RIGHT",
                column: "CardNumber",
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
                name: "IX_PROCESSOR_ProcessorKey",
                table: "PROCESSOR",
                column: "ProcessorKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PROCESSOR_IpAddress_Port",
                table: "PROCESSOR",
                columns: new[] { "IpAddress", "Port" },
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
                name: "DISTRIBUTE_ERROR");

            migrationBuilder.DropTable(
                name: "LOGIN_USER");

            migrationBuilder.DropTable(
                name: "PASS_RECORD");

            migrationBuilder.DropTable(
                name: "PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP");

            migrationBuilder.DropTable(
                name: "SYSTEM_CONFIG");

            migrationBuilder.DropTable(
                name: "THIRD_SERVER");

            migrationBuilder.DropTable(
                name: "CARD_DEVICE");

            migrationBuilder.DropTable(
                name: "CARD_DEVICE_RIGHT_GROUP");

            migrationBuilder.DropTable(
                name: "PASS_RIGHT");

            migrationBuilder.DropTable(
                name: "PROCESSOR");

            migrationBuilder.DropTable(
                name: "RELAY_DEVICE");

            migrationBuilder.DropTable(
                name: "SERIAL_CONFIG");
        }
    }
}
