using Microsoft.EntityFrameworkCore;
using NovusCapEkosistemHaritasi.Models;

namespace NovusCapEkosistemHaritasi.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EcosystemPoint> EcosystemPoints { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PageContent> PageContents { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Menu> Menus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure EcosystemPoint
            modelBuilder.Entity<EcosystemPoint>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<EcosystemPoint>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<EcosystemPoint>()
                .Property(e => e.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<EcosystemPoint>()
                .Property(e => e.Website)
                .HasMaxLength(200);

            modelBuilder.Entity<EcosystemPoint>()
                .Property(e => e.Icon)
                .HasMaxLength(100);

            modelBuilder.Entity<EcosystemPoint>()
                .Property(e => e.Address)
                .HasMaxLength(200);

            modelBuilder.Entity<EcosystemPoint>()
                .Property(e => e.City)
                .HasMaxLength(50);
                
            modelBuilder.Entity<EcosystemPoint>()
                .Property(e => e.District)
                .HasMaxLength(50);
                
            // Configure Category
            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);
                
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
                
            modelBuilder.Entity<Category>()
                .Property(c => c.Description)
                .HasMaxLength(100);
                
            modelBuilder.Entity<Category>()
                .Property(c => c.Icon)
                .HasMaxLength(100);
                
            // Configure relationship between EcosystemPoint and Category
            modelBuilder.Entity<EcosystemPoint>()
                .HasOne(e => e.Category)
                .WithMany(c => c.EcosystemPoints)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Configure PageContent
            modelBuilder.Entity<PageContent>()
                .HasKey(p => p.Id);
                
            modelBuilder.Entity<PageContent>()
                .Property(p => p.PageName)
                .IsRequired()
                .HasMaxLength(50);
                
            modelBuilder.Entity<PageContent>()
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(100);
                
            modelBuilder.Entity<PageContent>()
                .Property(p => p.Content)
                .IsRequired();
                
            // Configure ContactMessage
            modelBuilder.Entity<ContactMessage>()
                .HasKey(c => c.Id);
                
            modelBuilder.Entity<ContactMessage>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            modelBuilder.Entity<ContactMessage>()
                .Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);
                
            modelBuilder.Entity<ContactMessage>()
                .Property(c => c.Subject)
                .IsRequired()
                .HasMaxLength(200);
                
            modelBuilder.Entity<ContactMessage>()
                .Property(c => c.Message)
                .IsRequired();
                
            // Configure Menu
            modelBuilder.Entity<Menu>()
                .HasKey(m => m.Id);
                
            modelBuilder.Entity<Menu>()
                .Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(50);
                
            modelBuilder.Entity<Menu>()
                .HasOne(m => m.Parent)
                .WithMany(m => m.SubMenus)
                .HasForeignKey(m => m.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed initial data
            SeedData(modelBuilder);
        }

        // Bu metot, DbContext oluşturulduğunda çağrılır ve bağlantı ayarlarını yapılandırır
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Eğer bağlantı ayarları henüz yapılandırılmadıysa
            if (!optionsBuilder.IsConfigured)
            {
                // Varsayılan bağlantı ayarlarını kullan
                optionsBuilder.UseSqlite("Data Source=NovusCapEkosistemHaritasi.db");
            }
            
