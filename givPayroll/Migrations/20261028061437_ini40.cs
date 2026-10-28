using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini40 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personnel_Staff_StaffID",
                schema: "Payroll",
                table: "Personnel");

            migrationBuilder.AlterColumn<int>(
                name: "StaffID",
                schema: "Payroll",
                table: "Personnel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Personnel_Staff_StaffID",
                schema: "Payroll",
                table: "Personnel",
                column: "StaffID",
                principalSchema: "dbo",
                principalTable: "Staff",
                principalColumn: "StaffID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personnel_Staff_StaffID",
                schema: "Payroll",
                table: "Personnel");

            migrationBuilder.AlterColumn<int>(
                name: "StaffID",
                schema: "Payroll",
                table: "Personnel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Personnel_Staff_StaffID",
                schema: "Payroll",
                table: "Personnel",
                column: "StaffID",
                principalSchema: "dbo",
                principalTable: "Staff",
                principalColumn: "StaffID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
