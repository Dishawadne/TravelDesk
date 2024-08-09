using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelDesk.Migrations
{
    /// <inheritdoc />
    public partial class Deptadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 9, 15, 53, 21, 279, DateTimeKind.Local).AddTicks(3598));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 9, 15, 53, 21, 279, DateTimeKind.Local).AddTicks(3601));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 9, 15, 53, 21, 279, DateTimeKind.Local).AddTicks(3603));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 9, 15, 53, 21, 279, DateTimeKind.Local).AddTicks(3604));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Department",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 7, 14, 53, 46, 437, DateTimeKind.Local).AddTicks(7196));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 7, 14, 53, 46, 437, DateTimeKind.Local).AddTicks(7199));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 7, 14, 53, 46, 437, DateTimeKind.Local).AddTicks(7201));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 7, 14, 53, 46, 437, DateTimeKind.Local).AddTicks(7202));
        }
    }
}
