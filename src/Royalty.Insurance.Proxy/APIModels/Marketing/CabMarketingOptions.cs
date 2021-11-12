using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Royalty.Insurance.Proxy.APIModels.Marketing
{
    public class CabMarketingOptions
    {
        public List<Cargo_Typeoptions> cargo_typeOptions { get; set; }
        public List<Car_Typeoptions> car_typeOptions { get; set; }
        public List<Op_Typeoptions> op_typeOptions { get; set; }
        public List<Make> makes { get; set; }
        public List<Trailertype> trailerTypes { get; set; }
        public List<Inscomp_Options> inscomp_options { get; set; }
    }

    public class Cargo_Typeoptions
    {
        public string name { get; set; }
        [JsonPropertyName("ref")]
        public int _ref { get; set; }
    }

    public class Car_Typeoptions
    {
        public string name { get; set; }
        [JsonPropertyName("ref")]
        public int _ref { get; set; }
    }

    public class Op_Typeoptions
    {
        public string name { get; set; }
        [JsonPropertyName("ref")]
        public int _ref { get; set; }
    }

    public class Make
    {
        public string label { get; set; }
        public List<Option> options { get; set; }
    }

    public class Option
    {
        public string name { get; set; }
        [JsonPropertyName("ref")]
        public int _ref { get; set; }
    }

    public class Trailertype
    {
        public string name { get; set; }
        [JsonPropertyName("ref")]
        public int _ref { get; set; }
        public int unitCount { get; set; }
    }

    public class Inscomp_Options
    {
        public string name { get; set; }
        [JsonPropertyName("ref")]
        public int _ref { get; set; }
    }
}