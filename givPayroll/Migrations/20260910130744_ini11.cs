using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 5,
                column: "Source",
                value: "PayrollAdjustment");

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 15,
                column: "Source",
                value: "PayrollAdjustment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 5,
                column: "Source",
                value: "BonusMonthTable");

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 15,
                column: "Source",
                value: "PayrollAdjustmentDetail");
        }
    }
}
