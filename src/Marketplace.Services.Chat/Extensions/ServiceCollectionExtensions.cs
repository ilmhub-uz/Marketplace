using Marketplace.Common.Extensions;
using Marketplace.Services.Chat.Providers;

namespace Marketplace.Services.Chat.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddJwtConfiguration(configuration);

		services.AddHttpContextAccessor();
		services.AddScoped<UserProvider>();
	}
}