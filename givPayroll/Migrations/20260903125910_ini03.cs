using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EffectiveDate",
                schema: "ref",
                table: "SalaryItemRule",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                schema: "ref",
                table: "SalaryItem",
                columns: new[] { "Id", "AccAccountCode", "AccUniqueId", "CalculationMode", "DateChanged", "DateCreated", "FormulaValue", "Label", "PlusMinus", "Priority", "SalaryItemName", "Source", "Unit", "UserChanged", "UserCreated" },
                values: new object[] { 15, null, null, "Manual", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Penalty", -1, 111, "جریمه", "PayrollAdjustmentDetail", "Amount", null, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.AlterColumn<string>(
                name: "EffectiveDate",
                schema: "ref",
                table: "SalaryItemRule",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
