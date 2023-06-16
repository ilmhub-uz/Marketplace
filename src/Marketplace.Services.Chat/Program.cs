using Marketplace.Common.Extensions;
using Marketplace.Common.Loggers;
using Marketplace.Services.Chat.Context;
using Marketplace.Services.Chat.Extensions;
using Marketplace.Services.Chat.Hubs;
using Marketplace.Services.Chat.Managers;
using Marketplace.Services.Chat.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = CustomLogger
    .WriteLogToFileSendToTelegram(builder.Configuration, "ChatLogger.txt");

builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithToken();


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<ChatDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("ChatDb"));
});

builder.Services.AddScoped<ConversationManager>();

builder.Services.AddSignalR();
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

app.UseChatErrorHandlerMiddleware();
app.MigrateChatDbContext();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<ConversationHub>("/hubs/conversation");

app.MapControllers();

app.Run();