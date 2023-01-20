using WaterBar.Core.Options;

namespace WaterBar.Core.Services.Components;

public class DatetimeProvider : IComponentProvider
{
    private readonly StatusBarOptionItem _optionItem;

    public DatetimeProvider(StatusBarOptionItem optionItem)
    {
        _optionItem = optionItem;
    }

    public string FormatString()
        => DateTime.Now.ToString(_optionItem.Format);
}