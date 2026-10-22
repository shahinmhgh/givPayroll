using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Company",
            //    schema: "ref");

            //migrationBuilder.DeleteData(
            //    schema: "ref",
            //    table: "Parameters",
            //    keyColumn: "Id",
            //    keyValue: 1);

            //migrationBuilder.DeleteData(
            //    schema: "ref",
            //    table: "Parameters",
            //    keyColumn: "Id",
            //    keyValue: 2);

            //migrationBuilder.EnsureSchema(
            //    name: "dbo");

            //migrationBuilder.CreateTable(
            //    name: "Config",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        ActiveClientID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ActiveStockID = table.Column<int>(type: "int", nullable: false),
            //        CompanyID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        CompanyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        GeneralCustomerID = table.Column<int>(type: "int", nullable: true),
            //        ActivePriceListID = table.Column<int>(type: "int", nullable: true),
            //        ActiveProjectID = table.Column<long>(type: "bigint", nullable: true),
            //        DataReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        ActiveWholeSalePriceListID = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Config", x => x.ActiveClientID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Person",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        PersonID = table.Column<long>(type: "bigint", nullable: false),
            //        PersonTypeID = table.Column<int>(type: "int", nullable: false),
            //        PersonCategoryCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
            //        PersonFirstName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
            //        PersonLastName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
            //        PersonAddress = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
            //        PersonTel = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
            //        PersonMobile = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
            //        PersonFax = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
            //        PersonIsActive = table.Column<bool>(type: "bit", nullable: false),
            //        PersonNote = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
            //        PriceListID = table.Column<int>(type: "int", nullable: true),
            //        CompanyID = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
            //        DiscountListID = table.Column<int>(type: "int", nullable: true),
            //        DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UserCreated = table.Column<int>(type: "int", nullable: false),
            //        DateChanged = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        UserChanged = table.Column<int>(type: "int", nullable: true),
            //        PersonFullName = table.Column<string>(type: "varchar(511)", maxLength: 511, nullable: false),
            //        DiscountListUsage = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
            //        PersonGroupID = table.Column<int>(type: "int", nullable: false),
            //        DataReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        OldPersonID = table.Column<long>(type: "bigint", nullable: true),
            //        NewPersonID = table.Column<long>(type: "bigint", nullable: true),
            //        OriginPersonID = table.Column<long>(type: "bigint", nullable: true),
            //        ParentPersonID = table.Column<long>(type: "bigint", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Person", x => x.PersonID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PersonCategory",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        CategoryCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
            //        CategoryName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
            //        CategoryIsActive = table.Column<bool>(type: "bit", nullable: false),
            //        CategoryGroupCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
            //        DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UserCreated = table.Column<int>(type: "int", nullable: false),
            //        DateChanged = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        UserChanged = table.Column<int>(type: "int", nullable: true),
            //        AccountNature = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
            //        AccountType = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
            //        HasUniqueAccount = table.Column<bool>(type: "bit", nullable: false),
            //        HasSubsideryAccount = table.Column<bool>(type: "bit", nullable: false),
            //        DataReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PersonCategory", x => x.CategoryCode);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PersonCategoryGroups",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        CategoryCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
            //        PersonGroupID = table.Column<int>(type: "int", nullable: false),
            //        DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        UserCreated = table.Column<int>(type: "int", nullable: true),
            //        DateChanged = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        UserChanged = table.Column<int>(type: "int", nullable: true),
            //        DataReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PersonCategoryGroups", x => x.CategoryCode);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PersonCategoryLength",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        CategoryLevel = table.Column<int>(type: "int", nullable: false),
            //        SumLevelLength = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PersonCategoryLength", x => x.CategoryLevel);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PersonCategoryLevel",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        CategoryLevel = table.Column<int>(type: "int", nullable: false),
            //        CategoryLevelName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
            //        CategoryLevelLength = table.Column<int>(type: "int", nullable: false),
            //        DataReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PersonCategoryLevel", x => x.CategoryLevel);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PersonGroup",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        PersonGroupID = table.Column<int>(type: "int", nullable: false),
            //        PersonGroupName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
            //        DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        UserCreated = table.Column<int>(type: "int", nullable: true),
            //        DateChanged = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        UserChanged = table.Column<int>(type: "int", nullable: true),
            //        IsCustomer = table.Column<bool>(type: "bit", nullable: false),
            //        IsVendor = table.Column<bool>(type: "bit", nullable: false),
            //        IsPerson = table.Column<bool>(type: "bit", nullable: false),
            //        IsBank = table.Column<bool>(type: "bit", nullable: false),
            //        IsMoneyBox = table.Column<bool>(type: "bit", nullable: false),
            //        IsCost = table.Column<bool>(type: "bit", nullable: false),
            //        IsCostCenter = table.Column<bool>(type: "bit", nullable: false),
            //        IsStaff = table.Column<bool>(type: "bit", nullable: false),
            //        IsShopCustomer = table.Column<bool>(type: "bit", nullable: false),
            //        DataReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        MinID = table.Column<long>(type: "bigint", nullable: true),
            //        MaxID = table.Column<long>(type: "bigint", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PersonGroup", x => x.PersonGroupID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Project",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        ProjectID = table.Column<long>(type: "bigint", nullable: false),
            //        ProjectName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
            //        StartDate = table.Column<string>(type: "char(8)", maxLength: 8, nullable: true),
            //        EndDate = table.Column<string>(type: "char(8)", maxLength: 8, nullable: true),
            //        ManagerUserID = table.Column<int>(type: "int", nullable: true),
            //        Manager = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
            //        ProjectDesc = table.Column<string>(type: "varchar(max)", nullable: true),
            //        DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UserCreated = table.Column<int>(type: "int", nullable: true),
            //        DateChanged = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        UserChanged = table.Column<int>(type: "int", nullable: true),
            //        DataReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Project", x => x.ProjectID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Staff",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        StaffID = table.Column<int>(type: "int", nullable: false),
            //        StaffType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
            //        FirstName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
            //        LastName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
            //        Address = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
            //        Tel = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
            //        Mobile = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
            //        IsActive = table.Column<bool>(type: "bit", nullable: true),
            //        Note = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
            //        PersonID = table.Column<long>(type: "bigint", nullable: true),
            //        UserID = table.Column<int>(type: "int", nullable: true),
            //        VisibleInClientIDs = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
            //        StaffGroupID = table.Column<int>(type: "int", nullable: true),
            //        DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UserCreated = table.Column<int>(type: "int", nullable: true),
            //        DateChanged = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        UserChanged = table.Column<int>(type: "int", nullable: true),
            //        FullName = table.Column<string>(type: "varchar(511)", maxLength: 511, nullable: false),
            //        DataReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        OrgPostID = table.Column<int>(type: "int", nullable: true),
            //        PersonnelCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Staff", x => x.StaffID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Voucher",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        AutoID = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        VoucherID = table.Column<long>(type: "bigint", nullable: false),
            //        ClientID = table.Column<int>(type: "int", nullable: false),
            //        VoucherNo = table.Column<long>(type: "bigint", nullable: false),
            //        VoucherDate = table.Column<string>(type: "char(8)", maxLength: 8, nullable: false),
            //        VoucherTypeID = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
            //        VoucherDescription = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true),
            //        InitiateUserID = table.Column<int>(type: "int", nullable: true),
            //        InitiateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        LastModifyUserID = table.Column<int>(type: "int", nullable: true),
            //        LastModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        Finalized = table.Column<bool>(type: "bit", nullable: true),
            //        FinalizedByUserID = table.Column<int>(type: "int", nullable: true),
            //        VoucherRefID = table.Column<long>(type: "bigint", nullable: true),
            //        VoucherRefClientID = table.Column<int>(type: "int", nullable: true),
            //        VoucherRefType = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
            //        OldVoucherNo = table.Column<long>(type: "bigint", nullable: true),
            //        DataReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        VoucherFlag = table.Column<int>(type: "int", nullable: false),
            //        DailyNo = table.Column<long>(type: "bigint", nullable: true),
            //        WeeklyNo = table.Column<long>(type: "bigint", nullable: true),
            //        WeeklyDate = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
            //        MonthlyNo = table.Column<long>(type: "bigint", nullable: true),
            //        MonthlyDate = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Voucher", x => x.AutoID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "VoucherRow",
            //    schema: "dbo",
            //    columns: table => new
            //    {
            //        AutoID = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        VoucherID = table.Column<long>(type: "bigint", nullable: false),
            //        ClientID = table.Column<int>(type: "int", nullable: false),
            //        CategoryCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
            //        PersonID = table.Column<long>(type: "bigint", nullable: true),
            //        ProjectID = table.Column<long>(type: "bigint", nullable: true),
            //        Description = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true),
            //        Debit = table.Column<decimal>(type: "decimal(21,3)", nullable: true),
            //        Credit = table.Column<decimal>(type: "decimal(21,3)", nullable: true),
            //        InitiateUserID = table.Column<int>(type: "int", nullable: true),
            //        InitiateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        LastModifyUserID = table.Column<int>(type: "int", nullable: true),
            //        LastModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        VoucherRowRefID = table.Column<long>(type: "bigint", nullable: true),
            //        VoucherRowRefClientID = table.Column<int>(type: "int", nullable: true),
            //        VoucherRowRefType = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
            //        DateValue = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
            //        NumberValue = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
            //        AmountValue = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
            //        DataReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_VoucherRow", x => x.AutoID);
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Config",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Person",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PersonCategory",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PersonCategoryGroups",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PersonCategoryLength",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PersonCategoryLevel",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PersonGroup",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Staff",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Voucher",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "VoucherRow",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "Company",
                schema: "ref",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsuranceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "ref",
                table: "Company",
                columns: new[] { "Id", "Address", "CompanyName", "InsuranceCode" },
                values: new object[] { 1, "تهران - ...", "Giv", "" });

            migrationBuilder.InsertData(
                schema: "ref",
                table: "Parameters",
                columns: new[] { "Id", "OptionKey", "OptionName", "OptionValue", "Subject" },
                values: new object[,]
                {
                    { 1, "EmployeePercent", "درصد بیمه کارمند", "7", "Insurance" },
                    { 2, "CompanyPercent", "درصد بیمه کارفرما", "23", "Insurance" }
                });
        }
    }
}
