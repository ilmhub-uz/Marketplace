﻿using System.Text;
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
}