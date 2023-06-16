using System.Text;
using Marketplace.Common.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Marketplace.Common.Extensions;

public static class ExtensionAddJwtConfiguration
{
    public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(nameof(JwtOptions));
        services.Configure<JwtOptions>(section);

        var jwtOptions = section.Get<JwtOptions>();

        var signinKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(jwtOptions!.SecretKey));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtOptions.ValidIssuer,
                    ValidAudience = jwtOptions.ValidAudience,
                    IssuerSigningKey = signinKey,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                      /* Agar requestni headerida 'Authorization'
                            nomi bilan token junatilmasa
                            tokenni requestni querysidan olish */

                        if (string.IsNullOrEmpty(context.Token))
                        {
                            var accessToken = context.Request.Query["token"];
                            context.Token = accessToken;
                        }

                        // Faqat 'hubs' bilan boshlangan routelar uchun
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(context.Token) && path.StartsWithSegments("/hubs"))
                        {
                            var accessToken = context.Request.Query["token"];
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

    }
}