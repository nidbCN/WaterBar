namespace WaterBar.Core.Options;

#nullable disable
public class StatusBarOption
{
    public uint Interval { get; set; }
    public IList<StatusBarOptionItem> Display { get; set; }
}

public class StatusBarOptionItem
{
    public string Target { get; set; }
    public string Select { get; set; }
    public string Format { get; set; }
    public string Color { get; set; }
}
#nullable restore