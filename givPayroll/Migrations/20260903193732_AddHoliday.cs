using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class AddHoliday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.CreateTable(
                name: "Holiday",
                schema: "ref",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsOfficial = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holiday", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Holiday",
                schema: "ref");

            migrationBuilder.InsertData(
                schema: "ref",
                table: "SalaryItem",
                columns: new[] { "Id", "AccAccountCode", "AccUniqueId", "CalculationMode", "DateChanged", "DateCreated", "FormulaValue", "Label", "PlusMinus", "Priority", "SalaryItemName", "Source", "Unit", "UserChanged", "UserCreated" },
                values: new object[] { 11, null, null, "Manual", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "LoanInstallment", -1, 140, "قسط وام", "PersonnelInstallment", "Amount", null, 0 });
        }
    }
}
