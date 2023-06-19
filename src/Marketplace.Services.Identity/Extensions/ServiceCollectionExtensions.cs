using Marketplace.Common.Extensions;
using Marketplace.Services.Identity.Context;
using Marketplace.Services.Identity.Managers;
using Marketplace.Services.Identity.Providers;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Identity.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddJwtConfiguration(configuration);
        services.AddScoped<JwtTokenManager>();
		services.AddScoped<UserManager>();

		services.AddHttpContextAccessor();
		services.AddScoped<UserProvider>();
	}

	public static void MigrateIdentityDb(this WebApplication app)
	{
		if (app.Services.GetService<IdentityDbContext>() != null)
		{
			var identityDb = app.Services.GetRequiredService<IdentityDbContext>();
			identityDb.Database.Migrate();
		}
	}
}