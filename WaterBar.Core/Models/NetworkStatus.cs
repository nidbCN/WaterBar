namespace WaterBar.Core.Models;

public class NetworkStatus
{
    private readonly string _interfaceName;

    public NetworkStatus(string interfaceName)
    {
        _interfaceName = interfaceName;
        Speed = uint.Parse(
            File.ReadAllText($"/sys/class/net/{_interfaceName}/speed")
        );
    }

    public uint Speed { get; }

    public ulong ReceivedBytes => ulong.Parse(
        File.ReadAllText($"/sys/class/net/{_interfaceName}/statistics/rx_bytes")
    );

    public ulong SentBytes => ulong.Parse(
        File.ReadAllText($"/sys/class/net/{_interfaceName}/statistics/tx_bytes")
    );
}