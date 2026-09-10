using Newtonsoft.Json;

namespace QuicklSignManager.Dto.Request;

public class WebhookSubscriptionRequest
{

    [JsonProperty(PropertyName = "client_id")]
    public string ClientId { get; set; }

    [JsonProperty(PropertyName = "client_secret")]
    public string ClientSecret { get; set; }

    [JsonProperty(PropertyName = "webhook")]
    public WebhookRequest Webhook { get; set; }
}
