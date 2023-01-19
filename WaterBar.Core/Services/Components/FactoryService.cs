using WaterBar.Core.Options;

namespace WaterBar.Core.Services.Components;

public class FactoryService
{
    public IComponentService GetComponentService(StatusBarOptionItem optionItem)
        => optionItem.Target switch
        {
            "Network" => new NetworkService(optionItem),
            "Datetime" => new DatetimeService(optionItem),
            _ => throw new ArgumentOutOfRangeException(nameof(optionItem.Target)),
        };
}