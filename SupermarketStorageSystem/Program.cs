using Microsoft.EntityFrameworkCore;
using SupermarketStorageSystem.Applications;
using SupermarketStorageSystem.Applications.Services;
using SupermarketStorageSystem.Gates;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IMappingService, MappingService>();
builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<InventoryDbContext>());
builder.Services.AddScoped<IInventoryRepository, SqlInventoryRepository>();
builder.Services.AddScoped<IInventoryService, InventoryService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();