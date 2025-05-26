using BookSale.Managerment.DataAccess.Configuration;
using BookSale.Managerment.DataAccess;
using Microsoft.Extensions.Options;
using BookSale.Managerment.Ui.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Authorization;
using BookSale.Managerment.Domain.constants;
var builder = WebApplication.CreateBuilder(args);

// Đăng ký và config Database
builder.Services.RegisterDb(builder.Configuration);

// Thêm Dependency Injection
builder.Services.AddDependencyInjection();

// Đăng ký AutoMapper
builder.Services.AddAutoMapperConfiguration();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Đăng ký Razor Pages, thêm Session State Temp Data Provider để lưu trữ dữ liệu tạm thời trong phiên làm việc.
builder.Services.AddRazorPages()
                .AddSessionStateTempDataProvider();

// Đăng ký dịch vụ Razor Runtime Compilation.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Đăng ký dịch vụ MVC với localization.
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

// Đăng ký dịch vụ Authorization.
builder.Services.SetAuthorization();

// Thêm Anti Forgery Header
builder.Services.AddAntiForgery();

// Cấu hình Localization
builder.Services.LocalizationConfiguration();

// Đăng ký session state.
builder.Services.AddSession(options =>
{
    // Thiết lập thời gian chờ tối đa của một phiên làm việc.
    options.IdleTimeout = TimeSpan.FromSeconds(60);
});
builder.Services.AddScoped<UserInfoFilter>();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<UserInfoFilter>(); // Apply globally
});
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
    // Giá trị HSTS mặc định là 30 ngày.Bạn có thể muốn thay đổi điều này cho các kịch bản sản xuất, xem thêm tại https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = ctx =>
    {
        // Yêu cầu nhập thư viện sau:
        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=600");
    }
});

app.UseRouting();

// Sử dụng localization
var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
if (localizationOptions != null)
{
    app.UseRequestLocalization(localizationOptions.Value);
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .RequireAuthorization(Setup.AuthorizedAdminPolicy);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Sử dụng session.
app.UseSession();

await app.RunAsync();
