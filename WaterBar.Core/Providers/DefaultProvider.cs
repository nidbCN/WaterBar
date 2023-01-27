using WaterBar.Core.Options;

namespace WaterBar.Core.Providers;

public class DefaultProvider : IComponentProvider
{
    private readonly StatusBarOptionItem _optionItem;

    public DefaultProvider(StatusBarOptionItem optionItem)
    {
        _optionItem = optionItem;
    }

    public string FormatString()
        => _optionItem.Format;

    public Task<string> FormatStringAsync()
        => Task.Run(FormatString);
}