using System.Diagnostics;
using WaterBar.Core.Models;
using WaterBar.Core.Options;
using WaterBar.Core.Services.Components;

namespace WaterBar.Core.Providers;

public class ProcessorProvider : IComponentProvider
{
    private readonly StatusBarOptionItem _optionItem;
    private readonly ProcessStatus _status;

    public ProcessorProvider(StatusBarOptionItem optionItem)
        => _optionItem = optionItem;

    public string FormatString()
    {
        using var process = new Process
        {
            StartInfo = new("sh")
            {
                Arguments = "-c \"" +
                            @"top -bn2 -d 0.01 | grep 'Cpu(s)' | tail -1 | sed 's/.*, *\([0-9.]*\)%* id.*/\1/'" + '\"',
                Environment = { { "LC_NUMERIC", "en_US.UTF-8" } },
                RedirectStandardOutput = true,
            }
        };

        process.Start();
        var output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        var result = (100 - float.Parse(output)) / 100;
        return _optionItem.Format
            .Replace("total_usage", result.ToString("P2"));
    }

    public async Task<string> FormatStringAsync()
    {
        using var process = new Process
        {
            StartInfo = new("sh")
            {
                Arguments = "-c \"" +
                            @"top -bn2 -d 0.01 | grep 'Cpu(s)' | tail -1 | sed 's/.*, *\([0-9.]*\)%* id.*/\1/'" + '\"',
                Environment = { { "LC_NUMERIC", "en_US.UTF-8" } },
                RedirectStandardOutput = true,
            }
        };

        process.Start();

        var output = await process.StandardOutput.ReadToEndAsync();
        await process.WaitForExitAsync();

        var result = (100 - float.Parse(output)) / 100;
        return _optionItem.Format
            .Replace("total_usage", result.ToString("P2"));
    }
}