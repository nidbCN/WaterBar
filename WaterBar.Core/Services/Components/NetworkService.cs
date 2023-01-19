using WaterBar.Core.Models;
using WaterBar.Core.Options;

namespace WaterBar.Core.Services.Components;

public class NetworkService : IComponentService
{
    private readonly StatusBarOptionItem _optionItem;
    private readonly uint _interval;
    private readonly NetworkStatus _networkStatus;
    private ulong _lastReceivedBytes;
    private ulong _lastSentBytes;

    public NetworkService(StatusBarOptionItem optionItem, uint interval)
    {
        (_optionItem, _interval, _networkStatus) = (optionItem, interval, new(optionItem.Select));
    }

    public string FormatString()
    {
        var (upSpeed, downSpeed) = ((_networkStatus.SentBytes - _lastSentBytes) / (1024 * _interval)
            , (_networkStatus.ReceivedBytes - _lastReceivedBytes) / (1024 * _interval));

        (_lastSentBytes, _lastReceivedBytes) = (_networkStatus.SentBytes, _networkStatus.ReceivedBytes);

        return _optionItem.Format
            .Replace("%up_speed", upSpeed > 1024 ? $"{upSpeed / 1024}mb/s" : $"{upSpeed}kb/s")
            .Replace("%down_speed", downSpeed > 1024 ? $"{downSpeed / 1024}mb/s" : $"{downSpeed}kb/s")
            .Replace("%speed", _networkStatus.Speed.ToString());
    }
}