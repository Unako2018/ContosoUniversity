
using System;

namespace DataAccess.EntitySet;

public class ESignWebhookLog:Entity
{
    public string Key { get; set; }
    public Guid ProspectId { get; set; }
    public Guid CorrelationId { get; set; }
    public Guid ESignWebHookEventId { get; set; }
    public string DocumentStatusChanged { get; set; }
    public string Log { get; set; }
    public string SubscriberKey { get; set; }
    public string SubscriptionKey { get; set; }
    public  DateTime HookCreatedDate { get; set; }
    public ESignWebhookEvent ESignWebhookEvent { get; set; }
    public Student Prospect { get; set; }

}
