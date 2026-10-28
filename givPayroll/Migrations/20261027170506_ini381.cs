using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini381 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProjectId2",
                schema: "ref",
                table: "SalaryItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PersonID2",
                schema: "ref",
                table: "SalaryItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProjectId2",
                schema: "ref",
                table: "SalaryItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonID2",
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
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "PersonID2", "ProjectId2" },
                values: new object[] { 0, 0 });
        }
    }
}