            // Suppress PendingModelChangesWarning
            optionsBuilder.ConfigureWarnings(warnings => 
                warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = CategoryType.School42,
                    Description = "42 Yazılım Okulları",
                    Icon = "school-icon.png"
                },
                new Category
                {
                    Id = 2,
                    Name = CategoryType.University,
                    Description = "Üniversiteler",
                    Icon = "university-icon.png"
                },
                new Category
                {
                    Id = 3,
                    Name = CategoryType.TechHub,
                    Description = "Teknoloji Merkezleri",
                    Icon = "techhub-icon.png"
                },
                new Category
                {
                    Id = 4,
                    Name = CategoryType.EducationCenter,
                    Description = "Eğitim Merkezleri",
                    Icon = "education-icon.png"
                }
            );
            
            // Seed EcosystemPoints
            modelBuilder.Entity<EcosystemPoint>().HasData(
                new EcosystemPoint
                {
                    Id = 1,
                    Name = "42 İstanbul",
                    Description = "Akıllı cihazlar çağında yazılım geliştiricileri yetiştiren yenilikçi kodlama okulu.",
                    Website = "https://42istanbul.com.tr",
                    Latitude = 41.0082,
                    Longitude = 28.9784,
                    CategoryId = 1,
                    Icon = "school-icon.png",
                    Address = "Sanayi Mahallesi, 1655. Sokak, No:20, 34535 Beylikdüzü",
                    City = "İstanbul",
                    District = "Beylikdüzü"
                },
                new EcosystemPoint
                {
                    Id = 2,
                    Name = "42 Kocaeli",
                    Description = "Ücretsiz, eşit erişime açık ve yenilikçi eğitim modeliyle akredite bir yazılım okulu.",
                    Website = "https://42kocaeli.com.tr",
                    Latitude = 40.7654,
                    Longitude = 29.9408,
                    CategoryId = 1,
                    Icon = "school-icon.png",
                    Address = "Yeşilova, D130 Karayolu Caddesi, No:111, 41780 Körfez",
                    City = "Kocaeli",
                    District = "Körfez"
                },
                new EcosystemPoint
                {
                    Id = 3,
                    Name = "42 Ankara",
                    Description = "Yenilikçi pedagoji ile eğitim veren, ücretsiz kodlama okulu.",
                    Website = "https://42ankara.com.tr",
                    Latitude = 39.9334,
                    Longitude = 32.8597,
                    CategoryId = 1,
                    Icon = "school-icon.png",
                    Address = "Yakut Mahallesi, 950. Sokak, No:2, 06586 Etimesgut",
                    City = "Ankara",
                    District = "Etimesgut"
                }
            );
            
            // Seed PageContents
            modelBuilder.Entity<PageContent>().HasData(
                new PageContent
                {
                    Id = 1,
                    PageName = "About",
                    Title = "Hakkımızda",
                    Content = "<p>Bu platform, Türkiye'deki teknoloji ve eğitim ekosistemini haritalandırmak amacıyla oluşturulmuştur.</p><p>Amacımız, öğrencilere, girişimcilere ve teknoloji meraklılarına, bulundukları bölgelerdeki teknoloji ve eğitim olanaklarını keşfetmelerini sağlamaktır.</p>",
                    Subtitle = "Türkiye Teknoloji ve Eğitim Ekosistemi",
                    MetaDescription = "Türkiye'deki teknoloji ve eğitim ekosistemini haritalandıran platform",
                    CreatedAt = new DateTime(2023, 1, 1)
                },
                new PageContent
                {
                    Id = 2,
                    PageName = "Contact",
                    Title = "İletişim",
                    Content = "<p>Bizimle iletişime geçmek için aşağıdaki formu doldurabilirsiniz.</p>",
                    Subtitle = "Bize Ulaşın",
                    MetaDescription = "NovusCap Ekosistem Haritası iletişim sayfası",
                    CreatedAt = new DateTime(2023, 1, 1)
                },
                new PageContent
                {
                    Id = 3,
                    PageName = "Privacy",
                    Title = "Gizlilik Politikası",
                    Content = "<p>Bu gizlilik politikası, NovusCap Ekosistem Haritası web sitesinin kullanıcılarına ait kişisel verilerin nasıl toplandığını, kullanıldığını, paylaşıldığını ve korunduğunu açıklamaktadır.</p>",
                    Subtitle = "Gizlilik Politikamız",
                    MetaDescription = "NovusCap Ekosistem Haritası gizlilik politikası",
                    CreatedAt = new DateTime(2023, 1, 1)
                }
            );
            
            // Seed Menus
            modelBuilder.Entity<Menu>().HasData(
                new Menu
                {
                    Id = 1,
                    Title = "Ana Sayfa",
                    Url = "/",
                    Icon = "fas fa-home",
                    Order = 1,
                    IsActive = true
                },
                new Menu
                {
                    Id = 2,
                    Title = "Ekosistem Noktaları",
                    Url = "/EcosystemPoints",
                    Icon = "fas fa-map-marker-alt",
                    Order = 2,
                    IsActive = true
                },
                new Menu
                {
                    Id = 3,
                    Title = "Hakkımızda",
                    Url = "/Home/About",
                    Icon = "fas fa-info-circle",
                    Order = 3,
                    IsActive = true
                },
                new Menu
                {
                    Id = 4,
                    Title = "İletişim",
                    Url = "/Home/Contact",
                    Icon = "fas fa-envelope",
                    Order = 4,
                    IsActive = true
                }
            );
        }
    }
    
    public static class CategoryType
    {
        public const string School42 = "42 Okulu";
        public const string University = "Üniversite";
        public const string TechHub = "Teknoloji Merkezi";
        public const string EducationCenter = "Eğitim Merkezi";
    }
}