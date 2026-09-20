using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "FormulaValue",
            //    schema: "Payroll",
            //    table: "PayrollItem",
            //    type: "ntext",
            //    nullable: true);
            migrationBuilder.AlterColumn<string>(
name: "FormulaValue",
schema: "Payroll",
table: "PayrollItem",
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
            migrationBuilder.DropColumn(
                name: "FormulaValue",
                schema: "Payroll",
                table: "PayrollItem");
        }
    }
}
