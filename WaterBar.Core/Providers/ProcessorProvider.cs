using WaterBar.Core.Options;

namespace WaterBar.Core.Services.Components;

public class ProcessorProvider : IComponentProvider
{
    private readonly StatusBarOptionItem _optionItem;

    public ProcessorProvider(StatusBarOptionItem optionItem)
        => _optionItem = optionItem;

    public string FormatString()
    {
        return _optionItem.Format;
    }
}