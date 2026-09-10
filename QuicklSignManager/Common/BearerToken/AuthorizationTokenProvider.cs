using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace QuicklSignManager.Manager.Common.BearerToken
{
    public class AuthorizationTokenProvider : IAuthorizationTokenProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        public AuthorizationTokenProvider(HttpClient httpClient, IMemoryCache cache, ILogger logger)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<AuthorizationToken> GetAccessToken(string cacheKey, AuthorizationTokenRequest request)
        {
            HttpResponseMessage responseMessage;

            if (_cache.TryGetValue(cacheKey, out string token))
                return new AuthorizationToken
                {
                    Success = true,
                    access_token = token
                };

            var content = new Dictionary<string, string>
            {
                { "grant_type", request.GrantType },
                { "client_id", request.ClientId },
                { "scope", request.Scope },
                { "client_secret", request.ClientSecret }
            };

            if (request.GrantType.Equals(AuthorizationConstants.PasswordGrantType,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                content.Add("username", request.UserName);
                content.Add("password", request.Password);
            }

            using (_httpClient)
            {
                var tokenRequest = new HttpRequestMessage(HttpMethod.Post, request.Uri);

                var httpContent = new FormUrlEncodedContent(content);

                tokenRequest.Content = httpContent;
                responseMessage = await _httpClient.SendAsync(tokenRequest);
            }

            if (!responseMessage.IsSuccessStatusCode) return new AuthorizationToken { Success = false };

            var jwt = await responseMessage.Content.ReadAsStringAsync();
            var result = await Task.Run(() => JsonSerializer.Deserialize<AuthorizationToken>(jwt));

            var options = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(
                    TimeSpan.FromSeconds(Math.Max(0, result.expires_in - 300)));

            _cache.Set(cacheKey, result.access_token, options);
            result.Success = true;
            return result;

        }

        public async Task<AuthorizationToken> GetAccessToken_UsingPayload(string cacheKey, AuthorizationTokenRequest requestIncoming)
        {
            var expiresIn = 18000;// 5 hours in seconds
            // Try cache first
            if (_cache.TryGetValue(cacheKey, out string token))
            {
                return new AuthorizationToken
                {
                    Success = true,
                    access_token = token
                };
            }

            if (requestIncoming.Uri is null
                    || string.IsNullOrWhiteSpace(requestIncoming.ApiKey)
                    || string.IsNullOrWhiteSpace(requestIncoming.ClientId)
                    || string.IsNullOrWhiteSpace(requestIncoming.ClientSecret))
                return new AuthorizationToken { Success = false };


            var payload = new
            {
                client_id = requestIncoming.ClientId,
                client_secret = requestIncoming.ClientSecret,
                api_key = requestIncoming.ApiKey
            };

            var json = JsonSerializer.Serialize(payload);
            using var request = new HttpRequestMessage(HttpMethod.Post, requestIncoming.Uri)
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };
            request.Headers.Remove("Cache-Control");
            request.Headers.Add("Cache-Control", "no-cache");

            // Synchronously call the async API (this method is sync). Adjust to async if possible.
            var responseMessage = await _httpClient.SendAsync(request);

            if (!responseMessage.IsSuccessStatusCode)
                return new AuthorizationToken { Success = false };

            var responseJson = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TokenResponse>(responseJson);

            if (result == null)
                return new AuthorizationToken { Success = false };

            var options = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(Math.Max(0, expiresIn - 300)));

            _cache.Set(cacheKey, result.data.access_token, options);

            result.data.Success = true;
            return result.data;
        }
    }
}