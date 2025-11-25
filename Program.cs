using ASPCoreWebApplication.Models;
using ASPCoreWebApplication.Services.Implementation;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using ASPCoreWebApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// настраиваем DI и регистрируем MusicService
builder.Services.Configure<MusicStoreDatabaseSettings>(
    builder.Configuration.GetSection("MusicStoreDatabase"));

builder.Services.AddSingleton<MusicService>();

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var cfg = sp.GetRequiredService<IOptions<MusicStoreDatabaseSettings>>().Value;
    return new MongoClient(cfg.ConnectionString);
});

builder.Services.AddControllers().AddJsonOptions(
    options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddScoped<IMusicService, MusicService>();

builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
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
