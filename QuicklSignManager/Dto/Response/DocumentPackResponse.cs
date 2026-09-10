using Newtonsoft.Json;
using QuicklSignManager.Dto.Request;


namespace QuicklSignManager.Dto.Response;

public class DocumentPackResponse
{
    [JsonProperty(PropertyName = "data")]
    public DocumentPackResponseData Data { get; set; }
}

public class DocumentPackResponseData
{
    [JsonProperty(PropertyName = "document_pack")]
    public DocumentPackRequest DocumentPack { get; set; }

    [JsonProperty(PropertyName = "signing_link")]
    public string SigningLink { get; set; }
}
