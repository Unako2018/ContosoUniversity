using Newtonsoft.Json;
using System.Collections.Generic;

namespace QuicklSignManager.Dto.Response;

public class WebhookSubscriptionResponse
{
    [JsonProperty(PropertyName = "status")]
    public StatusInfo Status { get; set; }

    [JsonProperty(PropertyName = "data")]
    public WebhookCallbackData Data { get; set; }
}
/// <summary>
/// Status information in the response
/// </summary>
public class StatusInfo
{
    [JsonProperty(PropertyName = "status_code")]
    public int StatusCode { get; set; }
}

/// <summary>
/// Data wrapper containing webhook callback logs
/// </summary>
public class WebhookCallbackData
{

    [JsonProperty(PropertyName = "webhook")]
    public WebhookResponse Webhook { get; set; }
}

