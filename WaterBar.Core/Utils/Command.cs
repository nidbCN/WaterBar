using System.Diagnostics;

namespace WaterBar.Core.Utils;

public static class Command
{
    public static async Task<string> ExecuteAsync(string command, IDictionary<string, string?>? env = null)
    {
        using var process = new Process
        {
            StartInfo = new("sh")
            {
                Arguments = $"-c \"{command}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };

        foreach (var envItem in env ?? new Dictionary<string, string?>())
        {
            process.StartInfo.Environment.Add(envItem);
        }

        process.Start();
        var output = await process.StandardOutput.ReadToEndAsync();
        await process.WaitForExitAsync();

        return output;
    }
}