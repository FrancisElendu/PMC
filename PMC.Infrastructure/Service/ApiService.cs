using Azure;
using Microsoft.Extensions.Logging;
using PMC.Application.Service;
using System.Net.Http.Json;

namespace PMC.Infrastructure.Service
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiService> _logger;

        public ApiService(IHttpClientFactory httpClientFactory, ILogger<ApiService> logger)
        {
            _httpClient = httpClientFactory.CreateClient("ApiHttpClientConfig");
            _logger = logger;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var fullUrl = _httpClient.BaseAddress + url;
            try
            { 
                var response = await _httpClient.GetAsync(fullUrl);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("GET request to {Url} failed with status code {StatusCode} and content: {Content}", fullUrl, response.StatusCode, errorContent);
                    response.EnsureSuccessStatusCode();
                }

                var result = await response.Content.ReadFromJsonAsync<T>();
                if (result == null)
                {
                    throw new InvalidOperationException("Failed to deserialize the response content.");
                }
                return result;
            }

            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Network error occurred while executing GET request to {Url}", fullUrl);
                throw; // or return a custom error response
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while executing GET request");
                throw;
            }
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest requestData)
        {
            var fullUrl = _httpClient.BaseAddress + url;
            try
            {
                var response = await _httpClient.PostAsJsonAsync(fullUrl, requestData);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("GET request to {Url} failed with status code {StatusCode} and content: {Content}", fullUrl, response.StatusCode, errorContent);
                    response.EnsureSuccessStatusCode();
                }

                var result = await response.Content.ReadFromJsonAsync<TResponse>();
                if (result == null)
                {
                    throw new InvalidOperationException("Failed to deserialize the response content.");
                }
                return result;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Network error occurred while executing GET request to {Url}", fullUrl);
                throw; // or return a custom error response
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while executing POST request");
                throw;
            }
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest requestData)
        {
            var fullUrl = _httpClient.BaseAddress + url;
            try
            {
                var response = await _httpClient.PutAsJsonAsync(fullUrl, requestData);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("GET request to {Url} failed with status code {StatusCode} and content: {Content}", fullUrl, response.StatusCode, errorContent);
                    response.EnsureSuccessStatusCode();
                }

                var result = await response.Content.ReadFromJsonAsync<TResponse>();
                if (result == null)
                {
                    throw new InvalidOperationException("Failed to deserialize the response content.");
                }
                return result;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Network error occurred while executing GET request to {Url}", fullUrl);
                throw; // or return a custom error response
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while executing PUT request");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string url)
        {
            var fullUrl = _httpClient.BaseAddress + url;
            HttpResponseMessage? response = null;

            try
            {
                response = await _httpClient.DeleteAsync(fullUrl);
                response.EnsureSuccessStatusCode();
                return true; //return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Network error occurred while executing GET request to {Url}", fullUrl);
                throw; // or return a custom error response
            }
            catch (Exception ex)
            {
                // Now response is accessible here
                if (response != null)
                {
                    _logger.LogError(ex, "Error occurred while executing DELETE request to {Url}. Status code: {StatusCode}", fullUrl, response.StatusCode);
                }
                else
                {
                    _logger.LogError(ex, "Error occurred while executing DELETE request to {Url}. No response received.", fullUrl);
                }
                throw;
            }
        }
    }
}
