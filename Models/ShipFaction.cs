using System.Text.Json.Serialization;

namespace WarshipsRPGAlpha.Models
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
