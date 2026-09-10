using System;

namespace QuicklSignManager.Manager.Common.BearerToken;

public class AuthorizationTokenRequest {

    public AuthorizationTokenRequest(Uri uri, string clientId, string clientSecret, string scope,
        string grantType = "client_credentials", string userName = null, string password = null, string apiKey = null) {

        Uri = uri;
        ClientId = clientId;
        ClientSecret = clientSecret;
        Scope = scope;
        GrantType = grantType;
        UserName = userName;
        Password = password;
        ApiKey = apiKey;
    }

    public string GrantType { get; set; }

    public string TokenName { get; set; }
    public Uri Uri { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Scope { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ApiKey { get; set; }

}