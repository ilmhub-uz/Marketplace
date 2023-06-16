using Marketplace.Common.Extensions;
using Marketplace.Common.Loggers;
using Marketplace.Services.Identity.Context;
using Marketplace.Services.Identity.Extensions;
using Marketplace.Services.Identity.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


var logger = CustomLogger
    .WriteLogToFileSendToTelegram(builder.Configuration, "IdentityLogger.txt");

builder.Logging.AddSerilog(logger);

builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithToken();


builder.Services.AddDbContext<IdentityDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityDb"));
});
builder.Services.AddIdentity(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(cors =>
{
	cors.AllowAnyHeader()
		.AllowAnyMethod()
		.AllowAnyOrigin();
});

app.UseIdentityErrorHandlerMiddleware();

app.MigrateIdentityDb();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
