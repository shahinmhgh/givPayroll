using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini44 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "PayrollMonth",
            //    table: "VoucherModels");

            //migrationBuilder.DropColumn(
            //    name: "PayrollYear",
            //    table: "VoucherModels");

            //migrationBuilder.DropColumn(
            //    name: "PersonCategoryCode",
            //    table: "VoucherModels");

            //migrationBuilder.DropColumn(
            //    name: "SalaryItemName",
            //    table: "VoucherModels");

            //migrationBuilder.RenameColumn(
            //    name: "ProjectId",
            //    table: "VoucherModels",
            //    newName: "ProjectID");

            //migrationBuilder.RenameColumn(
            //    name: "Debit",
            //    table: "VoucherModels",
            //    newName: "debit");

            //migrationBuilder.RenameColumn(
            //    name: "Credit",
            //    table: "VoucherModels",
            //    newName: "credit");

            //migrationBuilder.RenameColumn(
            //    name: "SalaryItemId",
            //    table: "VoucherModels",
            //    newName: "VoucherID");

            //migrationBuilder.RenameColumn(
            //    name: "PlusMinus",
            //    table: "VoucherModels",
            //    newName: "InitiateUserID");

            //migrationBuilder.RenameColumn(
            //    name: "PersonnelId",
            //    table: "VoucherModels",
            //    newName: "ClientID");

            //migrationBuilder.AlterColumn<int>(
            //    name: "ProjectID",
            //    table: "VoucherModels",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0,
            //    oldClrType: typeof(int),
            //    oldType: "int",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<long>(
            //    name: "PersonID",
            //    table: "VoucherModels",
            //    type: "bigint",
            //    nullable: false,
            //    defaultValue: 0L,
            //    oldClrType: typeof(int),
            //    oldType: "int",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "debit",
            //    table: "VoucherModels",
            //    type: "decimal(18,2)",
            //    nullable: false,
            //    defaultValue: 0m,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(18,3)",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "credit",
            //    table: "VoucherModels",
            //    type: "decimal(18,2)",
            //    nullable: false,
            //    defaultValue: 0m,
            //    oldClrType: typeof(decimal),
            //    oldType: "decimal(18,3)",
            //    oldNullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "CategoryCode",
            //    table: "VoucherModels",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "Description",
            //    table: "VoucherModels",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "InitiateDate",
            //    table: "VoucherModels",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SalaryItemRuleId",
                schema: "Payroll",
                table: "PayrollItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PayrollDate",
                schema: "Payroll",
                table: "Payroll",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryCode",
                table: "VoucherModels");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "VoucherModels");

            migrationBuilder.DropColumn(
                name: "InitiateDate",
                table: "VoucherModels");

            migrationBuilder.DropColumn(
                name: "SalaryItemRuleId",
                schema: "Payroll",
                table: "PayrollItem");

            migrationBuilder.DropColumn(
                name: "PayrollDate",
                schema: "Payroll",
                table: "Payroll");

            migrationBuilder.RenameColumn(
                name: "debit",
                table: "VoucherModels",
                newName: "Debit");

            migrationBuilder.RenameColumn(
                name: "credit",
                table: "VoucherModels",
                newName: "Credit");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "VoucherModels",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "VoucherID",
                table: "VoucherModels",
                newName: "SalaryItemId");

            migrationBuilder.RenameColumn(
                name: "InitiateUserID",
                table: "VoucherModels",
                newName: "PlusMinus");

            migrationBuilder.RenameColumn(
                name: "ClientID",
                table: "VoucherModels",
                newName: "PersonnelId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Debit",
                table: "VoucherModels",
                type: "decimal(18,3)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Credit",
                table: "VoucherModels",
                type: "decimal(18,3)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "VoucherModels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PersonID",
                table: "VoucherModels",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "PayrollMonth",
                table: "VoucherModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PayrollYear",
                table: "VoucherModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PersonCategoryCode",
                table: "VoucherModels",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalaryItemName",
                table: "VoucherModels",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
