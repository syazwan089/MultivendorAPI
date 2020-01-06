using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiVendorAPI.Migrations
{
    public partial class revert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_users_UsersId",
                table: "users");

            migrationBuilder.AlterColumn<int>(
                name: "UsersId",
                table: "users",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StokisId",
                table: "users",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_users_UsersId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "StokisId",
                table: "users");

            migrationBuilder.AlterColumn<int>(
                name: "UsersId",
                table: "users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_users_users_UsersId",
                table: "users",
                column: "UsersId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
