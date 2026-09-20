using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsSystem",
                schema: "ref",
                table: "SalaryItem",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsSystem",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsSystem",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsSystem",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsSystem",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IsSystem", "SalaryItemName" },
                values: new object[] { 0, "تعدیلات افزایش" });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsSystem",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 7,
                column: "IsSystem",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsSystem",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 9,
                column: "IsSystem",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 10,
                column: "IsSystem",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 12,
                column: "IsSystem",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 13,
                column: "IsSystem",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 14,
                column: "IsSystem",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "IsSystem", "SalaryItemName" },
                values: new object[] { 0, "تعدیلات کاهشی" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSystem",
                schema: "ref",
                table: "SalaryItem");

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 5,
                column: "SalaryItemName",
                value: "پاداش");

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 15,
                column: "SalaryItemName",
                value: "جریمه");
        }
    }
}
