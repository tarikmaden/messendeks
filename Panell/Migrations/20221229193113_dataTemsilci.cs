using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Panell.Migrations
{
    public partial class dataTemsilci : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                           name: "Temsilci",
                           columns: table => new
                           {
                               ID = table.Column<int>(type: "int", nullable: false)
                                   .Annotation("SqlServer:Identity", "1, 1"),
                               title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               phone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                               adres = table.Column<string>(type: "nvarchar(max)", nullable: true)
                           },
                           constraints: table =>
                           {
                               table.PrimaryKey("PK_Temsilci", x => x.ID);
                           });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                            name: "Temsilci");
        }
    }
}
