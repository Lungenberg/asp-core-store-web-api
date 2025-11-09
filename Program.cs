using ASPCoreWebApplication.Models;
using ASPCoreWebApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// настраиваем DI и регистрируем MusicService
builder.Services.Configure<MusicStoreDatabaseSettings>(
    builder.Configuration.GetSection("MusicStoreDatabase"));

builder.Services.AddSingleton<MusicService>();

builder.Services.AddControllers().AddJsonOptions(
    options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddControllersWithViews();

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

app.Run();
