using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class NoissueData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssueDate",
                schema: "Payroll",
                table: "Personnel");

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueDate",
                schema: "Payroll",
                table: "Payroll",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssueDate",
                schema: "Payroll",
                table: "Payroll");

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueDate",
                schema: "Payroll",
                table: "Personnel",
                type: "datetime2",
                nullable: true);
        }
    }
}
