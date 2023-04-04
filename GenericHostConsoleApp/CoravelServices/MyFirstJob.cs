using Coravel.Invocable;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenericHostConsoleApp
{
    public class MyFirstJob : Coravel.Invocable.ICancellableInvocable, IInvocable
    {
        private readonly ILogger<MyFirstJob> _logger;
        public CancellationToken CancellationToken { get; set; }


        public MyFirstJob(ILogger<MyFirstJob> logger)
        {
            this._logger = logger;
            this.CancellationToken = new CancellationToken();
        }


        public async Task Invoke()
        {
            int length = 100;
            for (int i = 0; i < length; i++)
            {
                if (CancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning($"IsCancellationRequested= {CancellationToken.IsCancellationRequested}");
                    break;
                }                    

                _logger.LogInformation($"[{i+1}]MyFirstJob Works fine... my first schedule job.......!!! {DateTime.Now.ToString()}");
                await Task.Delay(100);
            }            

            await Task.FromResult(true);
        }
    }
}
