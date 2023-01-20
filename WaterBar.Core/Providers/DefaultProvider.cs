using WaterBar.Core.Options;

namespace WaterBar.Core.Services.Components;

public class DefaultProvider : IComponentProvider
{
    private readonly StatusBarOptionItem _optionItem;

    public DefaultProvider(StatusBarOptionItem optionItem)
    {
        _optionItem = optionItem;
    }

    public string FormatString()
    {
        return _optionItem.Format;
    }
}