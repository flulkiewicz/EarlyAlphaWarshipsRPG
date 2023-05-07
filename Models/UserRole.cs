using System.Text.Json.Serialization;

namespace WarshipsRPGAlpha.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole
    {
        User = 0,
        PowerUser = 1,
        Admin = 2,
    }
}
