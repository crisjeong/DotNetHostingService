using Coravel.Invocable;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenericHostConsoleApp
{
    public class MyFirstJob : IInvocable
    {
        private readonly ILogger<MyFirstJob> _logger;

        public MyFirstJob(ILogger<MyFirstJob> logger)
        {
            this._logger = logger;
        }

        public async Task Invoke()
        {
            _logger.LogInformation($"Works fine... my first schedule job.......!!! {DateTime.Now.ToString()}");

            await Task.FromResult(true);
        }
    }
}
