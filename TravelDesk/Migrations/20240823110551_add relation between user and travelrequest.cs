using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelDesk.Migrations
{
    /// <inheritdoc />
    public partial class addrelationbetweenuserandtravelrequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelRequests_Projects_ProjectId",
                table: "TravelRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelRequests_Users_UserId",
                table: "TravelRequests");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_TravelRequests_ProjectId",
                table: "TravelRequests");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "TravelRequests");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TravelRequests");

            //migrationBuilder.DropColumn(
            //    name: "ProjectId",
            //    table: "TravelRequests");

            migrationBuilder.RenameColumn(
                name: "ToLocation",
                table: "TravelRequests",
                newName: "ReasonForTravelling");

            //migrationBuilder.RenameColumn(
            //    name: "ReasonForTravel",
            //    table: "TravelRequests",
            //    newName: "ProjectName");

            migrationBuilder.RenameColumn(
                name: "FromLocation",
                table: "TravelRequests",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "TravelRequests",
                newName: "ManagerId");

            migrationBuilder.RenameColumn(
                name: "Comments",
                table: "TravelRequests",
                newName: "ModifiedBy");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "TravelRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AddharCard",
                table: "TravelRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 23, 16, 35, 49, 810, DateTimeKind.Local).AddTicks(7350));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 23, 16, 35, 49, 810, DateTimeKind.Local).AddTicks(7356));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 23, 16, 35, 49, 810, DateTimeKind.Local).AddTicks(7358));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 23, 16, 35, 49, 810, DateTimeKind.Local).AddTicks(7360));

            migrationBuilder.CreateIndex(
                name: "IX_TravelRequests_ManagerId",
                table: "TravelRequests",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelRequests_Users_ManagerId",
                table: "TravelRequests",
                column: "ManagerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelRequests_Users_UserId",
                table: "TravelRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelRequests_Users_ManagerId",
                table: "TravelRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelRequests_Users_UserId",
                table: "TravelRequests");

            migrationBuilder.DropIndex(
                name: "IX_TravelRequests_ManagerId",
                table: "TravelRequests");

            migrationBuilder.DropColumn(
                name: "AddharCard",
                table: "TravelRequests");

            migrationBuilder.RenameColumn(
                name: "ReasonForTravelling",
                table: "TravelRequests",
                newName: "ToLocation");

            migrationBuilder.RenameColumn(
                name: "ProjectName",
                table: "TravelRequests",
                newName: "ReasonForTravel");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "TravelRequests",
                newName: "Comments");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "TravelRequests",
                newName: "Department");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "TravelRequests",
                newName: "FromLocation");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TravelRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "TravelRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TravelRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "TravelRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "CreatedBy", "CreatedOn", "IsActive", "ModifiedBy", "ModifiedOn", "ProjectName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 8, 23, 11, 58, 47, 39, DateTimeKind.Local).AddTicks(3694), true, null, null, "Project Alpha" },
                    { 2, 1, new DateTime(2024, 8, 23, 11, 58, 47, 39, DateTimeKind.Local).AddTicks(3697), true, null, null, "Project Beta" },
                    { 3, 1, new DateTime(2024, 8, 23, 11, 58, 47, 39, DateTimeKind.Local).AddTicks(3699), true, null, null, "Project Gamma" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_TravelRequests_ProjectId",
                table: "TravelRequests",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelRequests_Projects_ProjectId",
                table: "TravelRequests",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelRequests_Users_UserId",
                table: "TravelRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
