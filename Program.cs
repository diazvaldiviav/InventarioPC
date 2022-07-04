using System.Collections.Generic;
using ProyectoInventario;
using ProyectoInventario.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlServer<ComputadoraContext>("Data Source=DESKTOP-QE9BDPM\\SQLEXPRESS;Initial Catalog=Inventario;user id=sa;password=Y3n2izzf");
builder.Services.AddScoped<IDiscoDuroService, DiscoDuroService>();
builder.Services.AddScoped<IMemoriaRamService, MemoriaRamService>();
builder.Services.AddScoped<IDisplayService, DisplayService>();
builder.Services.AddScoped<IMicroService, MicroService>();
builder.Services.AddScoped<IMotherBoardService, MotherboardService>();
builder.Services.AddScoped<ITecladoService, TecladoService>();
builder.Services.AddScoped<IComputadoraService, ComputadoraService>();
builder.Services.AddScoped<IImpresoraService, ImpresoraService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
