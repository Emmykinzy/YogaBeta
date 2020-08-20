using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Text.Json;
//using System.Text.Json.Serialization;
namespace YogaBeta.Model
{
    public class Chakra
    {
        [JsonProperty(PropertyName = "id")]
        //[JsonPropertyName("img")]
        public string Id { get; set; }
        public int ChakraNum { get; set; }
        public string Location { get; set; }
        public string Element { get; set; }
        public string Benefit { get; set; }
        public Poses[] Poses { get; set; }

        //public override string ToString() => JsonSerializer.Serialize<Chakra>(this);
    }

    public class Poses
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string picture { get; set; }

        //public override string ToString() => JsonSerializer.Serialize<Poses>(this);
    }
}
