using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class changeschema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PersonnelContract",
                newName: "PersonnelContract",
                newSchema: "Payroll");

            migrationBuilder.RenameTable(
                name: "Company",
                schema: "dbo",
                newName: "Company",
                newSchema: "ref");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "PersonnelContract",
                schema: "Payroll",
                newName: "PersonnelContract");

            migrationBuilder.RenameTable(
                name: "Company",
                schema: "ref",
                newName: "Company",
                newSchema: "dbo");
        }
    }
}
