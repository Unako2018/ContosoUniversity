using Newtonsoft.Json;

namespace QuicklSignManager.Dto.Shared;

public class StatusInfo
{
    [JsonProperty(PropertyName = "status_code")]
    public int StatusCode { get; set; }
}