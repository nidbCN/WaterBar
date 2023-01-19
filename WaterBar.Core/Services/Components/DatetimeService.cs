using WaterBar.Core.Options;

namespace WaterBar.Core.Services.Components;

public class DatetimeService : IComponentService
{
    private readonly StatusBarOptionItem _optionItem;

    public DatetimeService(StatusBarOptionItem optionItem)
    {
        _optionItem = optionItem;
    }

    public string FormatString()
        => DateTime.Now.ToString(_optionItem.Format);
}