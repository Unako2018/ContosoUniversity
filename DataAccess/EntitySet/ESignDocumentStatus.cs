namespace DataAccess.EntitySet;

public class ESignDocumentStatus : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Key { get; set; }
    public IList<ESignDocumentPack> ESignDocumentPacks { get; set; }
    public IList<ESignDocumentEntity> ESignDocumentEntities { get; set; }
}
