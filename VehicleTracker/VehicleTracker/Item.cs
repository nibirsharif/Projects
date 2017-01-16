using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker
{
    class Item
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public double Lat { get; set; }

        [JsonProperty(PropertyName = "long")]
        public double Long { get; set; }

        [JsonProperty(PropertyName = "Count")]
        public double Count { get; set; }

        [JsonProperty(PropertyName = "Density")]
        public string Density { get; set; }

        [JsonProperty(PropertyName = "Date")]
        public DateTime Date { get; set; }
    }
}
