using System.Collections.Generic;
using Newtonsoft.Json;

namespace QuicklSignManager.Dto.Request;

public class DocumentDefinition
{
    [JsonProperty(PropertyName = "document_creation_settings")]

    public DocumentCreationSetting DocumentCreationSettings { get; set; }

    /// <summary>
    /// name of the second child document under your document pack
    /// </summary>
    [JsonProperty(PropertyName = "document_name")]
    public string DocumentName { get; set; }

    /// <summary>
    ///  //find the pre-defined field in the syntax document using custom_id, apply data to this field using the value tag.
    ///  Assign this field to a signer using signatory_uuid that we specify below under "signatories"
    /// </summary>
    [JsonProperty(PropertyName = "data_fields")]
    public IEnumerable<DataField> DataFields { get; set; }

    /// <summary>
    /// key
    /// </summary>
    [JsonProperty(PropertyName = "key")]
    public string Key { get; set; }
    /// <summary>
    /// link to download document
    /// </summary>
    [JsonProperty(PropertyName = "serve_pdf_url")]
    public string ServePdfUrl { get; set; }

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
