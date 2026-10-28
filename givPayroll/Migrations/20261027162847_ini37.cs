using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini37 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectId",
                schema: "ref",
                table: "SalaryItem",
                newName: "ProjectId1");

            migrationBuilder.RenameColumn(
                name: "PersonID",
                schema: "ref",
                table: "SalaryItem",
                newName: "PersonID1");

            migrationBuilder.RenameColumn(
                name: "PersonCategoryCode",
                schema: "ref",
                table: "SalaryItem",
                newName: "PersonCategoryCode1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectId1",
                schema: "ref",
                table: "SalaryItem",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "PersonID1",
                schema: "ref",
                table: "SalaryItem",
                newName: "PersonID");

            migrationBuilder.RenameColumn(
                name: "PersonCategoryCode1",
                schema: "ref",
                table: "SalaryItem",
                newName: "PersonCategoryCode");
        }
    }
}
