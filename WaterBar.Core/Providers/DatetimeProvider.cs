using WaterBar.Core.Options;

namespace WaterBar.Core.Providers;

public class DatetimeProvider : IComponentProvider
{
    private readonly StatusBarOptionItem _optionItem;

    public DatetimeProvider(StatusBarOptionItem optionItem)
    {
        _optionItem = optionItem;
    }

    public string FormatString()
        => DateTime.Now.ToString(_optionItem.Format);

    public Task<string> FormatStringAsync()
        => Task.Run(FormatString);
}