using Marketplace.Services.Chat.Context;
using Marketplace.Services.Chat.Extensions;
using Marketplace.Services.Chat.Hubs;
using Marketplace.Services.Chat.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Description = "JWT Bearer. : \"Authorization: Bearer { token }\"",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[]{}
		}
	});
});

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

app.MigrateChatDbContext();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<ConversationHub>("/hubs/conversation");

app.MapControllers();

app.Run();