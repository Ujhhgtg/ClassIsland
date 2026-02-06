using System.Text.Json.Serialization;

namespace ClassIsland.Core.Models.Weather;

public class ForecastDaily
{
    [JsonPropertyName("precipitationProbability")]
    public StatusValueBase<List<string>> PrecipitationProbability { get; set; } = new();

    [JsonPropertyName("temperature")] public StatusUnitValueBase<List<RangedValue>> Temperature { get; set; } = new();
    [JsonPropertyName("weather")] public StatusValueBase<List<RangedValue>> Weather { get; set; } = new();
    
    [JsonPropertyName("sunRiseSet")] public StatusValueBase<List<RangedValue>> SunRiseSet { get; set; } = new();
    [JsonPropertyName("aqi")] public StatusValueBase<List<int>> Aqi { get; set; } = new();
    [JsonPropertyName("wind")] public ForecastWindInfo Wind { get; set; } = new();
    [JsonPropertyName("pubTime")] public DateTime PublishTime {get; set;} = DateTime.Now;
}