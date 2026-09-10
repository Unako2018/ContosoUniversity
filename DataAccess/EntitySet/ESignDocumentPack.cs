
using System;
using System.Collections.Generic;

namespace DataAccess.EntitySet;

public class ESignDocumentPack : Entity
{
    public string Name { get; set; }
    public string DocumentPackKey { get; set; }
    public string SigningLink { get; set; }
    public Guid ESignDocumentStatusId { get; set; }
    public Guid ProspectId { get; set; }
    public Student Prospect { get; set; }
    public ESignDocumentStatus ESignDocumentStatus { get; set; }
    public IList<ESignDocumentEntity> ESignDocumentEntities { get; set; }
}