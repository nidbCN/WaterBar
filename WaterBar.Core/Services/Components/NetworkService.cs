using WaterBar.Core.Models;
using WaterBar.Core.Options;

namespace WaterBar.Core.Services.Components;

public class NetworkService : IComponentService
{
    private record ReceiveAndSend(double receive, double send);

    private readonly StatusBarOptionItem _optionItem;
    private readonly NetworkStatus _status;

    private DateTime _lastTime;
    private ReceiveAndSend _lastReceiveAndSend;

    public NetworkService(StatusBarOptionItem optionItem)
    {
        (_optionItem, _status) = (optionItem, new(optionItem.Select));
    }

    public string FormatString()
    {
        var now = DateTime.Now;
        var interval = now - _lastTime;

        var speed = new ReceiveAndSend(
            (_status.SentBytes - _lastReceiveAndSend.receive) / (1024 * interval.TotalSeconds),
            (_status.ReceivedBytes - _lastReceiveAndSend.send) / (1024 * interval.TotalSeconds)
        );
        var (upStr, downStr) = (
            speed.send > 1024 ? $"{speed.send / 1024:F2}mb/s" : $"{speed.send:F2}kb/s",
            speed.receive > 1024 ? $"{speed.receive / 1024:F2}mb/s" : $"{speed.receive:F2}kb/s"
        );

        (_lastReceiveAndSend, _lastTime) = (new(_status.SentBytes, _status.ReceivedBytes), now);

        return _optionItem.Format
            .Replace("up_speed", upStr)
            .Replace("down_speed", downStr)
            .Replace("bandwidth", $"{_status.Speed}Mbps");
    }
}