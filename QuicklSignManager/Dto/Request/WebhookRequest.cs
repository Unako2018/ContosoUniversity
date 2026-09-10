using Newtonsoft.Json;

namespace QuicklSignManager.Dto.Request;

public class WebhookRequest
{
    [JsonProperty(PropertyName = "entity_key")]
    public string EntityKey { get; set; }

    [JsonProperty(PropertyName = "callback")]
    public string CallBack { get; set; }
}
