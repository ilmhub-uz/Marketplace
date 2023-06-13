using Marketplace.Services.Products.Managers;
using Marketplace.Services.Products.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ProductManager>();
builder.Services.AddScoped<CategoryManager>();
builder.Services.AddScoped<ICategoryRepository>();
builder.Services.AddScoped<IProductRepository>();

/*builder.Services.AddScoped<IProductRepository, ProductMongodbRepository>();*/

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
