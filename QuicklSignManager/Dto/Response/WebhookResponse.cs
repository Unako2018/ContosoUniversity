using Newtonsoft.Json;

namespace QuicklSignManager.Dto.Response;

public class WebhookResponse
{
    [JsonProperty(PropertyName = "key")]
    public string Key { get; set; }

    [JsonProperty(PropertyName = "entity_key")]
    public string EntityKey { get; set; }

    [JsonProperty(PropertyName = "callback")]
    public string CallBack { get; set; }
    [JsonProperty(PropertyName = "date_created")]
    public long DateCreated { get; set; }

    [JsonProperty(PropertyName = "date_updated")]
    public long DateUpdated { get; set; }
}
