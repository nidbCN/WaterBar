using WaterBar.Core.Options;

namespace WaterBar.Core.Services.Components;

public class FactoryService
{
    public IComponentService GetComponentService(StatusBarOptionItem optionItem, uint interval)
        => optionItem.Target switch
        {
            "Network" => new NetworkService(optionItem, interval),
            _ => throw new ArgumentOutOfRangeException(nameof(optionItem.Target)),
        };
}