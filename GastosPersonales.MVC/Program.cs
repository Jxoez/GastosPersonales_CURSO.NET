using GastosPersonales.Consumer;
using GastosPersonales.Modelos;
using Microsoft.EntityFrameworkCore;

CRUD<Categoria>.Endpoint = "https://localhost:7292/api/Categorias";
CRUD<Movimiento>.Endpoint = "https://localhost:7292/api/Movimientos";
CRUD<Presupuesto>.Endpoint = "https://localhost:7292/api/Presupuestos";
CRUD<Usuario>.Endpoint = "https://localhost:7292/api/Usuarios";


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GastosPersonalesAPIContext") ?? throw new InvalidOperationException("Connection string 'GastosPersonalesAPIContext' not found.");

builder.Services.AddDbContext<GastosPersonalesAPIContext>(options => options.UseNpgsql(connectionString));


// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
