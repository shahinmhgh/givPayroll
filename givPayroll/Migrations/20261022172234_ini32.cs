using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini32 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PersonID",
                schema: "ref",
                table: "SalaryItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                schema: "ref",
                table: "SalaryItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PersonID", "ProjectId" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PersonID", "ProjectId" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PersonID", "ProjectId" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PersonID", "ProjectId" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PersonID", "ProjectId" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PersonID", "ProjectId" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "PersonID", "ProjectId" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "PersonID", "ProjectId" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "PersonID", "ProjectId" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "PersonID", "ProjectId" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "PersonID", "ProjectId" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "PersonID", "ProjectId" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "PersonID", "ProjectId" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "PersonID", "ProjectId" },
                values: new object[] { 0, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectId",
                schema: "ref",
                table: "SalaryItem");

            migrationBuilder.AlterColumn<long>(
                name: "PersonID",
                schema: "ref",
                table: "SalaryItem",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 1,
                column: "PersonID",
                value: null);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 2,
                column: "PersonID",
                value: null);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 3,
                column: "PersonID",
                value: null);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 4,
                column: "PersonID",
                value: null);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 5,
                column: "PersonID",
                value: null);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 6,
                column: "PersonID",
                value: null);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 7,
                column: "PersonID",
                value: null);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 8,
                column: "PersonID",
                value: null);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 9,
                column: "PersonID",
                value: null);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 10,
                column: "PersonID",
                value: null);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 12,
                column: "PersonID",
                value: null);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 13,
                column: "PersonID",
                value: null);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 14,
                column: "PersonID",
                value: null);

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 15,
                column: "PersonID",
                value: null);
        }
    }
}
