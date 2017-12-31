
using System;
using System.Threading.Tasks;
using Bursa.Api.Data;
using Bursa.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Bursa.Api
{

///
    public static class ServiceCollectionExtensions
    {
        ///
        public static IServiceCollection AddCustomizedMVC(this IServiceCollection services)
        {
            services.AddResponseCompression();
            services.AddMvc();           
            services.AddSession();               
            return services;
        }  
    }
}