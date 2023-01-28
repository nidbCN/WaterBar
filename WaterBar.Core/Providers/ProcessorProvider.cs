using WaterBar.Core.Models;
using WaterBar.Core.Options;
using WaterBar.Core.Utils;

namespace WaterBar.Core.Providers;

public class ProcessorProvider : IComponentProvider
{
    private readonly StatusBarOptionItem _optionItem;
    private readonly ProcessStatus _status;

    public ProcessorProvider(StatusBarOptionItem optionItem)
        => _optionItem = optionItem;

    public async Task<string> FormatStringAsync()
    {
        var output =
            await Command.ExecuteAsync(
                @"top -bn2 -d 0.01 | grep 'Cpu(s)' | tail -1 | sed 's/.*, *\([0-9.]*\)%* id.*/\1/'",
                new Dictionary<string, string?> { { "LC_NUMERIC", "en_US.UTF-8" } });

        var result = (100 - float.Parse(output)) / 100;
        return _optionItem.Format
            .Replace("total_usage", result.ToString("P2"));
    }
}