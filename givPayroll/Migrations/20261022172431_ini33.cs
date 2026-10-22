using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonCategoryCode",
                schema: "Payroll",
                table: "PayrollAdjustment",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonID",
                schema: "Payroll",
                table: "PayrollAdjustment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                schema: "Payroll",
                table: "PayrollAdjustment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonCategoryCode",
                schema: "Payroll",
                table: "PayrollAdjustment");

            migrationBuilder.DropColumn(
                name: "PersonID",
                schema: "Payroll",
                table: "PayrollAdjustment");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                schema: "Payroll",
                table: "PayrollAdjustment");
        }
    }
}
