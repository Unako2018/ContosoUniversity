using Newtonsoft.Json;

namespace QuicklSignManager.Dto.Request;

public class DataField
{
    [JsonProperty(PropertyName = "custom_id")]
    public string CustomId { get; set; }

    [JsonProperty(PropertyName = "data")]
    public string Data { get; set; }

    [JsonProperty(PropertyName = "signatory_uuid")]
    public string SignatoryUUId { get; set; }
}
