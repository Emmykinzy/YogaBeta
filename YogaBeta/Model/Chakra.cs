using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YogaBeta.Model
{
    public class Chakra
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public int ChakraNum { get; set; }
        public string Location { get; set; }
        public string Element { get; set; }
        public string Benefit { get; set; }
        public Poses[] Poses { get; set; }

    }

    public class Poses
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string picture { get; set; }
    }
}
