using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using test_app.Data;
using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

// Configuración de conexión a MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 25))));

// ✅ Configurar la autenticación con esquema consistente
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "UserLoginCookie";
        options.LoginPath = "/Login";  // Redirige si no está autenticado
        options.AccessDeniedPath = "/AccessDenied"; 
    });

builder.Services.AddAuthorization();
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Habilitar la autenticación y autorización en orden correcto
app.UseAuthentication();  // ✅ Primero autenticación
app.UseAuthorization();   // ✅ Luego autorización

app.MapRazorPages();
app.Run();


