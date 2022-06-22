using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Quanta.Service.Migrations
{
    public partial class v15374 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PASS_RIGHT_AUXILIARY_DETAIL");

            migrationBuilder.DropTable(
                name: "PASS_RIGHT_AUXILIARY");

            migrationBuilder.CreateTable(
                name: "PASS_RIGHT_ACCESSIBLE_FLOOR",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Sign = table.Column<string>(nullable: true),
                    ElevatorGroupId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASS_RIGHT_ACCESSIBLE_FLOOR", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PASS_RIGHT_DESTINATION_FLOOR_FLOOR",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Sign = table.Column<string>(nullable: true),
                    ElevatorGroupId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASS_RIGHT_DESTINATION_FLOOR_FLOOR", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PASS_RIGHT_ACCESSIBLE_FLOOR_DETAIL",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    FloorId = table.Column<string>(nullable: true),
                    IsFront = table.Column<bool>(nullable: false),
                    IsRear = table.Column<bool>(nullable: false),
                    PassRightAccessibleFloorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASS_RIGHT_ACCESSIBLE_FLOOR_DETAIL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PASS_RIGHT_ACCESSIBLE_FLOOR_DETAIL_FLOOR_FloorId",
                        column: x => x.FloorId,
                        principalTable: "FLOOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PASS_RIGHT_ACCESSIBLE_FLOOR_DETAIL_PASS_RIGHT_ACCESSIBLE_FLO~",
                        column: x => x.PassRightAccessibleFloorId,
                        principalTable: "PASS_RIGHT_ACCESSIBLE_FLOOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PASS_RIGHT_DESTINATION_FLOOR_DETAIL",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    FloorId = table.Column<string>(nullable: true),
                    IsFront = table.Column<bool>(nullable: false),
                    IsRear = table.Column<bool>(nullable: false),
                    PassRightDestinationFloorId = table.Column<string>(nullable: true)
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
                name: "IX_PASS_RIGHT_ACCESSIBLE_FLOOR_DETAIL_FloorId",
                table: "PASS_RIGHT_ACCESSIBLE_FLOOR_DETAIL",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_PASS_RIGHT_ACCESSIBLE_FLOOR_DETAIL_PassRightAccessibleFloorId",
                table: "PASS_RIGHT_ACCESSIBLE_FLOOR_DETAIL",
                column: "PassRightAccessibleFloorId");

            migrationBuilder.CreateIndex(
                name: "IX_PASS_RIGHT_DESTINATION_FLOOR_DETAIL_FloorId",
                table: "PASS_RIGHT_DESTINATION_FLOOR_DETAIL",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_PASS_RIGHT_DESTINATION_FLOOR_DETAIL_PassRightDestinationFloo~",
                table: "PASS_RIGHT_DESTINATION_FLOOR_DETAIL",
                column: "PassRightDestinationFloorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PASS_RIGHT_ACCESSIBLE_FLOOR_DETAIL");

            migrationBuilder.DropTable(
                name: "PASS_RIGHT_DESTINATION_FLOOR_DETAIL");

            migrationBuilder.DropTable(
                name: "PASS_RIGHT_ACCESSIBLE_FLOOR");

            migrationBuilder.DropTable(
                name: "PASS_RIGHT_DESTINATION_FLOOR_FLOOR");

            migrationBuilder.CreateTable(
                name: "PASS_RIGHT_AUXILIARY",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    CreatedTime = table.Column<long>(type: "bigint", nullable: false),
                    Creator = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    EditedTime = table.Column<long>(type: "bigint", nullable: false),
                    Editor = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ElevatorGroupId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Sign = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASS_RIGHT_AUXILIARY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PASS_RIGHT_AUXILIARY_DETAIL",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    CreatedTime = table.Column<long>(type: "bigint", nullable: false),
                    Creator = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    EditedTime = table.Column<long>(type: "bigint", nullable: false),
                    Editor = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    FloorId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    IsDestinationFloor = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsFront = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsRear = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PassRightAuxiliaryId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true)
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
                name: "IX_PASS_RIGHT_AUXILIARY_DETAIL_PassRightAuxiliaryId",
                table: "PASS_RIGHT_AUXILIARY_DETAIL",
                column: "PassRightAuxiliaryId");
        }
    }
}
