using AppLogin.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebHotel_vesion1._0.Models;
using WebHotel_vesion1._0.Repositories.Implementation;
using WebHotel_vesion1._0.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
 .AddCookie(option =>
 {

     option.LoginPath = "/Acceso/Login";
     option.ExpireTimeSpan = TimeSpan.FromMinutes(15);
     option.AccessDeniedPath = "/Home/Privacy";

 }
 );

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionPorDefecto")));

builder.Services.AddScoped<IUsuario, UsuarioRepositorio>();
builder.Services.AddScoped<IRol,RolRepositorio>();
builder.Services.AddScoped<IUsuarioRol,UsuarioRolRepositorio>();
builder.Services.AddScoped<IHabitacion, HabitacionRepositorio>();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.Run();
