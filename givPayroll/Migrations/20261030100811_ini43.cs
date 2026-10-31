using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini43 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "PersonID1",
            //    schema: "ref",
            //    table: "SalaryItem",
            //    type: "int",
            //    nullable: true,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            //migrationBuilder.CreateTable(
            //    name: "VoucherModels",
            //    columns: table => new
            //    {
            //        PayrollYear = table.Column<int>(type: "int", nullable: false),
            //        PayrollMonth = table.Column<int>(type: "int", nullable: false),
            //        PersonnelId = table.Column<int>(type: "int", nullable: false),
            //        ProjectId = table.Column<int>(type: "int", nullable: true),
            //        PersonCategoryCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        PersonID = table.Column<int>(type: "int", nullable: true),
            //        SalaryItemName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        SalaryItemId = table.Column<int>(type: "int", nullable: false),
            //        PlusMinus = table.Column<int>(type: "int", nullable: false),
            //        Credit = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
            //        Debit = table.Column<decimal>(type: "decimal(18,3)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //    });

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "SalaryItem",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    column: "PersonID1",
            //    value: null);

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "SalaryItem",
            //    keyColumn: "Id",
            //    keyValue: 2,
            //    column: "PersonID1",
            //    value: null);

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "SalaryItem",
            //    keyColumn: "Id",
            //    keyValue: 3,
            //    column: "PersonID1",
            //    value: null);

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "SalaryItem",
            //    keyColumn: "Id",
            //    keyValue: 4,
            //    column: "PersonID1",
            //    value: null);

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "SalaryItem",
            //    keyColumn: "Id",
            //    keyValue: 5,
            //    column: "PersonID1",
            //    value: null);

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "SalaryItem",
            //    keyColumn: "Id",
            //    keyValue: 6,
            //    column: "PersonID1",
            //    value: null);

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "SalaryItem",
            //    keyColumn: "Id",
            //    keyValue: 7,
            //    column: "PersonID1",
            //    value: null);

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "SalaryItem",
            //    keyColumn: "Id",
            //    keyValue: 8,
            //    column: "PersonID1",
            //    value: null);

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "SalaryItem",
            //    keyColumn: "Id",
            //    keyValue: 9,
            //    column: "PersonID1",
            //    value: null);

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "SalaryItem",
            //    keyColumn: "Id",
            //    keyValue: 10,
            //    column: "PersonID1",
            //    value: null);

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "SalaryItem",
            //    keyColumn: "Id",
            //    keyValue: 12,
            //    column: "PersonID1",
            //    value: null);

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "SalaryItem",
            //    keyColumn: "Id",
            //    keyValue: 13,
            //    column: "PersonID1",
            //    value: null);

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "SalaryItem",
            //    keyColumn: "Id",
            //    keyValue: 14,
            //    column: "PersonID1",
            //    value: null);

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "SalaryItem",
            //    keyColumn: "Id",
            //    keyValue: 15,
            //    column: "PersonID1",
            //    value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoucherModels");

            migrationBuilder.AlterColumn<int>(
                name: "PersonID1",
                schema: "ref",
                table: "SalaryItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 1,
                column: "PersonID1",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 2,
                column: "PersonID1",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 3,
                column: "PersonID1",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 4,
                column: "PersonID1",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 5,
                column: "PersonID1",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 6,
                column: "PersonID1",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 7,
                column: "PersonID1",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 8,
                column: "PersonID1",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 9,
                column: "PersonID1",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 10,
                column: "PersonID1",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 12,
                column: "PersonID1",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 13,
                column: "PersonID1",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 14,
                column: "PersonID1",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 15,
                column: "PersonID1",
                value: 0);
        }
    }
}
