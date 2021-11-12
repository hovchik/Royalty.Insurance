using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace System.Common.Network
{
    public interface IHttpHelper
    {

        // Task<TResponse> Get<TResponse>(string url);
        Task<TResponse> Get<TResponse>(string url, List<JsonConverter> converters = null);
        Task<TResponse> Post<TResponse, TRequest>(string url, TRequest request, CancellationToken cancellationToken, List<JsonConverter> converters = null);
        Task<TResponse> Post<TResponse>(string url, CancellationToken cancellationToken);
        void AddAuthorization(string schema, string value);
        void AddHeader(string name, string value);
        void AddContentType(string value);
    }
}