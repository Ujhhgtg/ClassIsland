using System.Text.Json.Serialization;

namespace ClassIsland.Core.Models.Weather;

public class RangedValue
{
    [JsonPropertyName("from")] public string From { get; set; } = "";
    [JsonPropertyName("to")] public string To { get; set; } = "";
    
    public RangedValue OrderedBy(Func<string, int> predicate)
    {
        if (predicate.Invoke(From) < predicate.Invoke(To))
        {
            return new RangedValue { From = To, To = From };
        }

        return this;
    }
}