using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using baithi.Data;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ MVC
builder.Services.AddControllersWithViews();

// Đăng ký ComicSystemMemoryDbContext làm dịch vụ singleton
builder.Services.AddSingleton<ComicSystemMemoryDbContext>();

var app = builder.Build();

// Cấu hình middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Định tuyến mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
