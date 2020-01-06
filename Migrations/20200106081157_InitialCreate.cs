using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiVendorAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ruser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Facebook = table.Column<string>(nullable: true),
                    StripeKey = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    MasterId = table.Column<int>(nullable: false),
                    StokisId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Facebook = table.Column<string>(nullable: true),
                    StripeKey = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    MasterId = table.Column<int>(nullable: false),
                    StokisId = table.Column<int>(nullable: false),
                    UserToSendId = table.Column<int>(nullable: true),
                    UsersId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_Ruser_UserToSendId",
                        column: x => x.UserToSendId,
                        principalTable: "Ruser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_users_users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_UserToSendId",
                table: "users",
                column: "UserToSendId");

            migrationBuilder.CreateIndex(
                name: "IX_users_UsersId",
                table: "users",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "Ruser");
        }
    }
}
