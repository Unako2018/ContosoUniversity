namespace DataAccess.EntitySet;

public class ESignDocumentEntity : Entity
{
    public string DocumentName { get; set; }
    public string EntityKey { get; set; }
    public string ServePdfUrl { get; set; }
    public Guid ESignDocumentPackId { get; set; }
    public Guid ESignDocumentStatusId { get; set; }
    public ESignDocumentPack ESignDocumentPack { get; set; }
    public ESignDocumentStatus ESignDocumentStatus { get; set; }
    public IList<ESignWebhookSubscription> ESignWebhookSubscriptions { get; set; }
}
