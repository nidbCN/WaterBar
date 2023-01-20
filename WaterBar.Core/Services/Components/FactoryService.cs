using WaterBar.Core.Options;

namespace WaterBar.Core.Services.Components;

public class FactoryService
{
    public IComponentService GetComponentService(StatusBarOptionItem optionItem)
        => optionItem.Target switch
        {
            "Network" => new NetworkService(optionItem),
            "Datetime" => new DatetimeService(optionItem),
            "Processor" => new ProcessorService(optionItem),
            _ => new UnknownService(optionItem),
        };
}