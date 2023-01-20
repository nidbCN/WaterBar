using WaterBar.Core.Options;

namespace WaterBar.Core.Services.Components;

public class UnknownService : IComponentService
{
    private readonly StatusBarOptionItem _optionItem;

    public UnknownService(StatusBarOptionItem optionItem)
    {
        _optionItem = optionItem;
    }

    public string FormatString()
    {
        return _optionItem.Format;
    }
}