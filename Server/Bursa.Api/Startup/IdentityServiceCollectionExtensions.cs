
using System;
using System.Threading.Tasks;
using Bursa.Data;
using Bursa.Api.Models;
using Bursa.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Bursa.Api
{

///
    public static class IdentityServiceCollectionExtensions
    {
        ///
        public static IServiceCollection AddCustomizedIdentity(this IServiceCollection services, IHostingEnvironment env)
        {
            services.AddTransient<IUserStore<ApplicationUser>, Bursa.Api.Data.SQLite.UserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, Bursa.Api.Data.SQLite.RoleStore>();
            
            services.AddTransient<IPasswordHasher<ApplicationUser>, CustomPasswordHasher>();
            services.AddTransient<ILookupNormalizer, CustomNormalizer>();
           
            services.AddIdentity<ApplicationUser, ApplicationRole>();
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;                
            })
            .AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,                            
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "Kassadube.Security.Bearer",
                            ValidAudience = "Kassadube.Security.Bearer",
                            IssuerSigningKey = JwtSecurityKey.Create("Kassadube-secret-key")
                        };

                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = context =>
                            {
                                Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                                return Task.CompletedTask;
                            }
                        };
                }); 
            services.AddTransient<IEmailSender, EmailSender>();           
            return services;
        }
    
        
    }
}