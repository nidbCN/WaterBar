namespace WaterBar.Core.Providers;

public interface IComponentProvider
{
    public Task<string> FormatStringAsync();
}