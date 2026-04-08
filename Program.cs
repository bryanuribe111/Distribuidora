using DistribuidoraAseo.Data;
using DistribuidoraAseo.DAO.Interfaces;
using DistribuidoraAseo.DAO.Implementations;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddSingleton<DatabaseConnection>();

builder.Services.AddScoped<IRolDAO, RolDAO>();
builder.Services.AddScoped<IUsuarioDAO, UsuarioDAO>();
builder.Services.AddScoped<IClienteDAO, ClienteDAO>();
builder.Services.AddScoped<IProductoDAO, ProductoDAO>();
builder.Services.AddScoped<IMaterialDAO, MaterialDAO>();
builder.Services.AddScoped<IComposicionProductoDAO, ComposicionProductoDAO>();
builder.Services.AddScoped<IPedidoDAO, PedidoDAO>();
builder.Services.AddScoped<IDetallePedidoDAO, DetallePedidoDAO>();
builder.Services.AddScoped<ICompraDAO, CompraDAO>();
builder.Services.AddScoped<IDetalleCompraDAO, DetalleCompraDAO>();
builder.Services.AddScoped<IPreciosEspecialesDAO, PreciosEspecialesDAO>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
