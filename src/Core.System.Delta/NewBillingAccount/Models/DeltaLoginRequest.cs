using System.Text.Json.Serialization;

namespace Core.System.Delta.Models
{
    public class DeltaLoginRequest
    {
        [JsonPropertyName("userId")]
        public string Userid { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
