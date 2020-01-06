using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiVendorAPI.Migrations
{
    public partial class editagentID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StokisId",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "AgentId",
                table: "users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "StokisId",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
