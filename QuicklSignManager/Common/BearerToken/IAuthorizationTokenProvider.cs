using System.Threading.Tasks;

namespace QuicklSignManager.Manager.Common.BearerToken;

public interface IAuthorizationTokenProvider
{
    Task<AuthorizationToken> GetAccessToken(string cacheKey, AuthorizationTokenRequest request);

    Task<AuthorizationToken> GetAccessToken_UsingPayload(string cacheKey, AuthorizationTokenRequest requestIncoming);
}