using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini141 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
    name: "FormulaValue",
    schema: "ref",
    table: "SalaryItem",
    type: "nvarchar(max)",
    nullable: true,
    defaultValue: "",
    oldClrType: typeof(string),
    oldType: "ntext",
    oldDefaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FormulaValue",
                schema: "ref",
                table: "SalaryItem",
                type: "ntext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "ntext",
                oldNullable: true,
                oldDefaultValue: "");
        }
    }
}
