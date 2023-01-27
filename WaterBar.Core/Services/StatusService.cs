using System.Text.Json;
using Microsoft.Extensions.Options;
using WaterBar.Core.Models;
using WaterBar.Core.Options;
using WaterBar.Core.Services.Components;

namespace WaterBar.Core.Services;

public class StatusService : IStatusService
{
    private readonly FactoryService _factory;
    private readonly StatusBarOption _option;

    public StatusService(FactoryService factory, IOptions<StatusBarOption> options)
    {
        (_factory, _option) = (factory, options.Value);
    }

    public async Task StartOutput()
    {
        Console.WriteLine(JsonSerializer.Serialize(new
        {
            version = 1
        }));

        Console.WriteLine("[");

        var servicesList = _option.Display.Select(item =>
            _factory.GetComponentService(item)
        ).ToArray();

        while (true)
        {
            var taskList = servicesList.Select(
                async service => new StatusItem(await service.FormatStringAsync())
            ).ToArray();

            await Task.WhenAll(taskList);

            Console.Write(JsonSerializer.Serialize(
                taskList.Select(task => task.Result)
            ));
            Console.Write(",\n");

            Thread.Sleep(TimeSpan.FromSeconds(_option.Interval));
        }
    }
}