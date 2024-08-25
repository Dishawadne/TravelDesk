using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelDesk.Migrations
{
    /// <inheritdoc />
    public partial class addedHr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TicketUrl",
                table: "TravelRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 25, 15, 40, 53, 945, DateTimeKind.Local).AddTicks(9259));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 25, 15, 40, 53, 945, DateTimeKind.Local).AddTicks(9262));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 25, 15, 40, 53, 945, DateTimeKind.Local).AddTicks(9264));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 25, 15, 40, 53, 945, DateTimeKind.Local).AddTicks(9266));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketUrl",
                table: "TravelRequests");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 25, 12, 36, 58, 179, DateTimeKind.Local).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 25, 12, 36, 58, 179, DateTimeKind.Local).AddTicks(5586));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 25, 12, 36, 58, 179, DateTimeKind.Local).AddTicks(5594));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 25, 12, 36, 58, 179, DateTimeKind.Local).AddTicks(5602));
        }
    }
}
