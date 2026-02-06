using System.Text.Json.Serialization;

namespace ClassIsland.Core.Models.Weather;

public class ForecastWindInfo
{
    [JsonPropertyName("direction")] public StatusUnitValueBase<List<RangedValue>> Direction { get; set; } = new();
    [JsonPropertyName("speed")] public StatusUnitValueBase<List<RangedValue>> Speed { get; set; } = new();
}