using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Quanta.Service.Migrations
{
    public partial class v15373 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PASS_RIGHT_AUXILIARY_DETAIL_PASS_RIGHT_AUXILIARY_PassRightAu~",
                table: "PASS_RIGHT_AUXILIARY_DETAIL");

            migrationBuilder.DropForeignKey(
                name: "FK_PASS_RIGHT_AUXILIARY_DETAIL_PASS_RIGHT_AUXILIARY_PassRightA~1",
                table: "PASS_RIGHT_AUXILIARY_DETAIL");

            migrationBuilder.DropIndex(
                name: "IX_PASS_RIGHT_AUXILIARY_DETAIL_PassRightAuxiliaryEntityId",
                table: "PASS_RIGHT_AUXILIARY_DETAIL");

            migrationBuilder.DropColumn(
                name: "PassRightAuxiliaryEntityId",
                table: "PASS_RIGHT_AUXILIARY_DETAIL");

            migrationBuilder.AddForeignKey(
                name: "FK_PASS_RIGHT_AUXILIARY_DETAIL_PASS_RIGHT_AUXILIARY_PassRightAu~",
                table: "PASS_RIGHT_AUXILIARY_DETAIL",
                column: "PassRightAuxiliaryId",
                principalTable: "PASS_RIGHT_AUXILIARY",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PASS_RIGHT_AUXILIARY_DETAIL_PASS_RIGHT_AUXILIARY_PassRightAu~",
                table: "PASS_RIGHT_AUXILIARY_DETAIL");

            migrationBuilder.AddColumn<string>(
                name: "PassRightAuxiliaryEntityId",
                table: "PASS_RIGHT_AUXILIARY_DETAIL",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PASS_RIGHT_AUXILIARY_DETAIL_PassRightAuxiliaryEntityId",
                table: "PASS_RIGHT_AUXILIARY_DETAIL",
                column: "PassRightAuxiliaryEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PASS_RIGHT_AUXILIARY_DETAIL_PASS_RIGHT_AUXILIARY_PassRightAu~",
                table: "PASS_RIGHT_AUXILIARY_DETAIL",
                column: "PassRightAuxiliaryEntityId",
                principalTable: "PASS_RIGHT_AUXILIARY",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PASS_RIGHT_AUXILIARY_DETAIL_PASS_RIGHT_AUXILIARY_PassRightA~1",
                table: "PASS_RIGHT_AUXILIARY_DETAIL",
                column: "PassRightAuxiliaryId",
                principalTable: "PASS_RIGHT_AUXILIARY",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
