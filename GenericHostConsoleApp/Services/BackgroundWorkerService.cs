﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericHostConsoleApp
{
    public class BackgroundWorkerService : BackgroundService
    {
        private readonly ILogger<BackgroundWorkerService> _logger;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger,
            IHostApplicationLifetime applicationLifetime)
        {
            this._logger = logger;
            this._applicationLifetime = applicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var count = 1;
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"BackgroundWorkerService running at: {DateTimeOffset.Now}");
                await Task.Delay(1000, stoppingToken);

                count++;

                if (count > 10)
                {
                    _applicationLifetime.StopApplication();
                }
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StopAsync");
            return Task.CompletedTask;
        }
    }
}