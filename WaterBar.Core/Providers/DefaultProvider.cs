using WaterBar.Core.Options;
using WaterBar.Core.Utils;

namespace WaterBar.Core.Providers;

public class DefaultProvider : IComponentProvider
{
    private readonly StatusBarOptionItem _optionItem;

    public DefaultProvider(StatusBarOptionItem optionItem)
    {
        _optionItem = optionItem;
    }

    public Task<string> FormatStringAsync()
        => Task.Run(() => _optionItem.Format);
}