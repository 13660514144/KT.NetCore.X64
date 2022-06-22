using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Quanta.Service.Migrations
{
    public partial class v15375 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PASS_RIGHT_DESTINATION_FLOOR_DETAIL");

            migrationBuilder.AddColumn<string>(
                name: "FloorId",
                table: "PASS_RIGHT_DESTINATION_FLOOR_FLOOR",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFront",
                table: "PASS_RIGHT_DESTINATION_FLOOR_FLOOR",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRear",
                table: "PASS_RIGHT_DESTINATION_FLOOR_FLOOR",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PASS_RIGHT_DESTINATION_FLOOR_FLOOR_FloorId",
                table: "PASS_RIGHT_DESTINATION_FLOOR_FLOOR",
                column: "FloorId");

            migrationBuilder.AddForeignKey(
                name: "FK_PASS_RIGHT_DESTINATION_FLOOR_FLOOR_FLOOR_FloorId",
                table: "PASS_RIGHT_DESTINATION_FLOOR_FLOOR",
                column: "FloorId",
                principalTable: "FLOOR",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PASS_RIGHT_DESTINATION_FLOOR_FLOOR_FLOOR_FloorId",
                table: "PASS_RIGHT_DESTINATION_FLOOR_FLOOR");

            migrationBuilder.DropIndex(
                name: "IX_PASS_RIGHT_DESTINATION_FLOOR_FLOOR_FloorId",
                table: "PASS_RIGHT_DESTINATION_FLOOR_FLOOR");

            migrationBuilder.DropColumn(
                name: "FloorId",
                table: "PASS_RIGHT_DESTINATION_FLOOR_FLOOR");

            migrationBuilder.DropColumn(
                name: "IsFront",
                table: "PASS_RIGHT_DESTINATION_FLOOR_FLOOR");

            migrationBuilder.DropColumn(
                name: "IsRear",
                table: "PASS_RIGHT_DESTINATION_FLOOR_FLOOR");

            migrationBuilder.CreateTable(
                name: "PASS_RIGHT_DESTINATION_FLOOR_DETAIL",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    CreatedTime = table.Column<long>(type: "bigint", nullable: false),
                    Creator = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    EditedTime = table.Column<long>(type: "bigint", nullable: false),
                    Editor = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    FloorId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    IsFront = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsRear = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PassRightDestinationFloorId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASS_RIGHT_DESTINATION_FLOOR_DETAIL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PASS_RIGHT_DESTINATION_FLOOR_DETAIL_FLOOR_FloorId",
                        column: x => x.FloorId,
                        principalTable: "FLOOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PASS_RIGHT_DESTINATION_FLOOR_DETAIL_PASS_RIGHT_DESTINATION_F~",
                        column: x => x.PassRightDestinationFloorId,
                        principalTable: "PASS_RIGHT_DESTINATION_FLOOR_FLOOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PASS_RIGHT_DESTINATION_FLOOR_DETAIL_FloorId",
                table: "PASS_RIGHT_DESTINATION_FLOOR_DETAIL",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_PASS_RIGHT_DESTINATION_FLOOR_DETAIL_PassRightDestinationFloo~",
                table: "PASS_RIGHT_DESTINATION_FLOOR_DETAIL",
                column: "PassRightDestinationFloorId");
        }
    }
}
