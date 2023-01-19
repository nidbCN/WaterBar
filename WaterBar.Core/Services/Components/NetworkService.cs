using WaterBar.Core.Models;
using WaterBar.Core.Options;

namespace WaterBar.Core.Services.Components;

public class NetworkService : IComponentService
{
    private readonly StatusBarOptionItem _optionItem;
    private readonly NetworkStatus _status;

    private DateTime _lastTime;
    private ulong _lastReceivedBytes;
    private ulong _lastSentBytes;

    public NetworkService(StatusBarOptionItem optionItem)
    {
        (_optionItem, _status) = (optionItem, new(optionItem.Select));
    }

    public string FormatString()
    {
        var now = DateTime.Now;
        var interval = now - _lastTime;

        var (upSpeed, downSpeed) = (
            (_status.SentBytes - _lastSentBytes) / (1024 * interval.TotalSeconds),
            (_status.ReceivedBytes - _lastReceivedBytes) / (1024 * interval.TotalSeconds)
        );
        var (upStr, downStr) = (
            upSpeed > 1024 ? $"{upSpeed / 1024:F2}mb/s" : $"{upSpeed:F2}kb/s",
            downSpeed > 1024 ? $"{downSpeed / 1024:F2}mb/s" : $"{downSpeed:F2}kb/s"
        );

        (_lastSentBytes, _lastReceivedBytes, _lastTime) = (_status.SentBytes, _status.ReceivedBytes, now);

        return _optionItem.Format
            .Replace("up_speed", upStr)
            .Replace("down_speed", downStr)
            .Replace("bandwidth", $"{_status.Speed}Mbps");
    }
}