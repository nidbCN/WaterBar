using System.Text.Json;
using Microsoft.Extensions.Options;
using WaterBar.Core.Models;
using WaterBar.Core.Options;
using WaterBar.Core.Services.Components;

namespace WaterBar.Core.Services;

public class StatusService : IStatusService
{
    private readonly ILogger<IStatusService> _logger;
    private readonly FactoryService _factory;
    private readonly StatusBarOption _option;

    public StatusService(ILogger<IStatusService> logger, FactoryService factory, IOptions<StatusBarOption> options)
    {
        (_logger, _factory, _option) = (logger, factory, options.Value);
    }

    public void StartOutput()
    {
        Console.WriteLine(JsonSerializer.Serialize(new
        {
            version = 1
        }));

        Console.WriteLine("[");


        var servicesList = _option.Display.Select(item =>
            _factory.GetComponentService(item, _option.Interval)
        ).ToArray();

        while (true)
        {
            Console.Write(JsonSerializer.Serialize(
                servicesList.Select(
                    service => new StatusItem(service.FormatString())
                )
            ));
            Console.Write(",\n");

            Thread.Sleep(TimeSpan.FromSeconds(_option.Interval));
        }
    }
}