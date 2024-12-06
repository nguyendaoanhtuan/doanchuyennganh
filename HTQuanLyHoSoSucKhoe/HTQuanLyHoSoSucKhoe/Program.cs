using HTQuanLyHoSoSucKhoe.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "AuthCookie";
        options.LoginPath = "/DangNhap";  // Đường dẫn chung cho tất cả
        options.AccessDeniedPath = "/AccessDenied";  // Trang truy cập bị từ chối
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
});
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Điều hướng trước khi vào Home/Index
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/" || context.Request.Path == "/Home/Index")
    {
        // Kiểm tra người dùng đã đăng nhập chưa
        if (!context.User.Identity.IsAuthenticated)
        {
            context.Response.Redirect("/DangNhap");
            return;
        }

        // Điều hướng theo role
        if (context.User.IsInRole("Admin"))
        {
            context.Response.Redirect("/QuanLyBenhVien");
            return;
        }

        if (context.User.IsInRole("User"))
        {
            context.Response.Redirect("/Home");
            return;
        }

        // Nếu không có role, quay lại trang đăng nhập
        context.Response.Redirect("/DangNhap");
        return;
    }

    await next();
});

app.MapControllerRoute(
    name: "quanlybenhvien",
    pattern: "QuanLyBenhVien/{action=Index}/{id?}",
    defaults: new { controller = "QuanLyBenhVien", action = "Index" }) .RequireAuthorization("Admin");;



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "login",
    pattern: "DangNhap",
    defaults: new { controller = "Users", action = "Login" });

app.Run();
