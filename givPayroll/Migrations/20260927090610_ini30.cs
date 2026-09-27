using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInsuranceBase",
                schema: "Payroll",
                table: "PayrollAdjustment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxBase",
                schema: "Payroll",
                table: "PayrollAdjustment",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInsuranceBase",
                schema: "Payroll",
                table: "PayrollAdjustment");

            migrationBuilder.DropColumn(
                name: "IsTaxBase",
                schema: "Payroll",
                table: "PayrollAdjustment");
        }
    }
}
