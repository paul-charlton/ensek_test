using System;
using System.Net.Http;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text.Json;
using System.Threading.Tasks;

namespace EnergyManager.Client.Services
{
    /// <summary>
    /// Send requests to an endpoint.  Will handle retries etc
    /// </summary>
    internal interface IRequestService
    {
        IObservable<TResponse?> PostData<TResponse>(Uri uri, HttpContent content) where TResponse : class;
    }

    internal class RequestService : IRequestService
    {
        private static readonly JsonSerializerOptions _serialiserOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, AllowTrailingCommas = true };

        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        public IObservable<TResponse?> PostData<TResponse>(Uri uri, HttpContent content) where TResponse : class
        {
            var client = new HttpClient(GetInsecureHandler());
            return client.PostAsync(uri, content)
                .ToObservable()
                .Select(x => x.EnsureSuccessStatusCode())
                .SelectMany(x => Deserialize<TResponse>(x));
        }

        private static async Task<T?> Deserialize<T>(HttpResponseMessage response) where T : class
        {
            var contentStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<T>(contentStream, _serialiserOptions);
            return result;
        }
    }
}
