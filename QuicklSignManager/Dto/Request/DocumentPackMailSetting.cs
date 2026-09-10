using Newtonsoft.Json;

namespace QuicklSignManager.Dto.Request;

public class DocumentPackMailSetting
{

    /// <summary>
    /// flag to indicate whether the user who created this document receives a final copy email once the document has been signed and finalized
    /// </summary>
    [JsonProperty(PropertyName = "exclude_created_by_from_final_mail")]
    public string ExcludeCreateByFromFinalMail { get; set; }

    /// <summary>
    /// flag to include or exclude the completed document as an attachment when document has been signed and finalized
    /// </summary>
    [JsonProperty(PropertyName = "include_attachments_in_final_mail")]
    public string IncludeAttachmentsInFinalMail { get; set; }

    /// <summary>
    /// custom body to reflect in the email body
    /// </summary>
    [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

    /// <summary>
    /// subject to reflect for the email received by the signer
    /// </summary>
    [JsonProperty(PropertyName = "subject")]
    public string Subject { get; set; }

}
