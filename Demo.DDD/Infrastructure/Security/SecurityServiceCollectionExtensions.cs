using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Demo.DDD.Security
{
    public static class SecurityServiceCollectionExtensions
    {
        public static void ConfigureSecurity(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, ScopeAuthorizationHandler>();

            AuthenticationBuilder builder = services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                });

            builder.AddJwtBearer(x =>
            {
                x.SecurityTokenValidators.Clear();
                x.SecurityTokenValidators.Add(new JwtSecurityTokenHandler());
                x.Authority = "https://iam-test.danskenet.net/";
                x.SaveToken = true;

                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://iam-test.danskenet.net/",
                    ValidateAudience = false,
                    ValidateLifetime = true
                };

                x.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = async context =>
                    {
                        context.HttpContext.User = context.Principal;
                    }
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ReadPolicy.PolicyName,
                    builder =>
                    {
                        builder.RequireAuthenticatedUser();
                        builder.AddRequirements(new ScopesRequirement(new List<string> { { ReadPolicy.AdGroup } }));
                    });
                options.AddPolicy(WritePolicy.PolicyName,
                    builder =>
                    {
                        builder.RequireAuthenticatedUser();
                        builder.AddRequirements(new ScopesRequirement(new List<string> { { WritePolicy.AdGroup } }));
                    });
            });
        }
    }
}