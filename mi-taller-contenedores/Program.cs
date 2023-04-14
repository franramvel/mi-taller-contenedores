using AutoMapper;
using mi_taller_contenedores.ApiModels.MappingProfiles;
using mi_taller_contenedores.DB;
using mi_taller_contenedores.Servicios.API;
using mi_taller_contenedores.Servicios.Genericos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MainDbContext>(cfg =>
{ cfg.UseInMemoryDatabase("InMemoryAppDb"); }
);
//Mapper
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AllowNullDestinationValues = true;
    cfg.AddProfile(new MainMappingProfile());
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IFileManagementService, FileManagementService>();
builder.Services.AddScoped<IFacturaServices, FacturaServices>();
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
