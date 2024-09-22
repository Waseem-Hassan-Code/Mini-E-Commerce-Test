using DAL.Services;
using DAL.Services.IServices;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DAL.ApplicationDb.AppDbContext>(options =>
{
    options.UseSqlite($"Data Source={AppDomain.CurrentDomain.BaseDirectory}{"ProductsDatabase"}");
});

builder.Services.AddControllers();

builder.Services.AddScoped<IProductsData,ProductsData>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
