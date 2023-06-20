using Marketplace.Common.Extensions;
using Marketplace.Common.Loggers;
using Marketplace.Services.Organizations.Context;
using Marketplace.Services.Organizations.Extensions;
using Marketplace.Services.Organizations.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text.Json.Serialization;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);


var logger = CustomLogger
    .WriteLogToFileSendToTelegram(builder.Configuration, "OrganizationLogger.txt");

builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithToken();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<OrganizationsDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("OrganizationsDbContext"));
});
builder.Services.AddIdentity(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<OrganizationManager>();
builder.Services.AddScoped<OrganizationUserManager>();
builder.Services.AddScoped<IMapper, Mapper>();

var app = builder.Build();

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
