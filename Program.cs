using DeliveryYaBackend.Data;
using DeliveryYaBackend.Services;
using DeliveryYaBackend.Services.Interfaces;
using DeliveryYaBackend.Services.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DeliveryYaBackend.Repositories.Interfaces;
using DeliveryYaBackend.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
        };
    });

builder.Services.AddAuthorization();



// Configurar DbContext con MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 43))
    ));

// Registrar el Generic Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAdminComercioService, AdminComercioService>();
//builder.Services.AddScoped<IAdminCategoriaService, AdminCategoriaService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
//builder.Services.AddScoped<IRepartidorService, RepartidorService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IComercioService, ComercioService>();
builder.Services.AddScoped<IComercioCategoriaService, ComercioCategoriaService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ICategoriaProductoService, CategoriaProductoService>();
builder.Services.AddScoped<IComercioHorariosService, ComercioHorariosService>();
//builder.Services.AddScoped<IStockService, StockService>();
//builder.Services.AddScoped<ITarifaRepartidorService, TarifaRepartidorService>();

builder.Services.AddScoped<IItemPedidoService, ItemPedidoService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<JwtService>();

builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
//builder.Services.AddScoped<IRepartidorRepository, RepartidorRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IComercioRepository, ComercioRepository>();
builder.Services.AddScoped<IComercioCategoriaRepository, ComercioCategoriaRepository>();
builder.Services.AddScoped<IComercioHorariosRepository, ComercioHorariosRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaProductoRepository, CategoriaProductoRepository>();
//builder.Services.AddScoped<IStockRepository, StockRepository>();
//builder.Services.AddScoped<ITarifaRepartidorRepository, TarifaRepartidorRepository>();
builder.Services.AddScoped<IHorarioRepository, HorarioRepository>();
builder.Services.AddScoped<IItemPedidoRepository, ItemPedidoRepository>();

// Configuración CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Console.WriteLine($"JWT Secret: {builder.Configuration["JwtSettings:SecretKey"]}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();  // COMENTA ESTA LÍNEA TEMPORALMENTE

// ORDEN CORRECTO DE MIDDLEWARES:
app.UseCors("AllowAll");  // PRIMERO: CORS

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Urls.Add("http://localhost:5189");
//app.Urls.Add("http://192.168.1.40:5189"); // Muise IP
app.Urls.Add("http://192.168.1.43:5189"); // Viya IP
app.Urls.Add("http://0.0.0.0:5189");

app.Run();