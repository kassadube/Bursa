
using System;
using System.Threading.Tasks;
using Bursa.Api.Data;
using Bursa.Api.Models;
using Bursa.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Bursa.Data;
using Bursa.Model;
using Dapper.FluentMap;

namespace Bursa.Api
{

///
    public static class ServicesExtensions
    {
        ///
        public static IServiceCollection AddServices(this IServiceCollection services, IHostingEnvironment env)
        {
            //if (!(env.IsEnvironment("production") || env.IsEnvironment("QA")))
            //{
            //    services.AddScoped<INotificationRepository, Fleetcore.Data.SQLITE.NotificationRepository>();
            //    services.AddScoped<IAlertRepository, Fleetcore.Data.SQLITE.AlertRepository>();
            //    services.AddScoped<IReminderRepository, Fleetcore.Data.SQLITE.ReminderRepository>();
            //    services.AddScoped<IDictionaryRepository, Fleetcore.Data.SQLITE.DictionaryRepository>();
            //    services.AddScoped<IDTCRepository, Fleetcore.Data.SQLITE.DTCRepository>();
            //    services.AddScoped<IMeasurementRepository, Fleetcore.Data.SQLITE.MeasurementRepository>();
            //}
            //else{
            //    services.AddScoped<INotificationRepository, NotificationRepository>();
            //    services.AddScoped<IAlertRepository, AlertRepository>();
            //    services.AddScoped<IReminderRepository, ReminderRepository>();
            //    services.AddScoped<IDictionaryRepository, DictionaryRepository>();
            //    services.AddScoped<IDTCRepository, DTCRepository>();
            //    services.AddScoped<IMeasurementRepository, MeasurementRepository>();
            //}
            //services.AddScoped<NotificationProxy, NotificationProxy>();
            //services.AddScoped<AlertProxy, AlertProxy>();
            //services.AddScoped<ReminderProxy, ReminderProxy>();
            //services.AddSingleton<DictionaryProxy, DictionaryProxy>();
            //services.AddSingleton<MeasurementProxy, MeasurementProxy>();
            //services.AddScoped<DTCProxy, DTCProxy>();
            return services;
        }

       
        
    }
}