using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class payrollStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payroll_PayrollStatus_PayrollStatusID",
                schema: "Payroll",
                table: "Payroll");

            migrationBuilder.RenameColumn(
                name: "PayrollStatusID",
                schema: "Payroll",
                table: "Payroll",
                newName: "PayrollStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Payroll_PayrollStatusID",
                schema: "Payroll",
                table: "Payroll",
                newName: "IX_Payroll_PayrollStatusId");

            migrationBuilder.AlterColumn<int>(
                name: "PayrollStatusId",
                schema: "Payroll",
                table: "Payroll",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Payroll_PayrollStatus_PayrollStatusId",
                schema: "Payroll",
                table: "Payroll",
                column: "PayrollStatusId",
                principalSchema: "ref",
                principalTable: "PayrollStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payroll_PayrollStatus_PayrollStatusId",
                schema: "Payroll",
                table: "Payroll");

            migrationBuilder.RenameColumn(
                name: "PayrollStatusId",
                schema: "Payroll",
                table: "Payroll",
                newName: "PayrollStatusID");

            migrationBuilder.RenameIndex(
                name: "IX_Payroll_PayrollStatusId",
                schema: "Payroll",
                table: "Payroll",
                newName: "IX_Payroll_PayrollStatusID");

            migrationBuilder.AlterColumn<int>(
                name: "PayrollStatusID",
                schema: "Payroll",
                table: "Payroll",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payroll_PayrollStatus_PayrollStatusID",
                schema: "Payroll",
                table: "Payroll",
                column: "PayrollStatusID",
                principalSchema: "ref",
                principalTable: "PayrollStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
