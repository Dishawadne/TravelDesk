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
            //migrationBuilder.UpdateData(
            //    table: "Projects",
            //    keyColumn: "ProjectId",
            //    keyValue: 1,
            //    column: "CreatedOn",
            //    value: new DateTime(2024, 8, 23, 11, 58, 47, 39, DateTimeKind.Local).AddTicks(3694));

            //migrationBuilder.UpdateData(
            //    table: "Projects",
            //    keyColumn: "ProjectId",
            //    keyValue: 2,
            //    column: "CreatedOn",
            //    value: new DateTime(2024, 8, 23, 11, 58, 47, 39, DateTimeKind.Local).AddTicks(3697));

            //migrationBuilder.UpdateData(
            //    table: "Projects",
            //    keyColumn: "ProjectId",
            //    keyValue: 3,
            //    column: "CreatedOn",
            //    value: new DateTime(2024, 8, 23, 11, 58, 47, 39, DateTimeKind.Local).AddTicks(3699));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 23, 11, 58, 47, 39, DateTimeKind.Local).AddTicks(3517));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 23, 11, 58, 47, 39, DateTimeKind.Local).AddTicks(3520));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 23, 11, 58, 47, 39, DateTimeKind.Local).AddTicks(3522));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 23, 11, 58, 47, 39, DateTimeKind.Local).AddTicks(3524));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.UpdateData(
            //    table: "Projects",
            //    keyColumn: "ProjectId",
            //    keyValue: 1,
            //    column: "CreatedOn",
            //    value: new DateTime(2024, 8, 23, 11, 48, 22, 738, DateTimeKind.Local).AddTicks(8964));

            //migrationBuilder.UpdateData(
            //    table: "Projects",
            //    keyColumn: "ProjectId",
            //    keyValue: 2,
            //    column: "CreatedOn",
            //    value: new DateTime(2024, 8, 23, 11, 48, 22, 738, DateTimeKind.Local).AddTicks(8968));

            //migrationBuilder.UpdateData(
            //    table: "Projects",
            //    keyColumn: "ProjectId",
            //    keyValue: 3,
            //    column: "CreatedOn",
            //    value: new DateTime(2024, 8, 23, 11, 48, 22, 738, DateTimeKind.Local).AddTicks(8972));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 23, 11, 48, 22, 738, DateTimeKind.Local).AddTicks(8562));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 23, 11, 48, 22, 738, DateTimeKind.Local).AddTicks(8575));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 23, 11, 48, 22, 738, DateTimeKind.Local).AddTicks(8583));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 23, 11, 48, 22, 738, DateTimeKind.Local).AddTicks(8590));
        }
    }
}
