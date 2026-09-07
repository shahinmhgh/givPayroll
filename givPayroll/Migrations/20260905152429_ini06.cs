using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonnelOrderDate",
                table: "PersonnelOrder",
                newName: "IssueDate");

            migrationBuilder.AddColumn<string>(
                name: "EndDate",
                table: "PersonnelOrder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StartDate",
                table: "PersonnelOrder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "PersonnelOrder");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "PersonnelOrder");

            migrationBuilder.RenameColumn(
                name: "IssueDate",
                table: "PersonnelOrder",
                newName: "PersonnelOrderDate");
        }
    }
}
