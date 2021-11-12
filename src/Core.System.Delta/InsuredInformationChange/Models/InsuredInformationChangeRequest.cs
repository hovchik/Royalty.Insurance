using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class InsuredInformationChangeRequest
    {
        [JsonPropertyName("insuredInfo")]
        public InsuredInfo InsuredInfo { get; set; }
    }
}
