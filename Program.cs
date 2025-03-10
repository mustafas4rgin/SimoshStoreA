using SimoshStore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddHttpClient("Api.Data", client =>
{
    client.BaseAddress = new Uri("https://www.simosh.shop/");
});

builder.Services.AddBusinessService();


var app = builder.Build();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

}

// Configure the HTTP request pipeline for non-development environments
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Hata yönetimi
    app.UseHsts(); // HTTP Strict Transport Security (HSTS)
}

app.UseForwardedHeaders();
app.UseStaticFiles();  // Statik dosyalar için middleware

app.Run();
