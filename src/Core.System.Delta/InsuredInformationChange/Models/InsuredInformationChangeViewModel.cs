using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class InsuredInformationChangeViewModel
    {
        [JsonPropertyName("results")]
        public InsuredInformationChangeResponse Results { get; set; }
    }
}
