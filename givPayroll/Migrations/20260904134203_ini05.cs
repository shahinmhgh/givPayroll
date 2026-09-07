using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "ref",
                table: "Holiday",
                columns: new[] { "Id", "Date", "Description", "IsOfficial", "PersianDate", "Title", "Year" },
                values: new object[,]
                {
                    { 1001, new DateTime(2026, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/01/01", "عید سعید فطر و آغاز نوروز", 1405 },
                    { 1002, new DateTime(2026, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/01/02", "عید نوروز و تعطیل به مناسبت عید سعید فطر", 1405 },
                    { 1003, new DateTime(2026, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/01/03", "عید نوروز", 1405 },
                    { 1004, new DateTime(2026, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/01/04", "عید نوروز", 1405 },
                    { 1005, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/01/12", "روز جمهوری اسلامی ایران", 1405 },
                    { 1006, new DateTime(2026, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/01/13", "روز طبیعت", 1405 },
                    { 1007, new DateTime(2026, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/01/25", "شهادت امام جعفر صادق (ع)", 1405 },
                    { 1008, new DateTime(2026, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/03/06", "عید سعید قربان", 1405 },
                    { 1009, new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/03/14", "رحلت حضرت امام خمینی (ره) و عید سعید غدیر خم", 1405 },
                    { 1010, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/03/15", "قیام ۱۵ خرداد", 1405 },
                    { 1011, new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/04/03", "تاسوعای حسینی", 1405 },
                    { 1012, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/04/04", "عاشورای حسینی", 1405 },
                    { 1013, new DateTime(2026, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/05/13", "اربعین حسینی", 1405 },
                    { 1014, new DateTime(2026, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/05/21", "رحلت پیامبر اکرم (ص) و شهادت امام حسن مجتبی (ع)", 1405 },
                    { 1015, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/05/22", "شهادت امام رضا (ع)", 1405 },
                    { 1016, new DateTime(2026, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/05/30", "شهادت امام حسن عسکری (ع)", 1405 },
                    { 1017, new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/06/08", "میلاد پیامبر اکرم (ص) و میلاد امام جعفر صادق (ع)", 1405 },
                    { 1018, new DateTime(2026, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/08/22", "شهادت حضرت فاطمه زهرا (س)", 1405 },
                    { 1019, new DateTime(2026, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/10/02", "ولادت حضرت علی (ع) و روز پدر", 1405 },
                    { 1020, new DateTime(2027, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/10/16", "مبعث حضرت رسول اکرم (ص)", 1405 },
                    { 1021, new DateTime(2027, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/11/04", "ولادت حضرت قائم (عج) و جشن نیمه شعبان", 1405 },
                    { 1022, new DateTime(2027, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/11/22", "پیروزی انقلاب اسلامی ایران", 1405 },
                    { 1023, new DateTime(2027, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/12/09", "شهادت حضرت علی (ع)", 1405 },
                    { 1024, new DateTime(2027, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/12/19", "عید سعید فطر", 1405 },
                    { 1025, new DateTime(2027, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/12/20", "تعطیل به مناسبت عید سعید فطر", 1405 },
                    { 1026, new DateTime(2027, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "1405/12/29", "روز ملی شدن صنعت نفت ایران", 1405 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
