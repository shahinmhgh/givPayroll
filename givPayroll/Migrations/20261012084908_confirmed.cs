using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class confirmed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                schema: "Payroll",
                table: "Payroll",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Vouchered",
                schema: "Payroll",
                table: "Payroll",
                type: "bit",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "PayrollStatus",
            //    keyColumn: "Id",
            //    keyValue: 0,
            //    column: "StatusName",
            //    value: "");

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "PayrollStatus",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    column: "StatusName",
            //    value: "حضور و غیاب اشتباه");

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "PayrollStatus",
            //    keyColumn: "Id",
            //    keyValue: 100,
            //    column: "StatusName",
            //    value: "حضور و غیاب ناقص");

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "PayrollStatus",
            //    keyColumn: "Id",
            //    keyValue: 220,
            //    column: "StatusName",
            //    value: "ثبت شده");

            //migrationBuilder.UpdateData(
            //    schema: "ref",
            //    table: "PayrollStatus",
            //    keyColumn: "Id",
            //    keyValue: 300,
            //    column: "StatusName",
            //    value: "تایید شده");

            //migrationBuilder.InsertData(
            //    schema: "ref",
            //    table: "PayrollStatus",
            //    columns: new[] { "Id", "StatusName" },
            //    values: new object[] { 350, "ارسال به حسابداری" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ref",
                table: "PayrollStatus",
                keyColumn: "Id",
                keyValue: 350);

            migrationBuilder.DropColumn(
                name: "Confirmed",
                schema: "Payroll",
                table: "Payroll");

            migrationBuilder.DropColumn(
                name: "Vouchered",
                schema: "Payroll",
                table: "Payroll");

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "PayrollStatus",
                keyColumn: "Id",
                keyValue: 0,
                column: "StatusName",
                value: "جدید");

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "PayrollStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "StatusName",
                value: "حضور و غیاب ناقص");

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "PayrollStatus",
                keyColumn: "Id",
                keyValue: 100,
                column: "StatusName",
                value: "آماده محاسبه");

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "PayrollStatus",
                keyColumn: "Id",
                keyValue: 220,
                column: "StatusName",
                value: "تایید شده");

            migrationBuilder.UpdateData(
                schema: "ref",
                table: "PayrollStatus",
                keyColumn: "Id",
                keyValue: 300,
                column: "StatusName",
                value: "ارسال به حسابداری");
        }
    }
}
