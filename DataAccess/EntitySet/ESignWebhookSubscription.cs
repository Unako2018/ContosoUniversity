
using System;

namespace DataAccess.EntitySet;

public class ESignWebhookSubscription:Entity
{
    public Guid ProspectId { get; set; }
    public Guid ESignDocumentEntityId { get; set; }
    public ESignDocumentEntity ESignDocumentEntity { get; set; }
    public ESignWebhookEvent ESignWebhookEvent { get; set; }
    public Student Prospect { get; set; }
}
