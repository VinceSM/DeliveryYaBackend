using DeliveryYaBackend.Data;
using DeliveryYaBackend.Data.Repositories;
using DeliveryYaBackend.Data.Repositories.Interfaces;
using DeliveryYaBackend.Services;
using DeliveryYaBackend.Services.Interfaces;
using DeliveryYaBackend.Services.Mapping;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar DbContext con MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 43)) // Versión de tu MySQL
    ));

// Registrar el Generic Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IComercioService, ComercioService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<ITarifaRepartidorService, TarifaRepartidorService>();
builder.Services.AddScoped<IHorarioService, HorarioService>();
builder.Services.AddScoped<IItemPedidoService, ItemPedidoService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var origenesPermitidos = builder.Configuration.GetSection("origenesPermitidos").Get<string[]>()!;

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(opcionesCORS =>
    {
        opcionesCORS.WithOrigins(origenesPermitidos).AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
