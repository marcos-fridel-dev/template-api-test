using Shared.Resources.Properties;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Shared.Resources.Serialization.Geolocation.Argentina
{
    public class StateJson
    {
        public string id { get; set; }
        public string iso_nombre { get; set; }
    }

    public class StateSerialization
    {
        public static List<StateJson> Process()
        {
            try
            {
                string json = Encoding.UTF8.GetString(ArgentinaResource.StateArgentina);
                return JsonSerializer.Deserialize<List<StateJson>>(json) ?? new List<StateJson>();
            }
            catch (Exception e)
            {
                return new List<StateJson>();
            }
        }
    }
}