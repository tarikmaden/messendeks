using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Panell.Migrations
{
    public partial class myData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diller",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dil_adi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dil_kisaltma = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diller", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Dsayfalar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sayfa_adi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sayfa_desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sayfa_ozet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sayfa_icerik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sayfa_kategori = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    baglantili_sayfa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    baglantili_dil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sayfa_slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sayfa_title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sayfa_tarih = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sayfa_order = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sayfa_resim = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dsayfalar", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Galeri",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gelen_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    resim = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galeri", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Iletisim",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    facebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    instagram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    youtube = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    google = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    favicon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    maps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    text1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    text2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    text3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    text4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    text5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    text6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    text7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    text8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    text9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    text10 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iletisim", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sayfalar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sayfa_adi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sayfa_desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sayfa_ozet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sayfa_icerik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sayfa_kategori = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sayfa_slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sayfa_title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sayfa_tarih = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sayfa_order = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sayfa_resim = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sayfalar", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diller");

            migrationBuilder.DropTable(
                name: "Dsayfalar");

            migrationBuilder.DropTable(
                name: "Galeri");

            migrationBuilder.DropTable(
                name: "Iletisim");

            migrationBuilder.DropTable(
                name: "Sayfalar");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
