namespace QuicklSignManager.Manager.Common.BearerToken;

public class TokenResponse
{
    public AuthorizationToken data { get; set; }
    public TokenStatus status { get; set; }
}
public class TokenStatus
{
    public int status_code { get; set; }
}
