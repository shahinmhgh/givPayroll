using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace givPayroll.Migrations
{
    /// <inheritdoc />
    public partial class ini35 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "PersonCategoryResult",
            //    columns: table => new
            //    {
            //        PersonID = table.Column<long>(type: "bigint", nullable: false),
            //        PersonFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CategoryCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonCategoryResult");
        }
    }
}
