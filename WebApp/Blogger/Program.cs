using AspNetCoreHero.ToastNotification;
using Blogger.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<BloggerContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("mycon")));

builder.Services.AddNotyf(config => { config.DurationInSeconds = 6; 
    config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });

var app = builder.Build();
var config = app.Configuration;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
