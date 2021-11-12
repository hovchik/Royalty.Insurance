using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    internal class DeltaTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        public string Scope { get; set; }
    }
}
