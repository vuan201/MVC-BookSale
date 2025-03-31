using BookSale.Managerment.DataAccess.Configuration;
using BookSale.Managerment.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký db
builder.Services.RegisterDb(builder.Configuration);
builder.Services.AddDependencyInjection();
builder.Services.AddAutoMapperConfiguration();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Đăng ký Razor Pages, thêm Session State Temp Data Provider để lưu trữ dữ liệu tạm thời trong phiên làm việc.
builder.Services.AddRazorPages()
                .AddSessionStateTempDataProvider();

// Đăng ký dịch vụ Razor Runtime Compilation.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Đăng ký dịch vụ MVC.
builder.Services.AddControllersWithViews();

// Đăng ký session state.
builder.Services.AddSession(options => {
    // Thiết lập thời gian chờ tối đa của một phiên làm việc.
    options.IdleTimeout = TimeSpan.FromSeconds(60);
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
        // using Microsoft.AspNetCore.Http;
        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=600");
    }
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Sử dụng session.
app.UseSession();

app.Run();
