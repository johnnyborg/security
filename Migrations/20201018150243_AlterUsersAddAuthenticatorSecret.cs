using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Security.Migrations
{
    public partial class AlterUsersAddAuthenticatorSecret : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthenticatorSecret",
                table: "Users",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2020, 10, 18, 17, 2, 43, 322, DateTimeKind.Local).AddTicks(6496));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthenticatorSecret",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2020, 10, 15, 20, 41, 1, 234, DateTimeKind.Local).AddTicks(4458));
        }
    }
}
