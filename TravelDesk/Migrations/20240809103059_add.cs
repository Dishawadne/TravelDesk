using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelDesk.Migrations
{
    /// <inheritdoc />
    public partial class add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 9, 16, 0, 58, 718, DateTimeKind.Local).AddTicks(5297));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 9, 16, 0, 58, 718, DateTimeKind.Local).AddTicks(5304));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 9, 16, 0, 58, 718, DateTimeKind.Local).AddTicks(5306));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 9, 16, 0, 58, 718, DateTimeKind.Local).AddTicks(5307));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
