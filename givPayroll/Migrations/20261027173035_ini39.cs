using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini39 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaffID",
                schema: "Payroll",
                table: "Personnel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_StaffID",
                schema: "Payroll",
                table: "Personnel",
                column: "StaffID");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personnel_Staff_StaffID",
                schema: "Payroll",
                table: "Personnel");

            migrationBuilder.DropIndex(
                name: "IX_Personnel_StaffID",
                schema: "Payroll",
                table: "Personnel");

            migrationBuilder.DropColumn(
                name: "StaffID",
                schema: "Payroll",
                table: "Personnel");
        }
    }
}
