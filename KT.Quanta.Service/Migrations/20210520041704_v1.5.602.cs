using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Quanta.Service.Migrations
{
    public partial class v15602 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CARD_DEVICE_HANDLE_ELEVATOR_DEVICE_HandElevatorDeviceId",
                table: "CARD_DEVICE");

            migrationBuilder.DropForeignKey(
                name: "FK_HANDLE_ELEVATOR_DEVICE_PROCESSOR_ProcessorId",
                table: "HANDLE_ELEVATOR_DEVICE");

            migrationBuilder.DropForeignKey(
                name: "FK_PROCESSOR_FLOOR_FLOOR_FloorId",
                table: "PROCESSOR_FLOOR");

            migrationBuilder.DropTable(
                name: "ELEVATOR_GROUP_RELATION_FLOOR");

            migrationBuilder.DropIndex(
                name: "IX_CARD_DEVICE_HandElevatorDeviceId",
                table: "CARD_DEVICE");

            migrationBuilder.DropColumn(
                name: "RealFloorId",
                table: "FLOOR");

            migrationBuilder.DropColumn(
                name: "HandElevatorDeviceId",
                table: "CARD_DEVICE");

            migrationBuilder.AddColumn<string>(
                name: "PhysicsFloor",
                table: "FLOOR",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HandleElevatorDeviceId",
                table: "CARD_DEVICE",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ELEVATOR_GROUP_FLOOR",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    ElevatorGroupId = table.Column<string>(nullable: true),
                    FloorId = table.Column<string>(nullable: true),
                    RealFloorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ELEVATOR_GROUP_FLOOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ELEVATOR_GROUP_FLOOR_ELEVATOR_GROUP_ElevatorGroupId",
                        column: x => x.ElevatorGroupId,
                        principalTable: "ELEVATOR_GROUP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ELEVATOR_GROUP_FLOOR_FLOOR_FloorId",
                        column: x => x.FloorId,
                        principalTable: "FLOOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CARD_DEVICE_HandleElevatorDeviceId",
                table: "CARD_DEVICE",
                column: "HandleElevatorDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_ELEVATOR_GROUP_FLOOR_FloorId",
                table: "ELEVATOR_GROUP_FLOOR",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_ELEVATOR_GROUP_FLOOR_ElevatorGroupId_FloorId",
                table: "ELEVATOR_GROUP_FLOOR",
                columns: new[] { "ElevatorGroupId", "FloorId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CARD_DEVICE_HANDLE_ELEVATOR_DEVICE_HandleElevatorDeviceId",
                table: "CARD_DEVICE",
                column: "HandleElevatorDeviceId",
                principalTable: "HANDLE_ELEVATOR_DEVICE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HANDLE_ELEVATOR_DEVICE_PROCESSOR_ProcessorId",
                table: "HANDLE_ELEVATOR_DEVICE",
                column: "ProcessorId",
                principalTable: "PROCESSOR",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_PROCESSOR_FLOOR_FLOOR_FloorId",
                table: "PROCESSOR_FLOOR",
                column: "FloorId",
                principalTable: "FLOOR",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CARD_DEVICE_HANDLE_ELEVATOR_DEVICE_HandleElevatorDeviceId",
                table: "CARD_DEVICE");

            migrationBuilder.DropForeignKey(
                name: "FK_HANDLE_ELEVATOR_DEVICE_PROCESSOR_ProcessorId",
                table: "HANDLE_ELEVATOR_DEVICE");

            migrationBuilder.DropForeignKey(
                name: "FK_PROCESSOR_FLOOR_FLOOR_FloorId",
                table: "PROCESSOR_FLOOR");

            migrationBuilder.DropTable(
                name: "ELEVATOR_GROUP_FLOOR");

            migrationBuilder.DropIndex(
                name: "IX_CARD_DEVICE_HandleElevatorDeviceId",
                table: "CARD_DEVICE");

            migrationBuilder.DropColumn(
                name: "PhysicsFloor",
                table: "FLOOR");

            migrationBuilder.AddColumn<string>(
                name: "RealFloorId",
                table: "FLOOR",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HandleElevatorDeviceId",
                table: "CARD_DEVICE",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HandElevatorDeviceId",
                table: "CARD_DEVICE",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ELEVATOR_GROUP_RELATION_FLOOR",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    CreatedTime = table.Column<long>(type: "bigint", nullable: false),
                    Creator = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    EditedTime = table.Column<long>(type: "bigint", nullable: false),
                    Editor = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ElevatorGroupId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    FloorId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_CARD_DEVICE_HandElevatorDeviceId",
                table: "CARD_DEVICE",
                column: "HandElevatorDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_ELEVATOR_GROUP_RELATION_FLOOR_FloorId",
                table: "ELEVATOR_GROUP_RELATION_FLOOR",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_ELEVATOR_GROUP_RELATION_FLOOR_ElevatorGroupId_FloorId",
                table: "ELEVATOR_GROUP_RELATION_FLOOR",
                columns: new[] { "ElevatorGroupId", "FloorId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CARD_DEVICE_HANDLE_ELEVATOR_DEVICE_HandElevatorDeviceId",
                table: "CARD_DEVICE",
                column: "HandElevatorDeviceId",
                principalTable: "HANDLE_ELEVATOR_DEVICE",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HANDLE_ELEVATOR_DEVICE_PROCESSOR_ProcessorId",
                table: "HANDLE_ELEVATOR_DEVICE",
                column: "ProcessorId",
                principalTable: "PROCESSOR",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PROCESSOR_FLOOR_FLOOR_FloorId",
                table: "PROCESSOR_FLOOR",
                column: "FloorId",
                principalTable: "FLOOR",
                principalColumn: "Id");
        }
    }
}
