using Marketplace.Services.Organizations.Context;
using Marketplace.Services.Organizations.Extensions;
using Marketplace.Services.Organizations.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Description = "JWT Bearer. : \"Authorization: Bearer { token } \"",
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

builder.Services.AddDbContext<OrganizationsDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("OrganizationsDbContext"));
});
builder.Services.AddIdentity(builder.Configuration);
builder.Services.AddScoped<OrganizationManager>();
builder.Services.AddScoped<OrganizationUserManager>();

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
app.MigrateOrganizationDbContext();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
