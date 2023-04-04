using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericHostConsoleApp
{
    public sealed class WorkService : IHostedService
    {
        private ILogger<WorkService> _logger;
        private readonly IConfiguration _configuration;

        public WorkService(ILogger<WorkService> logger, 
            IHostApplicationLifetime applicationLifetime,
            IConfiguration configuration) 
        {
            _logger = logger;
            _configuration = configuration;

            applicationLifetime.ApplicationStarted.Register(OnStarted);
            applicationLifetime.ApplicationStopping.Register(OnStopping);
            applicationLifetime.ApplicationStopped.Register(OnStopped);
        }

        private void OnStarted()
        {
            _logger.LogInformation("2.OnStarted");
            var name = _configuration.GetSection("User").GetValue<string>("Name");
            var age = _configuration.GetSection("User").GetValue<int>("Age");
            _logger.LogInformation($"2.1 Name= {name}, Age= {age}");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("1.StartAsync");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("4.StopAsync");
            return Task.CompletedTask;
        }

        private void OnStopping()
        {
            _logger.LogInformation("3.OnStopping");
        }

        private void OnStopped()
        {
            _logger.LogInformation("5.OnStopped");
        }
    }
}
