using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Framework.Logger
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LogMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<LogMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch totalTime = Stopwatch.StartNew();
            Exception exception = null;
            try
            {
                _logger.LogInformation("Start LogMiddleware");
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                totalTime.Stop();
            }

            if (exception == null)
            {
                _logger.LogInformation($"Middleware ok {totalTime.ElapsedMilliseconds} ms");
            }
            else
            {
                _logger.LogError($"Middleware error {totalTime.ElapsedMilliseconds} ms {exception.Message}");

                // rethrow exception;
                throw exception;
            }
        }
    }
}
