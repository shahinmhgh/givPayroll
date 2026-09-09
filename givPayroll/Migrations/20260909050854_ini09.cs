using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini09 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PayrollAdjustmentPersonnel_PayrollAdjustmentId",
                schema: "Payroll",
                table: "PayrollAdjustmentPersonnel");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PersonnelContract",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AttendanceDate",
                schema: "Payroll",
                table: "Attendance",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateIndex(
                name: "IX_PayrollAdjustmentPersonnel_PayrollAdjustmentId_PersonnelId",
                schema: "Payroll",
                table: "PayrollAdjustmentPersonnel",
                columns: new[] { "PayrollAdjustmentId", "PersonnelId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PayrollAdjustmentPersonnel_PayrollAdjustmentId_PersonnelId",
                schema: "Payroll",
                table: "PayrollAdjustmentPersonnel");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PersonnelContract");

            migrationBuilder.AlterColumn<string>(
                name: "AttendanceDate",
                schema: "Payroll",
                table: "Attendance",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollAdjustmentPersonnel_PayrollAdjustmentId",
                schema: "Payroll",
                table: "PayrollAdjustmentPersonnel",
                column: "PayrollAdjustmentId");
        }
    }
}
