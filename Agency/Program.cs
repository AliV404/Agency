using Agency.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbcontext>(opt =>
{
    opt.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]);
});

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area=exists}/{controller=home}/{action=index}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}"
    );


app.Run();
