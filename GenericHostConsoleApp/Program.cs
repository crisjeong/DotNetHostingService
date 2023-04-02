using GenericHostConsoleApp.InjectableServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace GenericHostConsoleApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                  // 최소 지정 로그 레벨 : Info 레벨 이상 로그를 기록한다는 의미
                  .MinimumLevel.Information()
                  // 콘솔에도 내용을 남김
                  .WriteTo.Console()
                  //파일로 기록할 로그 파일명을 입력
                  .WriteTo.File(@".\log\log.txt", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
                  .CreateLogger();


            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<WorkService>();
                    services.Scan(scan => scan
                                        .FromAssemblyOf<ITransientService>()
                                        .AddClasses(classes => classes.AssignableTo<ITransientService>())
                                        .AsSelfWithInterfaces()
                                        .WithTransientLifetime()
                                        .AddClasses(classes => classes.AssignableTo<IScopedService>())
                                        .AsSelfWithInterfaces()
                                        .WithScopedLifetime()
                                        .AddClasses(classes => classes.AssignableTo<ISingletonSerivce>())
                                        .AsSelfWithInterfaces()
                                        .WithSingletonLifetime()
                    );
                })
                .ConfigureAppConfiguration((hostingContext, configuration) =>
                {
                    configuration.Sources.Clear();
                    IHostEnvironment env = hostingContext.HostingEnvironment;

                    configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .UseConsoleLifetime()
                .UseSerilog()
                .Build();

            await host.RunAsync();

            Log.CloseAndFlush();
        }
    }
}