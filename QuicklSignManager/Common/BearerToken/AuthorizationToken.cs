namespace QuicklSignManager.Manager.Common.BearerToken;

public class AuthorizationToken {

    public bool Success { get; set; }
    public string access_token { get; set; }
    public double expires_in { get; set; }
    public string token_type { get; set; }

}