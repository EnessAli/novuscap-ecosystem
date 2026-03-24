using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NovusCapEkosistemHaritasi.Migrations
{
    /// <inheritdoc />
    public partial class CreateEcosystemPointsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Markers");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_Menus_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PageContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MetaKeywords = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EcosystemPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    District = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcosystemPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcosystemPoints_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "42 Yazılım Okulları", "school-icon.png", "42 Okulu" },
                    { 2, "Üniversiteler", "university-icon.png", "Üniversite" },
                    { 3, "Teknoloji Merkezleri", "techhub-icon.png", "Teknoloji Merkezi" },
                    { 4, "Eğitim Merkezleri", "education-icon.png", "Eğitim Merkezi" }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Icon", "IsActive", "Order", "ParentId", "Title", "Url" },
                values: new object[,]
                {
                    { 1, "fas fa-home", true, 1, null, "Ana Sayfa", "/" },
                    { 2, "fas fa-map-marker-alt", true, 2, null, "Ekosistem Noktaları", "/EcosystemPoints" },
                    { 3, "fas fa-info-circle", true, 3, null, "Hakkımızda", "/Home/About" },
                    { 4, "fas fa-envelope", true, 4, null, "İletişim", "/Home/Contact" }
                });

            migrationBuilder.InsertData(
                table: "PageContents",
                columns: new[] { "Id", "Content", "CreatedAt", "MetaDescription", "MetaKeywords", "PageName", "Subtitle", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "<p>Bu platform, Türkiye'deki teknoloji ve eğitim ekosistemini haritalandırmak amacıyla oluşturulmuştur.</p><p>Amacımız, öğrencilere, girişimcilere ve teknoloji meraklılarına, bulundukları bölgelerdeki teknoloji ve eğitim olanaklarını keşfetmelerini sağlamaktır.</p>", new DateTime(2025, 5, 10, 6, 15, 33, 650, DateTimeKind.Local).AddTicks(4827), "Türkiye'deki teknoloji ve eğitim ekosistemini haritalandıran platform", null, "About", "Türkiye Teknoloji ve Eğitim Ekosistemi", "Hakkımızda", null },
                    { 2, "<p>Bizimle iletişime geçmek için aşağıdaki formu doldurabilirsiniz.</p>", new DateTime(2025, 5, 10, 6, 15, 33, 650, DateTimeKind.Local).AddTicks(5097), "NovusCap Ekosistem Haritası iletişim sayfası", null, "Contact", "Bize Ulaşın", "İletişim", null },
                    { 3, "<p>Bu gizlilik politikası, NovusCap Ekosistem Haritası web sitesinin kullanıcılarına ait kişisel verilerin nasıl toplandığını, kullanıldığını, paylaşıldığını ve korunduğunu açıklamaktadır.</p>", new DateTime(2025, 5, 10, 6, 15, 33, 650, DateTimeKind.Local).AddTicks(5108), "NovusCap Ekosistem Haritası gizlilik politikası", null, "Privacy", "Gizlilik Politikamız", "Gizlilik Politikası", null }
                });

            migrationBuilder.InsertData(
                table: "EcosystemPoints",
                columns: new[] { "Id", "Address", "CategoryId", "City", "Description", "District", "Icon", "Latitude", "Longitude", "Name", "Website" },
                values: new object[,]
                {
                    { 1, "Sanayi Mahallesi, 1655. Sokak, No:20, 34535 Beylikdüzü", 1, "İstanbul", "Akıllı cihazlar çağında yazılım geliştiricileri yetiştiren yenilikçi kodlama okulu.", "Beylikdüzü", "school-icon.png", 41.008200000000002, 28.978400000000001, "42 İstanbul", "https://42istanbul.com.tr" },
                    { 2, "Yeşilova, D130 Karayolu Caddesi, No:111, 41780 Körfez", 1, "Kocaeli", "Ücretsiz, eşit erişime açık ve yenilikçi eğitim modeliyle akredite bir yazılım okulu.", "Körfez", "school-icon.png", 40.7654, 29.940799999999999, "42 Kocaeli", "https://42kocaeli.com.tr" },
                    { 3, "Yakut Mahallesi, 950. Sokak, No:2, 06586 Etimesgut", 1, "Ankara", "Yenilikçi pedagoji ile eğitim veren, ücretsiz kodlama okulu.", "Etimesgut", "school-icon.png", 39.933399999999999, 32.859699999999997, "42 Ankara", "https://42ankara.com.tr" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EcosystemPoints_CategoryId",
                table: "EcosystemPoints",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ParentId",
                table: "Menus",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "EcosystemPoints");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "PageContents");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.CreateTable(
                name: "Markers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Markers",
                columns: new[] { "Id", "Address", "Category", "City", "Description", "Icon", "Latitude", "Longitude", "Name", "Website" },
                values: new object[,]
                {
                    { 1, "Sanayi Mahallesi, 1655. Sokak, No:20, 34535 Beylikdüzü", "42 Okulu", "İstanbul", "Akıllı cihazlar çağında yazılım geliştiricileri yetiştiren yenilikçi kodlama okulu.", "school-icon.png", 41.008200000000002, 28.978400000000001, "42 İstanbul", "https://42istanbul.com.tr" },
                    { 2, "Yeşilova, D130 Karayolu Caddesi, No:111, 41780 Körfez", "42 Okulu", "Kocaeli", "Ücretsiz, eşit erişime açık ve yenilikçi eğitim modeliyle akredite bir yazılım okulu.", "school-icon.png", 40.7654, 29.940799999999999, "42 Kocaeli", "https://42kocaeli.com.tr" },
                    { 3, "Yakut Mahallesi, 950. Sokak, No:2, 06586 Etimesgut", "42 Okulu", "Ankara", "Yenilikçi pedagoji ile eğitim veren, ücretsiz kodlama okulu.", "school-icon.png", 39.933399999999999, 32.859699999999997, "42 Ankara", "https://42ankara.com.tr" }
                });
        }
    }
}
