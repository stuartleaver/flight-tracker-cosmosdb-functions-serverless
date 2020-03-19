using Newtonsoft.Json;

namespace flighttracker.functions
{
    public class Flight
    {
        [JsonProperty("id")]
        public string Icao24 { get; set; }

        public string Callsign { get; set; }

        public string OriginCountry { get; set; }

        public float? Longitude { get; set; }

        public float? Latitude { get; set; }

        public float? Altitude { get; set; }

        public float? Velocity { get; set; }

        public float? VerticalRate { get; set; }

        public string Squawk { get; set; }

        public static Flight CreateFlight(string[] flightData)
        {
            return new Flight
            {
                Icao24 = flightData[0].GetStringValue(),

                Callsign = flightData[1].GetStringValue(),

                OriginCountry = flightData[2].GetStringValue(),

                Longitude = flightData[5].GetFloatValue(),

                Latitude = flightData[6].GetFloatValue(),

                Altitude = flightData[7].GetFloatValue(),

                Velocity = flightData[9].GetFloatValue(),

                VerticalRate = flightData[11].GetFloatValue(),

                Squawk = flightData[14].GetStringValue()
            };
        }
    }
}