using Microsoft.EntityFrameworkCore;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Services;
using DistribuidoraAseo.Services.Interfaces;
using DistribuidoraAseo.DAO.Interfaces;
using DistribuidoraAseo.DAO.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DatabaseConnection>(); 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    ));

builder.Services.AddScoped<IClienteDAO, ClienteDAOEF>();
builder.Services.AddScoped<IRolDAO, RolDAO>();
builder.Services.AddScoped<IUsuarioDAO, UsuarioDAO>();
builder.Services.AddScoped<IProductoDAO, ProductoDAOEF>();
builder.Services.AddScoped<IMaterialDAO, MaterialDAO>();
builder.Services.AddScoped<IComposicionProductoDAO, ComposicionProductoDAO>();
builder.Services.AddScoped<IPedidoDAO, PedidoDAO>();
builder.Services.AddScoped<IDetallePedidoDAO, DetallePedidoDAO>();
builder.Services.AddScoped<ICompraDAO, CompraDAO>();
builder.Services.AddScoped<IDetalleCompraDAO, DetalleCompraDAO>();
builder.Services.AddScoped<IPreciosEspecialesDAO, PreciosEspecialesDAO>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

var app = builder.Build();

app.UseCors("PermitirFrontend");

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();