using SimoshStore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddBusinessService();
builder.Services.AddJwtService(builder.Configuration);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
});

var app = builder.Build();

// Authentication middleware
app.UseAuthentication();  // Kimlik doğrulama işlemi

// Routing middleware (önce gelir)
app.UseRouting();

// Authorization middleware (yetkilendirme işlemi)
app.UseAuthorization();   // Yetkilendirme işlemi

// MapControllerRoute, controller ve action eşlemesi
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    if (app.Environment.IsDevelopment())
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        DbSeeder.SeedData(context);  // Veritabanı seed işlemi
    }
}

// Configure the HTTP request pipeline for non-development environments
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Hata yönetimi
    app.UseHsts(); // HTTP Strict Transport Security (HSTS)
}

app.UseHttpsRedirection();  // HTTPS yönlendirme
app.UseStaticFiles();  // Statik dosyalar için middleware

app.Run();
