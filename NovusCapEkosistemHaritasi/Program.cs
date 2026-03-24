using Microsoft.EntityFrameworkCore;
using NovusCapEkosistemHaritasi.DAL;
using NovusCapEkosistemHaritasi.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        // Döngüsel referans sorununu çözmek için ReferenceHandler.Preserve kullan
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 32;
    });

// Make Configuration accessible in views
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Add database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IEcosystemPointRepository, EcosystemPointRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPageContentRepository, PageContentRepository>();
builder.Services.AddScoped<IContactMessageRepository, ContactMessageRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();

// Register services
builder.Services.AddScoped<IEcosystemPointService, EcosystemPointService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPageContentService, PageContentService>();
builder.Services.AddScoped<IContactMessageService, ContactMessageService>();
builder.Services.AddScoped<IMenuService, MenuService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Veritabanı ve Migration işlemleri
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        
        // Instead of using migrations, use EnsureCreated to create the schema
        context.Database.EnsureCreated();
        
        /* Comment out migration code since it's causing issues
        if (app.Environment.IsDevelopment())
        {
            // Geçici olarak EnsureDeleted kaldırıldı, veritabanının silinmesini istemiyoruz
            // context.Database.EnsureDeleted();
            // context.Database.EnsureCreated();
            
            // Migration'ları uyguluyoruz
            context.Database.Migrate();
        }
        else
        {
            // Production ortamında sadece migration'ları uygula
            context.Database.Migrate();
        }
        */
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Veritabanı oluşturulurken bir hata oluştu.");
    }
}

app.Run();