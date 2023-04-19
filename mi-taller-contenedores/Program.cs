using AutoMapper;
using DB.Command.Interfaces;
using mi_taller_contenedores;
using mi_taller_contenedores.ApiModels.MappingProfiles;
using mi_taller_contenedores.DB;
using mi_taller_contenedores.DB.Command;
using mi_taller_contenedores.DB.Query;
using mi_taller_contenedores.DB.Query.Interfaces;
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
builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();
builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
builder.Services.AddTransient<Seeder>();

builder.Services.Scan(selector =>
{
    selector.FromAssemblyOf<IQueryDispatcher>()
            .AddClasses(filter =>
            {
                filter.AssignableTo(typeof(IQueryHandler<,>));
            })
            .AsImplementedInterfaces()
            .WithScopedLifetime();
});
builder.Services.Scan(selector =>
{
    selector.FromAssemblyOf<ICommandDispatcher>()
            .AddClasses(filter =>
            {
                filter.AssignableTo(typeof(ICommandHandler<,>));
            })
            .AsImplementedInterfaces()
            .WithScopedLifetime();
});
var app = builder.Build();
await SeedDataAsync(app);
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

async Task SeedDataAsync(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seeder>();
        await service.SeedAsync();
    }
}