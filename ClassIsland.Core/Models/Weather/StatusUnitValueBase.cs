using System.Text.Json.Serialization;

namespace ClassIsland.Core.Models.Weather;

public class StatusUnitValueBase<T> : StatusValueBase<T>
{
    [JsonPropertyName("value")] public T Value { get; set; } = default!;
    [JsonPropertyName("unit")] public string Unit { get; set; } = string.Empty;
}