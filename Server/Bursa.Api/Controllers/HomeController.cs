using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Bursa.Api.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Bursa.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment env;
        private readonly IDistributedCache cache;
        private readonly ILogger logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        /// <param name="cache"></param>
        /// <param name="logger"></param>
        public HomeController(IConfiguration configuration, IHostingEnvironment env, IDistributedCache cache, ILogger<HomeController> logger)
        {
            this.configuration = configuration;
            this.env = env;
            this.cache = cache;
            this.logger = logger;
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            logger.LogTrace("LOG FROM HOME");
            logger.LogDebug("LOG FROM HOME");
            logger.LogInformation("LOG FROM HOME");
            logger.LogWarning("LOG FROM HOME");
            logger.LogError("LOG FROM HOME");
            logger.LogCritical("LOG FROM HOME");

            var helloRedis = Encoding.UTF8.GetBytes("Hello Redis");
            HttpContext.Session.Set("hellokey", helloRedis);
            var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}");
            var url = location.AbsoluteUri;
            string conn = configuration["ConnectionStrings:SqliteConnection"];
            var envName = env.EnvironmentName;
            var value = cache.Get("lastServerStartTime");
            var startTimeString = "";
            if (value != null)
            {
                startTimeString = Encoding.UTF8.GetString(value);
            }

            return Json(new
            {
                id = "0.1.3-02",
                url = url,
                conn = conn,
                env = envName,
                startTimeString = startTimeString
            });
        }

        /// <summary>
        /// description for swagger
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IActionResult Index2()
        {
            byte[] helloRedis;
            HttpContext.Session.TryGetValue("hellokey", out helloRedis);
            var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}");
            var url = location.AbsoluteUri;
            string conn = configuration["ConnectionStrings:SqliteConnection"];
            var envName = env.EnvironmentName;
            var value = cache.Get("lastServerStartTime");
            var startTimeString = "";
            if (value != null)
            {
                startTimeString = Encoding.UTF8.GetString(value);
            }

            return Json(new
            {
                id = 3,
                url = url,
                conn = conn,
                env = envName,
                session =helloRedis ==null ? "session empty": Encoding.UTF8.GetString(helloRedis)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index3()
        {
            return Json(new
            {
                id = 3
            });
        }
        

        }
}
