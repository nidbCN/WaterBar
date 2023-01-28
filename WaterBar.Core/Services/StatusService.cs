using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using WaterBar.Core.Models;
using WaterBar.Core.Options;
using WaterBar.Core.Services.Components;

namespace WaterBar.Core.Services;

public class StatusService : IStatusService
{
    private readonly FactoryService _factory;
    private readonly StatusBarOption _option;
    private readonly JsonSerializerOptions _serializerOptions;

    public StatusService(FactoryService factory, IOptions<StatusBarOption> options)
        => (_factory, _option, _serializerOptions) = (factory, options.Value, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });

    public async Task StartOutput()
    {
        Console.WriteLine(JsonSerializer.Serialize(new
        {
            version = 1
        }));

        Console.WriteLine("[");

        var serviceAndItemList =
            _option.Display.Select(item => new
                {
                    Service = _factory.GetComponentService(item),
                    Status = StatusItem.FromOptionItem(item)
                }
            ).ToArray();

        while (true)
        {
            foreach (var item in serviceAndItemList)
            {
                item.Status.FullText = await item.Service.FormatStringAsync();
            }

            Console.Write(JsonSerializer.Serialize(
                serviceAndItemList.Select(item => item.Status), _serializerOptions)
            );

            Console.Write(",\n");

            Thread.Sleep(TimeSpan.FromSeconds(_option.Interval));
        }

        return;
    }
}