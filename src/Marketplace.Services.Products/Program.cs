using Marketplace.Common.Extensions;
using Marketplace.Common.Loggers;
using Marketplace.Services.Chat.Middleware;
using Marketplace.Services.Products.Managers;
using Marketplace.Services.Products.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = CustomLogger
    .WriteLogToFileSendToTelegram(builder.Configuration, "ProductLogger.txt");

builder.Logging.AddSerilog(logger);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddSwaggerGenWithToken();

builder.Services.AddScoped<ProductManager>();
builder.Services.AddScoped<CategoryManager>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

/*builder.Services.AddScoped<IProductRepository, ProductMongodbRepository>();*/

var app = builder.Build();

app.UseProductChatErrorMiddleware();
app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
