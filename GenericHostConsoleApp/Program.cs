using Coravel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace GenericHostConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                  // 최소 지정 로그 레벨 : Info 레벨 이상 로그를 기록한다는 의미
                  .MinimumLevel.Information()
                  // 콘솔에도 내용을 남김
                  .WriteTo.Console()
                  //파일로 기록할 로그 파일명을 입력
                  .WriteTo.File(@".\log\log.txt", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
                  .CreateLogger();

            try
            {
                Log.Information("Starting service..");

                var host = CreateHostBuilder(args).Build();
                host.Services.UseScheduler(scheduler =>
                {
                    var jobSchedule = scheduler.Schedule<MyFirstJob>();
                    jobSchedule
                        .EverySeconds(2)                        
                        .PreventOverlapping("MyFirstJob");
                });

                host.Run();
            }
            catch (System.Exception ex)
            {
                Log.Fatal(ex, "Exception in application");
            }
            finally
            {
                Log.Information("Exiting service");
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureServices((hostContext, services) =>
                {

                    services.AddScheduler();
                    services.AddTransient<MyFirstJob>();

                    services.AddHostedService<WorkService>();
                    services.AddHostedService<BackgroundWorkerService>();
                });

        }
       
    }   
}