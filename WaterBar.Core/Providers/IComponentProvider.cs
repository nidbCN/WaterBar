namespace WaterBar.Core.Providers;

public interface IComponentProvider
{
    public string FormatString();
    public Task<string> FormatStringAsync();
}