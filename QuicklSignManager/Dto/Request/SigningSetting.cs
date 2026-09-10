using Newtonsoft.Json;

namespace QuicklSignManager.Dto.Request;

public class SigningSetting
{
    /// <summary>
    /// all users can see attachments provided in the document
    /// </summary>
    [JsonProperty(PropertyName = "attachments_visible_to_all_signers")]
    public bool AttachmentsVisibleToAllSigners { get; set; }

    /// <summary>
    /// allow the singer to draw their signature
    /// </summary>
    [JsonProperty(PropertyName = "can_use_drawn_signature")]
    public bool CanUseDrawnSignature { get; set; }

    /// <summary>
    /// if an existing user, their pre-defined initial will auto populate within the initial field
    /// </summary>
    [JsonProperty(PropertyName = "can_use_one_click_initial")]
    public bool CanUseOneClickInitial { get; set; }

    /// <summary>
    /// if an existing user, their pre-defined signature will auto populate within the signature field
    /// </summary>
    [JsonProperty(PropertyName = "can_use_one_click_signature")]
    public bool CanUseOneClickSignature { get; set; }

    /// <summary>
    /// flag to allow signer to type out their signature
    /// </summary>
    [JsonProperty(PropertyName = "can_use_typed_signature")]
    public bool CanUseTypedSignature { get; set; }

    /// <summary>
    /// flag to allow signer to upload an image of their signature
    /// </summary>
    [JsonProperty(PropertyName = "can_use_uploaded_signature")]
    public bool CanUseUploadedSignature { get; set; }

    /// <summary>
    /// //this can be populated with a URL that the user will be sent to once signing has been completed
    /// </summary>
    [JsonProperty(PropertyName = "post_signing_url")]
    public string PostSigningUrl { get; set; }

    /// <summary>
    ///once the first signer has completed his/her fields, the second signer will then be prompt to complete his/her fields. 
    ///If false, both signers will be sent a request to sign the document at the same time
    /// </summary>
    [JsonProperty(PropertyName = "sequential_signing")]
    public bool SequentialSigning { get; set; }

    /// <summary>
    /// use_signing_wizard will direct the user to each field required for them to complete
    /// </summary>
    [JsonProperty(PropertyName = "use_signing_wizard")]
    public bool UseSigningWizard { get; set; }

    /// <summary>
    /// user will be prompt to enter in their OTP via email(if not mobile number provided) or SMS/Whatsapp
    /// </summary>
    [JsonProperty(PropertyName = "use_two_factor_authentication")]
    public bool UseTwoFactorAuthentication { get; set; }
}
