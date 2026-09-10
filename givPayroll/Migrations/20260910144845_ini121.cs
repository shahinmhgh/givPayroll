using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini121 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PersonnelOrderDetail",
                newName: "PersonnelOrderDetail",
                newSchema: "Payroll");

            migrationBuilder.RenameTable(
                name: "PersonnelOrder",
                newName: "PersonnelOrder",
                newSchema: "Payroll");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PersonnelOrderDetail",
                schema: "Payroll",
                newName: "PersonnelOrderDetail");

            migrationBuilder.RenameTable(
                name: "PersonnelOrder",
                schema: "Payroll",
                newName: "PersonnelOrder");
        }
    }
}
