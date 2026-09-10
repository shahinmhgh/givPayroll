using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlusMinus",
                schema: "Payroll",
                table: "PayrollItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PayrollPersianMonth",
                schema: "Payroll",
                table: "PayrollAdjustmentDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PayrollPersianYear",
                schema: "Payroll",
                table: "PayrollAdjustmentDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlusMinus",
                schema: "Payroll",
                table: "PayrollItem");

            migrationBuilder.DropColumn(
                name: "PayrollPersianMonth",
                schema: "Payroll",
                table: "PayrollAdjustmentDetail");

            migrationBuilder.DropColumn(
                name: "PayrollPersianYear",
                schema: "Payroll",
                table: "PayrollAdjustmentDetail");
        }
    }
}
