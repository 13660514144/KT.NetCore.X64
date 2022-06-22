using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Quanta.Service.Migrations
{
    public partial class v15376 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DopMaskRecords",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    ElevatorServer = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Operate = table.Column<string>(nullable: true),
                    IsSucess = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    SendData = table.Column<string>(nullable: true),
                    ReceiveData = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DopMaskRecords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DopMaskRecords");
        }
    }
}
