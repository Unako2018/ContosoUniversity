using Newtonsoft.Json;

namespace QuicklSignManager.Dto.Request;

public class DocumentPackRecipient
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "email")]
    public string Email { get; set; }
}
