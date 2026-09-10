using Newtonsoft.Json;
using System.Collections.Generic;

namespace QuicklSignManager.Dto.Request;

public class DocumentPackRequest
{
    [JsonProperty(PropertyName = "cc_recipients")]
    public IEnumerable<DocumentPackRecipient> Recipient { get; set; }

    [JsonProperty(PropertyName = "document_pack_name")]
    public string DocumentPackName { get; set; }

    [JsonProperty(PropertyName = "key")]
    public string Key { get; set; }

    [JsonProperty(PropertyName = "document_type")]
    public string DocumentType { get; set; }

    [JsonProperty(PropertyName = "documents")]
    public IEnumerable<DocumentDefinition> Documents { get; set; }

    /// <summary>
    /// not mandatory, this can be left out if you do not want to setup custom mail settings
    /// </summary>
    [JsonProperty(PropertyName = "mail_settings")]
    public DocumentPackMailSetting MailSettings { get; set; }

    /// <summary>
    /// specify your signatories details, in this case we are setting up two, one for each of the above documents
    /// </summary>
    [JsonProperty(PropertyName = "signatories")]
    public IEnumerable<Signatory> Signatories { get; set; }

    [JsonProperty(PropertyName = "signing_settings")]
    public SigningSetting SigningSettings { get; set; }
    /// <summary>
    /// status
    /// </summary>
    [JsonProperty(PropertyName = "status")]
    public string Status { get; set; }

    /// <summary>
    /// status detail
    /// </summary>
    [JsonProperty(PropertyName = "status_detail")]
    public string StatusDetail { get; set; }
}
