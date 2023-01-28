using Microsoft.Extensions.Options;
using WaterBar.Core.Options;
using WaterBar.Core.Providers;

namespace WaterBar.Core.Services.Components;

public class FactoryService
{
    public IComponentProvider GetComponentService(StatusBarOptionItem optionItem)
        => optionItem.Target switch
        {
            "Network" => new NetworkProvider(optionItem),
            "Datetime" => new DatetimeProvider(optionItem),
            "Processor" => new ProcessorProvider(optionItem),
            "Keyboard" => new KeyboardStatusProvider(optionItem),
            _ => new DefaultProvider(optionItem),
        };
}