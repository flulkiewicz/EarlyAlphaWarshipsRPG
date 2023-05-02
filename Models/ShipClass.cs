﻿using System.Text.Json.Serialization;

namespace TestApiV2.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ShipClass
    {
        Boat = 0,
        Frigate = 1,
        Destroyer = 2,
        Cruiser = 3,
    }
}
