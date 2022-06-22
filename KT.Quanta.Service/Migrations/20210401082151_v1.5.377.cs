using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Quanta.Service.Migrations
{
    public partial class v15377 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DopMaskRecords",
                table: "DopMaskRecords");

            migrationBuilder.DropColumn(
                name: "IsSucess",
                table: "DopMaskRecords");

            migrationBuilder.RenameTable(
                name: "DopMaskRecords",
                newName: "DOP_MASK_RECORD");

            migrationBuilder.AddColumn<bool>(
                name: "IsSuccess",
                table: "DOP_MASK_RECORD",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOP_MASK_RECORD",
                table: "DOP_MASK_RECORD",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DOP_MASK_RECORD",
                table: "DOP_MASK_RECORD");

            migrationBuilder.DropColumn(
                name: "IsSuccess",
                table: "DOP_MASK_RECORD");

            migrationBuilder.RenameTable(
                name: "DOP_MASK_RECORD",
                newName: "DopMaskRecords");

            migrationBuilder.AddColumn<bool>(
                name: "IsSucess",
                table: "DopMaskRecords",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DopMaskRecords",
                table: "DopMaskRecords",
                column: "Id");
        }
    }
}
