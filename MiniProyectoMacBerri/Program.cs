using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MiniProyectoMacBerri.Models;
using MiniProyectoMacBerri.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Inyección de dependencias del DBContextt
builder.Services.AddDbContext<MacberriprojectContext>(options =>
{
    //Configurando la cadena de conexión desde appsettings
    options.UseSqlServer(builder.Configuration.GetConnectionString("MacBerriContext"));
});

//Configuración de la authentificación
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    option =>
    {
        option.LoginPath = "/Home/Login";
        option.AccessDeniedPath = "/Home/Index";
    }
);

//Registrando servicios
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<IRoleService, RoleServiceImpl>();
builder.Services.AddScoped<IProductService, ProductServiceImpl>();
builder.Services.AddScoped<IServiceService, ServiceServiceImpl>();
builder.Services.AddScoped<ICartServices, CartServiceImpl>();

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

//Authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
