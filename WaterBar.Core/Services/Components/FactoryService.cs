using WaterBar.Core.Options;

namespace WaterBar.Core.Services.Components;

public class FactoryService
{
    public IComponentProvider GetComponentService(StatusBarOptionItem optionItem)
        => optionItem.Target switch
        {
            "Network" => new NetworkProvider(optionItem),
            "Datetime" => new DatetimeProvider(optionItem),
            "Processor" => new ProcessorProvider(optionItem),
            _ => new DefaultProvider(optionItem),
        };
}