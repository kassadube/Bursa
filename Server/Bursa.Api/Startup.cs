using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Distributed;
using Serilog;
using System.IO;
using System.Text;

namespace Bursa.Api
{
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 
        /// </summary>
        public IHostingEnvironment ENV { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {

            Configuration = configuration;
            ENV = env;
            var logDir = configuration["Serilog:LogDirectory"];
            // Build log to use serilog with console textfile and seq
            //seq should be install in the same server with the app
            //and should be used only in develop
            // Serilog.Events.LogEventLevel loglevel = this.Configuration.GetSection("Serilog:LogLevel").GetValue<Serilog.Events.LogEventLevel>("Default");
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Envirment", env.EnvironmentName)
                //.MinimumLevel.Is(loglevel)
                .WriteTo.Console()
                .WriteTo.RollingFile(Path.Combine(logDir, "log-{Date}.txt"))
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

        }


        /// <summary>
        ///  This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940        
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddLogging(loggingBuilder =>
            {
                // loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                loggingBuilder.AddSerilog(dispose: true);
            });

            services.AddCustomizedIdentity(ENV);
            services.AddServices(ENV);
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });

            //services.AddDistributedRedisCache(options =>
            //{
            //    options.Configuration = Configuration["Caching:Redis:Connection"]; //"PTIL-Dev.redis.cache.windows.net:6379,password=IYkOl41pupliOpUzjsyW/I3ayRLGV3WXwGvt/0TWINM=,ssl=False,abortConnect=False,connectRetry=5";
            //    options.InstanceName = Configuration["Caching:Redis:Name"]; //"redisCache";
            //});

            services.AddCustomizedMVC();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Bursa Core API", Description = "SwaggerCore API Test", Version = "v1" });
                c.OperationFilter<AddAuthTokenHeaderParameter>();
                c.IncludeXmlComments(System.AppDomain.CurrentDomain.BaseDirectory + @"Bursa.api.xml");
            });

        }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="cache"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDistributedCache cache, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //  app.UseMiddleware<TimerMiddleware>();

            app.UseSession();
            var serverStartTimeString = DateTime.Now.ToString();
            byte[] val = Encoding.UTF8.GetBytes(serverStartTimeString);
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(30));
           // cache.Set("lastServerStartTime", val, cacheEntryOptions);

            app.UseResponseCompression();
            app.UseAuthentication();
            app.UseCors("AllowAllOrigins");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                     name: "api",
                    template: "api/{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Fleet Core API");
            });
        }
    }
}
