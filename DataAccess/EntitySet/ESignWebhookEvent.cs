
using System.Collections.Generic;

namespace DataAccess.EntitySet;

public class ESignWebhookEvent: Entity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public bool IsFinalDocumentGenerated { get; set; }
    public IList<ESignWebhookLog> ESignWebhookLogs { get; set; }
}
