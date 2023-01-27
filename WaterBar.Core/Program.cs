using WaterBar.Core.Options;
using WaterBar.Core.Services;
using WaterBar.Core.Services.Components;

#nullable disable
IConfiguration configuration = null;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config => { configuration = config.Build(); })
    .ConfigureServices(services =>
    {
        // services.AddLogging();
        services.Configure<StatusBarOption>(
            configuration.GetSection(nameof(StatusBarOption))
        );

        services.AddSingleton<IStatusService, StatusService>();
        services.AddSingleton<FactoryService>();
    })
    .Build();

var statusService = host.Services.GetRequiredService<IStatusService>();

await statusService.StartOutput();

host.Run();
#nullable restore