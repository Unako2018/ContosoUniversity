
using BusinessObject.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QuicklSignManager.Constants;
using QuicklSignManager.Dto.Request;
using QuicklSignManager.Dto.Response;
using QuicklSignManager.Manager.Common.BearerToken;
using QuicklSignManager.Mappings;
using System.Net.Http.Headers;
using System.Text;

namespace QuicklSignManager.Manager;

public class QuicklySignApiManager : IQuicklySignApiManager
{
    private readonly HttpClient _httpClient;
    private readonly IAuthorizationTokenProvider _authorizationTokenProvider;
    private readonly ILogger _logger;
    private readonly ApiEndPoint _quicklSignApiEndPoint;
    private const string CacheKey = "QuicklySign-Token";

    public QuicklySignApiManager(HttpClient httpClient, IAuthorizationTokenProvider authorizationTokenProvider,
            ILogger logger, IOptions<ConnectedApis> connectedApisOptions)
    {
        _httpClient = httpClient;
        _authorizationTokenProvider = authorizationTokenProvider;
        _logger = logger;

        _logger.LogInformation(connectedApisOptions.Value.ToString());
        _quicklSignApiEndPoint = connectedApisOptions.Value.QuicklySignApi ??
                                  throw new Exception("QuicklySignApi endpoint settings missing!");
    }
    public async Task<QuicklySignResponse> SendEsignRequestAsync(DocumentPackRequest requestBody, CancellationToken cancellationToken = default)
    {
        var apiFailledMessage = $"QuicklSign API Failed to Subscribe to webhook for Proespect {requestBody.DocumentPackName}";

        await BuildAuthHeader();

        var requestContent =
            JsonConvert.SerializeObject(requestBody);

        var requestUrl = _quicklSignApiEndPoint.BaseUrl + ESignConstants.DocumentPackUrl;

        var result = await HttpGetApiResponse(HttpMethod.Post, requestUrl, requestContent, apiFailledMessage,
                     cancellationToken);


        if (!result.Response.IsSuccessStatusCode)
            return await Task.FromResult(new QuicklySignResponse { Success = false });

        var returnResult = JsonConvert.DeserializeObject<DocumentPackResponse>(result.JsonResponse);
        _logger.LogInformation(apiFailledMessage);
        return await Task.FromResult(returnResult.ToQuicklySignReponse());
    }

    public async Task<QuicklySignResponse> WebhookSubscriptionAsync(WebhookRequest requestBody, CancellationToken cancellationToken = default)
    {
        string apiFailedMessage = $"QuicklSign API Failed to Subscribe to webhook for Prospect {requestBody.EntityKey}";
        string uri = $"{_quicklSignApiEndPoint.BaseUrl}{ESignConstants.WebhookSubscriptionUrl}";

        await BuildAuthHeader();

        string requestContent =  JsonConvert.SerializeObject(new WebhookSubscriptionRequest
                                {
                                    ClientId = _quicklSignApiEndPoint.ClientId,
                                    ClientSecret = _quicklSignApiEndPoint.ClientSecret,
                                    Webhook = requestBody
                                });

        var (Response, JsonResponse) = await HttpGetApiResponse(HttpMethod.Post,
                                                uri,
                                                requestContent,
                                                apiFailedMessage,
                                                cancellationToken);

        if (!Response.IsSuccessStatusCode)
            return await Task.FromResult(new QuicklySignResponse { Success = false });

        var returnResult = JsonConvert.DeserializeObject<WebhookSubscriptionResponse>(JsonResponse);

        _logger.LogInformation($"QuicklSign API Successfully to Subscribe to webhook for Prospect {requestBody.EntityKey}");

        return await Task.FromResult(returnResult.ToQuicklySignReponse());
    }

    private async Task BuildAuthHeader()
    {

        var accessTokenUrl = new Uri(_quicklSignApiEndPoint.AccessUrl ??
                                     throw new Exception("QuicklySign Api access token url missing!"));

        var accessToken = await _authorizationTokenProvider.GetAccessToken_UsingPayload(CacheKey,
            new AuthorizationTokenRequest(accessTokenUrl, _quicklSignApiEndPoint.ClientId,
                _quicklSignApiEndPoint.ClientSecret, _quicklSignApiEndPoint.Scope, apiKey: _quicklSignApiEndPoint.ApiKey));

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken.access_token);
    }

    private async Task<(HttpResponseMessage Response, string JsonResponse)> HttpGetApiResponse(
        HttpMethod httpMethod,
    string uri, string requestContent, string errorMessage, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage
        {
            Method = httpMethod,
            RequestUri = new Uri(uri),
            Content = new StringContent(requestContent, Encoding.UTF8, "application/json")
        };

        try
        {
            _logger.LogInformation($"HttpClient BaseAddress: {_httpClient.BaseAddress}");
            _logger.LogInformation($"HttpClient DefaultRequestHeaders: {string.Join(", ", _httpClient.DefaultRequestHeaders.Select(h => $"{h.Key}={string.Join(",", h.Value)}"))}");
            var response = await _httpClient.SendAsync(request, cancellationToken);

        string jsonResponse;
        using (var content = response.Content)
        {
            jsonResponse = await content.ReadAsStringAsync(cancellationToken);
        }

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError(
                $"{errorMessage} " +
                $"{Environment.NewLine}Uri: {uri} " +
                $"{Environment.NewLine}Status: '{response.StatusCode} {response.ReasonPhrase}' " +
                $"{Environment.NewLine}Request: {requestContent} " +
                $"{Environment.NewLine}Response: {jsonResponse}");
        }
        return (response, jsonResponse);
        }
        catch (HttpRequestException httpEx)
        {
            _logger.LogError($"HttpRequestException - URL: {uri}, Message: {httpEx.Message}");
            if (httpEx.InnerException != null)
                _logger.LogError($"Inner Exception: {httpEx.InnerException.GetType().Name} - {httpEx.InnerException.Message}");
            throw;
        }
        catch (TaskCanceledException tcEx)
        {
            _logger.LogError($"TaskCanceledException (timeout) - URL: {uri}, Message: {tcEx.Message}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"QuicklySignApi API Exception - URL: {uri}, Exception Type: {ex.GetType().Name}, Message: {ex.Message}");
            //_logger.Exception(ex);

            if (ex.InnerException != null)
            {
              //  _logger.Exception(ex.InnerException);
                _logger.LogError($"Inner Exception Details - Type: {ex.InnerException.GetType().Name}, Message: {ex.InnerException.Message}");
            }

            throw;
        }
    }
}
