using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bursa.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class TimerMiddleware
    {
        private readonly ILogger<TimerMiddleware> logger;
        private readonly RequestDelegate next;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="next"></param>
        public TimerMiddleware(ILogger<TimerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();
            // context.Items["CorrelationId"] = Guid.NewGuid().ToString();
            logger.LogInformation($"About to start {context.Request.Method} {context.Request.GetDisplayUrl()} request");

            await next(context);
            sw.Stop();
            logger.LogInformation($"Request completed with status code: {context.Response.StatusCode} timed {sw.ElapsedMilliseconds} Milliseconds");
        }
    }
}
