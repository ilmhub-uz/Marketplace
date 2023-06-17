using Marketplace.Common.Options;
using Marketplace.Services.Chat.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Marketplace.Services.Chat.Extensions;

public static class ServiceCollectionExtensions
{
	private static void AddJwt(this IServiceCollection services, IConfiguration configuration)
	{
		var section = configuration.GetSection(nameof(JwtOptions));
		services.Configure<JwtOptions>(section);

		JwtOptions jwtOptions = section.Get<JwtOptions>()!;

		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				var signingKey = System.Text.Encoding.UTF32.GetBytes(jwtOptions.SigningKey);

				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidIssuer = jwtOptions.ValidIssuer,
					ValidAudience = jwtOptions.ValidAudience,
					ValidateIssuer = true,
					ValidateAudience = true,
					IssuerSigningKey = new SymmetricSecurityKey(signingKey),
					ValidateIssuerSigningKey = true,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero
				};

				options.Events = new JwtBearerEvents()
				{
					OnMessageReceived = async context =>
					{
						if (string.IsNullOrEmpty(context.Token))
						{
							//agar requestni headerida 'Authorization'
							//nomi bilan token junatilmasa
							//tokenni requestni querysidan
							//olish

							//barcha routelar uchun
							var accessToken = context.Request.Query["token"];
							context.Token = accessToken;

							// faqat 'hubs' bilan boshlangan routelar uchun
							/*var accessToken = context.Request.Query["access_token"];
			                var path = context.HttpContext.Request.Path;
			                if (!string.IsNullOrEmpty(accessToken) &&
			                    path.StartsWithSegments("/hubs"))
			                {
				                context.Token = accessToken;
			                }*/
						}
					}
				};
			});
	}

	public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddJwt(configuration);

		services.AddHttpContextAccessor();
		services.AddScoped<UserProvider>();
	}
}