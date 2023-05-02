using System.Text.Json.Serialization;

namespace TestApiV2.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ShipFaction
    {
        America = 0,
        Russia = 1,
        Germany = 2,
        Italy = 3,
    }
}
