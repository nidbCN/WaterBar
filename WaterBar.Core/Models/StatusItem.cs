using System.Text.Json.Serialization;

namespace WaterBar.Core.Models;

public class StatusItem
{
    public StatusItem(string fullText)
        => FullText = fullText;

    [JsonPropertyName("full_text")] public string FullText { get; set; }

    [JsonPropertyName("color")] public string? Color { get; set; }
}