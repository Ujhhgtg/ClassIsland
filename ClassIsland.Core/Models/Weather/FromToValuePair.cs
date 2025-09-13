using System.Text.Json.Serialization;

namespace ClassIsland.Core.Models.Weather;

public class FromToValuePair
{
    [JsonPropertyName("from")] public string From { get; set; } = "";
    [JsonPropertyName("to")] public string To { get; set; } = "";

    public FromToValuePair OrderedIf(Predicate<FromToValuePair> predicate)
    {
        return predicate.Invoke(this) ? new FromToValuePair { From = To, To = From } : this;
    }
}