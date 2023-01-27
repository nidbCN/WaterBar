namespace WaterBar.Core.Models;

public struct ProcessStatus
{
    public uint TotalUsage { get; set; }
    public IList<uint> UsageList { get; set; }
}