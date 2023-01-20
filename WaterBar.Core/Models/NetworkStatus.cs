namespace WaterBar.Core.Models;

public class NetworkStatus
{
    private readonly string _interfaceName;

    public NetworkStatus(string interfaceName)
    {
        _interfaceName = interfaceName;
    }

    public uint Bandwidth => uint.Parse(
        File.ReadAllText($"/sys/class/net/{_interfaceName}/speed")
    );

    public ulong ReceivedBytes => ulong.Parse(
        File.ReadAllText($"/sys/class/net/{_interfaceName}/statistics/rx_bytes")
    );

    public ulong SentBytes => ulong.Parse(
        File.ReadAllText($"/sys/class/net/{_interfaceName}/statistics/tx_bytes")
    );
}