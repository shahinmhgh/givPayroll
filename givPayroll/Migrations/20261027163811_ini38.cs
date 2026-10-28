using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini38 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonCategoryCode2",
                schema: "ref",
                table: "SalaryItem",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonID2",
                schema: "ref",
                table: "SalaryItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId2",
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
                columns: new[] { "PersonCategoryCode2", "PersonID2", "ProjectId2" },
                values: new object[] { null, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PersonCategoryCode2", "PersonID2", "ProjectId2" },
                values: new object[] { null, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PersonCategoryCode2", "PersonID2", "ProjectId2" },
                values: new object[] { null, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PersonCategoryCode2", "PersonID2", "ProjectId2" },
                values: new object[] { null, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PersonCategoryCode2", "PersonID2", "ProjectId2" },
                values: new object[] { null, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PersonCategoryCode2", "PersonID2", "ProjectId2" },
                values: new object[] { null, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "PersonCategoryCode2", "PersonID2", "ProjectId2" },
                values: new object[] { null, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "PersonCategoryCode2", "PersonID2", "ProjectId2" },
                values: new object[] { null, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "PersonCategoryCode2", "PersonID2", "ProjectId2" },
                values: new object[] { null, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "PersonCategoryCode2", "PersonID2", "ProjectId2" },
                values: new object[] { null, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "PersonCategoryCode2", "PersonID2", "ProjectId2" },
                values: new object[] { null, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "PersonCategoryCode2", "PersonID2", "ProjectId2" },
                values: new object[] { null, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "PersonCategoryCode2", "PersonID2", "ProjectId2" },
                values: new object[] { null, 0, 0 });

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "SalaryItem",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "PersonCategoryCode2", "PersonID2", "ProjectId2" },
                values: new object[] { null, 0, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonCategoryCode2",
                schema: "ref",
                table: "SalaryItem");

            migrationBuilder.DropColumn(
                name: "PersonID2",
                schema: "ref",
                table: "SalaryItem");

            migrationBuilder.DropColumn(
                name: "ProjectId2",
                schema: "ref",
                table: "SalaryItem");
        }
    }
}
