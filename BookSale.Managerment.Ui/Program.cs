using BookSale.Managerment.Ui;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BookSale.Managerment.DataAccess.DataAccess;
using BookSale.Managerment.DataAccess.Configuration;
using BookSale.Managerment.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký db
builder.Services.RegisterDb(builder.Configuration);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Đăng ký Razor Pages
builder.Services.AddRazorPages();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.AutoMigrations().GetAwaiter().GetResult();

app.SeedData(builder.Configuration).GetAwaiter().GetResult();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = ctx =>
    {
        // Requires the following import:
        // using Microsoft.AspNetCore.Http;
        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=600");
    }
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
