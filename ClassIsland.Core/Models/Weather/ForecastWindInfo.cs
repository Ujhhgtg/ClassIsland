using System.Text.Json.Serialization;

namespace ClassIsland.Core.Models.Weather;

public class ForecastWindInfo
{
    [JsonPropertyName("direction")] public StatusUnitValueBase<List<FromToValuePair>> Direction { get; set; } = new();
    [JsonPropertyName("speed")] public StatusUnitValueBase<List<FromToValuePair>> Speed { get; set; } = new();
}