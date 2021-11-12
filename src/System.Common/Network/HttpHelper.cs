using System.Collections.Generic;
using System.Common.Exceptions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace System.Common.Network
{
    public class HttpHelper : IHttpHelper, IDisposable
    {
        private readonly HttpClient _client;
        private bool _disposedValue;

        public HttpHelper()
        {
            _client = new HttpClient
            {
                Timeout = TimeSpan.FromMinutes(3)
            };
        }

        public IErrorHandling ErrorHandling { get; set; }

        public async Task<TResponse> Get<TResponse>(string url, List<JsonConverter> converters = null)
        {
            var task = await _client.GetAsync(url);
            var jsonString = await task.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var serializeOptions = new JsonSerializerOptions();
            converters?.ForEach(converter => serializeOptions.Converters.Add(converter));

            return await JsonSerializer.DeserializeAsync<TResponse>(jsonString, serializeOptions).ConfigureAwait(false);
        }

        public async Task<TResponse> Post<TResponse, TRequest>(string url, TRequest request, CancellationToken cancellationToken, List<JsonConverter> converters = null)
        {
            var json = JsonSerializer.Serialize(request);
            var data = new StringContent(json, Encoding.UTF8);
            var response = await _client.PostAsync(url, data, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                await HandleError(response, cancellationToken);
            }
            var model = await response.Content.ReadAsStreamAsync(cancellationToken);
            var serializeOptions = new JsonSerializerOptions();
            converters?.ForEach(converter => serializeOptions.Converters.Add(converter));
            var returnData = await JsonSerializer.DeserializeAsync<TResponse>(model, serializeOptions, cancellationToken);

            return returnData;
        }

        public async Task<TResponse> Post<TResponse>(string url, CancellationToken cancellationToken)
        {
            var response = await _client.PostAsync(url, null!, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                await HandleError(response, cancellationToken);
            }
            var model = await response.Content.ReadAsStreamAsync(cancellationToken);

            var returnData = await JsonSerializer.DeserializeAsync<TResponse>(model, cancellationToken: cancellationToken);

            return returnData;
        }

        public void AddAuthorization(string schema, string value)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(schema, value);
        }

        public void AddHeader(string name, string value)
        {
            _client.DefaultRequestHeaders.Add(name, value);
        }

        public void AddContentType(string value)
        {
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", value);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _client?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~HttpHelper()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private async Task HandleError(HttpResponseMessage responseMessage, CancellationToken cancellationToken)
        {
            if (ErrorHandling != null)
            {
                ErrorHandling.Handle(responseMessage);
            }
            else
            {
                throw new RestApiResponseException((int)responseMessage.StatusCode, await responseMessage.Content.ReadAsStringAsync(cancellationToken));
            }
        }
    }
}
