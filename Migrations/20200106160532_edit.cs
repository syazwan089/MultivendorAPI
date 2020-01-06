using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiVendorAPI.Migrations
{
    public partial class edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_users_UsersId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "users");

            migrationBuilder.AlterColumn<int>(
                name: "UsersId",
                table: "users",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_users_users_UsersId",
                table: "users",
                column: "UsersId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_users_UsersId",
                table: "users");

            migrationBuilder.AlterColumn<int>(
                name: "UsersId",
                table: "users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AgentId",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_users_users_UsersId",
                table: "users",
                column: "UsersId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
