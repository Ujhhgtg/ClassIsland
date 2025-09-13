using System.Text.Json.Serialization;

namespace ClassIsland.Core.Models.Weather;

public class FromToValuePair
{
    [JsonPropertyName("from")] public string From { get; set; } = "";
    [JsonPropertyName("to")] public string To { get; set; } = "";

    public FromToValuePair OrderedBy(Func<string, int> predicate)
    {
        if (predicate.Invoke(From) < predicate.Invoke(To))
        {
            return new FromToValuePair { From = To, To = From };
        }

        return this;
    }
}