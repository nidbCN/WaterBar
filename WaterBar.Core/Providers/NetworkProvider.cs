using System.Text;
using WaterBar.Core.Models;
using WaterBar.Core.Options;

namespace WaterBar.Core.Services.Components;

public class NetworkProvider : IComponentProvider
{
    private record ReceiveAndSend(double receive, double send);

    private const string UpSpeed = "up_speed";
    private const string DownSpeed = "down_speed";
    private const string Bandwidth = "bandwidth";

    private readonly StatusBarOptionItem _optionItem;
    private readonly NetworkStatus _status;

    private DateTime _lastTime;
    private ReceiveAndSend _lastReceiveAndSend = new(0, 0);

    public NetworkProvider(StatusBarOptionItem optionItem)
    {
        (_optionItem, _status) = (optionItem, new(optionItem.Select));
    }

    public string FormatString()
    {
        var now = DateTime.Now;
        var interval = now - _lastTime;

        var speed = new ReceiveAndSend(
            (_status.ReceivedBytes - _lastReceiveAndSend.send) / (1024 * interval.TotalSeconds),
            (_status.SentBytes - _lastReceiveAndSend.receive) / (1024 * interval.TotalSeconds)
        );
        var (upStr, downStr) = (
            speed.send > 1024 ? $"{speed.send / 1024:F2}mb/s" : $"{speed.send:F2}kb/s",
            speed.receive > 1024 ? $"{speed.receive / 1024:F2}mb/s" : $"{speed.receive:F2}kb/s"
        );

        (_lastReceiveAndSend, _lastTime) = (new(_status.SentBytes, _status.ReceivedBytes), now);

        var formatSpan = _optionItem.Format.AsSpan();
        var builder = new StringBuilder(formatSpan.Length);

        var startIndex = 0;

        var index = formatSpan[startIndex..].IndexOf(UpSpeed);
        if (index != -1)
        {
            builder.Append(formatSpan[startIndex..(startIndex + index)]);
            builder.Append(upStr);
            startIndex = startIndex + index + UpSpeed.Length;
        }

        index = formatSpan[startIndex..].IndexOf(DownSpeed);
        if (index != -1)
        {
            builder.Append(formatSpan[startIndex..(startIndex + index)]);
            builder.Append(downStr);
            startIndex = startIndex + index + DownSpeed.Length;
        }

        index = formatSpan[startIndex..].IndexOf(Bandwidth);
        if (index != -1)
        {
            builder.Append(formatSpan[startIndex..(startIndex + index)]);
            builder.Append($"{_status.Bandwidth}Mbps");
        }

        return builder.ToString();
    }
}