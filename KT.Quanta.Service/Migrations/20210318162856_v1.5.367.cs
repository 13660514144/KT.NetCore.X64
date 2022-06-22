using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Quanta.Service.Migrations
{
    public partial class v15367 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direction",
                table: "PASS_RIGHT_AUXILIARY");

            migrationBuilder.DropColumn(
                name: "FloorId",
                table: "PASS_RIGHT_AUXILIARY");

            migrationBuilder.DropColumn(
                name: "HandleDeviceId",
                table: "PASS_RIGHT_AUXILIARY");

            migrationBuilder.DropColumn(
                name: "IsDestinationFloor",
                table: "PASS_RIGHT_AUXILIARY");

            migrationBuilder.AddColumn<string>(
                name: "HandleElevatorDeviceId",
                table: "PASS_RIGHT_AUXILIARY",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HANDLE_ELEVATOR_DEVICE_AUXILIARY",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    HandleElevatorDeviceId = table.Column<string>(nullable: true),
                    Direction = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HANDLE_ELEVATOR_DEVICE_AUXILIARY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PASS_RIGHT_AUXILIARY_DETAIL",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    FloorId = table.Column<string>(nullable: true),
                    IsDestinationFloor = table.Column<bool>(nullable: false),
                    Direction = table.Column<string>(nullable: true),
                    PassRightAuxiliaryId = table.Column<string>(nullable: true),
                    PassRightAuxiliaryEntityId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASS_RIGHT_AUXILIARY_DETAIL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PASS_RIGHT_AUXILIARY_DETAIL_FLOOR_FloorId",
                        column: x => x.FloorId,
                        principalTable: "FLOOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PASS_RIGHT_AUXILIARY_DETAIL_PASS_RIGHT_AUXILIARY_PassRightAu~",
                        column: x => x.PassRightAuxiliaryEntityId,
                        principalTable: "PASS_RIGHT_AUXILIARY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PASS_RIGHT_AUXILIARY_DETAIL_PASS_RIGHT_AUXILIARY_PassRightA~1",
                        column: x => x.PassRightAuxiliaryId,
                        principalTable: "PASS_RIGHT_AUXILIARY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PASS_RIGHT_AUXILIARY_DETAIL_FloorId",
                table: "PASS_RIGHT_AUXILIARY_DETAIL",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_PASS_RIGHT_AUXILIARY_DETAIL_PassRightAuxiliaryEntityId",
                table: "PASS_RIGHT_AUXILIARY_DETAIL",
                column: "PassRightAuxiliaryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PASS_RIGHT_AUXILIARY_DETAIL_PassRightAuxiliaryId",
                table: "PASS_RIGHT_AUXILIARY_DETAIL",
                column: "PassRightAuxiliaryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HANDLE_ELEVATOR_DEVICE_AUXILIARY");

            migrationBuilder.DropTable(
                name: "PASS_RIGHT_AUXILIARY_DETAIL");

            migrationBuilder.DropColumn(
                name: "HandleElevatorDeviceId",
                table: "PASS_RIGHT_AUXILIARY");

            migrationBuilder.AddColumn<string>(
                name: "Direction",
                table: "PASS_RIGHT_AUXILIARY",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FloorId",
                table: "PASS_RIGHT_AUXILIARY",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HandleDeviceId",
                table: "PASS_RIGHT_AUXILIARY",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDestinationFloor",
                table: "PASS_RIGHT_AUXILIARY",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
