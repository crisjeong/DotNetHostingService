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
    // Coravel's Scheduling service is attempting to close but there are tasks still running.
    // App closing (in background) will be prevented until all tasks are completed.
    public class MyCorabelService : Coravel.Invocable.ICancellableInvocable, IInvocable
    {        
        private readonly ILogger<MyCorabelService> _logger;        
        public CancellationToken CancellationToken { get; set; }

        public MyCorabelService(ILogger<MyCorabelService> logger)
        {
            this._logger = logger;            
            this.CancellationToken = new CancellationToken();            
        }

        public async Task Invoke()
        {
            int length = 100;
            for (int i = 0; i < length; i++)
            {
                //application shutdown 시 IsCancellationRequested 값은 true 가 된다.
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
