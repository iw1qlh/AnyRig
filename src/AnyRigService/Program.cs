using AnyRigService;
using AnyRigService.Services;

static class Program
{

    //PS> sc.exe create "AnyRigService" binpath="src\AnyRigService\bin\Debug\net6.0\win-x64\AnyRigService.exe"

    static async Task Main(string[] args)
    {
        IHost host = Host.CreateDefaultBuilder(args)
        .UseWindowsService(options =>
        {
            options.ServiceName = "AnyRigService";
        })
        .ConfigureServices(services =>
        {
            services
            .AddSingleton<IRigsMachine, RigsMachine>()

            .AddHostedService<SocketWorker>()
            .AddHostedService<PipeWorker>()
            .AddHostedService<WebSocketWorker>()
            ;
        })
        //.UseConsoleLifetime()
        .Build();

        await host.RunAsync();
    }
}
