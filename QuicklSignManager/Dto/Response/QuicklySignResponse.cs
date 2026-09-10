using QuicklSignManager.Dto.Request;
using System.Collections.Generic;

namespace QuicklSignManager.Dto.Response;

public class QuicklySignResponse
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public string SigningLink { get; set; }
    public string Status { get; set; }
    public string StatusDetail { get; set; }
    public string Key { get; set; }
    public WebhookResponse WebhookResponse { get; set; }
    public IEnumerable<DocumentDefinition> Documents { get; set; }
}
