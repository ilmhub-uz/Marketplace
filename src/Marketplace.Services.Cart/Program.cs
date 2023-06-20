using Marketplace.Common.Extensions;
using Marketplace.Services.Cart.Providers;
using Marketplace.Services.Cart.Services.CartServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddJwtConfiguration(builder.Configuration);


builder.Services.AddSwaggerGenWithToken();

builder.Services.AddScoped<UserProvider>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "cache";
    options.InstanceName = "CartInstance";

});


var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseCors(cors =>
{
    cors.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
