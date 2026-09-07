using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1011);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1012);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1013);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1014);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1015);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1016);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1017);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1018);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1019);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1020);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1021);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1022);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1023);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1024);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1025);

            migrationBuilder.DeleteData(
                schema: "ref",
                table: "Holiday",
                keyColumn: "Id",
                keyValue: 1026);

            migrationBuilder.AddColumn<string>(
                name: "PersianDate",
                schema: "ref",
                table: "Holiday",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersianDate",
                schema: "ref",
                table: "Holiday");

            migrationBuilder.InsertData(
                schema: "ref",
                table: "Holiday",
                columns: new[] { "Id", "Date", "Description", "IsOfficial", "Title", "Year" },
                values: new object[,]
                {
                    { 1001, new DateTime(2026, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "عید سعید فطر و آغاز نوروز", 1405 },
                    { 1002, new DateTime(2026, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "عید نوروز و تعطیل به مناسبت عید سعید فطر", 1405 },
                    { 1003, new DateTime(2026, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "عید نوروز", 1405 },
                    { 1004, new DateTime(2026, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "عید نوروز", 1405 },
                    { 1005, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "روز جمهوری اسلامی ایران", 1405 },
                    { 1006, new DateTime(2026, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "روز طبیعت", 1405 },
                    { 1007, new DateTime(2026, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "شهادت امام جعفر صادق (ع)", 1405 },
                    { 1008, new DateTime(2026, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "عید سعید قربان", 1405 },
                    { 1009, new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "رحلت حضرت امام خمینی (ره) و عید سعید غدیر خم", 1405 },
                    { 1010, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "قیام ۱۵ خرداد", 1405 },
                    { 1011, new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "تاسوعای حسینی", 1405 },
                    { 1012, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "عاشورای حسینی", 1405 },
                    { 1013, new DateTime(2026, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "اربعین حسینی", 1405 },
                    { 1014, new DateTime(2026, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "رحلت پیامبر اکرم (ص) و شهادت امام حسن مجتبی (ع)", 1405 },
                    { 1015, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "شهادت امام رضا (ع)", 1405 },
                    { 1016, new DateTime(2026, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "شهادت امام حسن عسکری (ع)", 1405 },
                    { 1017, new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "میلاد پیامبر اکرم (ص) و میلاد امام جعفر صادق (ع)", 1405 },
                    { 1018, new DateTime(2026, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "شهادت حضرت فاطمه زهرا (س)", 1405 },
                    { 1019, new DateTime(2026, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "ولادت حضرت علی (ع) و روز پدر", 1405 },
                    { 1020, new DateTime(2027, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "مبعث حضرت رسول اکرم (ص)", 1405 },
                    { 1021, new DateTime(2027, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "ولادت حضرت قائم (عج) و جشن نیمه شعبان", 1405 },
                    { 1022, new DateTime(2027, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "پیروزی انقلاب اسلامی ایران", 1405 },
                    { 1023, new DateTime(2027, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "شهادت حضرت علی (ع)", 1405 },
                    { 1024, new DateTime(2027, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "عید سعید فطر", 1405 },
                    { 1025, new DateTime(2027, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "تعطیل به مناسبت عید سعید فطر", 1405 },
                    { 1026, new DateTime(2027, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "روز ملی شدن صنعت نفت ایران", 1405 }
                });
        }
    }
}
