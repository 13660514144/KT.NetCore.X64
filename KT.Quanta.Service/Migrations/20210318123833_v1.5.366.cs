using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Quanta.Service.Migrations
{
    public partial class v15366 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PASS_RIGHT_FLOOR_DIRECTION");

            migrationBuilder.CreateTable(
                name: "PASS_RIGHT_AUXILIARY",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    HandleDeviceId = table.Column<string>(nullable: true),
                    Sign = table.Column<string>(nullable: true),
                    FloorId = table.Column<string>(nullable: true),
                    IsDestinationFloor = table.Column<bool>(nullable: false),
                    Direction = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASS_RIGHT_AUXILIARY", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PASS_RIGHT_AUXILIARY");

            migrationBuilder.CreateTable(
                name: "PASS_RIGHT_FLOOR_DIRECTION",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    CreatedTime = table.Column<long>(type: "bigint", nullable: false),
                    Creator = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Direction = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    EditedTime = table.Column<long>(type: "bigint", nullable: false),
                    Editor = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    FloorId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Sign = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASS_RIGHT_FLOOR_DIRECTION", x => x.Id);
                });
        }
    }
}
