using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini122 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayrollMonth",
                schema: "Payroll",
                table: "PayrollAdjustmentDetail");

            migrationBuilder.DropColumn(
                name: "PayrollYear",
                schema: "Payroll",
                table: "PayrollAdjustmentDetail");

            migrationBuilder.AddColumn<DateTime>(
                name: "PayrollAdjustmentDate",
                schema: "Payroll",
                table: "PayrollAdjustmentDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayrollAdjustmentDate",
                schema: "Payroll",
                table: "PayrollAdjustmentDetail");

            migrationBuilder.AddColumn<int>(
                name: "PayrollMonth",
                schema: "Payroll",
                table: "PayrollAdjustmentDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PayrollYear",
                schema: "Payroll",
                table: "PayrollAdjustmentDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
