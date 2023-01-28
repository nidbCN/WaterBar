using WaterBar.Core.Models;
using WaterBar.Core.Options;

namespace WaterBar.Core.Providers;

public class KeyboardStatusProvider : IComponentProvider
{
    private readonly StatusBarOptionItem _optionItem;
    private readonly KeyboardStatus _status;

    public KeyboardStatusProvider(StatusBarOptionItem optionItem)
        => (_optionItem, _status) = (optionItem, new KeyboardStatus());

    public Task<string> FormatStringAsync()
        => Task.Run(() =>
        {
            _status.Update();
            return _optionItem.Format.Replace("caps_lock", _status.IsCapsLock ? "ON" : "OFF");
        });
}