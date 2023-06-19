using Marketplace.Common.Extensions;
using Marketplace.Services.Organizations.Providers;

namespace Marketplace.Services.Organizations.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
	{
        services.AddJwtConfiguration(configuration);
        services.AddHttpContextAccessor();
		services.AddScoped<UserProvider>();
	}
}