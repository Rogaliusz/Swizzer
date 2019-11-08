using Swizzer.Shared.Common.Exceptions;
using Swizzer.Shared.Common.Extensions;
using Swizzer.Shared.Common.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Client.Web.Api
{
    public interface IApiHttpWebService
    {
        Task<TResponse> SendAsync<TResponse>(HttpMethod method, string endpoint, object body = null);
    }

    public class ApiHttpWebService : IApiHttpWebService, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;

        public ApiHttpWebService(ApiSettings apiSettings)
        {
            _httpClient = new HttpClient();
            this._apiSettings = apiSettings;
        }

        public async Task<TResponse> SendAsync<TResponse>(HttpMethod method, string endpoint, object body = null)
        {
            var webRequest = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri($"{_apiSettings.Address}{endpoint}")
            };

            if (!_apiSettings.Token.IsEmpty())
            {
                webRequest.Headers.Add("Authorization", $"Bearer {_apiSettings.Token}");
            }

            if (body != null)
            {
                var json = SwizzerJsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                webRequest.Content = content;
            }

            var response = await _httpClient.SendAsync(webRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
     
            if (response.IsSuccessStatusCode)
            {
                return SwizzerJsonSerializer.Deserialize<TResponse>(responseContent);
            } 

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new SwizzerClientException(ServerErrorCodes.UnauthorizedAccess);
            }

            var error = SwizzerJsonSerializer.Deserialize<ErrorDto>(responseContent);

            throw new SwizzerClientException(error.ErrorCode);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
