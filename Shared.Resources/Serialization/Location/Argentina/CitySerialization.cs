using Shared.Resources.Properties;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Shared.Resources.Serialization.Geolocation.Argentina
{
    public class CityStateJson
    {
        public string id { get; set; }
        public string nombre { get; set; }
    }

    public class CityJson
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public CityStateJson provincia { get; set; }
    }

    public class CitySerialization
    {
        public static List<CityJson> Process()
        {
            try
            {
                string json = Encoding.UTF8.GetString(ArgentinaResource.CityArgentina);
                return JsonSerializer.Deserialize<List<CityJson>>(json) ?? new List<CityJson>();
            }
            catch (Exception e)
            {
                return new List<CityJson>();
            }
        }
    }
}