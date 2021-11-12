using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class PolicyCancellationViewModel
    {
        [JsonPropertyName("results")]
        public DeltaPolicyCancellationResponse Results { get; set; }
    }
}
