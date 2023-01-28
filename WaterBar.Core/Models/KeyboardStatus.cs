using WaterBar.Core.Utils;

namespace WaterBar.Core.Models;

public class KeyboardStatus
{
    private const string CapsLock = "Caps Lock";

    private string commandOutput;

    public void Update()
        => commandOutput = Command.ExecuteAsync("xset -q | grep 'Caps Lock'").Result;

    public bool IsCapsLock
    {
        get
        {
            var outputSpan = commandOutput.AsSpan();
            var capIndex = outputSpan.IndexOf(CapsLock) + 1;
            var capEndIndex = outputSpan[(capIndex + CapsLock.Length)..].IndexOf("01");
            var capStatus = outputSpan.Slice(capIndex + CapsLock.Length, capEndIndex).ToString()
                .Replace(" ", string.Empty);
            return capStatus == "on";
        }
    }
}